using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleMedical
{
    public class MedicalTypeModel : ModelBase
    {
        public MedicalTypeModel() { }

        public MedicalTypeModel(ConstMedicalType poco)
        {
            this.MedTypeID = poco.MedTypeID;
            this.MedTypeName = poco.MedTypeName;
            this.MedTypePosition = poco.MedTypePosition;
            this.IsChanged = false;
        }

        public uint MedTypeID { get; set; }
        public string MedTypeName { get; set; }
        public uint MedTypePosition { get; set; }

        public void Map(ConstMedicalType poco)
        {
            poco.MedTypeID = this.MedTypeID;
            poco.MedTypeName = this.MedTypeName;
            poco.MedTypePosition = this.MedTypePosition;
        }
    }
}
