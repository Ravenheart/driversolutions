using DriverSolutions.BOL.Models.ModuleDispatches;
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleDispatches
{
    public interface IDispatchManager
    {
        DispatchModel ActiveModel { get; set; }
        CheckResult SaveDispatch(DispatchModel model);
        CheckResult DeleteDispatch(DispatchModel model);
        List<DispatchCatalogModel> GetDispatches(uint[] drivers = null, uint[] companies = null, DateTime? fromDate = null, DateTime? toDate = null, bool? includeCancelled = null);


        List<DriverModel> GetDrivers();
        List<CompanyModel> GetCompanies();
        List<LocationModel> GetLocations(uint companyID);

        int GetLocationLunchTime(uint locationID);
    }
}
