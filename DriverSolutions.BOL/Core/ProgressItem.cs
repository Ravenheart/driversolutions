using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Core
{
    public class ProgressItem
    {
        public ProgressStatus Status { get; set; }
        public string Message { get; set; }

        public ProgressItem(ProgressStatus status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
    }
}
