using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleSystem
{
    public class CompanyModel : ModelBase
    {
        public CompanyModel()
        {
            this.CompanyCode = string.Empty;
            this.IsEnabled = true;
            this.Locations = new List<LocationModel>();
        }

        public uint CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyPostCode { get; set; }
        public string CompanyContactName { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public uint LunchTime { get; set; }
        public decimal TrainingTime { get; set; }
        public bool IsEnabled { get; set; }

        public List<LocationModel> Locations { get; set; }
    }
}
