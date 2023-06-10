using System.Windows;
using System.Windows.Input;

namespace MalinowskyCarRental
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2) return;


            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                Width = 1280; Height = 720;
                return;
            }
            
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                return;
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
