using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.BOL.Repositories.ModuleFinance;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleFinance
{
    public class HolidayManager : ManagerBase, IHolidayManager
    {
        public static HolidayManager Create()
        {
            return new HolidayManager(DB.GetContext());
        }

        private HolidayManager(DSModel db)
            : base(db)
        {
            this.ActiveModel = HolidayRepository.GetHolidays(db);
        }

        public List<HolidayModel> ActiveModel { get; set; }

        public List<HolidayModel> GetHolidays()
        {
            return HolidayRepository.GetHolidays(this.DbContext);
        }

        public CheckResult SaveHolidays(List<HolidayModel> model)
        {
            try
            {
                using (var db = DB.GetContext())
                {
                    KeyBinder key = new KeyBinder();
                    HolidayRepository.SaveHolidays(db, key, model);
                    db.SaveChanges();
                    key.BindKeys();
                    model.ForEach(f => f.IsChanged = false);
                    return new CheckResult();
                }
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }
    }
}
