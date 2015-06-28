using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleReports
{
    public class ReportFile
    {
        public DataSet DataSource { get; set; }
        public byte[] Report { get; set; }

        public ReportFile(DataSet data, byte[] report)
        {
            this.DataSource = data;
            this.Report = report;
        }
    }
}
