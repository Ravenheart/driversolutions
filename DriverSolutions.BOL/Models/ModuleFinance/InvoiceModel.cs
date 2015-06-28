using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleFinance
{
    public class InvoiceModel : ModelBase
    {
        public InvoiceModel()
        {
            this.InvoiceTypeID = (uint)InvoiceType.Invoice;
            this.Details = new List<InvoiceDetailModel>();
        }

        public uint InvoiceID { get; set; }
        public uint CompanyID { get; set; }
        public uint LocationID { get; set; }
        public uint InvoiceTypeID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceIssueDate { get; set; }
        public DateTime InvoicePeriodFrom { get; set; }
        public DateTime InvoicePeriodTo { get; set; }
        public string InvoiceNote { get; set; }
        public decimal LateCharge { get; set; }
        public uint LateChargeDays { get; set; }
        public bool IsConfirmed { get; set; }
        public uint UserID { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public decimal TotalAmount
        {
            get
            {
                return this.Details.Sum(d => d.InvoiceDetailRegularPay + d.InvoiceDetailOvertimePay) + LateCharge;
            }
        }

        public string ReceiverCompany { get; set; }
        public string ReceiverAddress { get; set; }

        public List<InvoiceDetailModel> Details { get; set; }

        public void Notify(string property)
        {
            this.RaisePropertyChanged(property);
        }
    }
}
