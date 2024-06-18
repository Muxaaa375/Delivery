
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Model
{
    public class Messages
    {
        public int Id { get; set; } // Код
        public int IdDelivery { get; set; } // Код доставки
        public int SenderId { get; set; } // Код отправителя
        public int ReceiverId { get; set; } // Код получателя
        public string MessageText { get; set; } // Сообщение
        public string ImagePath { get; set; } // Фотография
        public DateTime DateTime { get; set; } // Дата и время

        public Messages(int id, int idDelivery, int senderId, int receiverId, string messageText, string imagePath, DateTime dateTime)
        {
            Id = id;
            IdDelivery = idDelivery;
            SenderId = senderId;
            ReceiverId = receiverId;
            MessageText = messageText;
            ImagePath = imagePath;
            DateTime = dateTime;
        }
        public void Delete()
        {
            string SQL = $"DELETE FROM `messages` WHERE `Id` = {this.Id}";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
