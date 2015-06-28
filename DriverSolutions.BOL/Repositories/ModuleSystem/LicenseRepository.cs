using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Repositories.ModuleSystem
{
    public class LicenseRepository
    {
        public static List<LicenseModel> GetLicenses(DSModel db, bool? isEnabled = null)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var query = PredicateBuilder.True<ConstLicense>();
            if (isEnabled.HasValue)
                query = query.And(l => l.IsEnabled == isEnabled.Value);

            return db.ConstLicenses
                .Where(query)
                .Select(l => new LicenseModel()
                {
                    LicenseID = l.LicenseID,
                    LicenseName = l.LicenseName,
                    IsEnabled = l.IsEnabled,
                    IsChanged = false
                })
                .ToList();
        }
    }
}
