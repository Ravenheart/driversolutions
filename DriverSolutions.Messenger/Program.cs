using DriverSolutions.BOL.Repositories.ModuleMedical;
using DriverSolutions.BOL.Services;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.Messenger
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime checkDate = DateTime.Now.Date;

            string date = GetArg("CHECKDATE");
            if (!string.IsNullOrWhiteSpace(date))
            {
                checkDate = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }

            SMS sms = new SMS();

            using(var db = DB.GetContext())
            {
                var medicalNotifications = NotificationRepository.GetMedicalNotifications(db, checkDate);

                foreach (var med in medicalNotifications)
                {
                    if(string.IsNullOrWhiteSpace(med.CellPhone))
                        continue;

                    try
                    {
                        string cell = "1" + med.CellPhone.Replace("-", string.Empty).Replace(" ", string.Empty).Trim();
                        bool status = sms.SendSMS("ECDS", cell, med.Message);
                        NotificationRepository.UpdateMedicalNotificationStatus(db, med, status);
                    }
                    catch { }
                }
            }
        }

        private static string GetArg(string name)
        {
            string[] args = Environment.GetCommandLineArgs();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToUpperInvariant() == name.ToUpperInvariant())
                {
                    int index = i + 1;
                    if (args.Length >= index)
                        return args[index];

                    return string.Empty;
                }
            }

            return string.Empty;
        }
    }
}
