using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleDriver
{
    public class DriverFilterModel : ModelBase
    {
        public DriverFilterModel()
        {
            this.IncludeDisabled = true;
        }

        public void Clear()
        {
            var fil = new DriverFilterModel();
            this.DriverCode = fil.DriverCode;
            this.FirstName = fil.FirstName;
            this.SecondName = fil.SecondName;
            this.LastName = fil.LastName;
            this.CellPhone = fil.CellPhone;
            this.Email = fil.Email;
            this.LicenseID = fil.LicenseID;
            this.PermitID = fil.PermitID;
            this.IncludeDisabled = fil.IncludeDisabled;
        }

        public string DriverCode { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string LicenseID { get; set; }
        public string PermitID { get; set; }
        public bool IncludeDisabled { get; set; }
    }
}
