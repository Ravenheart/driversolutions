using DriverSolutions.BOL.Models.ModuleMedical;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleMedical
{
    public interface IDriverMedicalCatalogManager
    {
        DriverMedicalFilterModel Filter { get; set; }
        BindingList<DriverMedicalCatalogModel> ActiveMedicals { get; set; }
        BindingList<DriverMedicalReminderCatalogModel> ActiveReminders { get; set; }

        void RefreshMedicals();
        void RefreshReminders();

        List<UtilityModel<uint>> GetDrivers();
        List<UtilityModel<uint>> GetMedicalTypes();
    }
}
