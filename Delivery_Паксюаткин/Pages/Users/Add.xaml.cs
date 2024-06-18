using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Delivery_Паксюаткин.Classes;
using Microsoft.Win32;

namespace Delivery_Паксюаткин.Pages.Users
{
    public partial class Add : Page
    {
        List<RolesContext> AllRoles = RolesContext.Select();

        UsersContext userContext;
        private string imagePath;

        public Add(UsersContext userContext = null)
        {
            InitializeComponent();

            foreach (var item in AllRoles)
                role.Items.Add(item.Role);

            if (userContext != null)
            {
                this.userContext = userContext;
                fio.Text = userContext.FIO;
                imagePath = userContext.Image;
                phoneNumber.Text = userContext.PhoneNumber;
                address.Text = userContext.Address;
                role.SelectedIndex = AllRoles.FindIndex(x => x.Id == userContext.IdRole);
                login.Text = userContext.Login;
                pwd.Text = userContext.Password;
                bthAdd.Content = "Изменить";

                // Абсолютный путь для загрузки изображения
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (!string.IsNullOrEmpty(imagePath))
                {
                    string imagePathAbsolute = Path.Combine(projectDirectory, imagePath.Replace("/", "\\"));
                    LoadImage(imagePathAbsolute);
                }
            }
            else
            {
                bthAdd.Content = "Добавить";
            }
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
                string sourcePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(sourcePath);

                // Путь к проекту относительно текущей рабочей директории
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string destinationDirectory = Path.Combine(projectDirectory, "Image");

                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                    MessageBox.Show($"Создана директория: {destinationDirectory}");
                }

                string destinationPath = Path.Combine(destinationDirectory, fileName);

                // Копирование файла в папку Image
                File.Copy(sourcePath, destinationPath, true);

                // Сохраняю относительный путь для базы данных
                imagePath = Path.Combine("Image", fileName).Replace("\\", "/");

                imagePathText.Text = imagePath;
                LoadImage(destinationPath);
            }
        }

        private void LoadImage(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string absolutePath = Path.GetFullPath(path);
                if (File.Exists(absolutePath))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(absolutePath, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    imagePreview.Source = bitmap;
                }
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fio.Text))
            {
                MessageBox.Show("Необходимо указать ФИО");
                return;
            }
            if (string.IsNullOrWhiteSpace(phoneNumber.Text))
            {
                MessageBox.Show("Необходимо указать номер телефона");
                return;
            }
            if (string.IsNullOrWhiteSpace(role.Text))
            {
                MessageBox.Show("Необходимо указать роль");
                return;
            }

            if (this.userContext == null)
            {
                UsersContext newUser = new UsersContext(
                    0,
                    fio.Text,
                    string.IsNullOrEmpty(imagePath) ? null : imagePath,
                    phoneNumber.Text,
                    address.Text,
                    AllRoles.Find(x => x.Role == role.SelectedItem).Id,
                    login.Text,
                    pwd.Text
                );
                newUser.Add();
                MessageBox.Show("Запись успешно добавлена.");
            }
            else
            {
                userContext = new UsersContext(
                    userContext.Id,
                    fio.Text,
                    string.IsNullOrEmpty(imagePath) ? null : imagePath,
                    phoneNumber.Text,
                    address.Text,
                    AllRoles.Find(x => x.Role == role.SelectedItem).Id,
                    login.Text,
                    pwd.Text
                );
                userContext.Update();
                MessageBox.Show("Запись успешно обновлена.");
            }
            MainWindow.init.OpenPage(new Pages.Users.Main());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Users.Main());
        }
    }
}
