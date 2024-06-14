using Delivery_Паксюаткин.Classes.Common;
using Delivery_Паксюаткин.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Delivery_Паксюаткин.Classes
{
    public class UsersContext : Users
    {
        public UsersContext(int Id, string FIO, string Image, string PhoneNumber, string Address, int IdRole) : base(Id, FIO, Image, PhoneNumber, Address, IdRole) { }

        public static List<UsersContext> Select()
        {
            List<UsersContext> AllUsers = new List<UsersContext>();
            string SQL = "SELECT * FROM `users`";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllUsers.Add(new UsersContext(
                    Data.GetInt32(0),
                    Data.GetString(1),
                    Data.GetString(2),
                    Data.GetString(3),
                    Data.GetString(4),
                    Data.GetInt32(5)
               ));
            }
            Connection.CloseConnection(connection);
            return AllUsers;
        }

        public void Add()
        {
            string SQL = "INSERT INTO " +
                            "`users`(" +
                                "`FIO`, " +
                                "`Image`, " +
                                "`PhoneNumber`, " +
                                "`Address`, " +
                                "`IdRole`) " +
                           "VALUES (" +
                                $"'{this.FIO}', " +
                                $"'{this.Image}', " +
                                $"'{this.PhoneNumber}', " +
                                $"'{this.Address}', " +
                                $"'{this.IdRole}')";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }

        public void Update()
        {
            string SQL = "UPDATE " +
                            "`users` " +
                         "SET " +
                            $"`FIO`='{this.FIO}', " +
                            $"`Image`='{this.Image}', " +
                            $"`PhoneNumber`='{this.PhoneNumber}', " +
                            $"`Address`='{this.Address}', " +
                            $"`IdRole`='{this.IdRole}' " +
                         "WHERE " +
                            $"`Id`='{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Delete()
        {
            string SQL = $"DELETE FROM `users` WHERE `Id` = '{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
