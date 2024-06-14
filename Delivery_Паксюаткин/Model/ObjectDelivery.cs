using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Model
{
    public class ObjectDelivery
    {
        public int Id { get; set; }
        public int IdDelivery { get; set; }
        public string Image { get; set; }
        public int Weight { get; set; }
        public string Commit { get; set; }
        public string GetNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
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
