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
    public class ObjectDeliveryContext : ObjectDelivery
    {
        public ObjectDeliveryContext(int Id, int IdDelivery, string Image, int Weight, string Commit, string GetNumber, string Address, string Status) : base(Id, IdDelivery, Image, Weight, Commit, GetNumber, Address, Status) { }

        public static List<ObjectDeliveryContext> Select()
        {
            List<ObjectDeliveryContext> AllObjectDelivery = new List<ObjectDeliveryContext>();
            string SQL = "SELECT * FROM `object_delivery`";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllObjectDelivery.Add(new ObjectDeliveryContext(
                    Data.GetInt32(0),
                    Data.GetInt32(1),
                    Data.GetString(2),
                    Data.GetInt32(3),
                    Data.GetString(4),
                    Data.GetString(5),
                    Data.GetString(6),
                    Data.GetString(7)
               ));
            }
            Connection.CloseConnection(connection);
            return AllObjectDelivery;
        }

        public void Add()
        {
            string SQL = "INSERT INTO " +
                            "`object_delivery`(" +
                                "`IdDelivery`, " +
                                "`Image`, " +
                                "`Weight`, " +
                                "`Commit`, " +
                                "`GetNumber`, " +
                                "`Address`, " +
                                "`Status`) " +
                           "VALUES (" +
                                $"'{this.IdDelivery}', " +
                                $"'{this.Image}', " +
                                $"'{this.Weight}', " +
                                $"'{this.Commit}', " +
                                $"'{this.GetNumber}', " +
                                $"'{this.Address}', " +
                                $"'{this.Status}')";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }

        public void Update()
        {
            string SQL = "UPDATE " +
                            "`object_delivery` " +
                         "SET " +
                            $"`IdDelivery`='{this.IdDelivery}', " +
                            $"`Image`='{this.Image}', " +
                            $"`Weight`='{this.Weight}', " +
                            $"`Commit`='{this.Commit}', " +
                            $"`GetNumber`='{this.GetNumber}', " +
                            $"`Address`='{this.Address}', " +
                            $"`Status`='{this.Status}'" +
                         "WHERE " +
                            $"`Id`='{this.Id}'";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
        public void Delete()
        {
            string SQL = $"DELETE FROM `object_delivery` WHERE `Id` = {this.Id}";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
