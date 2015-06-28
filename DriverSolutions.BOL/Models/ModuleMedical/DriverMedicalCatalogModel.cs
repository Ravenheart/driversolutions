using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleMedical
{
    public class DriverMedicalCatalogModel
    {
        public uint DriverMedicalID { get; set; }
        public string DriverName { get; set; }
        public string MedTypeName { get; set; }
        public uint DriverID { get; set; }
        public DateTime ExaminationDate { get; set; }
        public DateTime ValidityDate { get; set; }
        /// <summary>
        /// 1 - None; 2 - Warning; 3 - Error
        /// </summary>
        public byte ReminderStatus { get; set; }
    }
}
