using DriverSolutions.BOL.Models.ModuleFinance;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Repositories.ModuleFinance
{
    public class HolidayRepository
    {
        public static List<HolidayModel> GetHolidays(DSModel db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.Holidays
                .Select(h => new HolidayModel()
                {
                    HolidayID = h.HolidayID,
                    HolidayDate = h.HolidayDate
                })
                .ToList();
        }

        public static void SaveHolidays(DSModel db, KeyBinder key, List<HolidayModel> model)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (model == null)
                throw new ArgumentNullException("model");

            var holidaysToDelete = db.Holidays.ToList();
            foreach (var h in holidaysToDelete.ToList())
            {
                var check = model.Where(hh => hh.HolidayDate == h.HolidayDate).FirstOrDefault();
                if (check == null)
                    db.Delete(h);
                else
                    model.Remove(check);
            }

            foreach (var h in model)
            {
                Holiday poco = new Holiday();
                poco.HolidayDate = h.HolidayDate;
                db.Add(poco);
                key.AddKey(poco, h, h.GetName(p => p.HolidayDate));
            }
        }
    }
}
