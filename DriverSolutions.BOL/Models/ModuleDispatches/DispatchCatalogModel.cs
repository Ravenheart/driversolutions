using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Models.ModuleDispatches
{
    public class DispatchCatalogModel : DispatchModel
    {
        public string CompanyName { get; set; }
        public string DriverName { get; set; }
        public string LocationName { get; set; }

        public double WorkTime
        {
            get
            {
                var time = this.TotalTime;
                if (time > 8.0d)
                    return 8.0d;

                return time;
            }
            set
            {
                if (value < 8.0d)
                {
                    this.ToDateTime = this.FromDateTime.AddHours(value);
                }
                else
                {
                    this.ToDateTime = this.FromDateTime.Add(TimeSpan.FromHours(value + (this.LunchTime / 60.0d))).AddHours(this.OverTime);
                }
            }
        }

        public double OverTime
        {
            get
            {
                var time = this.TotalTime;
                if (time > 8.0d)
                    return time - 8.0d;

                return 0.0d;
            }
            set
            {
                this.ToDateTime = this.FromDateTime.Add(TimeSpan.FromHours(value + (this.LunchTime / 60.0d))).AddHours(8.0d);
            }
        }

        public double TotalTime
        {
            get
            {
                return (this.ToDateTime - this.FromDateTime).TotalHours - (this.LunchTime / 60.0d);
            }
        }
    }
}
