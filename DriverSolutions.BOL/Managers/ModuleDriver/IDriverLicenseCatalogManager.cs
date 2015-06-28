using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleDriver
{
    public interface IDriverLicenseCatalogManager
    {
        DriverLicenseFilterModel Filter { get; set; }
        BindingList<DriverLicenseModel> Licenses { get; set; }

        void RefreshLicenses();
        void ClearFilter();

        List<UtilityModel<uint>> GetLicenseTypes();
        List<UtilityModel<uint>> GetDrivers();
    }
}
