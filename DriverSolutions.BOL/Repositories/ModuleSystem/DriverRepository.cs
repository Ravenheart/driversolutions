using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Repositories.ModuleSystem
{
    public class DriverRepository
    {
        public static DriverModel GetDriver(DSModel db, uint driverID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var poco = db.Drivers.Where(d => d.DriverID == driverID).FirstOrDefault();
            if (poco == null)
                return null;

            DriverModel mod = new DriverModel();
            mod.DriverID = poco.DriverID;
            mod.DriverCode = poco.DriverCode;
            mod.FirstName = poco.FirstName;
            mod.SecondName = poco.SecondName;
            mod.LastName = poco.LastName;
            mod.DateOfBirth = poco.DateOfBirth;
            mod.DateOfHire = poco.DateOfHire;
            mod.CellPhone = poco.CellPhone;
            mod.EmergencyPhone = poco.EmergencyPhone;
            mod.Email = poco.Email;
            mod.PayRateOverride = poco.PayRateOverride;
            mod.IsEnabled = poco.IsEnabled;
            mod.IsChanged = false;
            mod.Locations = poco.LocationsDrivers.Select(q => new LocationDriverModel() { LocationDriverID = q.LocationDriverID, CompanyID = q.Location.CompanyID, DriverID = q.DriverID, LocationID = q.LocationID, TravelPay = q.TravelPay, IsChanged = false }).ToList();

            return mod;
        }

        public static DriverModel GetDriver(DSModel db, string driverCode)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            uint driverID = db.Drivers
                .Where(d => d.DriverCode == driverCode)
                .Select(d => d.DriverID)
                .FirstOrDefault();
            if (driverID == 0)
                return null;

            return DriverRepository.GetDriver(db, driverID);
        }

        public static string PeekDriverCode(DSModel db, string prefix)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.ExecuteScalar<string>("SELECT PeekDriverCode(@Prefix)", new MySqlParameter("Prefix", prefix));
        }

        public static void SaveDriver(DSModel db, KeyBinder key, DriverModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.DriverID == 0)
                InsertDriver(db, key, model);
            else
                UpdateDriver(db, key, model);
        }

        private static void InsertDriver(DSModel db, KeyBinder key, DriverModel model)
        {
            Driver poco = new Driver();
            if (model.DriverCode == string.Empty)
                poco.DriverCode = "D" + DriverRepository.PeekDriverCode(db, "D");
            else
                poco.DriverCode = model.DriverCode;
            poco.FirstName = model.FirstName;
            poco.SecondName = model.SecondName;
            poco.LastName = model.LastName;
            poco.DateOfBirth = model.DateOfBirth;
            poco.DateOfHire = model.DateOfHire;
            poco.CellPhone = model.CellPhone;
            poco.EmergencyPhone = model.EmergencyPhone;
            poco.Email = model.Email;
            poco.PayRateOverride = model.PayRateOverride;
            poco.IsEnabled = model.IsEnabled;
            foreach (var l in model.Locations)
            {
                poco.LocationsDrivers.Add(
                    new LocationsDriver()
                    {
                        LocationID = l.LocationID,
                        Driver = poco,
                        TravelPay = l.TravelPay
                    });
                key.AddKey(poco, l, poco.GetName(p => p.DriverID));
            }
            db.Add(poco);

            key.AddKey(poco, model, poco.GetName(p => p.DriverID));
        }

        private static void UpdateDriver(DSModel db, KeyBinder key, DriverModel model)
        {
            var poco = db.Drivers.Where(d => d.DriverID == model.DriverID).FirstOrDefault();
            if (poco == null)
                throw new ArgumentException("No driver with this ID!");

            poco.DriverCode = model.DriverCode;
            poco.FirstName = model.FirstName;
            poco.SecondName = model.SecondName;
            poco.LastName = model.LastName;
            poco.DateOfBirth = model.DateOfBirth;
            poco.DateOfHire = model.DateOfHire;
            poco.CellPhone = model.CellPhone;
            poco.EmergencyPhone = model.EmergencyPhone;
            poco.Email = model.Email;
            poco.PayRateOverride = model.PayRateOverride;
            poco.IsEnabled = model.IsEnabled;

            foreach (var l in poco.LocationsDrivers.ToList())
            {
                db.Delete(l);
                poco.LocationsDrivers.Remove(l);
            }
            db.FlushChanges();
            foreach (var l in model.Locations)
            {
                poco.LocationsDrivers.Add(
                    new LocationsDriver()
                    {
                        LocationID = l.LocationID,
                        DriverID = l.DriverID,
                        TravelPay = l.TravelPay
                    });
            }
        }

        public static void DeleteDriver(DSModel db, DriverModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            if (model.DriverID != 0)
            {
                var poco = db.Drivers.Where(d => d.DriverID == model.DriverID).FirstOrDefault();
                if (poco != null)
                    db.Delete(poco);
            }
        }

        public static List<DriverModel> FindDrivers(DSModel db, DriverFilterModel filter)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (filter == null)
                throw new ArgumentNullException("filter");

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            string sql = SqlCache.Get(db, "drivers catalog load");
            if (!string.IsNullOrWhiteSpace(filter.DriverCode))
            {
                sql = sql.Replace("#DriverCode", string.Empty);
                parameters.Add(new MySqlParameter("DriverCode", filter.DriverCode + "%"));
            }
            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                sql = sql.Replace("#FirstName", string.Empty);
                parameters.Add(new MySqlParameter("FirstName", filter.FirstName + "%"));
            }
            if (!string.IsNullOrWhiteSpace(filter.SecondName))
            {
                sql = sql.Replace("#SecondName", string.Empty);
                parameters.Add(new MySqlParameter("SecondName", filter.SecondName + "%"));
            }
            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                sql = sql.Replace("#LastName", string.Empty);
                parameters.Add(new MySqlParameter("LastName", filter.LastName + "%"));
            }
            if (!string.IsNullOrWhiteSpace(filter.CellPhone))
            {
                sql = sql.Replace("#CellPhone", string.Empty);
                parameters.Add(new MySqlParameter("CellPhone", filter.CellPhone + "%"));
            }
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                sql = sql.Replace("#Email", string.Empty);
                parameters.Add(new MySqlParameter("Email", filter.Email + "%"));
            }
            if (!string.IsNullOrWhiteSpace(filter.LicenseID))
            {
                sql = sql.Replace("#LicenseID", string.Empty);
                sql = sql.Replace("@LicenseID", filter.LicenseID);
            }
            if (!string.IsNullOrWhiteSpace(filter.PermitID))
            {
                sql = sql.Replace("#PermitID", string.Empty);
                sql = sql.Replace("@PermitID", filter.PermitID);
            }

            string isEnabled = (filter.IncludeDisabled ? "0,1" : "1");
            sql = sql.Replace("@IsEnabled", isEnabled);

            parameters.Add(new MySqlParameter("TodayDate", DateTime.Now.Date));

            return db.ExecuteQuery<DriverModel>(sql, parameters.ToArray())
                .ToList();
        }
    }
}
