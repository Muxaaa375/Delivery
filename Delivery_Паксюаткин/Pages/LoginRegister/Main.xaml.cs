using Delivery_Паксюаткин.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Delivery_Паксюаткин.Pages.LoginRegister
{
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }

        private void OpenLogin(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(login.Text))
            {
                MessageBox.Show("Необходимо прописать Логин");
                return;
            }
            if (string.IsNullOrWhiteSpace(pwd.Text))
            {
                MessageBox.Show("Необходимо прописать Пароль");
                return;
            }

            var userRole = AuthenticateUser(login.Text, pwd.Text);
            if (!string.IsNullOrEmpty(userRole))
            {
                NavigateToRolePage(userRole);
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private string AuthenticateUser(string login, string password)
        {
            List<LoginnContext> logins = LoginnContext.Select();
            foreach (var user in logins)
            {
                if (user.Login == login && user.Password == password)
                {
                    return user.Role;
                }
            }
            return null;
        }

        private void NavigateToRolePage(string role)
        {
            switch (role)
            {
                case "Курьер":
                    MainWindow.init.OpenPage(new PagesCourier.Delivery.Main());
                    break;
                case "Пользователь":
                    MainWindow.init.OpenPage(new PagesUser.Delivery.Main());
                    break;
                case "Админ":
                    MainWindow.init.OpenPage(new Pages.Users.Main());
                    break;
                default:
                    MessageBox.Show("Неизвестная роль пользователя");
                    break;
            }
        }
    }
}
