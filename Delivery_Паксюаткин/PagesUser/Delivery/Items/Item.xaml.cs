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

namespace Delivery_Паксюаткин.PagesUser.Delivery.Items
{
    public partial class Item : UserControl
    {
        DeliveryContext delivery;
        UsersContext user;
        Main main;
        List<UsersContext> AllUsers;
        List<ObjectDeliveryContext> AllObjDel = ObjectDeliveryContext.Select();
        private UsersContext loggedInUser; // Поле для хранения информации о текущем пользователе

        public Item(DeliveryContext delivery, UsersContext user, Main main)
        {
            InitializeComponent();
            loggedInUser = App.CurrentUser; // Получаем текущего пользователя из класса App

            AllUsers = UsersContext.Select();
            var courier = AllUsers.Find(x => x.Id == delivery.DeliveryId && x.IdRole == 2);
            var customer = AllUsers.Find(x => x.Id == delivery.UserId && x.IdRole == 1);

            if (courier != null)
                delivery_Id.Text = courier.FIO;

            if (customer != null)
                userId.Text = loggedInUser.FIO; ;
            

            fromAddress.Text = delivery.FromAddress;
            status.Text = delivery.Status;
            commit.Text = delivery.Commit;
            price.Text = delivery.Price.ToString();
            date.Text = delivery.Date.ToString("dd.MM.yyyy HH:mm");
            objectId.Text = delivery.IdObject.ToString();
            objectId.Text = AllObjDel.Find(x => x.Id == delivery.IdObject).Commit;

            if (delivery.Status == "Доставлено")
            {
                btnDeleteRecord.IsEnabled = false;
                btnChatRecord.IsEnabled = false;
                btnEditRedord.IsEnabled = false;
            }
            if (delivery.Status == "Ожидает доставки")
            {
                btnChatRecord.IsEnabled = false;
            }
            if (delivery.Status == "У курьера")
            {
                btnEditRedord.IsEnabled = false;
            }

            this.delivery = delivery;
            this.user = user;
            this.main = main;
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите отменить заказ?", "Подтверждение отмены заказа", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                delivery.Delete();
                main.parent.Children.Remove(this);
            }
        }

        private void ChatRecord(object sender, RoutedEventArgs e)
        {
            try
            {
                string senderFIO = userId.Text;
                string deliveryFIO = delivery_Id.Text; 
                string deliveryCommit = commit.Text; 

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

                MainWindow.init.OpenPage(new PagesUser.Delivery.Chat(senderId, receiverId, deliveryId, deliveryCommit));
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

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите изменить заказ?", "Подтверждение изменения заказа", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow.init.OpenPage(new PagesUser.Delivery.Add(this.delivery));
            }
        }
    }        
}
