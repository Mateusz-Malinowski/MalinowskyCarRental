using System.Windows;
using System.Windows.Input;

namespace MalinowskyCarRental
{
    public partial class MainWindow : Window
    {
        private MalinowskyCarRentalEntities context = new MalinowskyCarRentalEntities();

        private CustomersView customersView;
        private RentalsView rentalsView;
        private EmployeesView employeesView;
        private CarsView carsView;

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
            contentControl.Content = new CustomersView(this, context);
        }

        private void ButtonCustomers_Click(object sender, RoutedEventArgs e)
        {
            if (customersView == null) customersView = new CustomersView(this, context);
            contentControl.Content = customersView;
        }

        private void ButtonRentals_Click(object sender, RoutedEventArgs e)
        {
            if (rentalsView == null) rentalsView = new RentalsView(this, context);
            contentControl.Content = rentalsView;
        }

        private void ButtonEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (employeesView == null) employeesView = new EmployeesView(this, context);
            contentControl.Content = employeesView;
        }

        private void ButtonCars_Click(object sender, RoutedEventArgs e)
        {
            if (carsView == null) carsView = new CarsView(this, context);
            contentControl.Content = carsView;
        }
    }
}
