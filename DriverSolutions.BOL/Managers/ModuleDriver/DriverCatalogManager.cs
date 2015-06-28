using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.BOL.Repositories.ModuleDriver;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleDriver
{
    public class DriverCatalogManager : IDriverCatalogManager
    {
        public static IDriverCatalogManager Create(bool isFilter = true)
        {
            var manager = new DriverCatalogManager();
            manager.IsFinder = isFilter;

            return manager;
        }

        protected DriverCatalogManager()
        {
            this.Filter = new DriverFilterModel();
            this.Drivers = new BindingList<DriverModel>();
        }

        public bool IsFinder { get; set; }
        public DriverFilterModel Filter { get; set; }
        public BindingList<DriverModel> Drivers { get; set; }
        public DriverModel ActiveDriver { get; set; }


        public void RefreshDrivers()
        {
            using (var db = DB.GetContext())
            {
                this.Drivers.Clear();
                this.Drivers.AddRange(DriverRepository.FindDrivers(db, this.Filter));
            }
        }

        public void ClearFilter()
        {
            this.Filter.Clear();
        }

        public List<UtilityModel<uint>> GetLicenses()
        {
            using (var db = DB.GetContext())
            {
                var lic = LicenseRepository.GetLicenses(db)
                    .Select(l => new UtilityModel<uint>(l.LicenseID, l.LicenseName))
                    .ToList();
                lic.Insert(0, new UtilityModel<uint>(0, "No license"));
                return lic;
            }
        }
        public List<UtilityModel<uint>> GetPermits()
        {
            using (var db = DB.GetContext())
            {
                return DriverLicenseRepository.GetPermits(db)
                    .Select(p => new UtilityModel<uint>(p.PermitID, p.PermitName))
                    .ToList();
            }
        }
    }
}
