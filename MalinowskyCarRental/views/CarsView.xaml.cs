using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;

namespace MalinowskyCarRental
{
    public partial class CarsView : UserControl
    {
        private MainWindow mainWindow;
        private MalinowskyCarRentalEntities context;
        private CollectionViewSource carsViewSource;

        public CarsView(in MainWindow mainWindow, in MalinowskyCarRentalEntities context)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.context = context;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            carsViewSource = (CollectionViewSource)FindResource("carsViewSource");
            context.Samochody.Load();
            carsViewSource.Source = context.Samochody.Local;
        }

        /// <summary>Deletes selected car and all rentals associated with it.</summary>
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            const string messageText = "Czy jesteś pewny? Wszystkie wypożyczenia tego samochodu" +
                " także zostaną usunięte.";
            MessageBoxResult result = MessageBox.Show(messageText, "Ostrzeżenie",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result != MessageBoxResult.Yes) return;

            Samochody current = carsViewSource.View.CurrentItem as Samochody;

            // delete all rentals related to this employee
            foreach (Wypozyczenia rental in current.Wypozyczenia.ToList())
            {
                Wypozyczenia rentalToDelete = (from o in context.Wypozyczenia.Local
                                               where o.id_samochodu == rental.id_samochodu
                                               select o).FirstOrDefault();
                context.Wypozyczenia.Remove(rentalToDelete);
            }

            context.Samochody.Remove(current);
            context.SaveChanges();
            carsViewSource.View.Refresh();
        }

        /// <summary>Opens cars form in "update mode".</summary>
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            CarsForm carsForm = new CarsForm(context, carsViewSource.View.CurrentItem as Samochody);
            carsForm.Owner = mainWindow;
            carsForm.ShowDialog();
            carsViewSource.View.Refresh();
        }

        /// <summary>Opens cars form in "add mode".</summary>
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            CarsForm carsForm = new CarsForm(context);
            carsForm.Owner = mainWindow;
            carsForm.ShowDialog();
            carsViewSource.View.Refresh();
        }
    }
}
