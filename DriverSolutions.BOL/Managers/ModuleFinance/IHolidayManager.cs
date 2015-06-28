using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleFinance
{
    public interface IHolidayManager
    {
        List<HolidayModel> ActiveModel { get; set; }

        List<HolidayModel> GetHolidays();
        CheckResult SaveHolidays(List<HolidayModel> model);
    }
}
