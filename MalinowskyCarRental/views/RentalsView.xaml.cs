using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;

namespace MalinowskyCarRental
{
    public partial class RentalsView : UserControl
    {
        private MainWindow mainWindow;
        private MalinowskyCarRentalEntities context;
        private CollectionViewSource rentalsViewSource;

        public RentalsView(in MainWindow mainWindow, in MalinowskyCarRentalEntities context)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.context = context;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            rentalsViewSource = (CollectionViewSource)FindResource("rentalsViewSource");
            context.Wypozyczenia.Load();
            rentalsViewSource.Source = context.Wypozyczenia.Local;
        }

        /// <summary>Deletes selected rental.</summary>
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            const string messageText = "Czy jesteś pewny, że chcesz usunąć dane o tym wypożyczeniu?";
            MessageBoxResult result = MessageBox.Show(messageText, "Ostrzeżenie",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result != MessageBoxResult.Yes) return;

            Wypozyczenia current = rentalsViewSource.View.CurrentItem as Wypozyczenia;

            context.Wypozyczenia.Remove(current);
            context.SaveChanges();
            rentalsViewSource.View.Refresh();
        }

        /// <summary>Opens rentals form in "update mode".</summary>
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            RentalsForm rentalsForm = new RentalsForm(context, rentalsViewSource.View.CurrentItem as Wypozyczenia);
            rentalsForm.Owner = mainWindow;
            rentalsForm.ShowDialog();
            rentalsViewSource.View.Refresh();
        }

        /// <summary>Opens rentals form in "add mode".</summary>
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            RentalsForm rentalsForm = new RentalsForm(context);
            rentalsForm.Owner = mainWindow;
            rentalsForm.ShowDialog();
            rentalsViewSource.View.Refresh();
        }
    }
}
