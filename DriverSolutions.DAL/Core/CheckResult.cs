using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.DAL
{
    public class CheckResult
    {
        public object Entity { get; set; }
        public bool Failed
        {
            get
            {
                return this.Items.Any(i => i.ErrorType == ErrorType.Error);
            }
        }
        public bool Warning
        {
            get
            {
                return this.Items.Any(i => i.ErrorType == ErrorType.Warning);
            }
        }
        public string Message
        {
            get
            {
                if (this.Items.Count == 0)
                    return string.Empty;

                return string.Join<string>("\r\n", this.Items.Where(i => i.ErrorType == ErrorType.Error).Select(i => i.Message));
            }
        }
        public string Property
        {
            get
            {
                if (this.Items.Count == 0)
                    return string.Empty;

                return this.Items.Where(i => i.ErrorType == ErrorType.Error).First().Property;
            }
        }
        public List<CheckItem> Items { get; set; }

        public CheckResult()
        {
            this.Items = new List<CheckItem>();
        }

        public CheckResult(object entity)
            : this()
        {
            this.Entity = entity;
        }

        public CheckResult(Exception ex)
            : this()
        {
            this.AddError("An exception has occured!\r\n\r\n" + ex.ToString(), string.Empty);
        }

        public void AddError(string message, string property = "")
        {
            this.Items.Add(
                new CheckItem(
                    ErrorType.Error,
                    message,
                    property));
        }

        public void AddWarning(string message, string property = "")
        {
            this.Items.Add(
                new CheckItem(
                    ErrorType.Warning,
                    message,
                    property));
        }
    }
}
