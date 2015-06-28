using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Managers.ModuleSystem
{
    public interface IUserManager
    {
        UserModel ActiveModel { get; set; }
        UserModel GetUser(uint userID);
        UserModel GetUser(string username, string password);

        CheckResult SaveUser(UserModel user);
    }
}
