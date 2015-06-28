using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace DriverSolutions.BOL.Repositories.ModuleDriver
{
    public class DriverLicenseRepository
    {
        public static DriverLicenseModel GetDriverLicense(DSModel db, uint driverLicenseID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var mod = db.DriversLicenses
                .Where(d => d.DriverLicenseID == driverLicenseID)
                .Select(d => new DriverLicenseModel(d))
                .FirstOrDefault();
            var permits = DriverLicenseRepository.GetDriverLicensePermits(db, driverLicenseID);
            foreach (var p in permits)
                mod.Permits.Add(p);

            var reminders = DriverLicenseRepository.GetDriverLicenseReminders(db, driverLicenseID);
            foreach (var r in reminders)
                mod.Reminders.Add(r);

            mod.IsChanged = false;

            return mod;
        }
        public static List<DriverLicensePermitModel> GetDriverLicensePermits(DSModel db, uint driverLicenseID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.DriversLicensesPermits
                .Where(d => d.DriverLicenseID == driverLicenseID)
                .Select(d => new DriverLicensePermitModel(d))
                .ToList();
        }
        public static List<DriverLicenseReminderModel> GetDriverLicenseReminders(DSModel db, uint driverLicenseID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.DriversLicensesReminders
                .Where(d => d.DriverLicenseID == driverLicenseID)
                .Select(d => new DriverLicenseReminderModel(d))
                .ToList();
        }

        public static void SaveDriverLicense(DSModel db, KeyBinder key, DriverLicenseModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.DriverLicenseID == 0)
                InsertDriverLicense(db, key, model);
            else
                UpdateDriverLicense(db, key, model);
        }
        private static void InsertDriverLicense(DSModel db, KeyBinder key, DriverLicenseModel model)
        {
            var poco = new DriversLicense();
            model.Map(poco);
            db.Add(poco);
            key.AddKey(poco, model, model.GetName(p => p.DriverLicenseID));
            db.FlushChanges();

            SaveDriverLicensePermits(db, key, poco, model);
            SaveDriverLicenseReminders(db, key, poco, model);
        }
        private static void UpdateDriverLicense(DSModel db, KeyBinder key, DriverLicenseModel model)
        {
            var poco = db.DriversLicenses.Where(d => d.DriverLicenseID == model.DriverLicenseID).FirstOrDefault();
            if (poco == null)
                throw new ArgumentNullException("poco");

            model.Map(poco);
            db.FlushChanges();

            SaveDriverLicensePermits(db, key, poco, model);
            SaveDriverLicenseReminders(db, key, poco, model);
        }
        private static void SaveDriverLicensePermits(DSModel db, KeyBinder key, DriversLicense poco, DriverLicenseModel model)
        {
            db.ExecuteNonQuery("DELETE FROM drivers_licenses_permits WHERE DriverLicenseID = @DriverLicenseID;",
                new MySqlParameter("DriverLicenseID", poco.DriverLicenseID));

            string sql = @"
                INSERT INTO drivers_licenses_permits (DriverLicenseID, PermitID) VALUES (@DriverLicenseID, @PermitID);
                SELECT LAST_INSERT_ID();";
            foreach (var per in model.Permits)
            {
                per.DriverLicenseID = poco.DriverLicenseID;
                per.DriverLicensePermitID = (uint)db.ExecuteScalar<ulong>(sql,
                    new MySqlParameter("DriverLicenseID", per.DriverLicenseID),
                    new MySqlParameter("PermitID", per.PermitID));
            }
        }
        private static void SaveDriverLicenseReminders(DSModel db, KeyBinder key, DriversLicense poco, DriverLicenseModel model)
        {
            db.ExecuteNonQuery("delete from drivers_licenses_reminders where DriverLicenseID = @DriverLicenseID;",
                new MySqlParameter("DriverLicenseID", poco.DriverLicenseID));

            string sql = @"
                INSERT INTO drivers_licenses_reminders 
                  (DriverLicenseID, ReminderID, ReminderType, ShouldRemind) 
                  VALUES 
                  (@DriverLicenseID, @ReminderID, @ReminderType, @ShouldRemind);
                SELECT LAST_INSERT_ID();";

            foreach (var rem in model.Reminders)
            {
                rem.DriverLicenseID = poco.DriverLicenseID;
                rem.DriverLicenseReminderID = (uint)db.ExecuteScalar<ulong>(sql,
                    new MySqlParameter("DriverLicenseID", rem.DriverLicenseID),
                    new MySqlParameter("ReminderID", rem.ReminderID),
                    new MySqlParameter("ReminderType", rem.ReminderType),
                    new MySqlParameter("ShouldRemind", rem.ShouldRemind));
            }
        }

        public static void DeleteDriverLicense(DSModel db, KeyBinder key, DriverLicenseModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            db.DriversLicensesPermits
                .Where(p => p.DriverLicenseID == model.DriverLicenseID)
                .DeleteAll();
            db.DriversLicensesReminders
                .Where(l => l.DriverLicenseID == model.DriverLicenseID)
                .DeleteAll();
            db.DriversLicenses
                .Where(d => d.DriverLicenseID == model.DriverLicenseID)
                .DeleteAll();
        }

        public static List<DriverLicenseModel> FindDriverLicenses(DSModel db, DriverLicenseFilterModel filter)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var q = PredicateBuilder.True<DriversLicense>();
            if (!string.IsNullOrWhiteSpace(filter.DriverID))
            {
                uint[] ids = filter.DriverID.Split(',').Select(d => Convert.ToUInt32(d.Trim())).ToArray();
                q = q.And(d => ids.Contains(d.DriverID));
            }
            if (!string.IsNullOrWhiteSpace(filter.LicenseID))
            {
                uint[] ids = filter.LicenseID.Split(',').Select(d => Convert.ToUInt32(d.Trim())).ToArray();
                q = q.And(d => ids.Contains(d.LicenseID));
            }
            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                q = q.And(d => d.Driver.FirstName.StartsWith(filter.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                q = q.And(d => d.Driver.LastName.StartsWith(filter.LastName));
            }
            if (filter.MVRFromDate.HasValue)
            {
                q = q.And(d => d.MVRReviewDate >= filter.MVRFromDate.Value.Date);
            }
            if (filter.MVRToDate.HasValue)
            {
                q = q.And(d => d.MVRReviewDate <= filter.MVRToDate.Value.Date);
            }


            return db.DriversLicenses
                .Where(q)
                .Select(l => new DriverLicenseModel(l))
                .ToList();
        }

        public static List<PermitModel> GetPermits(DSModel db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.ConstPermits
                .OrderBy(p => p.PermitPosition)
                .Select(p => new PermitModel(p))
                .ToList();
        }
    }
}
