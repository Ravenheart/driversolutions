using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverSolutions.DAL;
using Telerik.OpenAccess;
using MySql.Data.MySqlClient;

namespace DriverSolutions.BOL.Repositories.ModuleSystem
{
    public class CompanyRepository
    {
        public static CompanyModel GetCompany(DSModel db, uint companyID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var poco = db.Companies.Where(c => c.CompanyID == companyID).FirstOrDefault();
            if (poco == null)
                return null;

            CompanyModel mod = new CompanyModel();
            mod.CompanyID = poco.CompanyID;
            mod.CompanyName = poco.CompanyName;
            mod.CompanyCode = poco.CompanyCode;
            mod.CompanyAddress1 = poco.CompanyAddress1;
            mod.CompanyAddress2 = poco.CompanyAddress2;
            mod.CompanyCity = poco.CompanyCity;
            mod.CompanyState = poco.CompanyState;
            mod.CompanyPostCode = poco.CompanyPostCode;
            mod.CompanyFax = poco.CompanyFax;
            mod.CompanyPhone = poco.CompanyPhone;
            mod.CompanyEmail = poco.CompanyEmail;
            mod.LunchTime = poco.LunchTime;
            mod.TrainingTime = poco.TrainingTime;
            mod.IsEnabled = poco.IsEnabled;
            mod.Locations.AddRange(LocationRepository.GetLocations(db, companyID));

            mod.IsChanged = false;

            return mod;
        }

        public static CompanyModel GetCompany(DSModel db, string companyCode)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var companyID = db.Companies
                .Where(c => c.CompanyCode == companyCode)
                .Select(c => c.CompanyID)
                .FirstOrDefault();

            if (companyID == 0)
                return null;

            return CompanyRepository.GetCompany(db, companyID);
        }

        public static string PeekCompanyCode(DSModel db, string prefix)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.ExecuteScalar<string>("SELECT PeekCompanyCode(@Prefix)", new MySqlParameter("Prefix", prefix));
        }

        public static List<CompanyLicensePayRateModel> GetLicensesPayRates(DSModel db, uint companyID, DateTime? date = null)
        {
            var query = PredicateBuilder.True<CompaniesLicensesPayrate>();
            query = query.And(c => c.CompanyID == companyID);
            if (date.HasValue)
            {
                query = query.And(c => c.FromDate <= date.Value.Date && (c.ToDate >= date || !c.ToDate.HasValue));
            }

            return db.CompaniesLicensesPayrates
                .Where(query)
                .Select(c => new CompanyLicensePayRateModel()
                {
                    CompanyDriverID = c.CompanyDriverID,
                    CompanyID = c.CompanyID,
                    LicenseID = c.LicenseID,
                    PayRate = c.PayRate,
                    PayRateOT = c.PayRateOT,
                    FromDate = c.FromDate,
                    ToDate = c.ToDate,
                    IsChanged = false
                })
                .ToList();
        }

        public static List<CompanyInvoicingPayRateModel> GetInvoicePayRates(DSModel db, uint companyID, DateTime? date = null)
        {
            var query = PredicateBuilder.True<CompaniesInvoicingPayrate>();
            query = query.And(c => c.CompanyID == companyID);
            if (date.HasValue)
            {
                query = query.And(c => c.FromDate <= date.Value.Date && (c.ToDate >= date || !c.ToDate.HasValue));
            }

            return db.CompaniesInvoicingPayrates
                .Where(query)
                .Select(c => new CompanyInvoicingPayRateModel()
                {
                    CompanyInvoicingID = c.CompanyInvoicingID,
                    CompanyID = c.CompanyID,
                    LicenseID = c.LicenseID,
                    PayRate = c.PayRate,
                    PayRateOT = c.PayRateOT,
                    FromDate = c.FromDate,
                    ToDate = c.ToDate,
                    IsChanged = false
                })
                .ToList();
        }

        public static void SaveCompany(DSModel db, KeyBinder key, CompanyModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.CompanyID == 0)
                InsertCompany(db, key, model);
            else
                UpdateCompany(db, key, model);
        }

        private static void InsertCompany(DSModel db, KeyBinder key, CompanyModel model)
        {
            Company poco = new Company();
            poco.CompanyID = model.CompanyID;
            poco.CompanyName = model.CompanyName;
            if (model.CompanyCode == string.Empty)
                poco.CompanyCode = "C" + CompanyRepository.PeekCompanyCode(db, "C");
            else
                poco.CompanyCode = model.CompanyCode;
            poco.CompanyAddress1 = model.CompanyAddress1;
            poco.CompanyAddress2 = model.CompanyAddress2;
            poco.CompanyCity = model.CompanyCity;
            poco.CompanyState = model.CompanyState;
            poco.CompanyPostCode = model.CompanyPostCode;
            poco.CompanyContactName = model.CompanyContactName;
            poco.CompanyFax = model.CompanyFax;
            poco.CompanyPhone = model.CompanyPhone;
            poco.CompanyEmail = model.CompanyEmail;
            poco.LunchTime = model.LunchTime;
            poco.TrainingTime = model.TrainingTime;
            poco.IsEnabled = model.IsEnabled;

            foreach (var loc in model.Locations)
            {
                Location l = LocationRepository.SaveLocation(db, key, loc, poco);
                poco.Locations.Add(l);
                key.AddKey(l, loc, loc.GetName(p => p.LocationID));
                key.AddKey(poco, loc, loc.GetName(p => p.CompanyID));
            }

            db.Add(poco);

            key.AddKey(poco, model, model.GetName(p => p.CompanyID));
        }

        private static void UpdateCompany(DSModel db, KeyBinder key, CompanyModel model)
        {
            Company poco = db.Companies.Where(c => c.CompanyID == model.CompanyID).FirstOrDefault();
            if (poco == null)
                throw new ArgumentException("No company with the specified ID!");

            poco.CompanyID = model.CompanyID;
            poco.CompanyName = model.CompanyName;
            poco.CompanyCode = model.CompanyCode;
            poco.CompanyAddress1 = model.CompanyAddress1;
            poco.CompanyAddress2 = model.CompanyAddress2;
            poco.CompanyCity = model.CompanyCity;
            poco.CompanyState = model.CompanyState;
            poco.CompanyPostCode = model.CompanyPostCode;
            poco.CompanyContactName = model.CompanyContactName;
            poco.CompanyFax = model.CompanyFax;
            poco.CompanyPhone = model.CompanyPhone;
            poco.CompanyEmail = model.CompanyEmail;
            poco.LunchTime = model.LunchTime;
            poco.TrainingTime = model.TrainingTime;
            poco.IsEnabled = model.IsEnabled;

            List<Location> locationsToBeDeleted = poco.Locations.Where(dl => !model.Locations.Any(ml => ml.LocationID == dl.LocationID)).ToList();
            foreach (var del in locationsToBeDeleted)
            {
                db.Delete(del);
                poco.Locations.Remove(del);
            }

            foreach (var ins in model.Locations)
            {
                Location loc = LocationRepository.SaveLocation(db, key, ins, poco);
                poco.Locations.Add(loc);
                key.AddKey(loc, ins, ins.GetName(p => p.LocationID));
            }
        }

        public static void DeleteCompany(DSModel db, CompanyModel model)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            if (model.CompanyID != 0)
            {
                var poco = db.Companies.Where(c => c.CompanyID == model.CompanyID).FirstOrDefault();
                if (poco != null)
                {
                    db.Delete(poco);
                }
            }
        }

        public static void SaveLicensesPayRates(DSModel db, KeyBinder key, List<CompanyLicensePayRateModel> rates)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (rates == null)
                throw new ArgumentNullException("rates");

            if (rates.Count == 0)
                return;

            uint[] distCompanies = rates.Select(c => c.CompanyID).Distinct().ToArray();
            uint[] oldIDs = rates.Select(c => c.CompanyDriverID).ToArray();
            db.CompaniesLicensesPayrates.Where(c => distCompanies.Contains(c.CompanyID) && !oldIDs.Contains(c.CompanyDriverID)).DeleteAll();

            foreach (var pay in rates.Where(r => r.CompanyDriverID == 0))
            {
                CompaniesLicensesPayrate poco = new DAL.CompaniesLicensesPayrate();
                poco.CompanyID = pay.CompanyID;
                poco.LicenseID = pay.LicenseID;
                poco.PayRate = pay.PayRate;
                poco.PayRateOT = pay.PayRateOT;
                poco.FromDate = pay.FromDate;
                poco.ToDate = pay.ToDate;
                key.AddKey(poco, pay, pay.GetName(p => p.CompanyDriverID));
                db.Add(poco);
            }

            foreach (var pay in rates.Where(r => r.CompanyDriverID != 0))
            {
                CompaniesLicensesPayrate poco = db.CompaniesLicensesPayrates.Where(r => r.CompanyDriverID == pay.CompanyDriverID).FirstOrDefault();
                poco.CompanyID = pay.CompanyID;
                poco.LicenseID = pay.LicenseID;
                poco.PayRate = pay.PayRate;
                poco.PayRateOT = pay.PayRateOT;
                poco.FromDate = pay.FromDate;
                poco.ToDate = pay.ToDate;
            }
        }

        public static void SaveInvoicePayRates(DSModel db, KeyBinder key, List<CompanyInvoicingPayRateModel> rates)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (rates == null)
                throw new ArgumentNullException("rates");

            if (rates.Count == 0)
                return;

            uint[] distCompanies = rates.Select(c => c.CompanyID).Distinct().ToArray();
            uint[] oldIDs = rates.Select(c => c.CompanyInvoicingID).ToArray();
            db.CompaniesInvoicingPayrates.Where(c => distCompanies.Contains(c.CompanyID) && !oldIDs.Contains(c.CompanyInvoicingID)).DeleteAll();

            foreach (var pay in rates.Where(r => r.CompanyInvoicingID == 0))
            {
                CompaniesInvoicingPayrate poco = new DAL.CompaniesInvoicingPayrate();
                poco.CompanyID = pay.CompanyID;
                poco.LicenseID = pay.LicenseID;
                poco.PayRate = pay.PayRate;
                poco.PayRateOT = pay.PayRateOT;
                poco.FromDate = pay.FromDate;
                poco.ToDate = pay.ToDate;
                key.AddKey(poco, pay, pay.GetName(p => p.CompanyInvoicingID));
                db.Add(poco);
            }

            foreach (var pay in rates.Where(r => r.CompanyInvoicingID != 0))
            {
                CompaniesInvoicingPayrate poco = db.CompaniesInvoicingPayrates.Where(r => r.CompanyInvoicingID == pay.CompanyInvoicingID).FirstOrDefault();
                poco.CompanyID = pay.CompanyID;
                poco.LicenseID = pay.LicenseID;
                poco.PayRate = pay.PayRate;
                poco.PayRateOT = pay.PayRateOT;
                poco.FromDate = pay.FromDate;
                poco.ToDate = pay.ToDate;
            }
        }

        public static List<CompanyModel> FindCompanies(DSModel db, string name = null, string code = null, string address1 = null, string address2 = null,
            string city = null, string state = null, string postCode = null, string fax = null, string phone = null, string email = null, bool? includeDisabled = null)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var query = PredicateBuilder.True<Company>();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.And(c => c.CompanyName.StartsWith(name));
            if (!string.IsNullOrWhiteSpace(code))
                query = query.And(c => c.CompanyCode.StartsWith(code));
            if (!string.IsNullOrWhiteSpace(address1))
                query = query.And(c => c.CompanyAddress1.StartsWith(address1));
            if (!string.IsNullOrWhiteSpace(address2))
                query = query.And(c => c.CompanyAddress2.StartsWith(address2));
            if (!string.IsNullOrWhiteSpace(city))
                query = query.And(c => c.CompanyCity.StartsWith(city));
            if (!string.IsNullOrWhiteSpace(state))
                query = query.And(c => c.CompanyState.StartsWith(state));
            if (!string.IsNullOrWhiteSpace(postCode))
                query = query.And(c => c.CompanyPostCode.StartsWith(postCode));
            if (!string.IsNullOrWhiteSpace(fax))
                query = query.And(c => c.CompanyFax.StartsWith(fax));
            if (!string.IsNullOrWhiteSpace(phone))
                query = query.And(c => c.CompanyPhone.StartsWith(phone));
            if (!string.IsNullOrWhiteSpace(email))
                query = query.And(c => c.CompanyEmail.StartsWith(email));
            if (includeDisabled.HasValue && !includeDisabled.Value)
                query = query.And(c => c.IsEnabled);

            return db.Companies
                .Where(query)
                .Select(c => new CompanyModel()
                {
                    CompanyID = c.CompanyID,
                    CompanyName = c.CompanyName,
                    CompanyCode = c.CompanyCode,
                    CompanyAddress1 = c.CompanyAddress1,
                    CompanyAddress2 = c.CompanyAddress2,
                    CompanyCity = c.CompanyCity,
                    CompanyState = c.CompanyState,
                    CompanyPostCode = c.CompanyPostCode,
                    CompanyContactName = c.CompanyContactName,
                    CompanyFax = c.CompanyFax,
                    CompanyPhone = c.CompanyPhone,
                    CompanyEmail = c.CompanyEmail,
                    IsEnabled = c.IsEnabled,
                    IsChanged = false
                })
                .OrderBy(c => c.CompanyName)
                .ToList();
        }
    }
}
