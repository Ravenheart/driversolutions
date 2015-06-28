using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Validators.ModuleSystem
{
    public class CompanyValidator
    {
        public static CheckResult ValidateSave(DSModel db, CompanyModel model)
        {
            CheckResult res = new CheckResult(model);
            if (string.IsNullOrWhiteSpace(model.CompanyName))
                res.AddError("Please enter a Company Name!", model.GetName(p => p.CompanyName));
            if (model.CompanyCode != string.Empty)
            {
                var check = db.Companies.Where(c => c.CompanyCode == model.CompanyCode && c.CompanyID != model.CompanyID).FirstOrDefault();
                if (check != null)
                    res.AddError("Another company already uses this Company Code! Use Peek or leave blank to autogenerate!", model.GetName(p => p.CompanyCode));
            }
            if (model.TrainingTime < 0m)
                res.AddError("Traning time cannot be negative number!", model.GetName(p => p.TrainingTime));

            var groups = model.Locations.GroupBy(l => l.LocationCode).ToList();
            if (groups.Any(g => g.Count() > 1))
            {
                var gr = groups.Where(g => g.Count() > 1).FirstOrDefault();
                res.AddError(string.Format("Duplicate location codes! Code: {0}", gr.FirstOrDefault().LocationCode));
            }
            return res;
        }

        public static CheckResult ValidateDelete(DSModel db, CompanyModel model)
        {
            return new CheckResult(model);
        }

        public static CheckResult ValidateInvoiceRates(DSModel db, List<CompanyInvoicingPayRateModel> rates)
        {
            var dic = db.ConstLicenses.ToDictionary(d => d.LicenseID, d => d.LicenseName);
            CheckResult res = new CheckResult();
            for (int i = 0; i < rates.Count; i++)
            {
                var cur = rates[i];
                if (cur.ToDate.HasValue && cur.ToDate.Value.Date < cur.FromDate.Date)
                {
                    res.AddError(
                        string.Format("To date cannot be earlier than From date! License: {0} From: {1} To: {2}",
                        dic[cur.LicenseID],
                        cur.FromDate.ToString(GLOB.Formats.Date),
                        cur.ToDate.GetValueOrDefault().ToString(GLOB.Formats.Date)),
                        cur.GetName(p => p.ToDate));
                    return res;
                }

                if (rates.Count > 1)
                {
                    for (int q = 0; q < rates.Count; q++)
                    {
                        if (q == i)
                            continue;

                        //overlap = ! ( (end2 < start1) || (start2 > end1) ); 
                        var next = rates[q];
                        if (!((next.ToDate.HasValue && next.ToDate.Value.Date < cur.FromDate.Date) || (cur.ToDate.HasValue && next.FromDate.Date > cur.ToDate.Value.Date))
                            && next.LicenseID == cur.LicenseID)
                        {
                            res.AddError(
                                string.Format("Overlapping invoice dates detected!\r\n\r\nLicense: {0} From: {1} To: {2}\r\nLicense: {3} From: {4} To: {5}",
                                dic[cur.LicenseID],
                                cur.FromDate.ToString(GLOB.Formats.Date),
                                (cur.ToDate.HasValue ? cur.ToDate.GetValueOrDefault().ToString(GLOB.Formats.Date) : "NOW"),
                                dic[next.LicenseID],
                                next.FromDate.ToString(GLOB.Formats.Date),
                                (next.ToDate.HasValue ? next.ToDate.GetValueOrDefault().ToString(GLOB.Formats.Date) : "NOW"))
                                , cur.GetName(p => p.ToDate));
                            return res;
                        }
                    }
                }
            }

            return res;
        }

        public static CheckResult ValidateDriverRates(DSModel db, List<CompanyLicensePayRateModel> rates)
        {
            var dic = db.ConstLicenses.ToDictionary(d => d.LicenseID, d => d.LicenseName);
            CheckResult res = new CheckResult();
            for (int i = 0; i < rates.Count; i++)
            {
                var cur = rates[i];
                if (cur.ToDate.HasValue && cur.ToDate.Value.Date < cur.FromDate.Date)
                {
                    res.AddError(
                        string.Format("To date cannot be earlier than From date! License: {0} From: {1} To: {2}",
                        dic[cur.LicenseID],
                        cur.FromDate.ToString(GLOB.Formats.Date),
                        cur.ToDate.GetValueOrDefault().ToString(GLOB.Formats.Date)),
                        cur.GetName(p => p.ToDate));
                    return res;
                }

                if (rates.Count > 1)
                {
                    for (int q = 0; q < rates.Count; q++)
                    {
                        if (q == i)
                            continue;

                        //overlap = ! ( (end2 < start1) || (start2 > end1) ); 
                        var next = rates[q];
                        if (!((next.ToDate.HasValue && next.ToDate.Value.Date < cur.FromDate.Date) || (cur.ToDate.HasValue && next.FromDate.Date > cur.ToDate.Value.Date))
                            && next.LicenseID == cur.LicenseID)
                        {
                            res.AddError(
                                string.Format("Overlapping driver dates detected!\r\n\r\nLicense: {0} From: {1} To: {2}\r\nLicense: {3} From: {4} To: {5}",
                                dic[cur.LicenseID],
                                cur.FromDate.ToString(GLOB.Formats.Date),
                                (cur.ToDate.HasValue ? cur.ToDate.GetValueOrDefault().ToString(GLOB.Formats.Date) : "NOW"),
                                dic[next.LicenseID],
                                next.FromDate.ToString(GLOB.Formats.Date),
                                (next.ToDate.HasValue ? next.ToDate.GetValueOrDefault().ToString(GLOB.Formats.Date) : "NOW"))
                                , cur.GetName(p => p.ToDate));
                            return res;
                        }
                    }
                }
            }

            return res;
        }
    }
}
