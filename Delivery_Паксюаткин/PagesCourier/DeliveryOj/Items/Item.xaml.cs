using Delivery_Паксюаткин.Classes;
using Delivery_Паксюаткин.Model;
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

namespace Delivery_Паксюаткин.PagesCourier.DeliveryOj.Items
{
    public partial class Item : UserControl
    {
        DeliveryContext delivery;
        UsersContext user;
        Main main;
        List<UsersContext> AllUsers;
        List<ObjectDeliveryContext> AllObjects = ObjectDeliveryContext.Select();
        private UsersContext loggedInUser; // Поле для хранения информации о текущем пользователе
        private ObjectDeliveryContext objectDelivery;

        public Item(DeliveryContext delivery, UsersContext user, Main main, ObjectDeliveryContext objectDelivery)
        {
            InitializeComponent();
            loggedInUser = App.CurrentUser; // Получаем текущего пользователя из класса App
            AllUsers = UsersContext.Select();
            var courier = AllUsers.Find(x => x.Id == delivery.DeliveryId && x.IdRole == 2);
            var customer = AllUsers.Find(x => x.Id == delivery.UserId && x.IdRole == 1);
            if (courier != null)
                deliveryId.Text = loggedInUser.FIO;
            else
                deliveryId.Text = loggedInUser.FIO;

            if (customer != null)
                userId.Text = customer.FIO;
            else
                userId.Text = customer.FIO;
            fromAddress.Text = delivery.FromAddress;
            status.Text = delivery.Status;
            commit.Text = delivery.Commit;
            price.Text = delivery.Price.ToString();
            date.Text = delivery.Date.ToString("dd.MM.yyyy HH:mm");
            objectId.Text = delivery.IdObject.ToString();
            objectId.Text = AllObjects.Find(x => x.Id == delivery.IdObject).Commit;          
            if (objectDelivery != null && objectDelivery.Status == "Доставлено")
            {
                btnDo.IsEnabled = true;
            }
            else
            {
                btnDo.IsEnabled = false;
            }
            this.delivery = delivery;
            this.user = user;
            this.main = main;
            this.objectDelivery = objectDelivery;
        }

        private void DoRecord(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно доставили заказ?", "Подтверждение заказа", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                delivery.Status = "Доставлено";
                delivery.Update();
                status.Text = "Доставлено";
                MessageBox.Show("Заказ успешно доставлен.");
            }
        }


        private void ChatRecord(object sender, RoutedEventArgs e)
        {
            try
            {
                string senderFIO = userId.Text;
                string deliveryFIO = deliveryId.Text;
                string deliveryCommit = commit.Text;

                // Получение информации о пользователях и доставке
                UsersContext senderUser = UsersContext.Select().FirstOrDefault(u => u.FIO == senderFIO);
                UsersContext deliveryUser = UsersContext.Select().FirstOrDefault(u => u.FIO == deliveryFIO);

                if (senderUser == null)
                {
                    throw new Exception("Пользователь с указанным FIO не найден.");
                }

                if (deliveryUser == null)
                {
                    throw new Exception("Курьер с указанным FIO не найден.");
                }

                int senderId = senderUser.Id;
                int receiverId = deliveryUser.Id;

                DeliveryContext delivery = DeliveryContext.Select().FirstOrDefault(d => d.Commit == deliveryCommit);

                if (delivery == null)
                {
                    throw new Exception("Заказ с указанным commit не найден.");
                }

                int deliveryID = delivery.Id;

                // Открытие страницы чата с передачей необходимых данных
                MainWindow.init.OpenPage(new PagesCourier.DeliveryOj.Add(senderId, receiverId, deliveryID, deliveryCommit));
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Неверный формат входных данных: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void ObRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new PagesCourier.ObjectDelivery.Add(this.objectDelivery));
        }
    }
}
