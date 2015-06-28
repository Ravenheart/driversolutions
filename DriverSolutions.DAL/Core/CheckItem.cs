using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.DAL
{
    public class CheckItem
    {
        public ErrorType ErrorType { get; set; }
        public string Message { get; set; }
        public string Property { get; set; }

        public CheckItem(ErrorType type, string message, string property)
        {
            this.ErrorType = type;
            this.Message = message;
            this.Property = property;
        }
    }
}
