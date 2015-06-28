using DriverSolutions.BOL.Models.ModuleMedical;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleMedical
{
    public interface IDriverMedicalManager
    {
        DriverMedicalModel ActiveModel { get; set; }

        CheckResult Save();
        CheckResult Delete();

        List<UtilityModel<uint>> GetMedicalTypes();
        List<UtilityModel<uint>> GetReminderTypes();
        List<UtilityModel<uint>> GetReminderKinds();
    }
}
