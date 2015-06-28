using DriverSolutions.BOL.Models.ModuleDispatches;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Validators.ModuleDispatches
{
    public class DispatchValidator
    {
        public static CheckResult ValidateSave(DSModel db, DispatchModel model)
        {
            CheckResult res = new CheckResult(model);
            if (model.DriverID == 0)
                res.AddError("Please choose a Driver!", model.GetName(p => p.DriverID));
            if (model.CompanyID == 0)
                res.AddError("Please choose a Company!", model.GetName(p => p.CompanyID));
            if (model.LocationID == 0)
                res.AddError("Please choose a Location!", model.GetName(p => p.LocationID));
            if (model.ToDateTime < model.FromDateTime)
                res.AddError("End Date Time cannot be earlier than Start Date Time!", model.GetName(p => p.ToDateTime));
            if (model.TrainingTime < 0.0m)
                res.AddError("Training time cannot be negative!", model.GetName(p => p.TrainingTime));
            if (model.MiscCharge < 0m)
                res.AddError("Per diem cannot be negative!", model.GetName(p => p.MiscCharge));

            var checkLicense = db.DriversLicenses
                .Where(d => d.DriverID == model.DriverID &&
                    d.IssueDate <= model.FromDateTime.Date &&
                    d.ExpirationDate >= model.ToDateTime)
                .Count();
            if (checkLicense == 0)
                res.AddError("Driver has no valid license for the specified dispatch dates!", model.GetName(p => p.DriverID));

            //overlap check
            var check = db.Dispatches
                .Where(d =>
                    d.DispatchID != model.DispatchID &&
                    d.DriverID == model.DriverID &&
                    d.FromDateTime < model.ToDateTime &&
                    d.ToDateTime > model.FromDateTime)
                .FirstOrDefault();
            if (check != null)
                res.AddError(string.Format("This driver has overlapping dispatches!\r\n\r\nCompany: {0}\r\nLocation: {1}\r\nStart: {2}\r\nEnd: {3}",
                    check.Company.CompanyName,
                    check.Location.LocationName,
                    check.FromDateTime.ToString("g"),
                    check.ToDateTime.ToString("g")), model.GetName(p => p.FromDateTime));

            return res;
        }

        public static CheckResult ValidateDelete(DSModel db, DispatchModel model)
        {
            return new CheckResult(model);
        }
    }
}
