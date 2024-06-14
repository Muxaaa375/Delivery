using Delivery_Паксюаткин.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using Delivery_Паксюаткин.Classes;
using Delivery_Паксюаткин.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Delivery_Паксюаткин.Pages.Users.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        List<RolesContext> AllRoles = RolesContext.Select();
        UsersContext users;
        Main main;

        public Item(UsersContext users, Main main)
        {
            InitializeComponent();
            fio.Text = users.FIO;
            image.Text = users.Image;
            phoneNumber.Text = users.PhoneNumber;
            address.Text = users.Address;
            role.Text = AllRoles.Find(x => x.Id == users.IdRole).Role;

            this.users = users;
            this.main = main;
            DataContext = users;

            LoadImage(users.Image); // Передаем относительный путь к изображению
        }

        private void LoadImage(string relativeImagePath)
        {

            if (!string.IsNullOrEmpty(relativeImagePath))
            {
                // Формируем абсолютный путь
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string absolutePath = Path.Combine(basePath, relativeImagePath);

                // Проверяем, существует ли файл по сформированному абсолютному пути
                if (File.Exists(absolutePath))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(absolutePath, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; // Это важно для избежания блокировок файлов
                    bitmap.EndInit();
                    userImage.Source = bitmap;
                }
            }

        }

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Users.Add(this.users));
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                users.Delete();
                main.parent.Children.Remove(this);
                MessageBox.Show("Запись успешно удалена.");
            }
        }
    }
}
