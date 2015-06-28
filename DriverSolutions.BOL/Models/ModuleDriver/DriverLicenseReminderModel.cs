using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleDriver
{
    public class DriverLicenseReminderModel : ModelBase
    {
        public DriverLicenseReminderModel() { }
        public DriverLicenseReminderModel(DriversLicensesReminder poco)
        {
            this.DriverLicenseReminderID = poco.DriverLicenseReminderID;
            this.DriverLicenseID = poco.DriverLicenseID;
            this.ReminderID = poco.ReminderID;
            this.ReminderType = poco.ReminderType;
            this.ShouldRemind = poco.ShouldRemind;
            this.HasSentReminder = poco.HasSentReminder;
        }

        public uint DriverLicenseReminderID { get; set; }
        public uint DriverLicenseID { get; set; }
        public uint ReminderID { get; set; }
        public byte ReminderType { get; set; }
        public bool ShouldRemind { get; set; }
        public bool HasSentReminder { get; set; }

        public void Map(DriversLicensesReminder poco)
        {
            poco.DriverLicenseID = this.DriverLicenseID;
            poco.ReminderID = this.ReminderID;
            poco.ReminderType = this.ReminderType;
            poco.ShouldRemind = this.ShouldRemind;
            poco.HasSentReminder = this.HasSentReminder;
        }
    }
}
