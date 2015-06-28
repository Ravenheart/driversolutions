using DevExpress.XtraReports.UI;
using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleFinance
{
    public interface IInvoiceManager
    {
        InvoiceModel ActiveModel { get; set; }
        CheckResult SaveInvoice(InvoiceModel model);
        void RecalculateInvoice(InvoiceModel model);
        CheckResult DeleteInvoice(InvoiceModel model);
        void PeekInvoiceNumber(InvoiceModel model);

        List<UtilityModel<uint>> GetInvoiceTypes();
        XtraReport PrintInvoice(uint invoiceID);
    }
}
