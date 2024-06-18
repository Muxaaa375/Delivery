using Delivery_Паксюаткин.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Delivery_Паксюаткин.PagesUser.Delivery
{
    public partial class Add : Page
    {
        List<DeliveryContext> AllDelivery = DeliveryContext.Select();
        List<UsersContext> AllUsers = UsersContext.Select();
        List<ObjectDeliveryContext> AllObjects = ObjectDeliveryContext.Select();
        private DeliveryContext delivery;
        private UsersContext loggedInUser; // Поле для хранения информации о текущем пользователе

        public Add(DeliveryContext delivery = null)
        {
            InitializeComponent();
            loggedInUser = App.CurrentUser;

            foreach (var obj in AllObjects)
            {
                objectId.Items.Add(obj.Commit);
            }

            status.Text = "Ожидает доставки";
            date.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            if (delivery != null)
            {
                this.delivery = delivery;
                fromAddress.Text = delivery.FromAddress;
                status.Text = delivery.Status;
                committ.Text = delivery.Commit;
                price.Text = delivery.Price.ToString();
                userId.Text = AllUsers.FirstOrDefault(x => x.Id == delivery.UserId && x.IdRole == 2)?.FIO;
                deliveryIdd.Text = AllUsers.FirstOrDefault(x => x.Id == delivery.DeliveryId && x.IdRole == 1)?.FIO;
                objectId.SelectedIndex = AllObjects.FindIndex(x => x.Id == delivery.IdObject);
                bthAdd.Content = "Изменить";

                if (delivery.Status == "Доставлено")
                {
                    committ.IsEnabled = true;

                    objectId.IsEnabled = false;
                    fromAddress.IsEnabled = false;
                    price.IsEnabled = false;
                    bthAddOb.IsEnabled = false;
                }
                if (delivery.Status == "Ожидает доставки")
                {
                    committ.IsEnabled = false;
                }
            }
            else
            {
                
                bthAdd.Content = "Добавить";
            }

           

            if (loggedInUser != null)
            {
                userId.Text = loggedInUser.FIO;
                userId.IsReadOnly = true;
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fromAddress.Text) || string.IsNullOrWhiteSpace(price.Text) || objectId.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            int priceValue = int.Parse(price.Text);
            int userIdValue = loggedInUser.Id;
            int? deliveryIdValue = null; // Установите значение в null, если курьер не назначен

            if (!string.IsNullOrWhiteSpace(deliveryIdd.Text))
            {
                deliveryIdValue = AllUsers.First(x => x.FIO == deliveryIdd.Text && x.IdRole == 1).Id;
            }

            int objectIdValue = AllObjects.First(x => x.Commit == (string)objectId.SelectedItem).Id;
            DateTime creationDate = DateTime.ParseExact(date.Text, "dd.MM.yyyy HH:mm", null);

            if (delivery == null)
            {
                DeliveryContext newDelivery = new DeliveryContext(
                    0,
                    userIdValue,
                    deliveryIdValue,
                    objectIdValue,
                    fromAddress.Text,
                    "Ожидает доставки",
                    committ.Text.ToString(),
                    priceValue,
                    creationDate);
                newDelivery.Add();
                MessageBox.Show("Заказ успешно добавлен.");
            }
            else
            {
                delivery.UserId = userIdValue;
                delivery.DeliveryId = deliveryIdValue;
                delivery.IdObject = objectIdValue;
                delivery.FromAddress = fromAddress.Text;
                delivery.Status = status.Text;
                delivery.Commit = committ.Text.ToString();
                delivery.Price = priceValue;
                delivery.Date = creationDate;
                delivery.Update();
                MessageBox.Show("Заказ успешно обновлен.");
            }
            MainWindow.init.OpenPage(new PagesUser.Delivery.Main());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new PagesUser.Delivery.Main());
        }

        private void AddObRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new PagesUser.Delivery.ObjectDelivery());
        }
    }
}
