using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleSystem
{
    public class LocationModel : ModelBase
    {
        public LocationModel()
        {
            this.ConfirmationContact = new ContactModel();
            this.InvoiceContact = new ContactModel();
            this.DispatchContact = new ContactModel();
        }

        public uint LocationID { get; set; }
        public string LocationName { get; set; }
        public string LocationCode { get; set; }
        public uint CompanyID { get; set; }
        public string LocationAddress { get; set; }
        public string LocationPhone { get; set; }
        public string LocationFax { get; set; }
        public uint? ConfirmationContactID { get; set; }
        public uint? InvoiceContactID { get; set; }
        public uint? DispatchContactID { get; set; }
        public decimal TravelPay { get; set; }
        public string TravelPayName { get; set; }
        public int LunchTime { get; set; }
        public bool IsEnabled { get; set; }
        public bool IncludeConfirmation { get; set; }

        public ContactModel ConfirmationContact { get; set; }
        public ContactModel InvoiceContact { get; set; }
        public ContactModel DispatchContact { get; set; }
    }
}
