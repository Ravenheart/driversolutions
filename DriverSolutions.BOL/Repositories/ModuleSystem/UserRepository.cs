using DriverSolutions.BOL.Models.ModuleSystem;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DriverSolutions.BOL.Repositories.ModuleSystem
{
    public class UserRepository
    {
        public static UserModel GetUser(DSModel db, uint userID)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            var poco = db.Users.Where(u => u.UserID == userID).FirstOrDefault();
            if (poco == null)
                return null;

            UserModel mod = new UserModel();
            mod.UserID = poco.UserID;
            mod.Username = poco.Username;
            mod.FirstName = poco.FirstName;
            mod.SecondName = poco.SecondName;
            mod.LastName = poco.LastName;
            mod.IsEnabled = poco.IsEnabled;
            mod.IsAdmin = poco.IsAdmin;

            mod.IsChanged = false;

            return mod;
        }

        public static UserModel GetUser(DSModel db, string username, string password)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty or null!");
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty or null!");

            string hash = UserRepository.HashPassword(username, password);
            uint userID = db.Users
                    .Where(u => u.Username == username && u.Password == hash && u.IsEnabled)
                    .Select(u => u.UserID)
                    .FirstOrDefault();
            if (userID == 0)
                return null;

            return UserRepository.GetUser(db, userID);
        }

        public static void SaveUser(DSModel db, KeyBinder key, UserModel user)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (key == null)
                throw new ArgumentNullException("key");
            if (user == null)
                throw new ArgumentNullException("user");

            if (user.UserID == 0)
                InsertUser(db, key, user);
            else
                UpdateUser(db, key, user);
        }

        private static void InsertUser(DSModel db, KeyBinder key, UserModel user)
        {
            User poco = new User();
            poco.Username = user.Username;
            poco.Password = UserRepository.HashPassword(user.Username, user.Password);
            poco.FirstName = user.FirstName;
            poco.SecondName = user.SecondName;
            poco.LastName = user.LastName;
            poco.IsEnabled = user.IsEnabled;
            poco.IsAdmin = user.IsAdmin;
            key.AddKey(poco, user, user.GetName(p => p.UserID));
            db.Add(poco);
        }

        private static void UpdateUser(DSModel db, KeyBinder key, UserModel user)
        {
            var poco = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
            if (poco == null)
                throw new ArgumentException("No user with the specified ID!");

            poco.Username = user.Username;
            if (!string.IsNullOrWhiteSpace(user.Password))
                poco.Password = UserRepository.HashPassword(user.Username, user.Password);
            poco.FirstName = user.FirstName;
            poco.SecondName = user.SecondName;
            poco.LastName = user.LastName;
            poco.IsEnabled = user.IsEnabled;
            poco.IsAdmin = user.IsAdmin;
        }

        private static string HashPassword(string username, string password)
        {
            using (SHA512Managed sha = new SHA512Managed())
            {
                byte[] data = Encoding.UTF8.GetBytes(username + password);
                data = sha.ComputeHash(data);
                data = sha.ComputeHash(data);
                data = sha.ComputeHash(data);
                data = sha.ComputeHash(data);
                data = sha.ComputeHash(data);
                return BitConverter.ToString(data).Replace("-", string.Empty);
            }
        }

        public static List<UserModel> FindUsers(DSModel db, string username = null, string firstName = null, string secondName = null, string lastName = null)
        {
            var query = PredicateBuilder.True<User>();
            if (!string.IsNullOrWhiteSpace(username))
                query = query.And(q => q.Username.StartsWith(username));
            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.And(q => q.FirstName.StartsWith(firstName));
            if (!string.IsNullOrWhiteSpace(secondName))
                query = query.And(q => q.SecondName.StartsWith(secondName));
            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.And(q => q.LastName.StartsWith(lastName));

            return db.Users
                .Where(query)
                .Select(u => new UserModel()
                {
                    UserID = u.UserID,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    SecondName = u.SecondName,
                    LastName = u.LastName,
                    IsEnabled = u.IsEnabled,
                    IsAdmin = u.IsAdmin,
                    IsChanged = false
                })
                .ToList();
        }
    }
}
