using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleMedical
{
    public class DriverMedicalReminderModel : ModelBase
    {
        public DriverMedicalReminderModel()
        {
            this.ShouldRemind = true;
        }
        public DriverMedicalReminderModel(DriversMedicalsReminder poco)
        {
            this.DriverMedicalReminderID = poco.DriverMedicalReminderID;
            this.DriverMedicalID = poco.DriverMedicalID;
            this.ReminderID = poco.ReminderID;
            this.ReminderType = poco.ReminderType;
            this.ShouldRemind = poco.ShouldRemind;
            this.HasSentReminder = poco.HasSentReminder;
            this.IsChanged = false;
        }

        public uint DriverMedicalReminderID { get; set; }
        public uint DriverMedicalID { get; set; }
        public uint ReminderID { get; set; }
        public byte ReminderType { get; set; }
        public bool ShouldRemind { get; set; }
        public bool HasSentReminder { get; set; }

        public void Map(DriversMedicalsReminder poco)
        {
            poco.DriverMedicalReminderID = this.DriverMedicalReminderID;
            poco.DriverMedicalID = this.DriverMedicalID;
            poco.ReminderID = this.ReminderID;
            poco.ReminderType = this.ReminderType;
            poco.ShouldRemind = this.ShouldRemind;
            poco.HasSentReminder = this.HasSentReminder;
        }
    }
}
