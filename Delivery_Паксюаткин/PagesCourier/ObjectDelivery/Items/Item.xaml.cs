using Delivery_Паксюаткин.Classes;
using Delivery_Паксюаткин.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Delivery_Паксюаткин.PagesCourier.ObjectDelivery.Items
{
    public partial class Item : UserControl
    {
        private ObjectDeliveryContext objectDelivery;
        private Main main;

        public Item(ObjectDeliveryContext objectDelivery, Main main)
        {
            InitializeComponent();
            idDelivery.Text = objectDelivery.IdDelivery.ToString();
            weight.Text = objectDelivery.Weight.ToString();            
            commit.Text = objectDelivery.Commit;
            address.Text = objectDelivery.Address;
            getNumber.Text = objectDelivery.GetNumber;
            status.Text = objectDelivery.Status;

            this.objectDelivery = objectDelivery;
            this.main = main;

            LoadImage(objectDelivery.Image);
        }

        private void LoadImage(byte[] imageData)
        {
            if (imageData != null)
            {
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    imageControl.Source = bitmap;
                }
            }
        }

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new PagesCourier.ObjectDelivery.Add(objectDelivery));
        }
    }
}
