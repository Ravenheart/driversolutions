using DriverSolutions.BOL.Models.ModuleMedical;
using DriverSolutions.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace DriverSolutions.BOL.Repositories.ModuleMedical
{
    public class NotificationRepository
    {
        public static List<NotificationModel> GetMedicalNotifications(DSModel db, DateTime checkDate = default(DateTime))
        {
            if (db == null)
                throw new ArgumentNullException("db");

            if (checkDate == DateTime.MinValue)
                checkDate = DateTime.Now.Date;

            return db.ExecuteQuery<NotificationModel>("CALL GetMedicalNotifications(@CheckDate);", new MySqlParameter("CheckDate", checkDate.Date)).ToList();
        }

        /// <summary>
        /// Updates the status of the reminder (has been notified or not)
        /// </summary>
        /// <param name="db">Database</param>
        /// <param name="model">Notification</param>
        /// <param name="status">Has sent notification</param>
        public static void UpdateMedicalNotificationStatus(DSModel db, NotificationModel model, bool status)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (model == null)
                throw new ArgumentNullException("model");

            string sql = @"
                UPDATE drivers_medicals_reminders
                SET
                  HasSentReminder = @HasSentReminder
                WHERE
                  DriverMedicalReminderID = @DriverMedicalReminderID;";
            db.DriversMedicalsReminders
                .Where(r => r.DriverMedicalReminderID == model.DriverMedicalReminderID)
                .UpdateAll(u => u.Set(r => r.HasSentReminder, r => status));
        }
    }
}
