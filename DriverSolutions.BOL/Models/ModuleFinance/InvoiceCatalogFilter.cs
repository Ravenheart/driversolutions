using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverSolutions.DAL;

namespace DriverSolutions.BOL.Models.ModuleFinance
{
    public class InvoiceCatalogFilter : ModelBase
    {
        public InvoiceCatalogFilter()
        {
            this.InvoiceNumber = string.Empty;
            this.CompanyID = string.Empty;
            this.LocationID = string.Empty;

            this.IssuedFrom = DateTime.Now.StartOfWeek();
        }

        public string InvoiceNumber { get; set; }
        public DateTime? IssuedFrom { get; set; }
        public DateTime? IssuedTo { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
        public string CompanyID { get; set; }
        public string LocationID { get; set; }
    }
}
