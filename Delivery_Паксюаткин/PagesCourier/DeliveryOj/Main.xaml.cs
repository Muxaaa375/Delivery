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

namespace Delivery_Паксюаткин.PagesCourier.DeliveryOj
{
    public partial class Main : Page
    {
        List<DeliveryContext> AllDelivery = DeliveryContext.Select();
        public Main()
        {
            InitializeComponent();
            foreach (DeliveryContext item in AllDelivery)
            {
                if (item.Status == "У курьера")
                {
                    parent.Children.Add(new Items.Item(item, null, this));
                }
            }
        }

        private void OpenDeliveryOj(object sender, RoutedEventArgs e) { }

        private void OpenDelivery(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new PagesCourier.Delivery.Main());

        private void OpenObjectDelivery(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new PagesCourier.ObjectDelivery.Main());

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.LoginRegister.Main());
        }
    }
}
