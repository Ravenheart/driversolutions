using DriverSolutions.BOL.Models.ModuleDispatches;
using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.BOL.Repositories.ModuleDispatches;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.BOL.Validators.ModuleDispatches;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleDispatches
{
    public class DispatchManager : IDispatchManager
    {
        public static DispatchManager CreateEmpty()
        {
            return new DispatchManager();
        }
        public static DispatchManager CreateNew()
        {
            return new DispatchManager(new DispatchModel());
        }
        public static DispatchManager Create(DispatchModel model)
        {
            return new DispatchManager(model);
        }
        public static DispatchManager CreateEdit(uint dispatchID)
        {
            var db = DB.GetContext();
            var model = DispatchRepository.GetDispatch(db, dispatchID);
            if (model == null)
                throw new ArgumentException("No dispatch with the specified ID!");

            return new DispatchManager(model);
        }

        private DispatchManager() { }
        private DispatchManager(DispatchModel model)
        {
            this.ActiveModel = model;
        }

        public DispatchModel ActiveModel { get; set; }

        public CheckResult SaveDispatch(DispatchModel model)
        {
            try
            {
                using (var db = DB.GetContext())
                {
                    var check = DispatchValidator.ValidateSave(db, model);
                    if (check.Failed)
                        return check;

                    KeyBinder key = new KeyBinder();
                    DispatchRepository.SaveDispatch(db, key, model);
                    db.SaveChanges();
                    key.BindKeys();
                    return check;
                }
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }
        public CheckResult DeleteDispatch(DispatchModel model)
        {
            try
            {
                using (var db = DB.GetContext())
                {
                    var check = DispatchValidator.ValidateDelete(db, model);
                    if (check.Failed)
                        return check;

                    DispatchRepository.DeleteDispatch(db, model);
                    db.SaveChanges();
                    return check;
                }
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }

        public List<DispatchCatalogModel> GetDispatches(uint[] drivers = null, uint[] companies = null, DateTime? fromDate = null, DateTime? toDate = null, bool? includeCancelled = null)
        {
            using (var db = DB.GetContext())
            {
                return DispatchRepository.GetDispatches(db, drivers, companies, null, fromDate, toDate, includeCancelled);
            }
        }


        public List<DriverModel> GetDrivers()
        {
            using (var db = DB.GetContext())
            {
                DriverFilterModel filter = new DriverFilterModel();
                filter.IncludeDisabled = false;
                return DriverRepository.FindDrivers(db, filter);
            }
        }
        public List<CompanyModel> GetCompanies()
        {
            using (var db = DB.GetContext())
            {
                return CompanyRepository.FindCompanies(db);
            }
        }
        public List<LocationModel> GetLocations(uint companyID)
        {
            using (var db = DB.GetContext())
            {
                return LocationRepository.GetLocationsByCompany(db, companyID);
            }
        }

        public int GetLocationLunchTime(uint locationID)
        {
            using (var db = DB.GetContext())
            {
                return LocationRepository.GetLunchTime(db, locationID);
            }
        }
    }
}
