using DriverSolutions.BOL.Models.ModuleMedical;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Repositories.ModuleMedical
{
    public class MedicalTypeRepository
    {
        public static List<MedicalTypeModel> GetMedicalTypes(DSModel db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            return db.ConstMedicalTypes
                .OrderBy(m => m.MedTypePosition)
                .Select(m => new MedicalTypeModel(m))
                .ToList();
        }


    }
}
