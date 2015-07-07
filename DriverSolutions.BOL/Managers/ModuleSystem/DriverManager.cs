using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.BOL.Repositories.ModuleSystem;
using DriverSolutions.BOL.Validators.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleSystem
{
    public class DriverManager : ManagerBase, IDriverManager
    {
        public static DriverManager CreateNew()
        {
            return new DriverManager(DB.GetContext(), new DriverModel());
        }

        public static DriverManager CreateEdit(uint driverID)
        {
            var db = DB.GetContext();
            var model = DriverRepository.GetDriver(db, driverID);

            return new DriverManager(db, model);
        }

        public static DriverManager CreateEdit(DriverModel driver)
        {
            return new DriverManager(DB.GetContext(), driver);
        }

        private DriverManager(DSModel db, DriverModel model)
            : base(db)
        {
            this.ActiveModel = model;
        }

        public DriverModel ActiveModel { get; set; }

        public CheckResult SaveDriver(DriverModel model)
        {
            try
            {
                using (var db = DB.GetContext())
                {
                    var check = DriverValidator.ValidateSave(db, model);
                    if (check.Failed)
                        return check;

                    KeyBinder key = new KeyBinder();
                    DriverRepository.SaveDriver(db, key, model);
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

        public CheckResult DeleteDriver(DriverModel model)
        {
            try
            {
                using (var db = DB.GetContext())
                {
                    var check = DriverValidator.ValidateDelete(db, model);
                    if (check.Failed)
                        return check;

                    DriverRepository.DeleteDriver(db, model);
                    db.SaveChanges();
                    return check;
                }
            }
            catch (Exception ex)
            {
                CheckResult res = new CheckResult();
                res.AddError("The following driver has records in the database and cannot be deleted!", "DriverID");
                return res;
            }
        }

        public List<LocationModel> GetLocations(uint? companyID = null)
        {
            if (companyID.HasValue)
                return LocationRepository.GetLocationsByCompanies(this.DbContext, new uint[] { companyID.Value });

            return LocationRepository.GetLocationsByCompanies(this.DbContext);
        }

        public List<CompanyModel> GetCompanies()
        {
            return CompanyRepository.FindCompanies(this.DbContext);
        }

        public List<UtilityModel<uint>> GetLicenses()
        {
            using (var db = DB.GetContext())
            {
                //TODO Fix
                List<UtilityModel<uint>> list = new List<UtilityModel<uint>>();
                list.Add(new UtilityModel<uint>(1, "Class A"));
                list.Add(new UtilityModel<uint>(2, "Class B"));
                return list;
            }
        }

        public string PeekDriverCode()
        {
            return DriverRepository.PeekDriverCode(this.DbContext, this.ActiveModel.DriverCode.TrimEnd(new char[10] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }));
        }

        public void UpdateDriverCode()
        {
            this.ActiveModel.DriverCode = this.ActiveModel.DriverCode.TrimEnd(new char[10] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
            this.ActiveModel.DriverCode += this.PeekDriverCode();
        }
    }
}
