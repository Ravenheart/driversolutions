using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleSystem
{
    public class CompanyLicensePayRateModel : ModelBase
    {
        public CompanyLicensePayRateModel()
        {
            this.FromDate = DateTime.Now;
        }

        public uint CompanyDriverID { get; set; }
        public uint CompanyID { get; set; }
        public uint LicenseID { get; set; }
        public decimal PayRate { get; set; }
        public decimal PayRateOT { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
