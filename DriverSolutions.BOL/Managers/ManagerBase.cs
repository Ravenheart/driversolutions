using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers
{
    public class ManagerBase
    {
        public DSModel DbContext { get; set; }

        public ManagerBase(DSModel context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.DbContext = context;
        }
    }
}
