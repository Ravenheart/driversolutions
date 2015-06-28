using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleDriver
{
    public interface IDriverCatalogManager
    {
        bool IsFinder { get; set; }
        DriverFilterModel Filter { get; set; }
        BindingList<DriverModel> Drivers { get; set; }
        DriverModel ActiveDriver { get; set; }

        void RefreshDrivers();
        void ClearFilter();

        List<UtilityModel<uint>> GetLicenses();
        List<UtilityModel<uint>> GetPermits();
    }
}
