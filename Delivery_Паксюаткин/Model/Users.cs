﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Model
{
    public class Users
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public byte[] Image { get; set; } // Фотография
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int IdRole { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Users(int Id, string FIO, byte[] Image, string PhoneNumber, string Address, int IdRole, string Login, string Password)
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
