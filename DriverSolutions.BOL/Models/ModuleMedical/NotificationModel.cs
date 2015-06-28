﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleMedical
{
    public class NotificationModel : ModelBase
    {
        public uint DriverMedicalReminderID { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
    }
}
