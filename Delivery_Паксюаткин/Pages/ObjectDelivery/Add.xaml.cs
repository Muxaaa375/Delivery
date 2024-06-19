﻿using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Delivery_Паксюаткин.Classes;
using Microsoft.Win32;

namespace Delivery_Паксюаткин.Pages.ObjectDelivery
{
    public partial class Add : Page
    {
        ObjectDeliveryContext objectDeliveryContext;
        private byte[] imageBytes;

        public Add(ObjectDeliveryContext objectDeliveryContext = null)
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

                bthAdd.Content = "Изменить";

                if (objectDeliveryContext.Image != null)
                {
                    imageBytes = objectDeliveryContext.Image;
                    LoadImageFromBytes(objectDeliveryContext.Image);
                }
            }
            else
            {
                bthAdd.Content = "Добавить";
            }
            status.Items.Add("Ожидает доставки");
            status.Items.Add("У курьера");
            status.Items.Add("Доставлено");
        }

        private void PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string text = textBox.Text;

            if (!text.StartsWith("+7"))
            {
                textBox.Text = "+7";
                textBox.CaretIndex = textBox.Text.Length;
            }

            if (text.Length > 12)
            {
                textBox.Text = text.Substring(0, 12);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); // Разрешаем только цифры
            return !regex.IsMatch(text);
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

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IdDelivery.Text))
            {
                MessageBox.Show("Необходимо указать код доставки");
                return;
            }
            if (string.IsNullOrWhiteSpace(weight.Text))
            {
                MessageBox.Show("Необходимо указать вес");
                return;
            }
            if (string.IsNullOrWhiteSpace(address.Text))
            {
                MessageBox.Show("Необходимо указать адрес доставки");
                return;
            }
            if (string.IsNullOrWhiteSpace(getNumber.Text) || getNumber.Text.Length != 12)
            {
                MessageBox.Show("Необходимо указать номер получателя (12значный)");
                return;
            }
            if (string.IsNullOrWhiteSpace(status.Text))
            {
                MessageBox.Show("Необходимо указать статус");
                return;
            }

            int parsedWeight;
            if (!int.TryParse(weight.Text, out parsedWeight))
            {
                MessageBox.Show("Вес должен быть числом");
                return;
            }

            int parsedIdDelivery;
            if (!int.TryParse(IdDelivery.Text, out parsedIdDelivery))
            {
                MessageBox.Show("Код доставки должен быть числом");
                return;
            }

            if (this.objectDeliveryContext == null)
            {
                ObjectDeliveryContext newObjectDelivery = new ObjectDeliveryContext(
                    0,
                    parsedIdDelivery,
                    imageBytes,
                    parsedWeight,
                    commit.Text,
                    getNumber.Text,
                    address.Text,
                    status.Text
                );
                newObjectDelivery.Add();
                MessageBox.Show("Запись успешно добавлена.");
            }
            else
            {
                objectDeliveryContext.IdDelivery = parsedIdDelivery;
                objectDeliveryContext.Weight = parsedWeight;
                objectDeliveryContext.Image = imageBytes;
                objectDeliveryContext.Address = address.Text;
                objectDeliveryContext.GetNumber = getNumber.Text;
                objectDeliveryContext.Commit = commit.Text;
                objectDeliveryContext.Status = status.Text;
                objectDeliveryContext.Update();
                MessageBox.Show("Запись успешно обновлена.");
            }
            MainWindow.init.OpenPage(new Pages.ObjectDelivery.Main());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.ObjectDelivery.Main());
        }
    }
}
