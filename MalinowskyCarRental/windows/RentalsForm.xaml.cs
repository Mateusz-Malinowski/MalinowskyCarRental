using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MalinowskyCarRental
{
    public partial class RentalsForm : Window
    {
        private readonly MalinowskyCarRentalEntities context;
        private readonly Wypozyczenia rental;

        public RentalsForm(in MalinowskyCarRentalEntities context, in Wypozyczenia rental = null)
        {
            InitializeComponent();
            this.rental = rental;
            this.context = context;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.Klienci.Load();
            context.Pracownicy.Load();
            context.Samochody.Load();

            if (rental == null)
            {
                header.Content = "Dodaj wypożyczenie";
                confirmButton.Text = "Dodaj";
            }
            else
            {
                header.Content = $"Edytuj wypożyczenie o numerze {rental.id_wypozyczenia}";
                confirmButton.Text = "Edytuj";

                id_klienta.SelectedItem = rental.id_klienta;
                id_pracownika.SelectedItem = rental.id_pracownika;
                id_samochodu.SelectedItem = rental.id_samochodu;
                data_wypozyczenia.SelectedDate = rental.data_wypozyczenia;
                planowana_data_zwrotu.SelectedDate = rental.planowana_data_zwrotu;
                data_zwrotu.SelectedDate = rental.data_zwrotu;
                oplata_dodatkowa.Text = rental.oplata_dodatkowa.ToString();
            }

            id_klienta.ItemsSource = context.Klienci.Local.Select(customer => customer.id_klienta);
            id_pracownika.ItemsSource = context.Pracownicy.Local.Select(customer => customer.id_pracownika);
            id_samochodu.ItemsSource = context.Samochody.Local.Select(customer => customer.id_samochodu);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) => Close();
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            Wypozyczenia newRental;

            try
            {
                newRental = new Wypozyczenia()
                {
                    id_klienta = (int)id_klienta.SelectedItem,
                    id_pracownika = (int)id_pracownika.SelectedItem,
                    id_samochodu = (int)id_samochodu.SelectedItem,
                    data_wypozyczenia = (DateTime)data_wypozyczenia.SelectedDate,
                    planowana_data_zwrotu = (DateTime)planowana_data_zwrotu.SelectedDate,
                    data_zwrotu = data_zwrotu.SelectedDate
                };

                if (oplata_dodatkowa.Text != "") newRental.oplata_dodatkowa = int.Parse(oplata_dodatkowa.Text);

                if (rental != null) newRental.id_wypozyczenia = rental.id_wypozyczenia;

                context.Wypozyczenia.AddOrUpdate(newRental);
                context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Nie udało się zapisać zmian. Nie podano wymaganego pola lub format był błędny.",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Close();
        }
    }
}
