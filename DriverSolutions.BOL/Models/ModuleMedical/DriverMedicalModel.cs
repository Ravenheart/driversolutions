using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverSolutions.DAL;
using System.ComponentModel;

namespace DriverSolutions.BOL.Models.ModuleMedical
{
    public class DriverMedicalModel : ModelBase
    {
        public DriverMedicalModel()
        {
            this.Reminders = new BindingList<DriverMedicalReminderModel>();
        }
        public DriverMedicalModel(DriversMedical poco)
            : this()
        {
            this.DriverMedicalID = poco.DriverMedicalID;
            this.DriverID = poco.DriverID;
            this.MedTypeID = poco.MedTypeID;
            this.ExaminationDate = poco.ExaminationDate;
            this.ValidityDate = poco.ValidityDate;
            this.IsChanged = false;
        }

        public uint DriverMedicalID { get; set; }
        public uint DriverID { get; set; }
        public uint MedTypeID { get; set; }
        public DateTime ExaminationDate { get; set; }
        public DateTime ValidityDate { get; set; }
        public BindingList<DriverMedicalReminderModel> Reminders { get; set; }

        public void Map(DriversMedical poco)
        {
            poco.DriverMedicalID = this.DriverMedicalID;
            poco.DriverID = this.DriverID;
            poco.MedTypeID = this.MedTypeID;
            poco.ExaminationDate = this.ExaminationDate;
            poco.ValidityDate = this.ValidityDate;
        }
    }
}
