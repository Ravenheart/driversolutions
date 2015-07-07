using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.DAL
{
    public partial class Log
    {
        public string ToMessage()
        {
            LogType type = (LogType)this.LogType;
            return string.Format("LogType: {0} LogLocation: {1} LogMessage: {2}\r\n", type.ToString(), this.LogLocation, this.LogMessage);
        }
    }
}
