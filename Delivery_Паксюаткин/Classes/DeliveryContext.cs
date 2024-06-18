using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace Delivery_Паксюаткин.Classes
{
    public class DeliveryContext : Model.Delivery
    {
        public DeliveryContext(int Id, int UserId, int? DeliveryId, int IdObject, string FromAddress, string Status, string Commit, int Price, DateTime Date)
            : base(Id, UserId, DeliveryId, IdObject, FromAddress, Status, Commit, Price, Date) { }

        public static List<DeliveryContext> Select()
        {
            List<DeliveryContext> AllDelivery = new List<DeliveryContext>();
            string SQL = "SELECT * FROM `delivery`";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllDelivery.Add(new DeliveryContext(
                    Data.GetInt32(0),
                    Data.GetInt32(1),
                    Data.IsDBNull(2) ? (int?)null : Data.GetInt32(2),
                    Data.GetInt32(3),
                    Data.GetString(4),
                    Data.GetString(5),
                    Data.GetString(6),
                    Data.GetInt32(7),
                    Data.GetDateTime(8)
               ));
            }
            Connection.CloseConnection(connection);
            return AllDelivery;
        }

        public void Add()
        {
            string SQL = "INSERT INTO " +
                            "`delivery`(" +
                                "`UserId`, " +
                                "`DeliveryId`, " +
                                "`IdObject`, " +
                                "`FromAddress`, " +
                                "`Status`, " +
                                "`Commit`, " +
                                "`Price`, " +
                                "`Date`) " +
                           "VALUES (" +
                                $"'{this.UserId}', " +
                                (this.DeliveryId.HasValue ? $"'{this.DeliveryId.Value}'" : "NULL") + ", " +
                                $"'{this.IdObject}', " +
                                $"'{this.FromAddress}', " +
                                $"'{this.Status}', " +
                                $"'{this.Commit}', " +
                                $"'{this.Price}', " +
                                $"'{this.Date.ToString("yyyy-MM-dd HH:mm:ss")}')";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }

        public void Update()
        {
            string SQL = "UPDATE " +
                            "`delivery` " +
                         "SET " +
                            $"`UserId`='{this.UserId}', " +
                            $"`DeliveryId`={(this.DeliveryId.HasValue ? $"'{this.DeliveryId.Value}'" : "NULL")}, " +
                            $"`IdObject`='{this.IdObject}', " +
                            $"`FromAddress`='{this.FromAddress}', " +
                            $"`Status`='{this.Status}', " +
                            $"`Commit`='{this.Commit}', " +
                            $"`Price`='{this.Price}', " +
                            $"`Date`='{this.Date.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                         "WHERE " +
                            $"`Id`='{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }

        public void Delete()
        {
            string SQL = $"DELETE FROM `delivery` WHERE `Id` = '{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
