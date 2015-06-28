using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Validators.ModuleDriver
{
    public class DriverLicenseValidator
    {
        public static CheckResult ValidateSave(DSModel db, DriverLicenseModel model)
        {
            CheckResult res = new CheckResult(model);

            if (model.DriverID == 0)
                res.AddError("Driver cannot be empty!", model.GetName(p => p.DriverID));
            if (model.LicenseID == 0)
                res.AddError("License type cannot be empty!", model.GetName(p => p.LicenseID));
            if (model.IssueDate == DateTime.MinValue)
                res.AddError("Issue date cannot be empty!", model.GetName(p => p.IssueDate));
            if (model.ExpirationDate == DateTime.MinValue)
                res.AddError("Expiration date cannot be empty!", model.GetName(p => p.ExpirationDate));
            if (model.MVRReviewDate.HasValue && model.MVRReviewDate.Value.Date < model.IssueDate.Date)
                res.AddError("MVR review date cannot be earlier than Issue date!", model.GetName(p => p.MVRReviewDate));
            if (model.IssueDate.Date > model.ExpirationDate.Date)
                res.AddError("Expiration date cannot be earlier than Issue date!", model.GetName(p => p.ExpirationDate));
            if (model.Permits.Count == 0)
                res.AddError("At least one permit must be selected!", model.GetName(p => p.Permits));

            //overlap check
            var check = db.DriversLicenses
                .Where(d =>
                    d.DriverLicenseID != model.DriverLicenseID &&
                    d.DriverID == model.DriverID &&
                    d.IssueDate < model.ExpirationDate &&
                    d.ExpirationDate > model.IssueDate)
                .FirstOrDefault();
            if (check != null)
                res.AddError("There is an overlapping license already!");

            return res;
        }

        public static CheckResult ValidateDelete(DSModel db, DriverLicenseModel model)
        {
            CheckResult res = new CheckResult(model);

            var checkDispatch = db.Dispatches
                .Where(d => d.DriverID == model.DriverID &&
                    d.FromDateTime.Date >= model.IssueDate &&
                    d.ToDateTime.Date <= model.ExpirationDate)
                .FirstOrDefault();
            if (checkDispatch != null)
                res.AddError("Cannot delete license, a dispatch uses the license!");

            return res;
        }
    }
}
