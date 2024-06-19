using Delivery_Паксюаткин.Classes;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Delivery_Паксюаткин.PagesCourier.ObjectDelivery
{
    /// <summary>
    /// Логика взаимодействия для Ob.xaml
    /// </summary>
    public partial class Ob : Page
    {
        ObjectDeliveryContext objectDeliveryContext;
        private byte[] imageBytes;

        public Ob(ObjectDeliveryContext objectDeliveryContext = null)
        {
            InitializeComponent();
            if (objectDeliveryContext != null)
            {
                this.objectDeliveryContext = objectDeliveryContext;
                IdDelivery.Text = objectDeliveryContext.IdDelivery.ToString();
                weight.Text = objectDeliveryContext.Weight.ToString();

                address.Text = objectDeliveryContext.Address;
                getNumber.Text = objectDeliveryContext.GetNumber;
                commit.Text = objectDeliveryContext.Commit;
                status.Text = objectDeliveryContext.Status;

                

                if (objectDeliveryContext.Image != null)
                {
                    imageBytes = objectDeliveryContext.Image;
                    LoadImageFromBytes(objectDeliveryContext.Image);
                }
            }
            else
            {    }
            
        }

        private void UploadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileExtension = Path.GetExtension(openFileDialog.FileName).ToLower();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                    LoadImageFromBytes(imageBytes);
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите файл формата JPG, JPEG или PNG.", "Неподдерживаемый формат файла", MessageBoxButton.OK);
                }
            }
        }

        private void LoadImageFromBytes(byte[] imageData)
        {
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(imageData))
            {
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }
            imagePreview.Source = bitmap;
        }
       

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new PagesCourier.Delivery.Main());
        }
    }
}
