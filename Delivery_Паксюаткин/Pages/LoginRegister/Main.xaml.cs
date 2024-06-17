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
        public static UsersContext LoggedInUser { get; private set; }

        public Main()
        {
            InitializeComponent();
        }

        private void OpenLogin(object sender, RoutedEventArgs e)
        {
            string inputLogin = login.Text;
            string inputPassword = pwd.Password;

            // Проверка логина и пароля (примерный код)
            UsersContext user = ValidateUser(inputLogin, inputPassword); // Ваш метод проверки пользователя


            if (string.IsNullOrWhiteSpace(inputLogin))
            {
                MessageBox.Show("Необходимо указать логин");
                return;
            }
            if (string.IsNullOrWhiteSpace(inputPassword))
            {
                MessageBox.Show("Необходимо указать пароль");
                return;
            }

            if (user != null)
            {
                App.CurrentUser = user;
                MessageBox.Show("Успешный вход");

                // Определение пути в зависимости от роли пользователя
                string pagePath = DeterminePagePath(user.IdRole);
                if (!string.IsNullOrEmpty(pagePath))
                {
                    // Перенаправление на указанную страницу
                    NavigationService.Navigate(new Uri(pagePath, UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Не удалось определить страницу для роли пользователя.");
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private UsersContext ValidateUser(string login, string password)
        {
            List<UsersContext> allUsers = UsersContext.Select();

            foreach (UsersContext user in allUsers)
            {
                if (user.Login == login && user.Password == password)
                {
                    return user;
                }
            }

            return null;
        }

        private string DeterminePagePath(int roleId)
        {
            switch (roleId)
            {
                case 1: // Пользователь
                    return "/PagesUser/Delivery/Main.xaml";
                case 2: // Курьер
                    return "/PagesCourier/Delivery/Main.xaml";
                case 3: // Админ
                    return "/Pages/Users/Main.xaml";
                default:
                    return null;
            }
        }

        private void OpenRegister(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/LoginRegister/Register.xaml", UriKind.Relative));
        }
    }
}
