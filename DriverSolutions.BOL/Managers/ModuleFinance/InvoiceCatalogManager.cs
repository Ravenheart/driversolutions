using DevExpress.XtraReports.UI;
using DriverSolutions.BOL.Core;
using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.BOL.Repositories.ModuleFinance;
using DriverSolutions.BOL.Repositories.ModuleReports;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleFinance
{
    public class InvoiceCatalogManager : IInvoiceCatalogManager, IReportProgress
    {
        public static InvoiceCatalogManager Create()
        {
            return new InvoiceCatalogManager();
        }

        private InvoiceCatalogManager()
        {
            this.Filter = new InvoiceCatalogFilter();
            this.ActiveModel = new List<InvoiceCatalogModel>();
        }

        public InvoiceCatalogFilter Filter { get; set; }
        public List<InvoiceCatalogModel> ActiveModel { get; set; }

        public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<RunWorkerCompletedEventArgs> OnComplete;
        private void RaiseProgress(int percent, ProgressItem prog)
        {
            if (OnProgressChanged != null)
                OnProgressChanged(this, new ProgressChangedEventArgs(percent, prog));
        }
        private void RaiseComplete(ProgressItem prog)
        {
            if (OnComplete != null)
                OnComplete(this, new RunWorkerCompletedEventArgs(prog, null, false));
        }

        public void LoadInvoices(InvoiceCatalogFilter filter)
        {
            using (var db = DB.GetContext())
            {
                var invoices = InvoiceRepository.FindInvoices(db, filter);
                foreach (var inv in this.ActiveModel)
                {
                    var find = invoices.Where(i => i.InvoiceID == inv.InvoiceID).FirstOrDefault();
                    if (find != null)
                        find.IsMarked = inv.IsMarked;
                }

                this.ActiveModel.Clear();
                this.ActiveModel.AddRange(invoices);
            }
        }
        public void GenerateInvoices(uint[] companies, uint[] locations, DateTime periodFrom, DateTime periodTo)
        {
            Task.Run(() =>
                {
                    try
                    {
                        using (var db = DB.GetContext())
                        {
                            float index = 0.0f;
                            KeyBinder key = new KeyBinder();
                            foreach (uint l in locations)
                            {
                                var loc = LocationRepository.GetLocation(db, l);
                                var invoice = InvoiceRepository.CreateInvoice(db, loc.CompanyID, loc.LocationID, periodFrom, periodTo);
                                if (invoice.TotalAmount == 0.00m)
                                    continue;

                                int percent = (int)((index / (float)locations.Length) * 100.0f);
                                RaiseProgress(percent, new ProgressItem(ProgressStatus.OK,
                                    string.Format("Generating invoice, Location: {0}", loc.LocationName)));

                                index++;

                                InvoiceRepository.SaveInvoice(db, key, invoice);
                            }

                            db.SaveChanges();
                            key.BindKeys();
                            RaiseComplete(new ProgressItem(ProgressStatus.OK, "Invoice generation complete!"));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex, "InvoiceCatalogManager.GenerateInvoices()");
                        RaiseComplete(new ProgressItem(ProgressStatus.Error, ex.ToString()));
                    }
                });
        }
        public void RecalculateInvoices(uint[] invoices)
        {
            Task.Run(() =>
                {
                    try
                    {
                        float index = 0.0f;
                        foreach (uint invID in invoices)
                        {
                            var manager = InvoiceManager.CreateEdit(invID);
                            manager.RecalculateInvoice(manager.ActiveModel);

                            int percent = (int)((index / (float)invoices.Length) * 100.0f);
                            RaiseProgress(percent, new ProgressItem(ProgressStatus.OK,
                                string.Format("Recalculating invoice, Invoice#: {0}", manager.ActiveModel.InvoiceNumber)));

                            index++;

                            var check = manager.SaveInvoice(manager.ActiveModel);
                            if (check.Failed)
                            {
                                RaiseProgress(percent, new ProgressItem(ProgressStatus.Error, "An error occured while recalculating invoice: " + check.Message));
                            }
                        }
                        RaiseComplete(new ProgressItem(ProgressStatus.OK, "Recalculations complete!"));
                    }
                    catch (Exception ex)
                    {
                        RaiseComplete(new ProgressItem(ProgressStatus.Error, ex.Message));
                    }
                });
        }
        public void EmailInvoices(uint[] invoices)
        {
            Task.Run(() =>
               {
                   try
                   {
                       using (var db = DB.GetContext())
                       {
                           SmtpClient smtp = new SmtpClient(GLOB.Settings.Get<string>(3), GLOB.Settings.Get<int>(4));
                           smtp.EnableSsl = GLOB.Settings.Get<bool>(5);
                           smtp.Credentials = new NetworkCredential(GLOB.Settings.Get<string>(9), GLOB.Settings.Get<string>(10));

                           float index = 0.0f;
                           foreach (uint invID in invoices)
                           {
                               var invoice = InvoiceRepository.GetInvoice(db, invID);
                               int percent = (int)((index / (float)invoices.Length) * 100.0f);
                               index++;
                               try
                               {
                                   var location = LocationRepository.GetLocation(db, invoice.LocationID);
                                   if (!location.InvoiceContactID.HasValue)
                                       continue;

                                   RaiseProgress(percent, new ProgressItem(ProgressStatus.OK,
                                       string.Format("Emailing invoice, Invoice#: {0} Contact: {1} Email: {2}",
                                       invoice.InvoiceNumber,
                                       location.InvoiceContact.ContactName,
                                       location.InvoiceContact.ContactEmail)));

                                   XtraReport rep = this.RenderInvoice(invID);
                                   MemoryStream stream = new MemoryStream();
                                   rep.ExportToPdf(stream);

                                   MailMessage msg = new MailMessage(
                                       GLOB.Settings.Get<string>(11),//From
                                       location.InvoiceContact.ContactEmail,//To
                                       GLOB.Company.CompanyName + " - Invoice #" + invoice.InvoiceNumber,//Subject
                                       "Please see the attached invoice.");//Body

                                   stream.Position = 0;
                                   msg.Attachments.Add(
                                       new Attachment(
                                           stream,
                                           invoice.InvoiceNumber + ".pdf",
                                           "application/pdf"));

                                   if (location.IncludeConfirmation)
                                   {
                                       var confRep = this.RenderConfirmation(invoice.InvoiceID);
                                       MemoryStream confStream = new MemoryStream();
                                       confRep.ExportToPdf(confStream);
                                       confStream.Position = 0;
                                       msg.Attachments.Add(
                                           new Attachment(
                                               confStream,
                                               invoice.InvoiceNumber + "-Confirmation.pdf",
                                               "application/pdf"));
                                   }

                                   smtp.Send(msg);
                                   stream.Dispose();
                               }
                               catch (Exception ex)
                               {
                                   RaiseProgress(percent, new ProgressItem(ProgressStatus.Error, ex.Message));
                               }
                           }
                           RaiseComplete(new ProgressItem(ProgressStatus.OK, "Emailing invoices complete!"));
                       }
                   }
                   catch (Exception ex)
                   {
                       RaiseComplete(new ProgressItem(ProgressStatus.Error, ex.Message));
                   }
               });
        }
        public void EmailConfirmationInvoices(uint[] invoices)
        {
            Task.Run(() =>
            {
                try
                {
                    using (var db = DB.GetContext())
                    {
                        SmtpClient smtp = new SmtpClient(GLOB.Settings.Get<string>(3), GLOB.Settings.Get<int>(4));
                        smtp.EnableSsl = GLOB.Settings.Get<bool>(5);
                        smtp.Credentials = new NetworkCredential(GLOB.Settings.Get<string>(6), GLOB.Settings.Get<string>(7));

                        float index = 0.0f;
                        foreach (uint invID in invoices)
                        {
                            var invoice = InvoiceRepository.GetInvoice(db, invID);
                            int percent = (int)((index / (float)invoices.Length) * 100.0f);
                            index++;
                            try
                            {
                                var location = LocationRepository.GetLocation(db, invoice.LocationID);
                                if (!location.InvoiceContactID.HasValue)
                                    continue;

                                RaiseProgress(percent, new ProgressItem(ProgressStatus.OK,
                                    string.Format("Emailing confirmation, Confirmation: {0} Contact: {1} Email: {2}",
                                    invoice.ReceiverCompany,
                                    location.ConfirmationContact.ContactName,
                                    location.ConfirmationContact.ContactEmail)));

                                XtraReport rep = this.RenderConfirmation(invoice.InvoiceID);
                                MemoryStream stream = new MemoryStream();
                                rep.ExportToPdf(stream);

                                MailMessage msg = new MailMessage(
                                    GLOB.Settings.Get<string>(8),//From
                                    location.ConfirmationContact.ContactEmail,//To
                                    GLOB.Company.CompanyName + " - Confirm driver dispatches report (" + DateTime.Now.ToString("MM/dd/yyyy dddd") + ")",//Subject
                                    "These are the hours my drivers turned in last week. Please look them over and get back to me, so that payroll can be submitted and your invoice can be prepared. Thank you in advance, and have a great day.");//Body

                                stream.Position = 0;
                                msg.Attachments.Add(
                                    new Attachment(
                                        stream,
                                        invoice.InvoiceNumber + ".pdf",
                                        "application/pdf"));

                                smtp.Send(msg);
                                stream.Dispose();
                            }
                            catch (Exception ex)
                            {
                                RaiseProgress(percent, new ProgressItem(ProgressStatus.Error, ex.Message));
                            }
                        }
                        RaiseComplete(new ProgressItem(ProgressStatus.OK, "Emailing confirmations complete!"));
                    }
                }
                catch (Exception ex)
                {
                    RaiseComplete(new ProgressItem(ProgressStatus.Error, ex.Message));
                }
            });
        }
        public void ExportInvoices(uint[] invoices)
        {
            Task.Run(() =>
                {
                    try
                    {
                        string invoicesFolder = Path.Combine(Environment.CurrentDirectory, "invoices");
                        if (!Directory.Exists(invoicesFolder))
                            Directory.CreateDirectory(invoicesFolder);

                        float index = 0.0f;
                        foreach (uint invID in invoices)
                        {
                            int percent = (int)((index / (float)invoices.Length) * 100.0f);
                            index++;
                            XtraReport report = this.RenderInvoice(invID);

                            RaiseProgress(percent, new ProgressItem(ProgressStatus.OK,
                                string.Format("Exporting invoice, Invoice#: {0}",
                                report.Tag.ToString())));

                            string filePath = Path.Combine(invoicesFolder, report.Tag.ToString() + ".pdf");
                            try
                            {
                                report.ExportToPdf(filePath);
                            }
                            catch (IOException ex)
                            {
                                RaiseProgress(percent, new ProgressItem(ProgressStatus.Error,
                                "The following invoice file is open and cannot be replaced!\r\n\r\n" + ex.Message));
                            }
                        }
                        RaiseComplete(new ProgressItem(ProgressStatus.OK, "Invoices successfully exported!"));
                    }
                    catch (Exception ex)
                    {
                        RaiseComplete(new ProgressItem(ProgressStatus.OK, ex.Message));
                    }
                });
        }
        public void ExportConfirmations(uint[] invoices)
        {
            Task.Run(() =>
                {
                    try
                    {
                        string confFolder = Path.Combine(Environment.CurrentDirectory, "confirmations");
                        if (!Directory.Exists(confFolder))
                            Directory.CreateDirectory(confFolder);

                        float index = 0.0f;
                        foreach (uint invID in invoices)
                        {
                            int percent = (int)((index / (float)invoices.Length) * 100.0f);
                            index++;
                            XtraReport report = this.RenderConfirmation(invID);

                            RaiseProgress(percent, new ProgressItem(ProgressStatus.OK,
                                string.Format("Exporting confirmation, Confirmation: {0}",
                                report.Tag.ToString())));

                            string filePath = Path.Combine(confFolder, report.Tag.ToString() + ".pdf");
                            try
                            {
                                report.ExportToPdf(filePath);
                            }
                            catch (IOException ex)
                            {
                                RaiseProgress(percent, new ProgressItem(ProgressStatus.Error,
                                    "The following confirmation file is open and cannot be replaced!\r\n\r\n" + ex.Message));
                            }
                        }
                        RaiseComplete(new ProgressItem(ProgressStatus.OK, "Confirmations successfully exported!"));
                    }
                    catch (Exception ex)
                    {
                        RaiseComplete(new ProgressItem(ProgressStatus.OK, ex.Message));
                    }
                });
        }
        public XtraReport RenderInvoice(uint invoiceID)
        {
            using (var db = DB.GetContext())
            {
                DataSet data = InvoiceRepository.PrintInvoice(db, invoiceID);
                //data.WriteXmlSchema("D:\\schema.xml");
                var report = ReportBinder.BindReport(12, data);
                report.Tag = data.Tables[0].Rows[0]["InvoiceNumber"].ToString();

                return report;
            }
        }
        public XtraReport RenderConfirmation(uint invoiceID)
        {
            using (var db = DB.GetContext())
            {
                var invoice = InvoiceRepository.GetInvoice(db, invoiceID);

                uint[] companies = new uint[1] { invoice.CompanyID };
                uint[] locations = new uint[1] { invoice.LocationID };
                var report = ReportRepository.GenerateReport(db, 3, companies, locations, fromDate: invoice.InvoicePeriodFrom, toDate: invoice.InvoicePeriodTo);
                var rep = ReportBinder.BindReport(report);
                rep.Tag = invoice.InvoiceNumber;
                return rep;
            }
        }
        public void UpdateIsConfirmed(uint invoiceID, bool isConfirmed)
        {
            using (DSModel db = DB.GetContext())
            {
                InvoiceRepository.UpdateIsConfirmed(db, invoiceID, isConfirmed);
                db.SaveChanges();
            }
        }

        public List<UtilityModel<uint>> GetCompanies()
        {
            using (var db = DB.GetContext())
            {
                return CompanyRepository.FindCompanies(db)
                    .Select(c => new UtilityModel<uint>(c.CompanyID, c.CompanyName))
                    .ToList();
            }
        }
        public List<UtilityModel<uint>> GetLocations(uint[] companies = null)
        {
            if (companies == null)
                companies = this.GetCompanies().Select(c => c.Value).ToArray();

            using (var db = DB.GetContext())
            {
                return LocationRepository.GetLocationsByCompanies(db, companies)
                    .Select(l => new UtilityModel<uint>(l.LocationID, l.LocationName))
                    .ToList();
            }
        }

        public void MarkAll()
        {
            this.ActiveModel.ForEach(f => f.IsMarked = true);
        }
        public void UnmarkAll()
        {
            this.ActiveModel.ForEach(f => f.IsMarked = false);
        }
        public void MarkSelected(uint[] invoices)
        {
            this.ActiveModel
                .Where(f => invoices.Contains(f.InvoiceID))
                .ToList()
                .ForEach(f => f.IsMarked = true);
        }
        public void UnmarkSelected(uint[] invoices)
        {
            this.ActiveModel
                .Where(f => invoices.Contains(f.InvoiceID))
                .ToList()
                .ForEach(f => f.IsMarked = false);
        }
    }
}
