using DriverSolutions.BOL.Models.ModuleMedical;
using DriverSolutions.BOL.Repositories.ModuleMedical;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.BOL.Validators.ModuleMedicals;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleMedical
{
    public class DriverMedicalManager : IDriverMedicalManager
    {
        public static IDriverMedicalManager CreateEdit(uint driverMedicalID)
        {
            using (var db = DB.GetContext())
            {
                var manager = new DriverMedicalManager();
                manager.ActiveModel = DriverMedicalRepository.GetMedical(db, driverMedicalID);

                return manager;
            }
        }

        public static IDriverMedicalManager CreateNew(uint driverID = 0)
        {
            var manager = new DriverMedicalManager();
            manager.ActiveModel.ExaminationDate = DateTime.Now.Date;
            manager.ActiveModel.ValidityDate = DateTime.Now.Date.AddYears(1);

            return manager;
        }

        public static IDriverMedicalManager CreateNewFromOld(uint oldDriverMedicalID)
        {
            using (var db = DB.GetContext())
            {
                var manager = new DriverMedicalManager();
                manager.ActiveModel = DriverMedicalRepository.GetMedical(db, oldDriverMedicalID);
                manager.ActiveModel.DriverMedicalID = 0;
                manager.ActiveModel.ExaminationDate = manager.ActiveModel.ExaminationDate.Date.AddDays(1);
                manager.ActiveModel.ValidityDate = manager.ActiveModel.ExaminationDate.Date.AddYears(1);

                return manager;
            }
        }


        private DriverMedicalManager()
        {
            this.ActiveModel = new DriverMedicalModel();
        }

        public DriverMedicalModel ActiveModel { get; set; }

        public CheckResult Save()
        {
            var mod = this.ActiveModel;
            try
            {
                using (var db = DB.GetContext())
                {
                    var check = DriverMedicalValidator.ValidateSave(db, mod);
                    if (check.Failed)
                        return check;

                    KeyBinder key = new KeyBinder();
                    DriverMedicalRepository.SaveMedical(db, key, mod);
                    db.SaveChanges();
                    key.BindKeys();
                    mod.IsChanged = false;
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
            var mod = this.ActiveModel;

            try
            {
                using (var db = DB.GetContext())
                {
                    var check = DriverMedicalValidator.ValidateDelete(db, mod);
                    if (check.Failed)
                        return check;

                    DriverMedicalRepository.DeleteMedical(db, mod);
                    db.SaveChanges();
                    return check;
                }
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }

        public List<UtilityModel<uint>> GetMedicalTypes()
        {
            using (var db = DB.GetContext())
            {
                return MedicalTypeRepository.GetMedicalTypes(db)
                    .Select(m => new UtilityModel<uint>(m.MedTypeID, m.MedTypeName))
                    .ToList();
            }
        }
        public List<UtilityModel<uint>> GetReminderTypes()
        {
            using (var db = DB.GetContext())
            {
                return ReminderRepository.GetReminderTypes(db, isMedical: true)
                    .Select(r => new UtilityModel<uint>(r.ReminderID, r.ReminderName))
                    .ToList();
            }
        }
        public List<UtilityModel<uint>> GetReminderKinds()
        {
            using (var db = DB.GetContext())
            {
                var kinds = ReminderRepository.GetReminderKinds(db);
                kinds.RemoveAll(r => r.Value == 3);

                return kinds;
            }
        }
    }
}
