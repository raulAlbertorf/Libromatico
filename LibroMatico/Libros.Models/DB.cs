using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libros.Models
{
    public class DB
    {
        private static string connStr = "SERVER=localhost;DATABASE=libromatico_api;UID=libromatico_user;PASSWORD=123456";

        public static DataSet GetDataSet(MySqlCommand command)
        {
            var ds = new DataSet();
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                command.Connection = conn;
                var sqlda = new MySqlDataAdapter(command);
                sqlda.Fill(ds);
                conn.Close();
            }
            return ds;
        }

        public static int QueryCommand(MySqlCommand command)
        {
            int success;
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                command.Connection = conn;
                success = command.ExecuteNonQuery();
                conn.Close();
            }
            
            return success;
        }
    }
}