using DevExpress.XtraReports.UI;
using DriverSolutions.BOL.Core;
using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleFinance
{
    public interface IInvoiceCatalogManager : IReportProgress
    {
        InvoiceCatalogFilter Filter { get; set; }
        List<InvoiceCatalogModel> ActiveModel { get; set; }

        event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
        event EventHandler<RunWorkerCompletedEventArgs> OnComplete;

        void LoadInvoices(InvoiceCatalogFilter filter);
        void GenerateInvoices(uint[] companies, uint[] locations, DateTime periodFrom, DateTime periodTo);
        void RecalculateInvoices(uint[] invoices);
        void EmailInvoices(uint[] invoices);
        void EmailConfirmationInvoices(uint[] invoices);
        void ExportInvoices(uint[] invoices);
        void ExportConfirmations(uint[] invoices);
        XtraReport RenderInvoice(uint invoiceID);
        XtraReport RenderConfirmation(uint invoiceID);
        void UpdateIsConfirmed(uint invoiceID, bool isConfirmed);

        List<UtilityModel<uint>> GetCompanies();
        List<UtilityModel<uint>> GetLocations(uint[] companies = null);

        void MarkAll();
        void UnmarkAll();
        void MarkSelected(uint[] invoices);
        void UnmarkSelected(uint[] invoices);

    }
}
