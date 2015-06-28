using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleDriver
{
    public class DriverLicenseModel : ModelBase
    {
        public DriverLicenseModel()
        {
            this.Permits = new BindingList<DriverLicensePermitModel>();
            this.Reminders = new BindingList<DriverLicenseReminderModel>();
            this.IsChanged = false;
        }
        public DriverLicenseModel(DriversLicense poco)
            : this()
        {
            this.DriverLicenseID = poco.DriverLicenseID;
            this.DriverID = poco.DriverID;
            this.LicenseID = poco.LicenseID;
            this.IssueDate = poco.IssueDate;
            this.ExpirationDate = poco.ExpirationDate;
            this.MVRReviewDate = poco.MVRReviewDate;
            this.IsChanged = false;
        }

        public uint DriverLicenseID { get; set; }
        public uint DriverID { get; set; }
        public uint LicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? MVRReviewDate { get; set; }

        public BindingList<DriverLicensePermitModel> Permits { get; set; }
        public BindingList<DriverLicenseReminderModel> Reminders { get; set; }

        public void Map(DriversLicense poco)
        {
            poco.DriverID = this.DriverID;
            poco.LicenseID = this.LicenseID;
            poco.IssueDate = this.IssueDate;
            poco.ExpirationDate = this.ExpirationDate;
            poco.MVRReviewDate = this.MVRReviewDate;
        }
    }
}
