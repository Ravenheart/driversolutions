using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.DAL
{
    [ImplementPropertyChanged]
    public class UtilityModel<T>
    {
        public T Value { get; set; }
        public string Display { get; set; }
        public bool IsCheck { get; set; }

        public UtilityModel() { }

        public UtilityModel(T value, string display)
        {
            this.Value = value;
            this.Display = display;
        }
    }
}
