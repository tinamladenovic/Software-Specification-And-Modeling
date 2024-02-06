using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiveStarTours.Services;
using FiveStarTours.View.Owner;

namespace FiveStarTours.View
{
    public partial class AddAccommodationView : Window
    {
        public User LoggedInUser { get; set; }

        private readonly LocationsService _locationsRepository;
        private readonly AccommodationsService _accommodationService;


        public AddAccommodationView(User user)
        {
            InitializeComponent();
            DataContext = this;

            _locationsRepository = new LocationsService();
            _accommodationService = new AccommodationsService();

            LoggedInUser = user;


            // Adding state and city trough combobox:

            List<string> States = _locationsRepository.GetAllStates();
            stateComboBox.ItemsSource = States;
            stateComboBox.SelectedValuePath = ".";
            stateComboBox.DisplayMemberPath = ".";
        }

        //Name
        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }


        //Location - state and city
        private string selectedState;
        private string selectedCity;

        private void stateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stateComboBox.SelectedItem != null)
            {

                string selectedStateComboBox = stateComboBox.SelectedItem.ToString();
                List<string> citiesInState = _locationsRepository.GetCitiesInState(selectedStateComboBox);
                cityComboBox.ItemsSource = citiesInState;
                selectedState = stateComboBox.SelectedItem as string;
            }
        }

        private void cityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cityComboBox.SelectedItem != null)
            {
                selectedCity = cityComboBox.SelectedItem as string;
            }
        }

        private Location GetSelectedLocation()
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

        //Accommodation type
        public AccommodationType accommodationType;

        //Accommodation max guest number
        public string _maxGuestNum;
        public string MaxGuestNum
        {
            get => _maxGuestNum;
            set
            {
                if (value != _maxGuestNum)
                {
                    _maxGuestNum = value;
                    OnPropertyChanged();
                }
            }
        }


        //Min days to make reservation
        private string _minReservationDays;
        public string MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                if (value != _minReservationDays)
                {
                    _minReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }



        //Days possible to cancel
        private string _daysPossibleToCancel = "1";
        public string DaysPossibleToCancel
        {
            get => _daysPossibleToCancel;
            set
            {
                if (value != _daysPossibleToCancel)
                {
                    _daysPossibleToCancel = value;
                    OnPropertyChanged();
                }
            }
        }

        //Images
        private string _imageURLs;
        public string ImageURLs
        {
            get => _imageURLs;
            set
            {
                if (value != _imageURLs)
                {
                    _imageURLs = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(MaxGuestNum, out int guestNum))
            {
                MessageBox.Show("Please enter a valid numeric value for Max Guest Number.");
                return;
            }

            if (!int.TryParse(MinReservationDays, out int minDays))
            {
                MessageBox.Show("Please enter a valid numeric value for Min Reservation Days.");
                return;
            }

            if (!int.TryParse(DaysPossibleToCancel, out int cancelDays))
            {
                MessageBox.Show("Please enter a valid numeric value for Days Possible to Cancel.");
                return;
            }

            int MaximumGuests = int.Parse(MaxGuestNum);
            int MinDays = int.Parse(MinReservationDays);
            int DaysCancel = int.Parse(DaysPossibleToCancel);

            Location location = GetSelectedLocation();

            List<string> ImageURLsList = new List<string>();
            if (ImageURLs != null)
            {
                ImageURLsList = MakeUrlsList(ImageURLs);
            }
            else return;

            string AccType = AccommodationTypeComboBox.Text;

            if (AccType.Equals("apartment"))
            {
                accommodationType = AccommodationType.apartment;
            }
            else if (AccType.Equals("house"))
            {
                accommodationType = AccommodationType.house;
            }
            else if (AccType.Equals("cottage"))
            {
                accommodationType = AccommodationType.cottage;
            }



            Accommodation newAccommodation = new Accommodation(
                    AccommodationName,
                    location,
                    accommodationType,
                    MaximumGuests,
                    MinDays,
                    DaysCancel,
                    ImageURLsList
                );


            _accommodationService.Save(newAccommodation);
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
            Close();

        }


        public List<string> MakeUrlsList(string urls)
        {
            List<string> result = new List<string>();

            Array _urls = urls.Split(", ");

            foreach (string url in _urls)
            {
                result.Add(url);
            }

            return result;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
            Close();
        }


    }
    
}

