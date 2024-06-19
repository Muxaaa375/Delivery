using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Model
{
    public class Delivery
    {
        /// <summary>
        /// Код класса Доставка(Delivery)
        /// </summary>
        public int Id { get; set; } 
        public int UserId { get; set; } 
        public int? DeliveryId { get; set; } 
        public int IdObject { get; set; } 
        public string FromAddress { get; set; } 
        public string Status { get; set; } 
        public string Commit { get; set; } 
        public int Price { get; set; } 
        public DateTime Date { get; set; } 

        public Delivery(int Id, int UserId, int? DeliveryId, int IdObject, string FromAddress, string Status, string Commit, int Price, DateTime Date)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.DeliveryId = DeliveryId;
            this.IdObject = IdObject;
            this.FromAddress = FromAddress;
            this.Status = Status;
            this.Commit = Commit;
            this.Price = Price;
            this.Date = Date;
        }
    }
}
