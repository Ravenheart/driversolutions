using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Validators.ModuleSystem
{
    public class FileBlobValidator
    {
        public static CheckResult ValidateSave(DSModel db, FileBlobModel model)
        {
            CheckResult res = new CheckResult(model);

            if (string.IsNullOrWhiteSpace(model.BlobName))
                res.AddError("File name cannot bet empty!", model.GetName(p => p.BlobName));
            if (string.IsNullOrWhiteSpace(model.BlobExtension))
                res.AddError("File extension cannot be empty!", model.GetName(p => p.BlobExtension));
            if (model.BlobData == null || model.BlobData.Length == 0)
                res.AddError("File data cannot be empty!", model.GetName(p => p.BlobData));

            return res;
        }
        public static CheckResult ValidateSaveView(DSModel db, FileBlobViewModel model)
        {
            CheckResult res = new CheckResult(model);

            if (string.IsNullOrWhiteSpace(model.BlobName))
                res.AddError("File name cannot bet empty!", model.GetName(p => p.BlobName));
            if (string.IsNullOrWhiteSpace(model.BlobExtension))
                res.AddError("File extension cannot be empty!", model.GetName(p => p.BlobExtension));

            return res;
        }

        public static CheckResult ValidateDelete(DSModel db, FileBlobModel model)
        {
            CheckResult res = new CheckResult(model);

            return res;
        }
    }
}
