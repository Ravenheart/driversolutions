using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Validators.ModuleSystem
{
    public class DriverValidator
    {
        public static CheckResult ValidateSave(DSModel db, DriverModel model)
        {
            CheckResult res = new CheckResult(model);
            if (string.IsNullOrWhiteSpace(model.FirstName))
                res.AddError("Please enter a First Name of the driver!", model.GetName(p => p.FirstName));
            if (string.IsNullOrWhiteSpace(model.SecondName))
                res.AddWarning("Missing Second Name of the driver!", model.GetName(p => p.SecondName));
            if (string.IsNullOrWhiteSpace(model.LastName))
                res.AddError("Please enter a Last Name of the driver!", model.GetName(p => p.LastName));
            //if (model.LicenseID == 0)
            //    res.AddError("Please select a license!", model.GetName(p => p.LicenseID));
            //if (!model.LicenseExpirationDate.HasValue)
            //    res.AddError("Please enter a license expiration date", model.GetName(p => p.LicenseExpirationDate));
            if (model.PayRateOverride < 0.0m)
                res.AddError("Please enter a positive number for Pay Rate Override!", model.GetName(p => p.PayRateOverride));
            if (model.DriverCode != string.Empty)
            {
                var check = db.Drivers.Where(d => d.DriverCode == model.DriverCode && d.DriverID != model.DriverID).FirstOrDefault();
                if (check != null)
                    res.AddError("Another driver already uses this Driver Code! Use Peek or leave blank to autogenerate!", model.GetName(p => p.DriverCode));
            }
            foreach (var l in model.Locations)
            {
                if (l.LocationID == 0)
                    res.AddError("Please choose a Location for hte travel pay!", "LocationID");
                if (l.TravelPay == 0.0m)
                    res.AddError("Please enter a positive non-zero number for Travel Pay!", "TravelPay");
            }

            return res;
        }
        public static CheckResult ValidateDelete(DSModel db, DriverModel model)
        {
            return new CheckResult(model);
        }



    }
}
