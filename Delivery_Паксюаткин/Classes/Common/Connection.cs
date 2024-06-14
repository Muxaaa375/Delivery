using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Classes.Common
{
    public class Connection
    {
        public static readonly string confing = "server=127.0.0.1;port=3308;uid=root;pwd=;database=Delivery;";
        public static MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(confing);
            connection.Open();

            return connection;
        }
        public static MySqlDataReader Query(string SQL, MySqlConnection connection)
        {
            return new MySqlCommand(SQL, connection).ExecuteReader();
        }
        public static void CloseConnection(MySqlConnection connection)
        {
            connection.Close();
            MySqlConnection.ClearPool(connection);
        }
    }
}
