using DevExpress.XtraReports.UI;
using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.BOL.Repositories.ModuleFinance;
using DriverSolutions.BOL.Validators.ModuleFinance;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleFinance
{
    public class InvoiceManager : IInvoiceManager
    {
        public static InvoiceManager CreateEdit(uint invoiceID)
        {
            var db = DB.GetContext();
            var invoice = InvoiceRepository.GetInvoice(db, invoiceID);

            return new InvoiceManager(invoice);
        }

        public static InvoiceManager CreateNew(uint companyID, uint locationID, DateTime periodFrom, DateTime periodTo)
        {
            var db = DB.GetContext();
            var invoice = InvoiceRepository.CreateInvoice(db, companyID, locationID, periodFrom, periodTo);

            return new InvoiceManager(invoice);
        }

        private InvoiceManager(InvoiceModel invoice)
        {
            using (var db = DB.GetContext())
            {
                this.ActiveModel = invoice;

                var comp = db.CompanyInfos.FirstOrDefault();
            }
        }

        public InvoiceModel ActiveModel { get; set; }

        public CheckResult SaveInvoice(InvoiceModel model)
        {
            try
            {
                using (var db = DB.GetContext())
                {
                    var check = InvoiceValidator.ValidateSave(db, model);
                    if (check.Failed)
                        return check;

                    KeyBinder key = new KeyBinder();
                    InvoiceRepository.SaveInvoice(db, key, model);
                    db.SaveChanges();
                    key.BindKeys();
                    return check;

                }
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }

        public void RecalculateInvoice(InvoiceModel model)
        {
            using (var db = DB.GetContext())
            {
                InvoiceRepository.CalculateInvoice(db, model);
            }
        }

        public CheckResult DeleteInvoice(InvoiceModel model)
        {
            try
            {
                using (var db = DB.GetContext())
                {
                    var check = InvoiceValidator.ValidateDelete(db, model);
                    if (check.Failed)
                        return check;

                    InvoiceRepository.DeleteInvoice(db, model);
                    db.SaveChanges();
                    return check;
                }
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }

        public void PeekInvoiceNumber(InvoiceModel model)
        {
            using (var db = DB.GetContext())
            {
                model.InvoiceNumber = InvoiceRepository.PeekNumber(db, model);
            }
        }

        public List<UtilityModel<uint>> GetInvoiceTypes()
        {
            using (var db = DB.GetContext())
            {
                return InvoiceRepository.GetInvoiceTypes(db);
            }
        }

        public XtraReport PrintInvoice(uint invoiceID)
        {
            using (var db = DB.GetContext())
            {
                DataSet data = InvoiceRepository.PrintInvoice(db, invoiceID);
                //data.WriteXmlSchema("D:\\schema.xml");
                return ReportBinder.BindReport(12, data);
            }
        }
    }
}
