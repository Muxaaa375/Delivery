using Delivery_Паксюаткин.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Delivery_Паксюаткин.Pages.ObjectDelivery
{
    public partial class Main : Page
    {
        List<ObjectDeliveryContext> AllObjectDelivery = ObjectDeliveryContext.Select();
        public Main()
        {
            InitializeComponent();
            foreach (ObjectDeliveryContext item in AllObjectDelivery)
            {
                parent.Children.Add(new Items.Item(item, this));
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.ObjectDelivery.Add());
        private void Exit(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new PagesUser.Delivery.Add());
    }
}
