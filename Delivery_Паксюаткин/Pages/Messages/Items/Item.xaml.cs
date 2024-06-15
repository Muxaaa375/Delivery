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

namespace Delivery_Паксюаткин.Pages.Messages.Items
{
    public partial class Item : UserControl
    {
        private int senderId;
        private int receiverId;
        private int deliveryId;
        MessagesContext messages;
        Main main;

        public Item(int senderId, int receiverId, int deliveryId, string messageText, DateTime dateTime)
        {
            InitializeComponent();
            this.senderId = senderId;
            this.receiverId = receiverId;
            this.deliveryId = deliveryId;
            this.DataContext = new
            {
                MessageText = messageText,
                DateTime = dateTime
            };
        }

        private void GoToMessage_Click(object sender, RoutedEventArgs e)
        {
            var addPage = new Add(senderId, receiverId, deliveryId);
            MainWindow.init.OpenPage(addPage);
        }

        private void DeleteToMessage_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить этот чат?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    // Получаем список всех сообщений
                    var allMessages = MessagesContext.Select();

                    // Находим все сообщения, относящиеся к данному чату
                    var chatMessages = allMessages.FindAll(msg =>
                        msg.IdDelivery == deliveryId &&
                        ((msg.SenderId == senderId && msg.ReceiverId == receiverId) ||
                         (msg.SenderId == receiverId && msg.ReceiverId == senderId))
                    );

                    // Удаляем каждое сообщение
                    foreach (var message in chatMessages)
                    {
                        message.Delete();
                    }
                    MessageBox.Show("Чат успешно удалён.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении чата: {ex.Message}");
                }
            }
        }
    }
}
