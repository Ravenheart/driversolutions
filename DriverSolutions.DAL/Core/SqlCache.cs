using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.DAL
{
    public static class SqlCache
    {
        private static Dictionary<string, string> _Cache = new Dictionary<string, string>();

        public static string Get(string sqlName)
        {
            if (!_Cache.ContainsKey(sqlName))
            {
                using (var db = DB.GetContext())
                {
                    var sql = db.SqlQueries.Where(q => q.QueryName == sqlName).FirstOrDefault();
                    if (sql == null)
                        throw new ArgumentException("No such sql with the specified name!");

                    _Cache.Add(sqlName, sql.QuerySQL);
                }
            }

            return _Cache[sqlName];
        }

        public static string Get(DSModel db, string sqlName)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            if (!_Cache.ContainsKey(sqlName))
            {
                var sql = db.SqlQueries.Where(q => q.QueryName == sqlName).FirstOrDefault();
                if (sql == null)
                    throw new ArgumentException("No such sql with the specified name!");

                _Cache.Add(sqlName, sql.QuerySQL);
            }

            return _Cache[sqlName];
        }
    }
}
