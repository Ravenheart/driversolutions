using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleDriver
{
    public class DriverLicenseFilterModel : ModelBase
    {
        public string DriverID { get; set; }
        public uint DriverIDUint
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.DriverID))
                    return 0;

                string[] split = this.DriverID.Split(',');
                uint temp = 0;
                uint.TryParse(split[0].Trim(), out temp);

                return temp;
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? MVRFromDate { get; set; }
        public DateTime? MVRToDate { get; set; }
        public string LicenseID { get; set; }
    }
}
