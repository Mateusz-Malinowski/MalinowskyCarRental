using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Data.Entity;
using System.Linq;
using System;

namespace MalinowskyCarRental
{
    public partial class MainWindow : Window
    {
        private MalinowskyCarRentalEntities context = new MalinowskyCarRentalEntities();
        private CollectionViewSource klienciViewSource;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            klienciViewSource = (CollectionViewSource)FindResource("klienciViewSource");
            context.Klienci.Load();
            context.Wypozyczenia.Load();
            klienciViewSource.Source = context.Klienci.Local;
        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            const string messageText = "Czy jesteś pewny? Wszystkie wypożyczenia tego klienta także zostaną usunięte.";
            MessageBoxResult result = MessageBox.Show(messageText, "Ostrzeżenie",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result != MessageBoxResult.Yes) return;

            Klienci current = klienciViewSource.View.CurrentItem as Klienci;

            // delete all rentals related to this customer
            foreach (Wypozyczenia rental in current.Wypozyczenia.ToList())
            {
                Wypozyczenia rentalToDelete = (from o in context.Wypozyczenia.Local
                                               where o.id_wypozyczenia == rental.id_wypozyczenia
                                               select o).FirstOrDefault();
                context.Wypozyczenia.Remove(rentalToDelete);
            }

            context.Klienci.Remove(current);
            context.SaveChanges();
            klienciViewSource.View.Refresh();
        }
        
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            CustomerForm customerForm = new CustomerForm(context, klienciViewSource.View.CurrentItem as Klienci);
            customerForm.Owner = this;
            customerForm.ShowDialog();
            klienciViewSource.View.Refresh();
        }
        
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            CustomerForm customerForm = new CustomerForm(context);
            customerForm.Owner = this;
            customerForm.ShowDialog();
            klienciViewSource.View.Refresh();
        }
    }
}
