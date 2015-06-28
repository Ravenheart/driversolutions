using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Core
{
    public class CompanyInfoModel
    {
        public uint CompanyInfoID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyPostCode { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyManagerName { get; set; }
        public string CompanyInvoiceAddress { get; set; }
        public byte[] CompanyLogo { get; set; }

        public CompanyInfoModel() { }
        public CompanyInfoModel(CompanyInfo poco)
        {
            this.CompanyInfoID = poco.CompanyInfoID;
            this.CompanyName = poco.CompanyName;
            this.CompanyCity = poco.CompanyCity;
            this.CompanyState = poco.CompanyState;
            this.CompanyPostCode = poco.CompanyPostCode;
            this.CompanyAddress = poco.CompanyAddress;
            this.CompanyManagerName = poco.CompanyManagerName;
            this.CompanyInvoiceAddress = poco.CompanyInvoiceAddress;
            this.CompanyLogo = poco.CompanyLogo;
        }
    }
}
