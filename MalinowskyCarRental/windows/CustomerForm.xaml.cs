using System;
using System.Data.Entity.Migrations;
using System.Windows;
using System.Windows.Input;

namespace MalinowskyCarRental
{
    public partial class CustomerForm : Window
    {
        private readonly MalinowskyCarRentalEntities context;
        private readonly Klienci customer;

        /// <summary>
        /// Creates new customers form window.
        /// </summary>
        /// <param name="customer">
        /// if provided, form will update the <paramref name="customer"/> instead of adding new one
        /// </param>
        public CustomerForm(in MalinowskyCarRentalEntities context, in Klienci customer = null)
        {
            InitializeComponent();
            this.context = context;
            this.customer = customer;
        }

        /// <summary>
        /// Fires when window is loaded. Prefills data from customer if it was provided.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (customer != null)
            {
                header.Content = $"Edytuj klienta {customer.nazwisko}";
                confirmButton.Text = "Edytuj";

                pesel.Text = customer.pesel?.Trim();
                imie.Text = customer.imie?.Trim();
                nazwisko.Text = customer.nazwisko?.Trim();
                data_urodzenia.SelectedDate = customer.data_urodzenia;
                nr_telefonu.Text = customer.nr_telefonu?.Trim();
                kraj.Text = customer.kraj?.Trim();
                miasto.Text = customer.miasto?.Trim();
                ulica.Text = customer.ulica?.Trim();
                numer_domu.Text = customer.numer_domu?.Trim();
                numer_lokalu.Text = customer.numer_lokalu?.Trim();

                return;
            }

            header.Content = "Dodaj klienta";
            confirmButton.Text = "Dodaj";
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// Fires when "Add" or "Update" button was clicked. Adds or updates the customer.
        /// </summary>
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (pesel.Text.Length != 11) throw new FormatException();

                Klienci newCustomer = new Klienci()
                {
                    pesel = pesel.Text,
                    imie = imie.Text,
                    nazwisko = nazwisko.Text,
                    data_urodzenia = data_urodzenia.SelectedDate,
                    nr_telefonu = nr_telefonu.Text,
                    kraj = kraj.Text,
                    miasto = miasto.Text,
                    ulica = ulica.Text,
                    numer_domu = numer_domu.Text,
                    numer_lokalu = numer_lokalu.Text,
                };

                if (customer != null) newCustomer.id_klienta = customer.id_klienta;

                context.Klienci.AddOrUpdate(newCustomer);
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
