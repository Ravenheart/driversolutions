using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleSystem
{
    public class DriverModel : ModelBase
    {
        public DriverModel()
        {
            this.DriverCode = string.Empty;

            this.Locations = new List<LocationDriverModel>();
            this.IsEnabled = true;
        }

        public uint DriverID { get; set; }
        public string DriverCode { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfHire { get; set; }
        public string CellPhone { get; set; }
        public string EmergencyPhone { get; set; }
        public string Email { get; set; }
        //public uint LicenseID { get; set; }
        //public DateTime? LicenseExpirationDate { get; set; }
        public decimal? PayRateOverride { get; set; }
        public bool IsEnabled { get; set; }
        public List<LocationDriverModel> Locations { get; set; }

        /// <summary>
        /// Driver full name in the format "Andreev, Toshko"
        /// </summary>
        public string FullName
        {
            get
            {
                return this.LastName + ", " + this.FirstName;
            }
        }

        #region ReadOnly
        public uint LicenseID { get; set; }
        #endregion
    }
}
