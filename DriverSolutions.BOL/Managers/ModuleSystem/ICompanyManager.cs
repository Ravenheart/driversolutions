using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleSystem
{
    public interface ICompanyManager
    {
        CompanyModel ActiveModel { get; set; }

        List<CompanyLicensePayRateModel> LicensePayRates { get; set; }
        List<CompanyInvoicingPayRateModel> InvoicePayRates { get; set; }

        CheckResult SaveCompany(CompanyModel model, List<CompanyLicensePayRateModel> licenseRates, List<CompanyInvoicingPayRateModel> invoiceRates);
        CheckResult DeleteCompany(CompanyModel model);
        ContactModel GetContact(uint contactID);

        string PeekCompanyCode();
        void UpdateCompanyPeekCode();
        string PeekLocationCode();

        List<LicenseModel> GetLicenseClasses();
    }
}
