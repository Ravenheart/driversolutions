using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleFinance
{
    public class InvoiceCatalogModel : ModelBase
    {
        public uint InvoiceID { get; set; }

        public bool IsMarked { get; set; }
        public string InvoiceNumber { get; set; }
        public string CompanyName { get; set; }
        public string LocationName { get; set; }
        public DateTime InvoiceIssueDate { get; set; }
        public DateTime InvoicePeriodFrom { get; set; }
        public DateTime InvoicePeriodTo { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
