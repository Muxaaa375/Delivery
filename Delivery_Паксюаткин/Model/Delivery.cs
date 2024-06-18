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
        /// Код
        /// </summary>
        public int Id { get; set; } // Код
        public int UserId { get; set; } // Код заказчика
        public int? DeliveryId { get; set; } // Код курьера
        public int IdObject { get; set; } // Код объекта
        public string FromAddress { get; set; } // Адрес отправки
        public string Status { get; set; } // Статус
        public string Commit { get; set; } // Комментарий
        public int Price { get; set; } // Стоимость
        public DateTime Date { get; set; } // Дата и время

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
