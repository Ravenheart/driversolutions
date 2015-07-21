using DriverSolutions.BOL.Models.ModuleNotification;
using DriverSolutions.BOL.Models.ModuleNotification.Enums;
using DriverSolutions.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace DriverSolutions.BOL.Repositories.ModuleNotification
{
    public class NotificationRepository
    {
        /// <summary>
        /// Gets all medical notifications to be sent
        /// </summary>
        /// <param name="db">Database</param>
        /// <param name="checkDate">Date for which to check for notification</param>
        /// <returns></returns>
        public static List<NotificationModel> GetMedicalNotifications(DSModel db, DateTime checkDate = default(DateTime))
        {
            if (db == null)
                throw new ArgumentNullException("db");

            if (checkDate == DateTime.MinValue)
                checkDate = DateTime.Now.Date;

            return db.ExecuteQuery<NotificationModel>("CALL GetMedicalNotifications(@CheckDate);", new MySqlParameter("CheckDate", checkDate.Date)).ToList();
        }

        /// <summary>
        /// Gets all license notifications to be sent
        /// </summary>
        /// <param name="db"></param>
        /// <param name="checkDate"></param>
        /// <returns></returns>
        public static List<NotificationModel> GetLicenseNotifications(DSModel db, DateTime checkDate = default(DateTime))
        {
            if (db == null)
                throw new ArgumentNullException("db");

            if (checkDate == DateTime.MinValue)
                checkDate = DateTime.Now.Date;

            return db.ExecuteQuery<NotificationModel>("CALL GetLicenseNotifications(@CheckDate);", new MySqlParameter("CheckDate", checkDate.Date)).ToList();
        }

        /// <summary>
        /// Updates the status of the reminder (has been notified or not)
        /// </summary>
        /// <param name="db">Database</param>
        /// <param name="model">Notification</param>
        /// <param name="status">Has sent notification</param>
        public static void UpdateNotificationStatus(DSModel db, NotificationModel model, bool status)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.NotificationTypeID == (uint)NotificationType.Medical)
            {
                db.DriversMedicalsReminders
                    .Where(r => r.DriverMedicalReminderID == model.NotificationID)
                    .UpdateAll(u => u.Set(r => r.HasSentReminder, r => status));
            }
            else if (model.NotificationTypeID == (uint)NotificationType.License)
            {
                db.DriversLicensesReminders
                    .Where(r => r.DriverLicenseReminderID == model.NotificationID)
                    .UpdateAll(u => u.Set(r => r.HasSentReminder, r => status));
            }
        }
    }
}
