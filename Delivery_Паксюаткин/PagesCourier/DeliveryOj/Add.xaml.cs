using Delivery_Паксюаткин.Classes;
using Delivery_Паксюаткин.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Linq;

namespace Delivery_Паксюаткин.PagesCourier.DeliveryOj
{
    public partial class Add : Page
    {
        private int courierId;
        private int receiverId;
        private int deliveryId;
        private string commit;
        private List<Model.Messages> chatMessages = new List<Model.Messages>();
        private DispatcherTimer messageRefreshTimer;

        public Add(int courierId, int receiverId, int deliveryId, string commit)
        {
            InitializeComponent();
            this.courierId = courierId;
            this.receiverId = receiverId;
            this.deliveryId = deliveryId;
            this.commit = commit;

            UsersContext courier = UsersContext.Select().FirstOrDefault(u => u.Id == courierId);
            UsersContext receiver = UsersContext.Select().FirstOrDefault(u => u.Id == receiverId);

            if (courier != null) myName.Text = courier.FIO;
            if (receiver != null) interlocutor.Text = receiver.FIO;
            delivery.Text = commit;

            LoadMessages();

            messageRefreshTimer = new DispatcherTimer();
            messageRefreshTimer.Interval = TimeSpan.FromSeconds(2);
            messageRefreshTimer.Tick += MessageRefreshTimer_Tick;
            messageRefreshTimer.Start();
        }

        private void MessageRefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadMessages();
        }

        private void LoadMessages()
        {
            chatMessages = MessagesContext.Select().Where(m => m.IdDelivery == deliveryId &&
                                                               ((m.SenderId == courierId && m.ReceiverId == receiverId) ||
                                                                (m.SenderId == receiverId && m.ReceiverId == courierId)))
                                                    .OrderBy(m => m.DateTime)
                                                    .ToList();

            chatBox.Document.Blocks.Clear();

            foreach (var msg in chatMessages)
            {
                var sender = UsersContext.Select().FirstOrDefault(u => u.Id == msg.SenderId);
                if (sender != null)
                {
                    var paragraph = new Paragraph();
                    paragraph.Inlines.Add(new Run($"{msg.DateTime.ToString("dd.MM.yyyy HH:mm")}: {sender.FIO}\n"));
                    if (!string.IsNullOrEmpty(msg.MessageText))
                    {
                        paragraph.Inlines.Add(new Run($"{msg.MessageText}\n\n"));
                    }
                    if (msg.ImagePath != null && msg.ImagePath.Length > 0)
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = new MemoryStream(msg.ImagePath);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();

                        Image image = new Image { Source = bitmap, Height = 100, Width = 100 };
                        InlineUIContainer container = new InlineUIContainer(image);
                        paragraph.Inlines.Add(container);
                        paragraph.Inlines.Add(new Run("\n")); // Разделяем изображение от текста
                    }
                    chatBox.Document.Blocks.Add(paragraph);
                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new PagesCourier.DeliveryOj.Main());
        }

        private void sendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(messageText.Text))
            {
                Model.Messages newMessage = new Model.Messages(0, deliveryId, receiverId, courierId, messageText.Text, null, DateTime.Now);
                MessagesContext.Add(newMessage);
                messageText.Clear();
                LoadMessages();
            }
            else
            {
                MessageBox.Show("Сообщение не может быть пустым.");
            }
        }

        private void sendImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string sourcePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(sourcePath);

                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string destinationDirectory = Path.Combine(projectDirectory, "Image");

                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                string destinationPath = Path.Combine(destinationDirectory, fileName);
                File.Copy(sourcePath, destinationPath, true);

                byte[] imageData;
                using (FileStream fs = new FileStream(destinationPath, FileMode.Open, FileAccess.Read))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }

                Model.Messages newMessage = new Model.Messages(0, deliveryId, receiverId, courierId, null, imageData, DateTime.Now);
                MessagesContext.Add(newMessage);
                LoadMessages();
            }
        }

    }
}

