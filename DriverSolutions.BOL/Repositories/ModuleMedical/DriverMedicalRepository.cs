using DriverSolutions.BOL.Models.ModuleMedical;
using DriverSolutions.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace DriverSolutions.BOL.Repositories.ModuleMedical
{
    public class DriverMedicalRepository
    {
        public static DriverMedicalModel GetMedical(DSModel db, uint driverMedicalID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var poco = db.DriversMedicals.Where(m => m.DriverMedicalID == driverMedicalID).FirstOrDefault();
            var med = new DriverMedicalModel(poco);
            var reminders = DriverMedicalRepository.GetReminders(db, driverMedicalID);
            foreach (var r in reminders)
                med.Reminders.Add(r);
            med.IsChanged = false;

            return med;
        }
        public static List<DriverMedicalReminderModel> GetReminders(DSModel db, uint driverMedicalID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.DriversMedicalsReminders
                .Where(r => r.DriverMedicalID == driverMedicalID)
                .Select(r => new DriverMedicalReminderModel(r))
                .ToList();
        }

        public static void DeleteMedical(DSModel db, DriverMedicalModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (model == null)
                throw new ArgumentNullException("model");

            db.DriversMedicals
                .Where(m => m.DriverMedicalID == model.DriverMedicalID)
                .DeleteAll();
        }

        public static void SaveMedical(DSModel db, KeyBinder key, DriverMedicalModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.DriverMedicalID == 0)
                InsertMedical(db, key, model);
            else
                UpdateMedical(db, key, model);
        }
        private static void InsertMedical(DSModel db, KeyBinder key, DriverMedicalModel model)
        {
            DriversMedical poco = new DriversMedical();
            model.Map(poco);
            db.Add(poco);

            key.AddKey(poco, model, model.GetName(p => p.DriverMedicalID));
            db.FlushChanges();
            SaveReminders(db, key, model, poco);
        }
        private static void UpdateMedical(DSModel db, KeyBinder key, DriverMedicalModel model)
        {
            var poco = db.DriversMedicals
                .Where(m => m.DriverMedicalID == model.DriverMedicalID)
                .FirstOrDefault();
            model.Map(poco);
            db.FlushChanges();

            SaveReminders(db, key, model, poco);
        }
        private static void SaveReminders(DSModel db, KeyBinder key, DriverMedicalModel model, DriversMedical poco)
        {
            string sqlDelete = @"DELETE FROM drivers_medicals_reminders WHERE DriverMedicalID = @DriverMedicalID AND DriverMedicalReminderID NOT IN (@DriverMedicalReminderID);";
            string ids = "0";
            if (model.Reminders.Count > 0)
                ids = string.Join<uint>(",", model.Reminders.Select(r => r.DriverMedicalReminderID));

            sqlDelete = sqlDelete.Replace("@DriverMedicalReminderID", ids);

            db.ExecuteNonQuery(sqlDelete, new MySqlParameter("DriverMedicalID", poco.DriverMedicalID));
            string sqlInsert = @"
                INSERT INTO drivers_medicals_reminders
                  (DriverMedicalID, ReminderID, ReminderType, ShouldRemind) VALUES (@DriverMedicalID, @ReminderID, @ReminderType, @ShouldRemind);
                SELECT LAST_INSERT_ID();";
            string sqlUpdate = @"
                UPDATE drivers_medicals_reminders
                SET
                  DriverMedicalID = @DriverMedicalID, 
                  ReminderID = @ReminderID, 
                  ReminderType = @ReminderType, 
                  ShouldRemind = @ShouldRemind
                WHERE
                  DriverMedicalReminderID = @DriverMedicalReminderID;";

            foreach (var rem in model.Reminders)
            {
                if (rem.DriverMedicalReminderID == 0)
                {
                    rem.DriverMedicalID = poco.DriverMedicalID;
                    UtilityModel<uint> temp = new UtilityModel<uint>();
                    temp.Value = db.ExecuteQuery<uint>(sqlInsert,
                        new MySqlParameter("DriverMedicalID", rem.DriverMedicalID),
                        new MySqlParameter("ReminderID", rem.ReminderID),
                        new MySqlParameter("ReminderType", rem.ReminderType),
                        new MySqlParameter("ShouldRemind", rem.ShouldRemind))
                        .First();
                    key.AddKey(temp, rem, "Value", rem.GetName(p => p.DriverMedicalReminderID));
                }
                else
                {
                    rem.DriverMedicalID = poco.DriverMedicalID;
                    db.ExecuteNonQuery(sqlUpdate,
                        new MySqlParameter("DriverMedicalReminderID", rem.DriverMedicalReminderID),
                        new MySqlParameter("DriverMedicalID", rem.DriverMedicalID),
                        new MySqlParameter("ReminderID", rem.ReminderID),
                        new MySqlParameter("ReminderType", rem.ReminderType),
                        new MySqlParameter("ShouldRemind", rem.ShouldRemind));
                }
            }
        }

        public static List<DriverMedicalCatalogModel> FindMedicals(DSModel db, DriverMedicalFilterModel filter)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (filter == null)
                throw new ArgumentNullException("filter");

            string sql = SqlCache.Get(db, "medicals catalog refresh");
            List<MySqlParameter> par = new List<MySqlParameter>();
            if (!string.IsNullOrWhiteSpace(filter.DriverID))
            {
                sql = sql.Replace("#DriverID", string.Empty);
                sql = sql.Replace("@DriverID", filter.DriverID);
            }
            if (!string.IsNullOrWhiteSpace(filter.MedTypeID))
            {
                sql = sql.Replace("#MedTypeID", string.Empty);
                sql = sql.Replace("@MedTypeID", filter.MedTypeID);
            }
            if (filter.ValidityDateFrom.HasValue)
            {
                sql = sql.Replace("#ValidityDateFrom", string.Empty);
                par.Add(new MySqlParameter("ValidityDateFrom", filter.ValidityDateFrom.Value.Date));
            }
            if (filter.ValidityDateTo.HasValue)
            {
                sql = sql.Replace("#ValidityDateTo", string.Empty);
                par.Add(new MySqlParameter("ValidityDateTo", filter.ValidityDateTo.Value.Date));
            }

            return db.ExecuteQuery<DriverMedicalCatalogModel>(
                sql,
                par.ToArray())
                .ToList();
        }
    }
}
