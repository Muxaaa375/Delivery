using Delivery_Паксюаткин.Classes;
using Delivery_Паксюаткин.Model;
using Delivery_Паксюаткин.PagesUser.Delivery;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Delivery_Паксюаткин.PagesCourier.Delivery.Items
{
    public partial class Item : UserControl
    {
        DeliveryContext delivery;
        UsersContext user;
        Main main;
        List<UsersContext> AllUsers;
        List<ObjectDeliveryContext> AllObjDel = ObjectDeliveryContext.Select();
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
                deliveryId.Text = courier.FIO;
            if (customer != null)
                userId.Text = customer.FIO;

            fromAddress.Text = delivery.FromAddress;
            status.Text = delivery.Status;
            commit.Text = delivery.Commit;
            price.Text = delivery.Price.ToString();
            date.Text = delivery.Date.ToString("dd.MM.yyyy HH:mm");
            objectId.Text = delivery.IdObject.ToString();
            objectId.Text = AllObjDel.Find(x => x.Id == delivery.IdObject).Commit;

            this.delivery = delivery;
            this.user = user;
            this.main = main;
            this.objectDelivery = objectDelivery;
        }

        private void DoRecord(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите принять заказ?", "Подтверждение заказа", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                delivery.Status = "У курьера";
                delivery.DeliveryId = loggedInUser.Id;
                delivery.Update();
                status.Text = "У курьера";
                deliveryId.Text = loggedInUser.FIO;


                MessageBox.Show("Заказ успешно принят.");
            }           
        }

        private void DoObRecord(object sender, RoutedEventArgs e)
        {

        }
    }
}
