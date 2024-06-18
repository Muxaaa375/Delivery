using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Model
{
    public class Roles
    {
        public int Id { get; set; } // Код
        public string Role { get; set; } // Роль
        public Roles(int Id, string Role)
        {
            this.Id = Id;
            this.Role = Role;
        }
    }
}
