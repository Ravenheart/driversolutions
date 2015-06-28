using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverSolutions.DAL;

namespace DriverSolutions.BOL.Models.ModuleSystem
{
    public class LocationDriverModel : ModelBase
    {
        public LocationDriverModel() { }
        public LocationDriverModel(LocationsDriver poco)
        {
            this.LocationDriverID = poco.LocationDriverID;
            this.CompanyID = poco.Location.CompanyID;
            this.LocationID = poco.LocationID;
            this.DriverID = poco.DriverID;
            this.TravelPay = poco.TravelPay;
        }

        public uint LocationDriverID { get; set; }
        public uint CompanyID { get; set; }
        public uint LocationID { get; set; }
        public uint DriverID { get; set; }
        public decimal TravelPay { get; set; }
    }
}
