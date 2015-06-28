using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Validators.ModuleSystem
{
    public class UserValidator
    {
        public static CheckResult ValidateSave(DSModel db, UserModel user)
        {
            CheckResult res = new CheckResult();
            if (string.IsNullOrWhiteSpace(user.Username))
                res.AddError("Please enter a Username!", user.GetName(p => p.Username));
            if (string.IsNullOrWhiteSpace(user.FirstName))
                res.AddError("Please enter a First Name!", user.GetName(p => p.FirstName));
            if (string.IsNullOrWhiteSpace(user.LastName))
                res.AddError("Please enter a Last Name!", user.GetName(p => p.LastName));

            var check = db.Users.Where(u => u.Username == user.Username && u.UserID != user.UserID).FirstOrDefault();
            if (check != null)
                res.AddError("Another user already uses this username!", user.GetName(p => p.Username));

            return res;
        }
    }
}
