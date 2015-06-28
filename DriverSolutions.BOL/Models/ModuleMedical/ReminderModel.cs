using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleMedical
{
    public class ReminderModel : ModelBase
    {
        public ReminderModel() { }

        public ReminderModel(ConstReminder poco)
        {
            this.ReminderID = poco.ReminderID;
            this.ReminderName = poco.ReminderName;
            this.ReminderInterval = poco.ReminderInterval;
            this.ReminderType = poco.ReminderType;
            this.IsMedical = poco.IsMedical;
            this.IsLicense = poco.IsLicense;
            this.IsChanged = false;
        }

        public uint ReminderID { get; set; }
        public string ReminderName { get; set; }
        public uint ReminderInterval { get; set; }
        public uint ReminderType { get; set; }
        public bool IsMedical { get; set; }
        public bool IsLicense { get; set; }

        public void Map(ConstReminder poco)
        {
            poco.ReminderID = this.ReminderID;
            poco.ReminderName = this.ReminderName;
            poco.ReminderInterval = this.ReminderInterval;
            poco.ReminderType = this.ReminderType;
            poco.IsMedical = this.IsMedical;
            poco.IsLicense = this.IsLicense;
        }
    }
}
