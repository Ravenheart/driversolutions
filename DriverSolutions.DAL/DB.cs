using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.DAL
{
    public class DB
    {
        static DB()
        {
            string server = "localhost";
            string port = "3306";
            string database = "trucking";

            if (File.Exists("config.txt"))
            {
                string[] lines = File.ReadAllLines("config.txt");
                foreach (string l in lines)
                {
                    string[] tmp = l.Split('=');
                    string key = tmp[0].Trim().ToUpperInvariant();
                    string val = tmp[1].Trim();

                    if (key == "SERVER")
                        server = val;
                    else if (key == "PORT")
                        port = val;
                    else if (key == "DATABASE")
                        database = val;
                }
            }

            DB.ConnectionString = string.Format("Server={0}; Port={1}; Database={2}; uid=root; pwd=100000; AllowUserVariables=True; UseCompression=True; CharSet=utf8;",
                server,
                port,
                database);
        }

        public static string ConnectionString { get; set; }

        public static DSModel GetContext()
        {
            var db = new DSModel(DB.ConnectionString);
            db.ADO = new DSModel.ADOQuery(db);
            return db;
        }
    }
}
