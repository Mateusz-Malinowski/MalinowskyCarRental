using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MalinowskyCarRental
{
    public partial class CarsForm : Window
    {
        private readonly MalinowskyCarRentalEntities context;
        private readonly Samochody car;

        /// <summary>
        /// Creates new cars form window.
        /// </summary>
        /// <param name="car">
        /// if provided, form will update the <paramref name="car"/> instead of adding new one
        /// </param>
        public CarsForm(in MalinowskyCarRentalEntities context, in Samochody car = null)
        {
            InitializeComponent();
            this.car = car;
            this.context = context;
        }

        /// <summary>
        /// Fires when window is loaded. Prefills data from car if it was provided.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.Typy_samochodow.Load();
            context.Bazy.Load();
            context.Stany_samochodu.Load();

            if (car == null)
            {
                header.Content = "Dodaj samochód";
                confirmButton.Text = "Dodaj";
            }
            else
            {
                header.Content = $"Edytuj samochód o numerze {car.id_samochodu}";
                confirmButton.Text = "Edytuj";

                id_typu.SelectedItem = car.id_typu;
                id_bazy.SelectedItem = car.id_bazy;
                id_stanu_samochodu.SelectedItem = car.id_stanu_samochodu;
                vin.Text = car.vin;
                przebieg.Text = car.przebieg.ToString();
                cena_za_dzien.Text = car.cena_za_dzien.ToString();
            }

            id_typu.ItemsSource = context.Typy_samochodow.Local.Select(car => car.id_typu);
            id_bazy.ItemsSource = context.Bazy.Local.Select(car => car.id_bazy);
            id_stanu_samochodu.ItemsSource = context.Stany_samochodu.Local.Select(car => car.id_stanu_samochodu);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// Fires when "Add" or "Update" button was clicked. Adds or updates the car.
        /// </summary>
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            Samochody newCar;

            try
            {
                newCar = new Samochody()
                {
                    id_typu = (int)id_typu.SelectedItem,
                    id_bazy = (int)id_bazy.SelectedItem,
                    id_stanu_samochodu = (int)id_stanu_samochodu.SelectedItem,
                    vin = vin.Text,
                };

                if (przebieg.Text != "") newCar.przebieg = int.Parse(przebieg.Text);
                if (cena_za_dzien.Text != "") newCar.cena_za_dzien = short.Parse(cena_za_dzien.Text);

                if (car != null) newCar.id_samochodu = car.id_samochodu;

                context.Samochody.AddOrUpdate(newCar);
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
