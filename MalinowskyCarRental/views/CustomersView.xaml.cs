using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;

namespace MalinowskyCarRental
{
    public partial class CustomersView : UserControl
    {
        private MainWindow mainWindow;
        private MalinowskyCarRentalEntities context;
        private CollectionViewSource customersViewSource;

        public CustomersView(in MainWindow mainWindow, in MalinowskyCarRentalEntities context)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.context = context;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            customersViewSource = (CollectionViewSource)FindResource("customersViewSource");
            context.Klienci.Load();
            customersViewSource.Source = context.Klienci.Local;
        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            const string messageText = "Czy jesteś pewny? Wszystkie wypożyczenia tego klienta także zostaną usunięte.";
            MessageBoxResult result = MessageBox.Show(messageText, "Ostrzeżenie",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result != MessageBoxResult.Yes) return;

            Klienci current = customersViewSource.View.CurrentItem as Klienci;

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
            customersViewSource.View.Refresh();
        }

        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            CustomerForm customerForm = new CustomerForm(context, customersViewSource.View.CurrentItem as Klienci);
            customerForm.Owner = mainWindow;
            customerForm.ShowDialog();
            customersViewSource.View.Refresh();
        }

        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            CustomerForm customerForm = new CustomerForm(context);
            customerForm.Owner = mainWindow;
            customerForm.ShowDialog();
            customersViewSource.View.Refresh();
        }
    }
}
