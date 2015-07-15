using DriverSolutions.BOL.Models.ModuleNotification;
using DriverSolutions.BOL.Models.ModuleNotification.Enums;
using DriverSolutions.BOL.Repositories.ModuleNotification;
using DriverSolutions.BOL.Services;
using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

            using (var db = DB.GetContext())
            {
                SMSService sms = new SMSService();
                EmailService email = new EmailService();
                var medicalNotifications = NotificationRepository.GetMedicalNotifications(db, checkDate);
                var licenseNotifications = NotificationRepository.GetLicenseNotifications(db, checkDate);

                //medicals
                foreach (var med in medicalNotifications)
                {
                    if (!string.IsNullOrWhiteSpace(med.CellPhone))
                        SendSMS(db, sms, med);
                    if (!string.IsNullOrWhiteSpace(med.Email))
                        SendEmail(db, email, med);
                }

                //licenses
                foreach (var lic in licenseNotifications)
                {
                    if (!string.IsNullOrWhiteSpace(lic.CellPhone))
                        SendSMS(db, sms, lic);
                    if (!string.IsNullOrWhiteSpace(lic.Email))
                        SendEmail(db, email, lic);
                }
            }
        }

        private static void SendSMS(DSModel db, SMSService sms, NotificationModel notice)
        {
            try
            {
                string senderNumber = GLOB.Settings.Get<string>(16);
                string[] managementNumbers = GLOB.Settings.Get<string>(17).Split(';');

                List<string> numbersToSend = new List<string>();
                if (notice.ReminderType == (uint)ReminderType.Driver || notice.ReminderType == (uint)ReminderType.MVR)
                    numbersToSend.Add("1" + notice.CellPhone.Replace("-", string.Empty).Replace(" ", string.Empty).Trim());
                if (notice.ReminderType == (uint)ReminderType.Management || notice.ReminderType == (uint)ReminderType.MVR)
                    numbersToSend.AddRange(managementNumbers);

                bool status = false;
                foreach (string number in numbersToSend)
                {
                    try
                    {
                        if (sms.SendSMS(senderNumber, number, notice.Message))
                            status = true;
                    }
                    catch { }
                }

                NotificationRepository.UpdateNotificationStatus(db, notice, status);
            }
            catch { }
        }

        private static void SendEmail(DSModel db, EmailService email, NotificationModel notice)
        {
            try
            {
                string senderEmail = GLOB.Settings.Get<string>(20);
                string[] managementEmails = GLOB.Settings.Get<string>(21).Split(';');

                List<string> emailsToSend = new List<string>();
                if (notice.ReminderType == (uint)ReminderType.Driver || notice.ReminderType == (uint)ReminderType.MVR)
                    emailsToSend.Add(notice.Email);
                if (notice.ReminderType == (uint)ReminderType.Management || notice.ReminderType == (uint)ReminderType.MVR)
                    emailsToSend.AddRange(managementEmails);

                bool status = false;
                foreach (string address in emailsToSend)
                {
                    try
                    {
                        if (!email.SendEmail(senderEmail, address, notice.Message, notice.Message).Result.Failed)
                            status = true;
                    }
                    catch { }
                }

                NotificationRepository.UpdateNotificationStatus(db, notice, status);
            }
            catch { }
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
