using Delivery_Паксюаткин.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            // Проверка на допустимые значения роли
            int selectedRoleId = AllRoles.Find(x => x.Role == role.SelectedItem.ToString()).Id;
            if (selectedRoleId != 1 && selectedRoleId != 2)
            {
                MessageBox.Show("Выбранная роль для вас недоступна. Пожалуйста, выберите роль Пользователь или Курьер.");
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
            MainWindow.init.OpenPage(new Pages.LoginRegister.Main());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.LoginRegister.Main());
        }
    }
}
