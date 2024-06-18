using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Model
{
    public class Users
    {
        public int Id { get; set; } // Код
        public string FIO { get; set; } // ФИО
        public string Image { get; set; } // Фотография
        public string PhoneNumber { get; set; } // Номер телефона
        public string Address { get; set; } // Адрес
        public int IdRole { get; set; } // Код роли
        public string Login { get; set; } // Логин
        public string Password { get; set; } // Пароль

        public Users(int Id, string FIO, string Image, string PhoneNumber, string Address, int IdRole, string Login, string Password)
        {
            this.Id = Id;
            this.FIO = FIO;
            this.Image = Image;
            this.PhoneNumber = PhoneNumber;
            this.Address = Address;
            this.IdRole = IdRole;
            this.Login = Login;
            this.Password = Password;
        }
    }
}
