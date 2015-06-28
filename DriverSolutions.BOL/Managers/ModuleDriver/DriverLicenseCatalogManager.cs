using AutoMapper;
using DriverSolutions.BOL.Models.ModuleDriver;
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
    public class DriverLicenseCatalogManager : IDriverLicenseCatalogManager
    {
        public static IDriverLicenseCatalogManager Create(uint driverID = 0)
        {
            var manager = new DriverLicenseCatalogManager();
            if (driverID != 0)
                manager.Filter.DriverID = driverID.ToString();

            return manager;
        }

        protected DriverLicenseCatalogManager()
        {
            this.Filter = new DriverLicenseFilterModel();
            this.Licenses = new BindingList<DriverLicenseModel>();
        }

        public DriverLicenseFilterModel Filter { get; set; }
        public BindingList<DriverLicenseModel> Licenses { get; set; }

        public void RefreshLicenses()
        {
            using (var db = DB.GetContext())
            {
                this.Licenses.Clear();
                this.Licenses.AddRange(DriverLicenseRepository.FindDriverLicenses(db, this.Filter));
            }
        }
        public void ClearFilter()
        {
            var fil = new DriverLicenseFilterModel();
            Mapper.Map(fil, this.Filter);
        }

        public List<UtilityModel<uint>> GetLicenseTypes()
        {
            using (var db = DB.GetContext())
            {
                return LicenseRepository.GetLicenses(db)
                    .Select(l => new UtilityModel<uint>(l.LicenseID, l.LicenseName))
                    .ToList();
            }
        }
        public List<UtilityModel<uint>> GetDrivers()
        {
            using (var db = DB.GetContext())
            {
                DriverFilterModel filter = new DriverFilterModel();
                filter.IncludeDisabled = true;
                return DriverRepository.FindDrivers(db, filter)
                    .Select(d => new UtilityModel<uint>(d.DriverID, d.FullName))
                    .ToList();
            }
        }
    }
}
