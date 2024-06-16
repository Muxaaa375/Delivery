using Delivery_Паксюаткин.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Delivery_Паксюаткин.PagesCourier.DeliveryOj
{
    public partial class Add : Page
    {
        private int senderId;
        private int receiverId;
        private int deliveryId;
        private string commit;
        private List<Model.Messages> chatMessages = new List<Model.Messages>();

        public Add(int senderId, int receiverId, int deliveryId, string commit)
        {
            InitializeComponent();
            this.senderId = senderId;
            this.receiverId = receiverId;
            this.deliveryId = deliveryId;
            this.commit = commit;

            UsersContext sender = UsersContext.Select().FirstOrDefault(u => u.Id == senderId);
            UsersContext receiver = UsersContext.Select().FirstOrDefault(u => u.Id == receiverId);

            if (sender != null) myName.Text = sender.FIO;
            if (receiver != null) interlocutor.Text = receiver.FIO;
            delivery.Text = commit;

            LoadMessages();
        }

        private void LoadMessages()
        {
            chatMessages = MessagesContext.Select().Where(m => m.IdDelivery == deliveryId &&
                                                               ((m.SenderId == senderId && m.ReceiverId == receiverId) ||
                                                                (m.SenderId == receiverId && m.ReceiverId == senderId)))
                                                    .OrderBy(m => m.DateTime)
                                                    .ToList();

            chatBox_1.Clear();
            chatBox_2.Clear();

            foreach (var msg in chatMessages)
            {
                var sender = UsersContext.Select().FirstOrDefault(u => u.Id == msg.SenderId);
                if (sender != null)
                {
                    if (msg.SenderId == senderId)
                    {
                        chatBox_1.AppendText($"{msg.DateTime.ToString("dd.MM.yyyy HH:mm")}: {sender.FIO}\n");
                        chatBox_1.AppendText($"{msg.MessageText}\n\n");
                    }
                    else
                    {
                        chatBox_2.AppendText($"{msg.DateTime.ToString("dd.MM.yyyy HH:mm")}: {sender.FIO}\n");
                        chatBox_2.AppendText($"{msg.MessageText}\n\n");
                    }
                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new PagesCourier.DeliveryOj.Main());
        }

        private void sendMessageButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(messageText_1.Text))
            {
                Model.Messages newMessage = new Model.Messages(0, deliveryId, senderId, receiverId, messageText_1.Text, null, DateTime.Now);
                MessagesContext.Add(newMessage);
                messageText_1.Clear();
                LoadMessages();
            }
            else
            {
                MessageBox.Show("Сообщение не может быть пустым.");
            }
        }

        private void sendMessageButton_Click_2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(messageText_2.Text))
            {
                Model.Messages newMessage = new Model.Messages(0, deliveryId, receiverId, senderId, messageText_2.Text, null, DateTime.Now);
                MessagesContext.Add(newMessage);
                messageText_2.Clear();
                LoadMessages();
            }
            else
            {
                MessageBox.Show("Сообщение не может быть пустым.");
            }
        }
    }
}
