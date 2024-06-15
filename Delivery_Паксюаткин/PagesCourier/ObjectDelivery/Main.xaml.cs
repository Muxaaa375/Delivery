﻿using Delivery_Паксюаткин.Classes;
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

namespace Delivery_Паксюаткин.PagesCourier.ObjectDelivery
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

        private void OpenDeliveryOj(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new PagesCourier.DeliveryOj.Main());

        private void OpenDelivery(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new PagesCourier.Delivery.Main());

        private void OpenObjectDelivery(object sender, RoutedEventArgs e) { }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.LoginRegister.Main());
        }
    }
}
