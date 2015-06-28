using DriverSolutions.BOL.Models.ModuleDriver;
using DriverSolutions.BOL.Repositories.ModuleDriver;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.BOL.Validators.ModuleDriver;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleDriver
{
    public class DriverLicenseManager : IDriverLicenseManager
    {
        public static IDriverLicenseManager CreateEdit(uint driverLicenseID)
        {
            using (var db = DB.GetContext())
            {
                var manager = new DriverLicenseManager();
                manager.ActiveModel = DriverLicenseRepository.GetDriverLicense(db, driverLicenseID);
                manager.RefreshPermits();

                return manager;
            }
        }

        public static IDriverLicenseManager CreateNew(uint driverID = 0)
        {
            var manager = new DriverLicenseManager();
            manager.ActiveModel.DriverID = driverID;
            manager.ActiveModel.IssueDate = DateTime.Now.Date;
            manager.ActiveModel.ExpirationDate = DateTime.Now.Date.AddYears(1);
            manager.ActiveModel.MVRReviewDate = DateTime.Now.Date.AddYears(1);
            manager.RefreshPermits();

            return manager;
        }

        public static IDriverLicenseManager CreateRenew(uint oldDriverLicenseID)
        {
            using (var db = DB.GetContext())
            {
                var manager = new DriverLicenseManager();
                manager.ActiveModel = DriverLicenseRepository.GetDriverLicense(db, oldDriverLicenseID);
                manager.ActiveModel.DriverLicenseID = 0;
                manager.ActiveModel.IssueDate = manager.ActiveModel.ExpirationDate.AddDays(1);
                manager.ActiveModel.ExpirationDate = manager.ActiveModel.IssueDate.AddYears(1);
                if (manager.ActiveModel.MVRReviewDate.HasValue)
                    manager.ActiveModel.MVRReviewDate = manager.ActiveModel.MVRReviewDate.Value.AddYears(1);

                manager.RefreshPermits();

                return manager;
            }
        }

        protected DriverLicenseManager()
        {
            this.ActiveModel = new DriverLicenseModel();
            this.Permits = new BindingList<UtilityModel<uint>>();
        }

        public DriverLicenseModel ActiveModel { get; set; }
        public BindingList<UtilityModel<uint>> Permits { get; set; }

        public CheckResult Save()
        {
            var model = this.ActiveModel;

            try
            {
                using (var db = DB.GetContext())
                {
                    KeyBinder key = new KeyBinder();

                    model.Permits.Clear();
                    var permits = this.Permits
                        .Where(p => p.IsCheck)
                        .Select(p =>
                            new DriverLicensePermitModel()
                            {
                                PermitID = p.Value
                            });
                    model.Permits.AddRange(permits);

                    var check = DriverLicenseValidator.ValidateSave(db, model);
                    if (check.Failed)
                        return check;

                    DriverLicenseRepository.SaveDriverLicense(db, key, model);
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
        public CheckResult Delete()
        {
            var model = this.ActiveModel;

            try
            {
                using (var db = DB.GetContext())
                {
                    KeyBinder key = new KeyBinder();

                    var check = DriverLicenseValidator.ValidateDelete(db, model);
                    if (check.Failed)
                        return check;

                    DriverLicenseRepository.DeleteDriverLicense(db, key, model);
                    db.SaveChanges();
                    return check;
                }
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }

        public List<UtilityModel<uint>> GetLicenseClasses()
        {
            using (var db = DB.GetContext())
            {
                return LicenseRepository.GetLicenses(db, true)
                    .Select(l => new UtilityModel<uint>(l.LicenseID, l.LicenseName))
                    .ToList();
            }
        }
        public List<UtilityModel<uint>> GetReminderTypes()
        {
            using (var db = DB.GetContext())
            {
                return ReminderRepository.GetReminderTypes(db, isLicense: true)
                    .Select(r => new UtilityModel<uint>(r.ReminderID, r.ReminderName))
                    .ToList();
            }
        }
        public List<UtilityModel<uint>> GetReminderKinds()
        {
            using (var db = DB.GetContext())
            {
                return ReminderRepository.GetReminderKinds(db);
            }
        }

        private void RefreshPermits()
        {
            using (var db = DB.GetContext())
            {
                this.Permits.Clear();
                var perms = DriverLicenseRepository.GetPermits(db)
                    .Select(p => new UtilityModel<uint>(p.PermitID, p.PermitName));
                this.Permits.AddRange(perms);
                var check = this.Permits.Where(p => this.ActiveModel.Permits.Any(pp => pp.PermitID == p.Value));
                foreach (var permit in check)
                    permit.IsCheck = true;
            }
        }
    }
}
