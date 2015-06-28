using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Validators.ModuleFinance
{
    public class InvoiceValidator
    {
        public static CheckResult ValidateSave(DSModel db, InvoiceModel model)
        {
            CheckResult res = new CheckResult(model);
            if (model.CompanyID == 0)
                res.AddError("Please choose a Company!", model.GetName(p => p.CompanyID));
            if (model.LocationID == 0)
                res.AddError("Please choose a Location!", model.GetName(p => p.LocationID));
            if (model.InvoicePeriodFrom == DateTime.MinValue)
                res.AddError("Please enter a date for Period From!", model.GetName(p => p.InvoicePeriodFrom));
            if (model.InvoicePeriodTo == DateTime.MinValue)
                res.AddError("Please enter a date for Period To!", model.GetName(p => p.InvoicePeriodTo));
            if (model.InvoicePeriodTo < model.InvoicePeriodFrom)
                res.AddError("The date for Pertiod To cannot be earlier than Period From!", model.GetName(p => p.InvoicePeriodTo));

            return res;
        }

        public static CheckResult ValidateDelete(DSModel db, InvoiceModel model)
        {
            return new CheckResult(model);
        }
    }
}
