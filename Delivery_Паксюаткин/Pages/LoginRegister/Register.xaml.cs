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

namespace Delivery_Паксюаткин.Pages.LoginRegister
{
    public partial class Register : Page
    {
        List<RolesContext> AllRoles = RolesContext.Select();

        UsersContext userContext;
        public Register()
        {
            InitializeComponent();
            foreach (var item in AllRoles)
                role.Items.Add(item.Role);

            

            if (userContext != null)
            {
                fio.Text = userContext.FIO;
                phoneNumber.Text = userContext.PhoneNumber;
                login.Text = userContext.Login;
                pwd.Text = userContext.Password;
                role.SelectedIndex = AllRoles.FindIndex(x => x.Id == userContext.IdRole);
            }
        }

        private void OpenRegister(object sender, RoutedEventArgs e)
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
            if (string.IsNullOrWhiteSpace(login.Text))
            {
                MessageBox.Show("Необходимо указать логин");
                return;
            }
            if (string.IsNullOrWhiteSpace(pwd.Text))
            {
                MessageBox.Show("Необходимо указать пароль");
                return;
            }
            if (role.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо указать роль");
                return;
            }

            if (userContext == null)
            {
                userContext = new UsersContext(
                    0,
                    fio.Text,
                    null,
                    phoneNumber.Text,
                    null,
                    AllRoles.Find(x => x.Role == role.SelectedItem.ToString()).Id,
                    login.Text,
                    pwd.Text
                );
                userContext.Add();
                MessageBox.Show("Регистрация успешно завершена.");
            }
            else
            {
                userContext = new UsersContext(
                    userContext.Id,
                    fio.Text,
                    null,
                    phoneNumber.Text,
                    null,
                    AllRoles.Find(x => x.Role == role.SelectedItem.ToString()).Id,
                    login.Text,
                    pwd.Text
                );
                userContext.Update();
                MessageBox.Show("Запись успешно обновлена.");
            }
            MainWindow.init.OpenPage(new Pages.LoginRegister.Main());
        }
    }
}
