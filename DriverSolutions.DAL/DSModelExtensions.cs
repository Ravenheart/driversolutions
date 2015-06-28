using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.DAL
{
    public partial class DSModel
    {
        public ADOQuery ADO { get; set; }

        public class ADOQuery
        {
            private DSModel DB;
            public ADOQuery(DSModel context)
            {
                this.DB = context;
            }

            public DataSet SelectTwo(string sql, params MySqlParameter[] parameters)
            {
                var con = DB.Connection;
                using (MySqlCommand cmd = new MySqlCommand(sql, (MySqlConnection)con.StoreConnection))
                using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
                {
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                    DataSet ds = new DataSet("data");
                    adp.Fill(ds);
                    return ds;
                }
            }

            public DataTable Select(string sql, params MySqlParameter[] parameters)
            {
                var con = DB.Connection;
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                    DataTable data = new DataTable("data");
                    using (var reader = cmd.ExecuteReader())
                    {
                        data.Load(reader, LoadOption.OverwriteChanges);
                        return data;
                    }
                }
            }

            public DataRow GetOne(string sql, params MySqlParameter[] parameters)
            {
                DataTable dt = Select(sql, parameters);
                if (dt.Rows.Count > 0)
                    return dt.Rows[0];

                return null;
            }

            public DataSet ProcedureSelectTwo(string procedureName, params MySqlParameter[] parameters)
            {
                var con = DB.Connection;
                using (MySqlCommand cmd = new MySqlCommand(procedureName, (MySqlConnection)con.StoreConnection))
                using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                    DataSet ds = new DataSet("data");
                    adp.Fill(ds);
                    return ds;
                }
            }

        }
    }
}
