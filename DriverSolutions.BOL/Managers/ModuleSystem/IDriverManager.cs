using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleSystem
{
    public interface IDriverManager
    {
        DriverModel ActiveModel { get; set; }

        CheckResult SaveDriver(DriverModel model);
        CheckResult DeleteDriver(DriverModel model);
        List<LocationModel> GetLocations(uint? companyID = null);
        List<CompanyModel> GetCompanies();
        List<UtilityModel<uint>> GetLicenses();
        string PeekDriverCode();
        void UpdateDriverCode();
    }
}
