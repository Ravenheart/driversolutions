using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleSystem
{
    public class LicenseModel : ModelBase
    {
        public uint LicenseID { get; set; }
        public string LicenseName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
