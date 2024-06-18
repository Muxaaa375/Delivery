using Delivery_Паксюаткин.Classes;
using Delivery_Паксюаткин.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Delivery_Паксюаткин.Classes
{
    public class MessagesContext : Messages
    {
        public MessagesContext(int Id, int IdDelivery, int SenderId, int GetId, string Message, byte[] ImagePath, DateTime DateTime)
            : base(Id, IdDelivery, SenderId, GetId, Message, ImagePath, DateTime) { }

        public static List<Messages> Select()
        {
            List<Messages> allMessages = new List<Messages>();
            string SQL = "SELECT Id, IdDelivery, SenderId, ReceiverId, MessageText, ImagePath, DateTime FROM messages"; // Исправлено на ImagePath
            MySqlConnection connection = Connection.OpenConnection();

            using (MySqlDataReader data = Connection.Query(SQL, connection))
            {
                while (data.Read())
                {
                    byte[] imagePath = null;
                    if (!(data["ImagePath"] is DBNull))
                    {
                        long bytes = data.GetBytes(data.GetOrdinal("ImagePath"), 0, null, 0, 0); // Использование GetOrdinal для получения индекса столбца
                        imagePath = new byte[bytes];
                        data.GetBytes(data.GetOrdinal("ImagePath"), 0, imagePath, 0, (int)bytes);
                    }

                    allMessages.Add(new Messages(
                        data.GetInt32(0),
                        data.GetInt32(1),
                        data.GetInt32(2),
                        data.GetInt32(3),
                        data.GetString(4),
                        imagePath,
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
                         $"VALUES ('{message.IdDelivery}', '{message.SenderId}', '{message.ReceiverId}', '{message.MessageText}', @ImagePath, '{message.DateTime:yyyy-MM-dd HH:mm:ss}')";

            MySqlConnection connection = Connection.OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
            {
                cmd.Parameters.AddWithValue("@ImagePath", message.ImagePath);
                cmd.ExecuteNonQuery();
            }
            Connection.CloseConnection(connection);
        }
    }
}


