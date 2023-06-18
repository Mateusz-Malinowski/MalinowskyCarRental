using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MalinowskyCarRental
{
    public partial class EmployeesForm : Window
    {
        private readonly MalinowskyCarRentalEntities context;
        private readonly Pracownicy employee;

        public EmployeesForm(in MalinowskyCarRentalEntities context, in Pracownicy employee = null)
        {
            InitializeComponent();
            this.context = context;
            this.employee = employee;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (employee == null)
            {
                header.Content = "Dodaj pracownika";
                confirmButton.Text = "Dodaj";
            }
            else
            {
                header.Content = $"Edytuj pracownika {employee.nazwisko}";
                confirmButton.Text = "Edytuj";

                id_bazy.SelectedItem = employee.id_bazy;
                pesel.Text = employee.pesel?.Trim();
                imie.Text = employee.imie?.Trim();
                nazwisko.Text = employee.nazwisko?.Trim();
                data_urodzenia.SelectedDate = employee.data_urodzenia;
                nr_telefonu.Text = employee.nr_telefonu?.Trim();
                data_zatrudnienia.SelectedDate = employee.data_zatrudnienia;
                stanowisko.Text = employee.stanowisko?.Trim();
                stawka_godzinowa.Text = employee.stawka_godzinowa.ToString();
                kraj.Text = employee.kraj?.Trim();
                miasto.Text = employee.miasto?.Trim();
                ulica.Text = employee.ulica?.Trim();
                numer_domu.Text = employee.numer_domu?.Trim();
                numer_lokalu.Text = employee.numer_lokalu?.Trim();
            }

            id_bazy.ItemsSource = context.Bazy.Local.Select(location => location.id_bazy);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) => Close();
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pesel.Text.Length != 11) throw new FormatException();

                Pracownicy newEmployee = new Pracownicy()
                {
                    id_bazy = (int)id_bazy.SelectedItem,
                    pesel = pesel.Text,
                    imie = imie.Text,
                    nazwisko = nazwisko.Text,
                    data_urodzenia = data_urodzenia.SelectedDate,
                    nr_telefonu = nr_telefonu.Text,
                    data_zatrudnienia = data_zatrudnienia.SelectedDate,
                    stanowisko = stanowisko.Text,
                    kraj = kraj.Text,
                    miasto = miasto.Text,
                    ulica = ulica.Text,
                    numer_domu = numer_domu.Text,
                    numer_lokalu = numer_lokalu.Text
                };

                if (stawka_godzinowa.Text != "") newEmployee.stawka_godzinowa = short.Parse(stawka_godzinowa.Text);

                if (employee != null) newEmployee.id_pracownika = employee.id_pracownika;

                context.Pracownicy.AddOrUpdate(newEmployee);
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
