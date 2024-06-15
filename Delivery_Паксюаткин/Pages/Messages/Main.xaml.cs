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
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            LoadMessages();
        }

        private void LoadMessages()
        {
            List<Model.Messages> allMessages = MessagesContext.Select();
            var groupedMessages = allMessages
                .GroupBy(m => new { m.SenderId, m.ReceiverId, m.IdDelivery })
                .Select(g => g.First()) // Берём первое сообщение из каждой группы
                .ToList();

            foreach (var message in groupedMessages)
            {
                var sender = UsersContext.Select().FirstOrDefault(u => u.Id == message.SenderId);
                var receiver = UsersContext.Select().FirstOrDefault(u => u.Id == message.ReceiverId);
                var delivery = DeliveryContext.Select().FirstOrDefault(d => d.Id == message.IdDelivery);

                if (sender != null && receiver != null && delivery != null)
                {
                    parent.Children.Add(new Items.Item(message.SenderId, message.ReceiverId, message.IdDelivery, message.MessageText, message.DateTime));
                }
            }
        }

        private void OpenUsers(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.Users.Main());

        private void OpenObjectDelivery(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.ObjectDelivery.Main());

        private void OpenDelivery(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.Delivery.Main());

        private void OpenMessages(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.Messages.Main());

        private void AddRecord(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.Messages.NewMessageDialog());

        private void Exit(object sender, RoutedEventArgs e) => MainWindow.init.OpenPage(new Pages.LoginRegister.Main());
    }
}
