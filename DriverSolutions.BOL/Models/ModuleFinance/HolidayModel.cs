using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleFinance
{
    public class HolidayModel : ModelBase
    {
        public uint HolidayID { get; set; }
        public DateTime HolidayDate { get; set; }
    }
}
