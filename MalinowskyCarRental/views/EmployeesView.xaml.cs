using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;

namespace MalinowskyCarRental
{
    public partial class EmployeesView : UserControl
    {
        private MainWindow mainWindow;
        private MalinowskyCarRentalEntities context;
        private CollectionViewSource employeesViewSource;

        public EmployeesView(in MainWindow mainWindow, in MalinowskyCarRentalEntities context)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.context = context;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            employeesViewSource = (CollectionViewSource)FindResource("employeesViewSource");
            context.Pracownicy.Load();
            context.Bazy.Load();
            employeesViewSource.Source = context.Pracownicy.Local;
        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            const string messageText = "Czy jesteś pewny? Wszystkie wypożyczenia przypisane do tego pracownika" +
                " także zostaną usunięte.";
            MessageBoxResult result = MessageBox.Show(messageText, "Ostrzeżenie",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result != MessageBoxResult.Yes) return;

            Pracownicy current = employeesViewSource.View.CurrentItem as Pracownicy;

            // delete all rentals related to this employee
            foreach (Wypozyczenia rental in current.Wypozyczenia.ToList())
            {
                Wypozyczenia rentalToDelete = (from o in context.Wypozyczenia.Local
                                               where o.id_wypozyczenia == rental.id_wypozyczenia
                                               select o).FirstOrDefault();
                context.Wypozyczenia.Remove(rentalToDelete);
            }

            context.Pracownicy.Remove(current);
            context.SaveChanges();
            employeesViewSource.View.Refresh();
        }

        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            EmployeesForm employeesForm = new EmployeesForm(context, employeesViewSource.View.CurrentItem as Pracownicy);
            employeesForm.Owner = mainWindow;
            employeesForm.ShowDialog();
            employeesViewSource.View.Refresh();
        }

        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            EmployeesForm employeesForm = new EmployeesForm(context);
            employeesForm.Owner = mainWindow;
            employeesForm.ShowDialog();
            employeesViewSource.View.Refresh();
        }
    }
}
