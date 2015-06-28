using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverSolutions.DAL;

namespace DriverSolutions.BOL.Models.ModuleMedical
{
    public class DriverMedicalFilterModel:ModelBase
    {
        public DriverMedicalFilterModel()
        {
            this.ValidityDateFrom = DateTime.Now.StartOfMonth();
            this.ValidityDateTo = DateTime.Now.EndOfMonth();
        }

        public string DriverID { get; set; }
        public string MedTypeID { get; set; }
        public DateTime? ValidityDateFrom { get; set; }
        public DateTime? ValidityDateTo { get; set; }
    }
}
