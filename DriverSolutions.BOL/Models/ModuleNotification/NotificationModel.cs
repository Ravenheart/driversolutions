using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleNotification
{
    public class NotificationModel : ModelBase
    {
        public uint NotificationID { get; set; }
        public uint NotificationTypeID { get; set; }
        public uint ReminderType { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
    }
}
