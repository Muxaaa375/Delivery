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

namespace Delivery_Паксюаткин.Pages.Messages
{
    public partial class NewMessageDialog : Page
    {
        List<UsersContext> AllUsers = UsersContext.Select();
        List<DeliveryContext> AllDeliveries = DeliveryContext.Select();

        public NewMessageDialog()
        {
            InitializeComponent();
            foreach (var item in AllUsers)
            {
                if (item.IdRole == 2)
                    receiverComboBox.Items.Add(item.FIO);
                else if (item.IdRole == 1)
                    senderComboBox.Items.Add(item.FIO);
            }

            foreach (var delivery in AllDeliveries)
            {
                deliveryComboBox.Items.Add(delivery.Commit);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Messages.Main());
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (senderComboBox.SelectedIndex == -1 || receiverComboBox.SelectedIndex == -1 || deliveryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать пользователя, курьера и заказ");
                return;
            }

            int senderId = AllUsers[senderComboBox.SelectedIndex].Id;
            int receiverId = AllUsers[receiverComboBox.SelectedIndex].Id;
            int deliveryId = AllDeliveries[deliveryComboBox.SelectedIndex].Id;

            MainWindow.init.OpenPage(new Pages.Messages.Add(senderId, receiverId, deliveryId));
        }
    }
}
