using DriverSolutions.BOL.Models.ModuleMedical;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Repositories.ModuleSystem
{
    public class ReminderRepository
    {
        public static List<ReminderModel> GetReminderTypes(DSModel db, bool? isMedical = null, bool? isLicense = null)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var query = PredicateBuilder.True<ConstReminder>();
            if (isMedical.HasValue)
                query = query.And(q => q.IsMedical == isMedical.Value);
            if (isLicense.HasValue)
                query = query.And(q => q.IsLicense == isLicense.Value);

            return db.ConstReminders
                .Where(query)
                .Select(r => new ReminderModel(r))
                .ToList();
        }

        public static List<UtilityModel<uint>> GetReminderKinds(DSModel db)
        {
            List<UtilityModel<uint>> list = new List<UtilityModel<uint>>();
            list.Add(new UtilityModel<uint>(1, "Driver"));
            list.Add(new UtilityModel<uint>(2, "Management"));
            list.Add(new UtilityModel<uint>(3, "MVR"));

            return list;
        }
    }
}
