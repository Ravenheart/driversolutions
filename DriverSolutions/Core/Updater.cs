using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverSolutions.Core
{
    public class Updater
    {
        /// <summary>
        /// Check for new versions
        /// </summary>
        /// <returns>True if newer version exists, otherwise False</returns>
        public static bool CheckVersion()
        {
            using (var db = DB.GetContext())
            {
                var check = db.ExecuteQuery<DbVersion>(@"
                    SELECT v.VersionID
                         , v.VersionNumber
                    FROM
                      db_versions v
                    WHERE
                      v.IsReady = 1
                    ORDER BY
                      v.VersionNumber DESC
                    LIMIT
                      1;")
                    .FirstOrDefault();
                if (check == null)
                    return false;

                Version local = new Version(Application.ProductVersion);
                Version online = new Version(check.VersionNumber);

                return (online > local);
            }
        }

        public static void UpdateApplication()
        {
            using (var db = DB.GetContext())
            {
                var patcher = db.ExecuteScalar<byte[]>(@"
                    SELECT v.VersionPatcher
                    FROM
                      db_versions v
                    WHERE
                      v.IsReady = 1
                    ORDER BY
                      v.VersionNumber DESC
                    LIMIT
                      1;");

                File.WriteAllBytes("DriverSolutions.Patcher.exe", patcher);
                Process.Start("DriverSolutions.Patcher.exe");
                Environment.Exit(0);
            }
        }
    }
}
