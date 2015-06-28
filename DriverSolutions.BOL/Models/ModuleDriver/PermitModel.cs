using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleDriver
{
    public class PermitModel : ModelBase
    {
        public uint PermitID { get; set; }
        public string PermitName { get; set; }
        public string PermitCode { get; set; }
        public bool IsEnabled { get; set; }

        public PermitModel() { }
        public PermitModel(ConstPermit poco)
        {
            this.PermitID = poco.PermitID;
            this.PermitName = poco.PermitName;
            this.PermitCode = poco.PermitCode;
            this.IsEnabled = poco.IsEnabled;
        }

        public void Map(ConstPermit poco)
        {
            poco.PermitID = this.PermitID;
            poco.PermitName = this.PermitName;
            poco.PermitCode = this.PermitCode;
            poco.IsEnabled = this.IsEnabled;
        }
    }
}
