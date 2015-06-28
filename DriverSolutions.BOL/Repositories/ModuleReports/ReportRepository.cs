using DriverSolutions.BOL.Models.ModuleReports;
using DriverSolutions.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Repositories.ModuleReports
{
    public class ReportRepository
    {
        public static ReportFile GenerateReport(DSModel db, uint reportID, uint[] companies = null, uint[] locations = null, uint[] drivers = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var report = db.Reports.Where(r => r.ReportID == reportID).FirstOrDefault();
            if (report == null)
                throw new ArgumentException("No report with the specified ID!");

            string sql = report.SqlQuery.QuerySQL;
            List<MySqlParameter> par = new List<MySqlParameter>();
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
            if (drivers != null && drivers.Length > 0)
            {
                sql = sql.Replace("#DriverID", string.Empty);
                sql = sql.Replace("@DriverID", string.Join<uint>(",", drivers));
            }
            if (fromDate.HasValue && fromDate.Value != DateTime.MinValue)
            {
                sql = sql.Replace("#FromDate", string.Empty);
                par.Add(new MySqlParameter("FromDate", fromDate.Value.Date));
            }
            if (toDate.HasValue && toDate.Value != DateTime.MinValue)
            {
                sql = sql.Replace("#ToDate", string.Empty);
                par.Add(new MySqlParameter("ToDate", toDate.Value.Date));
            }

            DataSet data = db.ADO.SelectTwo(sql, par.ToArray());
            byte[] rep = report.FileObject.FileBlob;
            return new ReportFile(data, rep);
        }
        public static List<UtilityModel<uint>> GetReports(DSModel db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.Reports
                .Select(r => new UtilityModel<uint>(r.ReportID, r.ReportName))
                .ToList();
        }
    }
}
