using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.BOL.Models.ModuleMedical;
using DriverSolutions.BOL.Repositories.ModuleMedical;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleMedical
{
    public class DriverMedicalCatalogManager : IDriverMedicalCatalogManager
    {
        public static IDriverMedicalCatalogManager Create()
        {
            DriverMedicalCatalogManager manager = new DriverMedicalCatalogManager();

            return manager;
        }

        private DriverMedicalCatalogManager()
        {
            this.Filter = new DriverMedicalFilterModel();
            this.ActiveMedicals = new BindingList<DriverMedicalCatalogModel>();
            this.ActiveReminders = new BindingList<DriverMedicalReminderCatalogModel>();
        }

        public DriverMedicalFilterModel Filter { get; set; }
        public BindingList<DriverMedicalCatalogModel> ActiveMedicals { get; set; }
        public BindingList<DriverMedicalReminderCatalogModel> ActiveReminders { get; set; }


        public void RefreshMedicals()
        {
            using (var db = DB.GetContext())
            {
                this.ActiveMedicals.Clear();
                var items = DriverMedicalRepository.FindMedicals(db, this.Filter);
                foreach (var i in items)
                    this.ActiveMedicals.Add(i);
            }
        }
        public void RefreshReminders()
        {
            
        }


        public List<UtilityModel<uint>> GetDrivers()
        {
            using (var db = DB.GetContext())
            {
                DriverFilterModel filter = new DriverFilterModel();
                filter.IncludeDisabled = false;
                return DriverRepository
                    .FindDrivers(db, filter)
                    .Select(d => new UtilityModel<uint>(d.DriverID, d.DriverCode + " - " + d.FullName))
                    .ToList();
            }
        }
        public List<UtilityModel<uint>> GetMedicalTypes()
        {
            using (var db = DB.GetContext())
            {
                return MedicalTypeRepository.GetMedicalTypes(db)
                    .Select(m => new UtilityModel<uint>(m.MedTypeID, m.MedTypeName))
                    .ToList();
            }
        }
    }
}
