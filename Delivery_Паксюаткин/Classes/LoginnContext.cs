using Delivery_Паксюаткин.Classes.Common;
using Delivery_Паксюаткин.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Classes
{
    public class LoginnContext : Loginn
    {
        public LoginnContext(int Id, string Login, string Password, string Role) : base(Id, Login, Password, Role) { }

        public static List<LoginnContext> Select()
        {
            List<LoginnContext> AllLogins = new List<LoginnContext>();
            string SQL = "SELECT * FROM `Login`";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllLogins.Add(new LoginnContext(
                    Data.GetInt32(0),
                    Data.GetString(1),
                    Data.GetString(2),
                    Data.GetString(3)
               ));
            }
            Connection.CloseConnection(connection);
            return AllLogins;
        }

        public void Add()
        {
            string SQL = "INSERT INTO `Login`(`Login`, `Password`, `Role`) " +
                         "VALUES (" +
                         $"'{this.Login}', " +
                         $"'{this.Password}', " +
                         $"'{this.Role}')";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }

        public void Update()
        {
            string SQL = "UPDATE `Login` SET " +
                         $"`Login`='{this.Login}', " +
                         $"`Password`='{this.Password}', " +
                         $"`Role`='{this.Role}' " +
                         $"WHERE `Id`='{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }

        public void Delete()
        {
            string SQL = $"DELETE FROM `Login` WHERE `Id` = '{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
