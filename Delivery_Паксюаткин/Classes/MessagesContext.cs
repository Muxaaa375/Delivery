using Delivery_Паксюаткин.Classes;
using Delivery_Паксюаткин.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Classes
{
    public class MessagesContext : Messages
    {
        public MessagesContext(int Id, int IdDelivery, int SenderId, int GetId, string Message, string Image, DateTime DateTime)
            : base(Id, IdDelivery, SenderId, GetId, Message, Image, DateTime) { }

        public static List<Messages> Select()
        {
            List<Messages> allMessages = new List<Messages>();
            string SQL = "SELECT * FROM `messages`";
            MySqlConnection connection = Connection.OpenConnection();

            using (MySqlDataReader data = Connection.Query(SQL, connection))
            {
                while (data.Read())
                {
                    allMessages.Add(new Messages(
                        data.GetInt32(0),
                        data.GetInt32(1),
                        data.GetInt32(2),
                        data.GetInt32(3),
                        data.GetString(4),
                        data.GetString(5),
                        data.GetDateTime(6)
                    ));
                }
            }

            Connection.CloseConnection(connection);
            return allMessages;
        }

        public static void Add(Messages message)
        {
            string SQL = $"INSERT INTO `messages` (`IdDelivery`, `SenderId`, `ReceiverId`, `MessageText`, `ImagePath`, `DateTime`) " +
                         $"VALUES ('{message.IdDelivery}', '{message.SenderId}', '{message.ReceiverId}', '{message.MessageText}', '{message.ImagePath}', '{message.DateTime:yyyy-MM-dd HH:mm:ss}')";

            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }

    }
}
