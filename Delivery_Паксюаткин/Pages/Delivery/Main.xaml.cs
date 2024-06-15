using Delivery_Паксюаткин.Classes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Delivery_Паксюаткин.Pages.Delivery
{
    public partial class Main : Page
    {
        List<DeliveryContext> AllDelivery = DeliveryContext.Select();

        public Main()
        {
            InitializeComponent();
            foreach (DeliveryContext item in AllDelivery)
            {

                parent.Children.Add(new Items.Item(item, null, this));
            }
        }

        private void OpenUsers(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.Users.Main());

        private void OpenObjectDelivery(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.ObjectDelivery.Main());

        private void OpenDelivery(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.Delivery.Main());

        private void OpenMessages(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.Messages.Main());

        private void AddRecord(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.Delivery.Add());

        private void Exit(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.LoginRegister.Main());
    }
}
