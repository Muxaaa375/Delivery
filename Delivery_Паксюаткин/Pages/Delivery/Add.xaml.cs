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

namespace Delivery_Паксюаткин.Pages.Delivery
{
    public partial class Add : Page
    {
        List<DeliveryContext> AllDelivery = DeliveryContext.Select();
        List<UsersContext> AllUsers = UsersContext.Select();
        List<ObjectDeliveryContext> AllObjects = ObjectDeliveryContext.Select();
        DeliveryContext delivery;

        public Add(DeliveryContext delivery = null)
        {
            InitializeComponent();

            foreach (var item in AllUsers)
            {
                if (item.IdRole == 1)
                    deliveryId.Items.Add(item.FIO);
                else if (item.IdRole == 2)
                    userId.Items.Add(item.FIO);
            }

            foreach (var obj in AllObjects)
            {
                objectId.Items.Add(obj.Commit);
            }

            status.Items.Add("Ожидает доставки");
            status.Items.Add("У курьера");
            status.Items.Add("Доставлено");

            date.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            if (delivery != null)
            {
                this.delivery = delivery;
                fromAddress.Text = delivery.FromAddress;
                status.Text = delivery.Status;
                commit.Text = delivery.Commit;
                price.Text = delivery.Price.ToString();
                userId.SelectedIndex = AllUsers.FindIndex(x => x.Id == delivery.UserId && x.IdRole == 2);
                deliveryId.SelectedIndex = AllUsers.FindIndex(x => x.Id == delivery.DeliveryId && x.IdRole == 1);
                objectId.SelectedIndex = AllObjects.FindIndex(x => x.Id == delivery.IdObject);
                bthAdd.Content = "Изменить";
            }
            else
            {
                status.SelectedIndex = 0;
                bthAdd.Content = "Добавить";
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            int priceValue = int.Parse(price.Text);
            if (status.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите статус");
                return;
            }
            if (userId.SelectedIndex == -1 || deliveryId.SelectedIndex == -1 || objectId.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать заказчика, курьера и объект доставки");
                return;
            }

            int userIdValue = AllUsers.First(x => x.FIO == (string)userId.SelectedItem && x.IdRole == 2).Id;
            int deliveryIdValue = AllUsers.First(x => x.FIO == (string)deliveryId.SelectedItem && x.IdRole == 1).Id;
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
                    status.Text,
                    commit.Text,
                    priceValue,
                    creationDate);
                newDelivery.Add();
                MessageBox.Show("Запись успешно добавлена.");
            }
            else
            {
                delivery = new DeliveryContext(
                    delivery.Id,
                    userIdValue,
                    deliveryIdValue,
                    objectIdValue,
                    fromAddress.Text,
                    status.Text,
                    commit.Text,
                    priceValue,
                    creationDate);
                delivery.Update();
                MessageBox.Show("Запись успешно обновлена.");
            }
            MainWindow.init.OpenPage(new Pages.Delivery.Main());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Delivery.Main());
        }
    }
}
