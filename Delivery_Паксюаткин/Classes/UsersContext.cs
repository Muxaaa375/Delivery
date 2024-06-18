
using Delivery_Паксюаткин.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Delivery_Паксюаткин.Classes
{
    public class UsersContext : Users
    {
        public UsersContext(int Id, string FIO, byte[] Image, string PhoneNumber, string Address, int IdRole, string Login, string Password)
            : base(Id, FIO, Image, PhoneNumber, Address, IdRole, Login, Password) { }

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
                    Data.IsDBNull(2) ? null : (byte[])Data["Image"],
                    Data.GetString(3),
                    Data.IsDBNull(4) ? null : Data.GetString(4),
                    Data.GetInt32(5),
                    Data.GetString(6),
                    Data.GetString(7)
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
                                "`IdRole`, " +
                                "`Login`, " +
                                "`Password`) " +
                           "VALUES (" +
                                $"'{this.FIO}', " +
                                $"@image, " +
                                $"'{this.PhoneNumber}', " +
                                $"'{this.Address}', " +
                                $"'{this.IdRole}', " +
                                $"'{this.Login}', " +
                                $"'{this.Password}')";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlCommand command = new MySqlCommand(SQL, connection);
            command.Parameters.AddWithValue("@image", this.Image);
            command.ExecuteNonQuery();
            Connection.CloseConnection(connection);
        }

        public void Update()
        {
            string SQL = "UPDATE " +
                            "`users` " +
                         "SET " +
                            $"`FIO`='{this.FIO}', " +
                            $"`Image`=@image, " +
                            $"`PhoneNumber`='{this.PhoneNumber}', " +
                            $"`Address`='{this.Address}', " +
                            $"`IdRole`='{this.IdRole}', " +
                            $"`Login`='{this.Login}', " +
                            $"`Password`='{this.Password}' " +
                         "WHERE " +
                            $"`Id`='{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlCommand command = new MySqlCommand(SQL, connection);
            command.Parameters.AddWithValue("@image", this.Image);
            command.ExecuteNonQuery();
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
