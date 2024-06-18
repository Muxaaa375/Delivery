using Delivery_Паксюаткин.Classes;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

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
            phoneNumber.Text = users.PhoneNumber;
            address.Text = users.Address;
            role.Text = AllRoles.Find(x => x.Id == users.IdRole).Role;

            this.users = users;
            this.main = main;
            DataContext = users;

            LoadImage(users.Image);
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
