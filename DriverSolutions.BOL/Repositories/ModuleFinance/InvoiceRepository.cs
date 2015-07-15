using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Repositories.ModuleFinance
{
    public class InvoiceRepository
    {
        public static InvoiceModel GetInvoice(DSModel db, uint invoiceID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var poco = db.Invoices
                .Where(i => i.InvoiceID == invoiceID)
                .FirstOrDefault();

            if (poco == null)
                return null;

            InvoiceModel mod = new InvoiceModel();
            mod.InvoiceID = poco.InvoiceID;
            mod.CompanyID = poco.CompanyID;
            mod.LocationID = poco.LocationID;
            mod.InvoiceTypeID = poco.InvoiceTypeID;
            mod.InvoiceNumber = poco.InvoiceNumber;
            mod.InvoiceIssueDate = poco.InvoiceIssueDate;
            mod.InvoicePeriodFrom = poco.InvoicePeriodFrom;
            mod.InvoicePeriodTo = poco.InvoicePeriodTo;
            mod.InvoiceNote = poco.InvoiceNote;
            mod.LateCharge = poco.LateCharge;
            mod.LateChargeDays = poco.LateChargeDays;
            mod.IsConfirmed = poco.IsConfirmed;
            mod.UserID = poco.UserID;
            mod.LastUpdateTime = poco.LastUpdateTime;
            mod.Details = InvoiceRepository.GetDetails(db, invoiceID);

            mod.ReceiverCompany = poco.Company.CompanyName + " - " + poco.Location.LocationName;
            mod.ReceiverAddress = poco.Location.LocationAddress;

            mod.IsChanged = false;

            return mod;
        }

        public static InvoiceModel GetInvoice(DSModel db, string invoiceNumber, int year = 0)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var query = PredicateBuilder.True<Invoice>();
            query = query.And(q => q.InvoiceNumber == invoiceNumber);
            if (year != 0)
                query = query.And(q => q.InvoiceIssueDate.Year == year);

            uint invID = db.Invoices
                .Where(query)
                .Select(i => i.InvoiceID)
                .FirstOrDefault();
            if (invID == 0)
                return null;

            return InvoiceRepository.GetInvoice(db, invID);
        }

        private static List<InvoiceDetailModel> GetDetails(DSModel db, uint invoiceID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.InvoicesDetails
                .Where(i => i.InvoiceID == invoiceID)
                .Select(i => new InvoiceDetailModel()
                {
                    InvoiceDetailID = i.InvoiceDetailID,
                    InvoiceID = i.InvoiceID,
                    InvoiceDetailDate = i.InvoiceDetailDate,
                    InvoiceDetailName = i.InvoiceDetailName,
                    InvoiceDetailTotalTime = i.InvoiceDetailTotalTime,
                    InvoiceDetailOverTime = i.InvoiceDetailOverTime,
                    InvoiceDetailRegularRate = i.InvoiceDetailRegularRate,
                    InvoiceDetailOverRate = i.InvoiceDetailOverRate,
                    InvoiceDetailRegularPay = i.InvoiceDetailRegularPay,
                    InvoiceDetailOvertimePay = i.InvoiceDetailOvertimePay,
                    InvoiceDetailGroupName = i.InvoiceDetailGroupName,
                    InvoiceDetailGroupPosition = i.InvoiceDetailGroupPosition,
                    IsChanged = false
                })
                .ToList();
        }

        public static void SaveInvoice(DSModel db, KeyBinder key, InvoiceModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            model.UserID = GLOB.User.UserID;
            model.LastUpdateTime = DateTime.Now;
            if (model.InvoiceID == 0)
                InsertInvoice(db, key, model);
            else
                UpdateInvoice(db, key, model);
        }

        private static void InsertInvoice(DSModel db, KeyBinder key, InvoiceModel model)
        {
            var poco = new Invoice();
            poco.CompanyID = model.CompanyID;
            poco.LocationID = model.LocationID;
            poco.InvoiceTypeID = model.InvoiceTypeID;
            poco.InvoiceNumber = InvoiceRepository.NextNumber(db, model);
            poco.InvoiceIssueDate = model.InvoiceIssueDate;
            poco.InvoicePeriodFrom = model.InvoicePeriodFrom;
            poco.InvoicePeriodTo = model.InvoicePeriodTo;
            poco.InvoiceNote = model.InvoiceNote;
            poco.LateCharge = model.LateCharge;
            poco.LateChargeDays = model.LateChargeDays;
            poco.IsConfirmed = model.IsConfirmed;
            poco.UserID = model.UserID;
            poco.LastUpdateTime = model.LastUpdateTime;
            key.AddKey(poco, model, model.GetName(p => p.InvoiceID));
            db.Add(poco);

            foreach (var d in model.Details)
            {
                var det = new InvoicesDetail();
                det.InvoiceDetailDate = d.InvoiceDetailDate;
                det.InvoiceDetailName = d.InvoiceDetailName;
                det.InvoiceDetailTotalTime = d.InvoiceDetailTotalTime;
                det.InvoiceDetailOverTime = d.InvoiceDetailOverTime;
                det.InvoiceDetailRegularRate = d.InvoiceDetailRegularRate;
                det.InvoiceDetailOverRate = d.InvoiceDetailOverRate;
                det.InvoiceDetailRegularPay = d.InvoiceDetailRegularPay;
                det.InvoiceDetailOvertimePay = d.InvoiceDetailOvertimePay;
                det.InvoiceDetailGroupName = d.InvoiceDetailGroupName;
                det.InvoiceDetailGroupPosition = d.InvoiceDetailGroupPosition;
                det.Invoice = poco;
                key.AddKey(det, d, d.GetName(p => p.InvoiceDetailID));
                key.AddKey(poco, d, d.GetName(p => p.InvoiceID));
                poco.InvoicesDetails.Add(det);
                db.Add(det);
            }
        }

        private static void UpdateInvoice(DSModel db, KeyBinder key, InvoiceModel model)
        {
            var poco = db.Invoices.Where(i => i.InvoiceID == model.InvoiceID).FirstOrDefault();
            if (poco == null)
                throw new ArgumentException("No invoice with the specified ID!");

            poco.CompanyID = model.CompanyID;
            poco.LocationID = model.LocationID;
            poco.InvoiceTypeID = model.InvoiceTypeID;
            poco.InvoiceNumber = model.InvoiceNumber;
            poco.InvoiceIssueDate = model.InvoiceIssueDate;
            poco.InvoicePeriodFrom = model.InvoicePeriodFrom;
            poco.InvoicePeriodTo = model.InvoicePeriodTo;
            poco.InvoiceNote = model.InvoiceNote;
            poco.LateCharge = model.LateCharge;
            poco.LateChargeDays = model.LateChargeDays;
            poco.IsConfirmed = model.IsConfirmed;
            poco.UserID = model.UserID;
            poco.LastUpdateTime = model.LastUpdateTime;

            foreach (var d in poco.InvoicesDetails.ToList())
            {
                db.Delete(d);
                poco.InvoicesDetails.Remove(d);
            }

            foreach (var d in model.Details)
            {
                var det = new InvoicesDetail();
                det.InvoiceDetailDate = d.InvoiceDetailDate;
                det.InvoiceDetailName = d.InvoiceDetailName;
                det.InvoiceDetailTotalTime = d.InvoiceDetailTotalTime;
                det.InvoiceDetailOverTime = d.InvoiceDetailOverTime;
                det.InvoiceDetailRegularRate = d.InvoiceDetailRegularRate;
                det.InvoiceDetailOverRate = d.InvoiceDetailOverRate;
                det.InvoiceDetailRegularPay = d.InvoiceDetailRegularPay;
                det.InvoiceDetailOvertimePay = d.InvoiceDetailOvertimePay;
                det.InvoiceDetailGroupName = d.InvoiceDetailGroupName;
                det.InvoiceDetailGroupPosition = d.InvoiceDetailGroupPosition;
                det.Invoice = poco;
                det.InvoiceID = poco.InvoiceID;
                key.AddKey(det, d, d.GetName(p => p.InvoiceDetailID));
                poco.InvoicesDetails.Add(det);
                db.Add(det);
            }
        }

        public static void DeleteInvoice(DSModel db, InvoiceModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.InvoiceID != 0)
            {
                var poco = db.Invoices.Where(i => i.InvoiceID == model.InvoiceID).FirstOrDefault();
                if (poco != null)
                    db.Delete(poco);
            }
        }

        public static string PeekNumber(DSModel db, InvoiceModel model)
        {
            return db.ExecuteScalar<string>("SELECT PeekInvoiceNumber(@CompID,@LocID,@InvYear);",
                new MySqlParameter("CompID", model.CompanyID),
                new MySqlParameter("LocID", model.LocationID),
                new MySqlParameter("InvYear", model.InvoiceIssueDate.Year));
        }

        public static string NextNumber(DSModel db, InvoiceModel model)
        {
            return db.ExecuteScalar<string>("SELECT NextInvoiceNumber(@CompID,@LocID,@InvYear);",
                new MySqlParameter("CompID", model.CompanyID),
                new MySqlParameter("LocID", model.LocationID),
                new MySqlParameter("InvYear", model.InvoiceIssueDate.Year));
        }

        public static InvoiceModel CreateInvoice(DSModel db, uint companyID, uint locationID, DateTime periodFrom, DateTime periodTo)
        {
            InvoiceModel i = new InvoiceModel();
            i.CompanyID = companyID;
            i.LocationID = locationID;
            i.InvoiceIssueDate = DateTime.Now;
            i.InvoiceNumber = InvoiceRepository.PeekNumber(db, i);
            i.InvoicePeriodFrom = periodFrom;
            i.InvoicePeriodTo = periodTo;
            i.LateChargeDays = 15;
            InvoiceRepository.CalculateInvoice(db, i);


            return i;
        }

        public static void CalculateInvoice(DSModel db, InvoiceModel invoice)
        {
            var details = db.ExecuteQuery<InvoiceDetailModel>("CALL InvoiceCalculate(@CompanyID, @LocationID, @PeriodFrom, @PeriodTo);",
                new MySqlParameter("CompanyID", invoice.CompanyID),
                new MySqlParameter("LocationID", invoice.LocationID),
                new MySqlParameter("PeriodFrom", invoice.InvoicePeriodFrom),
                new MySqlParameter("PeriodTo", invoice.InvoicePeriodTo));

            invoice.Details.Clear();
            invoice.Details.AddRange(details);
        }

        public static List<UtilityModel<uint>> GetInvoiceTypes(DSModel db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.ConstInvoiceTypes
                .Select(t => new UtilityModel<uint>(t.InvoiceTypeID, t.InvoiceTypeName))
                .ToList();
        }

        public static DataSet PrintInvoice(DSModel db, uint invoiceID)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (invoiceID == 0)
                throw new ArgumentException("invoiceID cannot be 0!", "invoiceID");

            string sql = SqlCache.Get("invoice print");
            return db.ADO.SelectTwo(sql, new MySqlParameter("InvoiceID", invoiceID));
        }

        public static void UpdateIsConfirmed(DSModel db, uint invoiceID, bool isConfirmed)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (invoiceID == 0)
                throw new ArgumentException("invoiceID cannot be 0!", "invoiceID");

            var poco = db.Invoices
                .Where(i => i.InvoiceID == invoiceID)
                .FirstOrDefault();

            if (poco == null)
                throw new ArgumentException("No such invoice with the specified ID!");

            poco.IsConfirmed = isConfirmed;
        }


        public static List<InvoiceCatalogModel> FindInvoices(DSModel db, InvoiceCatalogFilter filter)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (filter == null)
                throw new ArgumentNullException("filter");

            string sql = SqlCache.Get(db, "invoice catalog load");

            List<MySqlParameter> par = new List<MySqlParameter>();
            if (!string.IsNullOrWhiteSpace(filter.InvoiceNumber))
            {
                sql = sql.Replace("#InvoiceNumber", string.Empty);
                par.Add(new MySqlParameter("InvoiceNumber", filter.InvoiceNumber + "%"));
            }
            if (filter.IssuedFrom.HasValue && filter.IssuedFrom.Value != DateTime.MinValue)
            {
                sql = sql.Replace("#IssuedFrom", string.Empty);
                par.Add(new MySqlParameter("IssuedFrom", filter.IssuedFrom.Value.Date));
            }
            if (filter.IssuedTo.HasValue && filter.IssuedTo.Value != DateTime.MinValue)
            {
                sql = sql.Replace("#IssuedTo", string.Empty);
                par.Add(new MySqlParameter("IssuedTo", filter.IssuedTo.Value.Date));
            }
            if (filter.PeriodFrom.HasValue && filter.PeriodFrom.Value != DateTime.MinValue)
            {
                sql = sql.Replace("#PeriodFrom", string.Empty);
                par.Add(new MySqlParameter("PeriodFrom", filter.PeriodFrom.Value.Date));
            }
            if (filter.PeriodTo.HasValue && filter.PeriodTo.Value != DateTime.MinValue)
            {
                sql = sql.Replace("#PeriodTo", string.Empty);
                par.Add(new MySqlParameter("PeriodTo", filter.PeriodTo.Value.Date));
            }


            if (!string.IsNullOrWhiteSpace(filter.CompanyID))
            {
                sql = sql.Replace("#CompanyID", string.Empty);
                sql = sql.Replace("@CompanyID", filter.CompanyID);
            }
            if (!string.IsNullOrWhiteSpace(filter.LocationID))
            {
                sql = sql.Replace("#LocationID", string.Empty);
                sql = sql.Replace("@LocationID", filter.LocationID);
            }

            return db.ExecuteQuery<InvoiceCatalogModel>(sql, par.ToArray()).ToList();
        }
    }
}
