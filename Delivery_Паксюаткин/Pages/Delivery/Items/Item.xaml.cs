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

namespace Delivery_Паксюаткин.Pages.Delivery.Items
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
        }

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Delivery.Add(this.delivery));
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                delivery.Delete();
                main.parent.Children.Remove(this);
            }
        }
    }
}
