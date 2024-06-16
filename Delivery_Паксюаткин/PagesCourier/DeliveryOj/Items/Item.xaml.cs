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

namespace Delivery_Паксюаткин.PagesCourier.DeliveryOj.Items
{
    public partial class Item : UserControl
    {
        DeliveryContext delivery;
        UsersContext user;
        Main main;
        List<UsersContext> AllUsers;
        List<ObjectDeliveryContext> AllObjects = ObjectDeliveryContext.Select();

        public Item(DeliveryContext delivery, UsersContext user, Main main)
        {
            InitializeComponent();


            AllUsers = UsersContext.Select();


            var courier = AllUsers.Find(x => x.Id == delivery.DeliveryId && x.IdRole == 1);
            var customer = AllUsers.Find(x => x.Id == delivery.UserId && x.IdRole == 2);


            if (courier != null)
                delivery_Id.Text = courier.FIO;
            if (customer != null)
                userId.Text = customer.FIO;

            fromAddress.Text = delivery.FromAddress;
            status.Text = delivery.Status;
            commit.Text = delivery.Commit;
            price.Text = delivery.Price.ToString();
            date.Text = delivery.Date.ToString("dd.MM.yyyy HH:mm");
            objectId.Text = delivery.IdObject.ToString();
            objectId.Text = AllObjects.Find(x => x.Id == delivery.IdObject).Commit;

            this.delivery = delivery;
            this.user = user;
            this.main = main;
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
                string deliveryFIO = delivery_Id.Text;
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

                int deliveryId = delivery.Id;

                // Открытие страницы чата с передачей необходимых данных
                MainWindow.init.OpenPage(new PagesCourier.DeliveryOj.Add(senderId, receiverId, deliveryId, deliveryCommit));
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
    }
}
