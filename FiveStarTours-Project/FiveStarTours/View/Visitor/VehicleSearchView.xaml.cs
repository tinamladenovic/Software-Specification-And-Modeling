using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using FiveStarTours.View.Traveler;
using FiveStarTours.View.Driver;
using FiveStarTours.View.VehicleOnAdress;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FiveStarTours.View.Visitor
{
    /// <summary>
    /// Interaction logic for VehicleSearchView.xaml
    /// </summary>
    public partial class VehicleSearchView : Window, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }

        public static ObservableCollection<Vehicle> Vehicles { get; set; }
        public Vehicle SelectedVehicle { get; set; }

        private readonly VehicleService _vehicleRepository;
        private readonly LocationsService _locationsRepository;
        private readonly DrivingsService _drivingsRepository;
        private readonly ReservedDrivingsService _reservedDrivingsRepository;
        public VehicleSearchView(User user)
        {
            LoggedInUser = user;

            InitializeComponent();
            DataContext = this;
            _vehicleRepository = new VehicleService();
            _locationsRepository = new LocationsService();
            _drivingsRepository = new DrivingsService();
            _reservedDrivingsRepository = new ReservedDrivingsService();
            Vehicles = new ObservableCollection<Vehicle>(_vehicleRepository.GetAll());

            List<string> starting = _locationsRepository.GetAllStates();
            StartingState.ItemsSource = starting;
            StartingState.SelectedValuePath = ".";
            StartingState.DisplayMemberPath = ".";

            List<string> destination = _locationsRepository.GetAllStates();
            DestinationState.ItemsSource = destination;
            DestinationState.SelectedValuePath = ".";
            DestinationState.DisplayMemberPath = ".";

            // Poll the shared object to detect changes
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            // Check if the shared object's notification message has changed
            string notification = NotificationManager.Instance.Notification;
            if (!string.IsNullOrEmpty(notification))
            {
                // Handle the notification message
                MessageBox.Show(notification);

                // Reset the notification after handling
                NotificationManager.Instance.Notification = null;
            }
        }
        private void OnNotificationReceived(string message)
        {
            // Handle the notification message
            MessageBox.Show(message);

        }
        public string selectedStartingCity;
        public string selectedStartingState;
        public string selectedDestinationCity;
        public string selectedDestinationState;
        private Location GetSelectedLocation(string selectedCity)
        {
            foreach (var location in _locationsRepository.GetAll())
            {
                if (location.City == selectedCity)
                {
                    return location;
                }
            }
            return null;
        }



        private string _startingStreet;
        public string StartingStreet
        {
            get => _startingStreet;
            set
            {
                if (_startingStreet != value)
                {
                    _startingStreet = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _destinationStreet;
        public string DestinationStreet
        {
            get => _destinationStreet;
            set
            {
                if (_destinationStreet != value)
                {
                    _destinationStreet = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _startingTime;
        public DateTime StartingTime
        {
            get => _startingTime;
            set
            {
                if (_startingTime != value)
                {
                    _startingTime = value;
                    OnPropertyChanged();
                }
            }
        }
        private void StartingState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartingState.SelectedItem != null)
            {

                string selectedStarting = StartingState.SelectedItem.ToString();
                List<string> citiesInState = _locationsRepository.GetCitiesInState(selectedStarting);
                StartingCity.ItemsSource = citiesInState;
                selectedStartingState = StartingCity.SelectedItem as string;
            }
        }


        private void StartingCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartingCity.SelectedItem != null)
            {
                selectedStartingCity = StartingCity.SelectedItem as string;
            }
        }

        private void DestinationState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DestinationState.SelectedItem != null)
            {

                string selectedDestination = DestinationState.SelectedItem.ToString();
                List<string> citiesInState = _locationsRepository.GetCitiesInState(selectedDestination);
                DestinationCity.ItemsSource = citiesInState;
                selectedDestinationState = DestinationCity.SelectedItem as string;
            }
        }

        private void DestinationCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DestinationCity.SelectedItem != null)
            {
                selectedDestinationCity = DestinationCity.SelectedItem as string;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        /*private void Refresh(List<Vehicle> vehicles)
        {
            Vehicles.Clear();
            foreach (Vehicle vehicle in vehicles)
            {
                Vehicles.Add(vehicle);
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string startingState = StartingState.Text;
                string startingCity = StartingCity.Text;
                string startingStreet = StartingStreet.Text;
                string destinationState = DestinationState.Text;
                string destinationCity = DestinationCity.Text;
                string destinationStreet = DestinationStreet.Text;

               
                List<Vehicle> seachedVehicles = _vehicleRepository.SearchVehicles(startingState, startingCity);
                Refresh(seachedVehicles);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid inputs");
            }

        }*/

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {

            Location startingLocation = new Location();
            Location destinationLocation = new Location();
            startingLocation = GetSelectedLocation(selectedStartingCity);
            destinationLocation = GetSelectedLocation(selectedDestinationCity);

            ReservedDrivings result = new ReservedDrivings(startingLocation.Id, destinationLocation.Id, startingLocation, StartingStreet, destinationLocation, DestinationStreet, StartingTime, LoggedInUser.Id);
            _reservedDrivingsRepository.Save(result);
            MessageBox.Show("Reservation has just made.");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InformationsButton_Click(object sender, RoutedEventArgs e)
        {
            ToursListingView toursListing = new ToursListingView(LoggedInUser);
            toursListing.Show();
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ReservedToursView reservedTours = new ReservedToursView(LoggedInUser);
            reservedTours.Show();
        }
    }
}