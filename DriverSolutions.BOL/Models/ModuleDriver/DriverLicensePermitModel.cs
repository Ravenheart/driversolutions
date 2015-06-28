using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleDriver
{
    public class DriverLicensePermitModel : ModelBase
    {
        public DriverLicensePermitModel() { }
        public DriverLicensePermitModel(DriversLicensesPermit poco)
        {
            this.DriverLicensePermitID = poco.DriverLicensePermitID;
            this.DriverLicenseID = poco.DriverLicenseID;
            this.PermitID = poco.PermitID;
            this.IsChanged = false;
        }

        public uint DriverLicensePermitID { get; set; }
        public uint DriverLicenseID { get; set; }
        public uint PermitID { get; set; }

        public void Map(DriversLicensesPermit poco)
        {
            poco.DriverLicenseID = this.DriverLicenseID;
            poco.PermitID = this.PermitID;
        }
    }
}
