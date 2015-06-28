using DriverSolutions.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Repositories.ModuleDispatches
{
    public class DispatchReportRepository
    {
        public static DataSet GenerateDriverReport(DSModel db, uint[] companies = null, uint[] locations = null, uint[] drivers = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            string sql = @"
                SELECT
                  *
                FROM dispatches_payments dp
                WHERE dp.DispatchID != 0
                #CompanyID AND dp.CompanyID IN (@CompanyID)
                #LocationID AND dp.LocationID IN (@LocationID)
                #DriverID AND dp.DriverID IN (@DriverID)
                #FromDate AND dp.DispatchDate >= @FromDate
                #ToDate AND dp.DispatchDate <= @ToDate";
            List<MySqlParameter> par = new List<MySqlParameter>();
            if(companies != null & companies.Length>0)
            {
                sql = sql.Replace("#CompanyID", string.Empty);
                sql = sql.Replace("@CompanyID", string.Join<uint>(",", companies));
            }
            if (locations != null & locations.Length > 0)
            {
                sql = sql.Replace("#LocationID", string.Empty);
                sql = sql.Replace("@LocationID", string.Join<uint>(",", locations));
            }
            if (drivers != null & drivers.Length > 0)
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

            return db.ADO.SelectTwo(sql, par.ToArray());
        }

        #region ViewSQL
        public const string SQLDispatchesRaw = @"
            SELECT ds.DispatchID
                 , date(ds.FromDateTime) AS DispatchDate
                 , time(ds.FromDateTime) AS DispatchFromTime
                 , time(ds.ToDateTime) AS DispatchToTime
                 , c.CompanyID
                 , c.CompanyName
                 , c.CompanyCode
                 , l.LocationID
                 , l.LocationName
                 , l.LocationCode
                 , d.DriverID
                 , concat(d.LastName, ' ,', d.FirstName) AS DriverName
                 , d.DriverCode
                 , lic.LicenseID
                 , lic.LicenseName
                 , ds.IsCancelled
                 , ds.IsConfirmed
                 , (
                   CASE
                   WHEN ds.SpecialPayRate IS NOT NULL THEN
                     ds.SpecialPayRate
                   WHEN d.PayRateOverride > 0.0 THEN
                     d.PayRateOverride
                   WHEN pay.PayRate IS NOT NULL THEN
                     pay.PayRate
                   ELSE
                     0.0
                   END) AS DriverPayRate
                 , ifnull(payc.PayRate, 0.0) AS CompanyPayRate
                 , ifnull(payc.PayRateOT, 0.0) AS CompanyPayRateOT
                 , round(sum((timestampdiff(MINUTE, ds.FromDateTime, ds.ToDateTime) - (ds.LunchTime )) / 60.0), 2) AS TotalTime
            FROM
              dispatches ds
            INNER JOIN companies c
            ON ds.CompanyID = c.CompanyID
            INNER JOIN locations l
            ON ds.LocationID = l.LocationID
            INNER JOIN drivers d
            ON ds.DriverID = d.DriverID
            INNER JOIN const_licenses lic
            ON d.LicenseID = lic.LicenseID
            LEFT JOIN companies_licenses_payrate pay
            ON (pay.CompanyID = c.CompanyID
            AND pay.LicenseID = d.LicenseID
            AND ds.FromDateTime >= pay.FromDate
            AND (ds.ToDateTime <= pay.ToDate
            OR pay.ToDate IS NULL))
            LEFT JOIN companies_invoicing_payrate payc
            ON (payc.CompanyID = c.CompanyID
            AND payc.LicenseID = d.LicenseID
            AND ds.FromDateTime >= payc.FromDate
            AND (ds.ToDateTime <= payc.ToDate
            OR payc.ToDate IS NULL))
            GROUP BY
              c.CompanyID
            , l.LocationID
            , d.DriverID
            , DispatchDate;";

        public const string SQLDispatchesHours = @"
            SELECT ds.*
                 , ifnull(ld.TravelPay, 0.00) AS DriverTravelPay
                 , if(ld.TravelPay IS NOT NULL, l.TravelPay, 0.00) AS CompanyTravelPay
                 , (
                   CASE
                   WHEN h.HolidayID IS NOT NULL THEN
                     0.0
                   WHEN ds.TotalTime > 8.0 THEN
                     8.0
                   ELSE
                     ds.TotalTime
                   END) AS DriverRegularTime
                 , (
                   CASE
                   WHEN h.HolidayID IS NOT NULL THEN
                     ds.TotalTime
                   WHEN ds.TotalTime > 8.0 THEN
                     ds.TotalTime - 8.0
                   ELSE
                     0.0
                   END) AS DriverOverTime
                 , (
                   CASE
                   WHEN ds.IsCancelled = 1 THEN
                     8.0
                   WHEN h.HolidayID IS NOT NULL OR
                     weekday(ds.DispatchDate) IN (5, 6) THEN
                     0.0
                   WHEN ds.TotalTime > 8.0 THEN
                     8.0
                   ELSE
                     ds.TotalTime
                   END) AS CompanyRegularTime
                 , (
                   CASE
                   WHEN ds.IsCancelled = 1 THEN
                     0.0
                   WHEN h.HolidayID IS NOT NULL OR
                     weekday(ds.DispatchDate) IN (5, 6) THEN
                     ds.TotalTime
                   WHEN ds.TotalTime > 8.0 THEN
                     ds.TotalTime - 8.0
                   ELSE
                     0.0
                   END) AS CompanyOverTime
            FROM
              dispatches_raw ds
            INNER JOIN (locations l)
            ON (ds.LocationID = l.LocationID)
            LEFT JOIN holidays h
            ON h.HolidayDate = ds.DispatchDate
            LEFT JOIN locations_drivers ld
            ON ds.LocationID = ld.LocationID
            AND ds.DriverID = ld.DriverID;";

        public const string SQLDispatchesPayments = @"
            SELECT ds.*
                 , ds.DriverRegularTime * ds.DriverPayRate AS DriverRegularPay
                 , ds.DriverOverTime * ds.DriverPayRate * 1.5 AS DriverOverTimePay
                 , (ds.DriverRegularTime * ds.DriverPayRate) + ((ds.DriverOverTime * ds.DriverPayRate) * 1.5) + ds.DriverTravelPay AS DriverTotalPay
                 , ds.CompanyRegularTime * ds.CompanyPayRate AS CompanyRegularPay
                 , ds.CompanyOverTime * ds.CompanyPayRateOT AS CompanyOverTimePay
                 , (ds.CompanyRegularTime * ds.CompanyPayRate) + (ds.CompanyOverTime * ds.CompanyPayRateOT) + ds.CompanyTravelPay AS CompanyTotalPay
            FROM
              dispatches_hours AS ds;";
        #endregion
    }
}
