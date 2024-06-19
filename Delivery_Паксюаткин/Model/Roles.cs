using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Model
{
    public class Roles
    {
        /// <summary>
        /// Код класса Ролей(Roles)
        /// </summary>
        public int Id { get; set; } 
        public string Role { get; set; } 
        public Roles(int Id, string Role)
        {
            this.Id = Id;
            this.Role = Role;
        }
    }
}
