using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.BOL.Validators.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleSystem
{
    public class CompanyManager : ManagerBase, ICompanyManager
    {
        public static CompanyManager CreateNew()
        {
            return new CompanyManager(DB.GetContext(), new CompanyModel());
        }

        public static CompanyManager CreateEdit(uint companyID)
        {
            var db = DB.GetContext();
            var company = CompanyRepository.GetCompany(db, companyID);
            return CompanyManager.CreateEdit(company);
        }

        public static CompanyManager CreateEdit(CompanyModel company)
        {
            var db = DB.GetContext();
            var license = CompanyRepository.GetLicensesPayRates(db, company.CompanyID);
            var invoice = CompanyRepository.GetInvoicePayRates(db, company.CompanyID);

            return new CompanyManager(db, company, license, invoice);
        }

        private CompanyManager(DSModel db, CompanyModel company) : this(db, company, new List<CompanyLicensePayRateModel>(), new List<CompanyInvoicingPayRateModel>()) { }
        private CompanyManager(DSModel db, CompanyModel company, List<CompanyLicensePayRateModel> license, List<CompanyInvoicingPayRateModel> invoice)
            : base(db)
        {
            this.ActiveModel = company;
            this.LicensePayRates = license;
            this.InvoicePayRates = invoice;
        }

        public CompanyModel ActiveModel { get; set; }

        public List<CompanyLicensePayRateModel> LicensePayRates { get; set; }
        public List<CompanyInvoicingPayRateModel> InvoicePayRates { get; set; }

        public CheckResult SaveCompany(CompanyModel model, List<CompanyLicensePayRateModel> licenseRates, List<CompanyInvoicingPayRateModel> invoiceRates)
        {
            try
            {
                using (var db = DB.GetContext())
                {
                    var check = CompanyValidator.ValidateSave(db, model);
                    if (check.Failed)
                        return check;

                    var checkLicense = CompanyValidator.ValidateDriverRates(db, licenseRates);
                    if (checkLicense.Failed)
                        return checkLicense;

                    var checkInvoice = CompanyValidator.ValidateInvoiceRates(db, invoiceRates);
                    if (checkInvoice.Failed)
                        return checkInvoice;

                    KeyBinder key = new KeyBinder();
                    CompanyRepository.SaveCompany(db, key, model);
                    if (model.CompanyID != 0)
                    {
                        CompanyRepository.SaveLicensesPayRates(db, key, licenseRates);
                        CompanyRepository.SaveInvoicePayRates(db, key, invoiceRates);
                    }
                    db.SaveChanges();
                    key.BindKeys();
                    model.IsChanged = false;

                    return check;
                }
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }
        public CheckResult DeleteCompany(CompanyModel model)
        {
            try
            {
                using (var db = DB.GetContext())
                {
                    var check = CompanyValidator.ValidateDelete(db, model);
                    if (check.Failed)
                        return check;

                    CompanyRepository.DeleteCompany(db, model);
                    db.SaveChanges();
                    return check;
                }
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }
        public ContactModel GetContact(uint contactID)
        {
            return ContactRepository.GetContact(this.DbContext, contactID);
        }

        public string PeekCompanyCode()
        {
            return CompanyRepository.PeekCompanyCode(this.DbContext, this.ActiveModel.CompanyCode.TrimEnd(new char[10] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }));
        }
        public void UpdateCompanyPeekCode()
        {
            this.ActiveModel.CompanyCode = this.ActiveModel.CompanyCode.TrimEnd(new char[10] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
            this.ActiveModel.CompanyCode += this.PeekCompanyCode();
        }
        public string PeekLocationCode()
        {
            return LocationRepository.PeekLocationCode(this.DbContext, "L");
        }

        public List<LicenseModel> GetLicenseClasses()
        {
            using (var db = DB.GetContext())
            {
                return LicenseRepository.GetLicenses(db, true);
            }
        }
    }
}
