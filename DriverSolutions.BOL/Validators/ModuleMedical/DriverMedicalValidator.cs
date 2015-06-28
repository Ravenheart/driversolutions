using DriverSolutions.BOL.Models.ModuleMedical;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Validators.ModuleMedicals
{
    public class DriverMedicalValidator
    {
        public static CheckResult ValidateSave(DSModel db, DriverMedicalModel model)
        {
            CheckResult res = new CheckResult(model);

            if (model.DriverID == 0)
                res.AddError("Driver cannot be empty!", model.GetName(p => p.DriverID));
            if (model.MedTypeID == 0)
                res.AddError("Medical type cannot be empty!", model.GetName(p => p.MedTypeID));
            if (model.ExaminationDate == DateTime.MinValue)
                res.AddError("Examination date cannot be empty!", model.GetName(p => p.ExaminationDate));
            if (model.ValidityDate == DateTime.MinValue)
                res.AddError("Validity date cannot be empty!", model.GetName(p => p.ValidityDate));
            if (model.ValidityDate.Date <= model.ExaminationDate.Date)
                res.AddError("Validity date cannot be earlier than Examination date!", model.GetName(p => p.ValidityDate));

            return res;
        }

        public static CheckResult ValidateDelete(DSModel db, DriverMedicalModel model)
        {
            CheckResult res = new CheckResult(model);

            return res;
        }
    }
}
