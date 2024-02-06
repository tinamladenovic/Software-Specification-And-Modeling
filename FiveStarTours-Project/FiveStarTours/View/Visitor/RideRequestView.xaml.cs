using FiveStarTours.Model;
using FiveStarTours.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace FiveStarTours.View.Visitor
{
    /// <summary>
    /// Interaction logic for RideRequestView.xaml
    /// </summary>
    public partial class RideRequestView : Window, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }
        private readonly LocationsService _locationsRepository;
        private readonly LanguagesService _languageRepository;
        private readonly GroupRideService _groupRideRepository;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _language;
        public string Lang
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _dateTime;
        public string DateTime
        {
            get => _dateTime;
            set
            {
                if (value != _dateTime)
                {
                    _dateTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _passengerNumber;
        public string PassengerNumber
        {
            get => _passengerNumber;
            set
            {
                if (value != _passengerNumber)
                {
                    _passengerNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _startingStreet;
        public string StartingStreet
        {
            get => _startingStreet;
            set
            {
                if (value != _startingStreet)
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
                if (value != _destinationStreet)
                {
                    _destinationStreet = value;
                    OnPropertyChanged();
                }
            }
        }
        public RideRequestView(User user)
        {
            InitializeComponent();
            DataContext = this;
            _locationsRepository = new LocationsService();
            _languageRepository = new LanguagesService();
            _groupRideRepository = new GroupRideService();


            List<string> StartingStates = _locationsRepository.GetAllStates();
            startingStateComboBox.ItemsSource = StartingStates;
            startingStateComboBox.SelectedValuePath = ".";
            startingStateComboBox.DisplayMemberPath = ".";

            List<string> DestinationStates = _locationsRepository.GetAllStates();
            destinationStateComboBox.ItemsSource = DestinationStates;
            destinationStateComboBox.SelectedValuePath = ".";
            destinationStateComboBox.DisplayMemberPath = ".";
        }
        private string selectedStartingState;
        private string selectedStartingCity;

        private string selectedDestinationState;
        private string selectedDestinationCity;

        private void startingStateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (startingStateComboBox.SelectedItem != null)
            {

                string selectedStartingStateComboBox = startingStateComboBox.SelectedItem.ToString();
                List<string> citiesInStartingState = _locationsRepository.GetCitiesInState(selectedStartingStateComboBox);
                startingCityComboBox.ItemsSource = citiesInStartingState;
                selectedStartingState = startingStateComboBox.SelectedItem as string;
            }
        }

        private void startingCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (startingCityComboBox.SelectedItem != null)
            {
                selectedStartingCity = startingCityComboBox.SelectedItem as string;
            }
        }

        private void destinationStateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (destinationStateComboBox.SelectedItem != null)
            {

                string selectedDestinationStateComboBox = destinationStateComboBox.SelectedItem.ToString();
                List<string> citiesInDestinationState = _locationsRepository.GetCitiesInState(selectedDestinationStateComboBox);
                destinationCityComboBox.ItemsSource = citiesInDestinationState;
                selectedDestinationState = destinationStateComboBox.SelectedItem as string;
            }
        }

        private void destinationCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (destinationCityComboBox.SelectedItem != null)
            {
                selectedDestinationCity = destinationCityComboBox.SelectedItem as string;
            }
        }

        private Location GetSelectedStartingLocation()
        {
            foreach (var location in _locationsRepository.GetAll())
            {
                if (location.City == selectedStartingCity)
                {
                    return location;
                }
            }

            return null;
        }

        public bool CheckStartingLocation()
        {
            if (selectedStartingCity == null)
            {
                MessageBox.Show("You must select state and city.");
                return false;
            }

            return true;
        }
        private Location GetSelectedDestinationLocation()
        {
            foreach (var location in _locationsRepository.GetAll())
            {
                if (location.City == selectedDestinationCity)
                {
                    return location;
                }
            }

            return null;
        }

        public bool CheckDestinationLocation()
        {
            if (selectedDestinationCity == null)
            {
                MessageBox.Show("You must select state and city.");
                return false;
            }

            return true;
        }

        List<DateTime> DateTimes = new List<DateTime>();
        private DateTime lastSelectedDate;

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            lastSelectedDate = (DateTime)Calendar.SelectedDate;

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            datesTextBox.Clear();
            if (Calendar.SelectedDate == null)
            {
                MessageBox.Show("Please select date first!");
                return;
            }
            DateTime date = (DateTime)Calendar.SelectedDate;
            TimeSpan time = TimeSpan.Parse(timeTextBox.Text);
            DateTime newDate = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
            DateTimes.Add(newDate);
            foreach (DateTime dateTime in DateTimes)
            {
                datesTextBox.AppendText(dateTime.ToString() + "\n");
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Location startingLocation = new Location();
            if (CheckStartingLocation())
            {
                startingLocation = GetSelectedStartingLocation();
            }
            else return;

            Location destinationLocation = new Location();
            if (CheckDestinationLocation())
            {
                destinationLocation = GetSelectedDestinationLocation();
            }
            else return;

            

            if (Language == null)
            {
                MessageBox.Show("You must enter language of tour.");
                return;
            }

            int PassNumber;
            if (PassengerNumber == null)
            {
                MessageBox.Show("You must enter maximum guests number.");
                return;
            }
            else
            {
                PassNumber = int.Parse(PassengerNumber);
                if (PassNumber < 1)
                {
                    MessageBox.Show("Maximum number of guests must be greater then 0.");
                    return;
                }
            }

            Language language = new Language(Lang);
            _languageRepository.Save(language);

            

            GroupRide groupRide = new GroupRide(PassNumber, startingLocation.Id, startingLocation, StartingStreet, destinationLocation.Id, destinationLocation, DestinationStreet, language.Id, language, DateTimes.ElementAt(0), false, false);
            _groupRideRepository.Save(groupRide);

            Close();
        }

        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReservedToursButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GiftCardsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowTourButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BackgroundComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
