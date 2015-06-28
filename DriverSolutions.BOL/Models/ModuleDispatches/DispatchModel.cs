using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleDispatches
{
    public class DispatchModel : ModelBase
    {
        public DispatchModel()
        {
            this.UserID = GLOB.User.UserID;
            this.UserName = GLOB.User.ToString();
            this.LastUpdateTime = DateTime.Now;
        }

        public uint DispatchID { get; set; }
        public uint DriverID { get; set; }
        public uint CompanyID { get; set; }
        public uint LocationID { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }

        [Obsolete()]
        public int LunchTime { get; set; }
        [Obsolete()]
        public decimal TrainingTime { get; set; }

        public decimal? SpecialPayRate { get; set; }
        public decimal MiscCharge { get; set; }
        public string Note { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsConfirmed { get; set; }
        public bool HasLunch { get; set; }
        public bool HasTraining { get; set; }
        public uint UserID { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public double CancelledHours
        {
            get
            {
                return (ToDateTime - FromDateTime).TotalHours;
            }
            set
            {
                FromDateTime = FromDateTime.Date;
                ToDateTime = FromDateTime.Date.AddHours(value);
            }
        }

        //readonly
        public string UserName { get; set; }

        public DispatchModel NewCopy()
        {
            DispatchModel mod = new DispatchModel();
            mod.DriverID = this.DriverID;
            mod.CompanyID = this.CompanyID;
            mod.LocationID = this.LocationID;
            mod.FromDateTime = this.FromDateTime;
            mod.ToDateTime = this.ToDateTime;
            mod.SpecialPayRate = this.SpecialPayRate;
            mod.MiscCharge = this.MiscCharge;
            mod.Note = this.Note;
            mod.IsCancelled = this.IsCancelled;
            mod.IsConfirmed = this.IsConfirmed;
            mod.HasLunch = this.HasLunch;
            mod.HasTraining = this.HasTraining;
            mod.IsChanged = false;

            return mod;
        }

    }
}
