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

namespace Delivery_Паксюаткин.PagesUser.Delivery
{
    public partial class Main : Page
    {
        List<DeliveryContext> AllDelivery = DeliveryContext.Select();

        private UsersContext loggedInUser; // Поле для хранения информации о текущем пользователе
        public Main()
        {
            InitializeComponent();
            loggedInUser = App.CurrentUser; // Получаем текущего пользователя из класса App
            DisplayLoggedInUserLogin();
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            foreach (DeliveryContext item in AllDelivery)
            {
                parent.Children.Add(new Items.Item(item, null, this));
            }
        }

        private void DisplayLoggedInUserLogin()
        {
            if (loggedInUser != null)
            {
                login.Text = loggedInUser.Login; // Отображаем логин текущего пользователя
            }
            else
            {
                MessageBox.Show("Пользователь не вошел в систему.");
            }
        }       


        private void AddRecord(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new PagesUser.Delivery.Add());

        private void Exit(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.LoginRegister.Main());
    }
}
