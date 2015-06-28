using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.BOL.Validators.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleSystem
{
    public class FileBlobManager : IFileBlobManager
    {
        public static IFileBlobManager Create(FileBlobCreationInfoModel creation)
        {
            FileBlobManager manager = new FileBlobManager();
            manager.CreationInfo = creation;
            manager.ActiveModel.DriverID = creation.DriverID;

            return manager;
        }

        public static IFileBlobManager CreateEdit(uint blobID)
        {
            using (var db = DB.GetContext())
            {
                FileBlobManager manager = new FileBlobManager();
                manager.ActiveModel = FileBlobRepository.GetBlob(db, blobID);
                manager.CreationInfo.DriverID = manager.ActiveModel.DriverID;

                return manager;
            }
        }

        private FileBlobManager()
        {
            this.ActiveModel = new FileBlobModel();
            this.CreationInfo = new FileBlobCreationInfoModel();
            this.ActiveFiles = new BindingList<FileBlobViewModel>();
        }

        public FileBlobCreationInfoModel CreationInfo { get; set; }
        public FileBlobModel ActiveModel { get; set; }
        public BindingList<FileBlobViewModel> ActiveFiles { get; set; }

        public virtual void RefreshFileList()
        {
            using (var db = DB.GetContext())
            {
                this.ActiveFiles.Clear();
                var files = FileBlobRepository.GetViewBlobsByDriver(db, this.CreationInfo.DriverID.GetValueOrDefault());
                foreach (var file in files)
                {
                    this.ActiveFiles.Add(file);
                }
            }
        }


        public CheckResult SaveFile(FileBlobModel model)
        {
            if (!model.IsChanged)
                return new CheckResult(model);

            using (DSModel db = DB.GetContext())
            {
                var check = FileBlobValidator.ValidateSave(db, model);
                if (check.Failed)
                    return check;

                KeyBinder key = new KeyBinder();
                try
                {
                    FileBlobRepository.SaveBlob(db, key, model);
                    db.SaveChanges();
                    key.BindKeys();
                    model.IsChanged = false;
                    return new CheckResult(model);
                }
                catch (Exception ex)
                {
                    key.RollbackKeys();
                    return new CheckResult(ex);
                }
            }
        }
        public CheckResult SaveFile(FileBlobViewModel model)
        {
            if (!model.IsChanged)
                return new CheckResult(model);

            using (var db = DB.GetContext())
            {
                var check = FileBlobValidator.ValidateSaveView(db, model);
                if (check.Failed)
                    return check;

                KeyBinder key = new KeyBinder();
                try
                {
                    FileBlobRepository.SaveBlobView(db, key, model);
                    db.SaveChanges();
                    key.BindKeys();
                    model.IsChanged = false;
                    return new CheckResult(model);
                }
                catch (Exception ex)
                {
                    key.RollbackKeys();
                    return new CheckResult(ex);
                }
            }
        }
        public CheckResult DeleteFile(FileBlobModel model)
        {
            using (var db = DB.GetContext())
            {
                var check = FileBlobValidator.ValidateDelete(db, model);
                if (check.Failed)
                    return check;

                KeyBinder key = new KeyBinder();
                try
                {
                    FileBlobRepository.DeleteBlob(db, key, model);
                    db.SaveChanges();
                    key.BindKeys();
                    model.IsChanged = false;
                    return new CheckResult(model);
                }
                catch (Exception ex)
                {
                    key.RollbackKeys();
                    return new CheckResult(ex);
                }
            }
        }
        public CheckResult DeleteFile(FileBlobViewModel model)
        {
            FileBlobModel mod = new FileBlobModel();
            mod.BlobID = model.BlobID;

            return this.DeleteFile(mod);
        }

        public CheckResult PreviewFile(FileBlobViewModel model)
        {
            CheckResult res = new CheckResult(model);
            using (var db = DB.GetContext())
            {
                var file = FileBlobRepository.GetBlob(db, model.BlobID);
                if (file == null)
                {
                    res.AddError("No such file!", string.Empty);
                    return res;
                }

                string directory = Path.Combine(Environment.CurrentDirectory, "files");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                string fileName = Path.Combine(directory, ExtensionMethods.SafeFileName(file.BlobName) + file.BlobExtension);
                if (File.Exists(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch (Exception ex)
                    {
                        return new CheckResult(ex);
                    }
                }

                try
                {
                    File.WriteAllBytes(fileName, file.BlobData);
                    ProcessStartInfo info = new ProcessStartInfo(fileName);
                    info.UseShellExecute = true;
                    Process.Start(info);
                    return res;
                }
                catch (Exception ex)
                {
                    return new CheckResult(ex);
                }
            }
        }
        public CheckResult AttachMultipleFiles(string[] files)
        {
            using (var db = DB.GetContext())
            {
                KeyBinder key = new KeyBinder();
                try
                {
                    foreach (string file in files)
                    {
                        FileBlobModel mod = new FileBlobModel();
                        mod.BlobName = Path.GetFileNameWithoutExtension(file);
                        mod.BlobExtension = Path.GetExtension(file);
                        mod.BlobData = File.ReadAllBytes(file);
                        mod.DriverID = this.CreationInfo.DriverID;

                        FileBlobRepository.SaveBlob(db, key, mod);
                    }

                    db.SaveChanges();
                    key.BindKeys();
                    return new CheckResult();
                }
                catch (Exception ex)
                {
                    key.RollbackKeys();
                    return new CheckResult(ex);
                }
            }
        }
    }
}
