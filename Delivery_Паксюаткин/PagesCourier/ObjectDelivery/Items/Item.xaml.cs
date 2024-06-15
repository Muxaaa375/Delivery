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
            image.Text = objectDelivery.Image;
            commit.Text = objectDelivery.Commit;
            address.Text = objectDelivery.Address;
            getNumber.Text = objectDelivery.GetNumber;
            status.Text = objectDelivery.Status;

            this.objectDelivery = objectDelivery;
            this.main = main;

            // Load the image into the Image control
            LoadImage(objectDelivery.Image);
        }

        private void LoadImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string absolutePath = System.IO.Path.Combine(projectDirectory, imagePath.Replace("/", "\\"));
                if (System.IO.File.Exists(absolutePath))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(absolutePath, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    var imageControl = (Image)this.FindName("imageControl");
                    if (imageControl != null)
                    {
                        imageControl.Source = bitmap;
                    }
                }
            }
        }

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new PagesCourier.ObjectDelivery.Add(objectDelivery));
        }
    }
}
