using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.DAL
{
    public class Logger
    {
        public static void Log(LogType type, string location, string message)
        {
            using (var db = DB.GetContext())
            {
                Logger.Log(db, type, location, message);
                db.SaveChanges();
            }
        }

        public static void Log(DSModel db, LogType type, string location, string message)
        {
            DriverSolutions.DAL.Log log = new Log();
            try
            {
                log.LogType = (byte)type;
                log.LogLocation = location;
                log.LogMessage = message;
                db.Add(log);
                db.FlushChanges();
            }
            catch (Exception ex)
            {
                try
                {
                    File.AppendAllText("error.log", log.ToMessage());
                }
                catch { }
            }
        }

        public static void Log(Exception ex, string location = "")
        {
            using (var db = DB.GetContext())
            {
                Logger.Log(db, ex, location);
                db.SaveChanges();
            }
        }

        public static void Log(DSModel db, Exception ex, string location = "")
        {
            Logger.Log(db, LogType.Error, (location == "" ? "Error" : location), ex.ToString());
            db.FlushChanges();
        }
    }

    public enum LogType
    {
        Default = 0,
        Select = 1,
        Insert = 2,
        Update = 3,
        Delete = 4,
        Error = 5
    }
}
