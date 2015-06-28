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
    public interface IDriverLicenseManager
    {
        DriverLicenseModel ActiveModel { get; set; }
        BindingList<UtilityModel<uint>> Permits { get; set; }

        CheckResult Save();
        CheckResult Delete();

        List<UtilityModel<uint>> GetLicenseClasses();
        List<UtilityModel<uint>> GetReminderTypes();
        List<UtilityModel<uint>> GetReminderKinds();
    }
}
