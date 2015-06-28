using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleSystem
{
    public class UserModel : ModelBase
    {
        public uint UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsAdmin { get; set; }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName + " (" + this.Username + ")";
        }
    }
}
