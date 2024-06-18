using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Model
{
    public class ObjectDelivery
    {
        public int Id { get; set; } // Код
        public int IdDelivery { get; set; } // Код доставки
        public string Image { get; set; } // Фотография
        public int Weight { get; set; } // Вес
        public string Commit { get; set; } // Комментарий
        public string GetNumber { get; set; } // Номер получателя
        public string Address { get; set; } // Адрес доставки
        public string Status { get; set; } // Статус
        public ObjectDelivery(int Id, int IdDelivery, string Image, int Weight, string Commit, string GetNumber, string Address, string Status)
        {
            this.Id = Id;
            this.IdDelivery = IdDelivery;
            this.Image = Image;
            this.Weight = Weight;
            this.Commit = Commit;
            this.GetNumber = GetNumber;
            this.Address = Address;
            this.Status = Status;
        }
    }
}
