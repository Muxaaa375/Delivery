using Delivery_Паксюаткин.Classes.Common;
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
        public int Id { get; set; }
        public int IdDelivery { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageText { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateTime { get; set; }

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
