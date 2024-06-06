using System.Windows;
using System.Windows.Controls;

namespace Delivery_Паксюаткин
{
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public MainWindow()
        {
            InitializeComponent();
            OpenPage(new Pages.Login.Main());
            init = this;
        }

        public void OpenPage(Page Page)
        {
            frame.Navigate(Page);
        }
    }
}
