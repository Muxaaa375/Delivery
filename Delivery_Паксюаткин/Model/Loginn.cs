using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Model
{
    public class Loginn
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public Loginn(int Id, string Login, string Password, string Role)
        {
            this.Id = Id;
            this.Login = Login;
            this.Password = Password;
            this.Role = Role;
        }
    }
}
