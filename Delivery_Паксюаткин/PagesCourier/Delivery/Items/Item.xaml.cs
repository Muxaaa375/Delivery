using Delivery_Паксюаткин.Classes;
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

        public Item(DeliveryContext delivery, UsersContext user, Main main)
        {
            InitializeComponent();


            AllUsers = UsersContext.Select();


            var courier = AllUsers.Find(x => x.Id == delivery.DeliveryId && x.IdRole == 1);
            var customer = AllUsers.Find(x => x.Id == delivery.UserId && x.IdRole == 2);


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
        }

        private void DoRecord(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите принять заказ?", "Подтверждение заказа", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                delivery.Status = "У курьера";
                delivery.Update();
                status.Text = "У курьера";
                MessageBox.Show("Заказ успешно принят.");
            }
        }
    }
}
