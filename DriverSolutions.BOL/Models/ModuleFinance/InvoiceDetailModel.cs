using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleFinance
{
    public class InvoiceDetailModel : ModelBase
    {
        public uint InvoiceDetailID { get; set; }
        public uint InvoiceID { get; set; }
        public DateTime? InvoiceDetailDate { get; set; }
        public string InvoiceDetailName { get; set; }
        public decimal InvoiceDetailTotalTime { get; set; }
        public decimal InvoiceDetailOverTime { get; set; }
        public decimal InvoiceDetailRegularRate { get; set; }
        public decimal InvoiceDetailOverRate { get; set; }
        public decimal InvoiceDetailRegularPay { get; set; }
        public decimal InvoiceDetailOvertimePay { get; set; }
        public string InvoiceDetailGroupName { get; set; }
        public uint InvoiceDetailGroupPosition { get; set; }
    }
}
