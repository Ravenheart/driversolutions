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
    public class UserManager : ManagerBase, IUserManager
    {
        public static UserManager Create()
        {
            return new UserManager(DB.GetContext(), new UserModel());
        }

        public static UserManager CreateEdit(uint userID)
        {
            var db = DB.GetContext();
            var user = UserRepository.GetUser(db, userID);

            return new UserManager(db, user);
        }

        public static UserManager CreateEdit(UserModel user)
        {
            var db = DB.GetContext();

            return new UserManager(db, user);
        }

        private UserManager(DSModel context, UserModel user)
            : base(context)
        {
            this.ActiveModel = user;
        }

        public UserModel ActiveModel { get; set; }

        public UserModel GetUser(uint userID)
        {
            return UserRepository.GetUser(this.DbContext, userID);
        }

        public UserModel GetUser(string username, string password)
        {
            return UserRepository.GetUser(this.DbContext, username, password);
        }

        public CheckResult SaveUser(UserModel user)
        {
            try
            {
                using (var db = DB.GetContext())
                {
                    var check = UserValidator.ValidateSave(db, user);
                    if (check.Failed)
                        return check;

                    KeyBinder key = new KeyBinder();
                    UserRepository.SaveUser(db, key, user);
                    db.SaveChanges();
                    key.BindKeys();
                    user.IsChanged = false;
                    return check;
                }
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }
    }
}
