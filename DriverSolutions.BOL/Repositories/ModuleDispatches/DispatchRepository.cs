using DriverSolutions.BOL.Models.ModuleDispatches;
using DriverSolutions.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Repositories.ModuleDispatches
{
    public class DispatchRepository
    {
        public static List<DispatchCatalogModel> GetDispatches(DSModel db, uint[] drivers = null, uint[] companies = null, uint[] locations = null, DateTime? fromDate = null, DateTime? toDate = null, bool? includeCancelled = null)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            string sql = @"
                SELECT
                  d.DispatchID,
                  d.DriverID,
                  d.CompanyID,
                  d.LocationID,
                  d.FromDateTime,
                  d.ToDateTime,

                  IF(d.HasTraining, c.TrainingTime, 0) AS TrainingTime,
                  IF(d.HasLunch, c.LunchTime, 0) AS LunchTime,

                  d.SpecialPayRate,
                  d.MiscCharge,
                  d.Note,
                  d.IsCancelled,
                  d.IsConfirmed,
                  d.HasLunch,
                  d.HasTraining,
                  d.UserID,
                  d.LastUpdateTime,

                  CONCAT(c.CompanyName, ' - ', c.CompanyCode) AS CompanyName,
                  CONCAT(dr.LastName, ', ', dr.FirstName) AS DriverName,
                  CONCAT(l.LocationName, ' - ', l.LocationCode) AS LocationName,

                  CONCAT(u.FirstName, ' ', u.LastName, ' (', u.UserName, ')') AS UserName

                FROM dispatches d
                  INNER JOIN companies c
                    ON d.CompanyID = c.CompanyID
                  INNER JOIN drivers dr
                    ON d.DriverID = dr.DriverID
                  INNER JOIN locations l
                    ON d.LocationID = l.LocationID
                  INNER JOIN users u
                    ON d.UserID = u.UserID
                WHERE 1 = 1
                #DriverID AND d.DriverID IN (@DriverID)
                #CompanyID AND d.CompanyID IN (@CompanyID)
                #LocationID AND d.LocationID IN (@LocationID)
                #FromDate AND d.FromDateTime >= DATE(@FromDate)
                #ToDate AND d.ToDateTime <= DATE_ADD(DATE(@ToDate),INTERVAL 86399 SECOND)
                #IsCancelled AND d.IsCancelled = @IsCancelled";
            List<MySqlParameter> pars = new List<MySqlParameter>();
            if (drivers != null && drivers.Length > 0)
            {
                sql = sql.Replace("#DriverID", string.Empty);
                sql = sql.Replace("@DriverID", string.Join<uint>(",", drivers));
            }
            if (companies != null && companies.Length > 0)
            {
                sql = sql.Replace("#CompanyID", string.Empty);
                sql = sql.Replace("@CompanyID", string.Join<uint>(",", companies));
            }
            if (locations != null && locations.Length > 0)
            {
                sql = sql.Replace("#LocationID", string.Empty);
                sql = sql.Replace("@LocationID", string.Join<uint>(",", locations));
            }
            if (fromDate.HasValue && fromDate.GetValueOrDefault() != DateTime.MinValue)
            {
                sql = sql.Replace("#FromDate", string.Empty);
                pars.Add(new MySqlParameter("FromDate", fromDate.Value.Date));
            }
            if (toDate.HasValue && toDate.GetValueOrDefault() != DateTime.MinValue)
            {
                sql = sql.Replace("#ToDate", string.Empty);
                pars.Add(new MySqlParameter("ToDate", toDate.Value.Date));
            }


            if (includeCancelled.HasValue && includeCancelled.Value == false)
            {
                sql = sql.Replace("#IsCancelled", string.Empty);
                pars.Add(new MySqlParameter("IsCancelled", false));
            }

            var items = db.ExecuteQuery<DispatchCatalogModel>(sql, pars.ToArray()).ToList();
            items.ForEach(i => i.IsChanged = false);
            return items;
        }

        public static List<DispatchModel> GetDispatches(DSModel db, params uint[] dispatchIDs)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (dispatchIDs.Length == 0)
                return new List<DispatchModel>();

            string sql = @"
                SELECT
                  d.DispatchID,
                  d.DriverID,
                  d.CompanyID,
                  d.LocationID,
                  d.FromDateTime,
                  d.ToDateTime,

                  IF(d.HasLunch, c.LunchTime, 0) AS LunchTime,
                  IF(d.HasTraining, c.TrainingTime, 0) AS TrainingTime,

                  d.SpecialPayRate,
                  d.MiscCharge,
                  d.Note,
                  d.IsCancelled,
                  d.IsConfirmed,
                  d.HasLunch,
                  d.HasTraining,
                  d.UserID,
                  d.LastUpdateTime,
                  CONCAT(u.FirstName, ' ', u.LastName, ' (', u.Username, ')') AS UserName
                FROM dispatches d
                  INNER JOIN users u
                    ON d.UserID = u.UserID
                  INNER JOIN companies c
                    ON d.CompanyID = c.CompanyID
                WHERE d.DispatchID IN (@DispatchID)
                GROUP BY d.DispatchID;";
            sql = sql.Replace("@DispatchID", string.Join<uint>(",", dispatchIDs));
            var items = db.ExecuteQuery<DispatchModel>(sql).ToList();
            items.ForEach(i => i.IsChanged = false);

            return items;
        }

        public static DispatchModel GetDispatch(DSModel db, uint dispatchID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return DispatchRepository.GetDispatches(db, dispatchID).FirstOrDefault();
        }

        public static void SaveDispatch(DSModel db, KeyBinder key, DispatchModel disp)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (disp == null)
                throw new ArgumentNullException("disp");

            disp.UserID = GLOB.User.UserID;
            disp.LastUpdateTime = DateTime.Now;
            if (disp.DispatchID == 0)
                InsertDispatch(db, key, disp);
            else
                UpdateDispatch(db, key, disp);
        }

        private static void InsertDispatch(DSModel db, KeyBinder key, DispatchModel model)
        {
            Dispatch poco = new Dispatch();
            poco.DriverID = model.DriverID;
            poco.CompanyID = model.CompanyID;
            poco.LocationID = model.LocationID;
            poco.FromDateTime = model.FromDateTime;
            poco.ToDateTime = model.ToDateTime;
            poco.LunchTime = model.LunchTime;
            poco.TrainingTime = model.TrainingTime;
            poco.SpecialPayRate = model.SpecialPayRate;
            poco.MiscCharge = model.MiscCharge;
            poco.Note = model.Note;
            poco.IsCancelled = model.IsCancelled;
            poco.IsConfirmed = model.IsConfirmed;
            poco.HasLunch = model.HasLunch;
            poco.HasTraining = model.HasTraining;
            poco.UserID = model.UserID;
            poco.LastUpdateTime = model.LastUpdateTime;
            db.Add(poco);
            key.AddKey(poco, model, model.GetName(p => p.DispatchID));
        }

        private static void UpdateDispatch(DSModel db, KeyBinder key, DispatchModel model)
        {
            var poco = db.Dispatches.Where(d => d.DispatchID == model.DispatchID).FirstOrDefault();
            if (poco == null)
                throw new ArgumentNullException("No dispatch with the specified ID!");

            poco.DriverID = model.DriverID;
            poco.CompanyID = model.CompanyID;
            poco.LocationID = model.LocationID;
            poco.FromDateTime = model.FromDateTime;
            poco.ToDateTime = model.ToDateTime;
            poco.LunchTime = model.LunchTime;
            poco.TrainingTime = model.TrainingTime;
            poco.SpecialPayRate = model.SpecialPayRate;
            poco.MiscCharge = model.MiscCharge;
            poco.Note = model.Note;
            poco.IsCancelled = model.IsCancelled;
            poco.IsConfirmed = model.IsConfirmed;
            poco.HasLunch = model.HasLunch;
            poco.HasTraining = model.HasTraining;
            poco.UserID = model.UserID;
            poco.LastUpdateTime = model.LastUpdateTime;
        }

        public static void DeleteDispatch(DSModel db, DispatchModel disp)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (disp == null)
                throw new ArgumentNullException("disp");

            if (disp.DispatchID != 0)
            {
                var poco = db.Dispatches.Where(d => d.DispatchID == disp.DispatchID).FirstOrDefault();
                if (poco != null)
                    db.Delete(poco);
            }
        }
    }
}
