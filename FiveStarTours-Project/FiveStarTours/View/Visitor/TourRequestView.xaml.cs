// Modeled on CommentForm from InitialProject

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Model.Enums;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FiveStarTours.View.Visitor
{
    /// <summary>
    /// Interaction logic for TourRequestView.xaml
    /// </summary>
    public partial class TourRequestView : Window, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }
        private readonly TourRequestService _tourRequestRepository;
        private readonly LocationsService _locationsRepository;
        private readonly LanguagesService _languageRepository;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
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

        private string _maxGuests;
        public string MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _start;
        public string Start
        {
            get => _start;
            set
            {
                if (value != _start)
                {
                    _start = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _end;
        public string End
        {
            get => _end;
            set
            {
                if (value != _end)
                {
                    _end = value;
                    OnPropertyChanged();
                }
            }
        }




        public TourRequestView(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _locationsRepository = new LocationsService();
            _tourRequestRepository = new TourRequestService();
            _languageRepository = new LanguagesService();


            List<string> States = _locationsRepository.GetAllStates();
            stateComboBox.ItemsSource = States;
            stateComboBox.SelectedValuePath = ".";
            stateComboBox.DisplayMemberPath = ".";

        }

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

        public bool CheckLocation()
        {
            if (selectedCity == null)
            {
                MessageBox.Show("You must select state and city.");
                return false;
            }

            return true;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MakeButton_Click(object sender, RoutedEventArgs e)
        {

            Location location = new Location();
            if (CheckLocation())
            {
                location = GetSelectedLocation();
            }
            else return;

            if (Description == null)
            {
                MessageBox.Show("You must enter description of tour.");
                return;
            }

            if (Language == null)
            {
                MessageBox.Show("You must enter language of tour.");
                return;
            }

            int MaximumGuests;
            if (MaxGuests == null)
            {
                MessageBox.Show("You must enter maximum guests number.");
                return;
            }
            else
            {
                MaximumGuests = int.Parse(MaxGuests);
                if (MaximumGuests < 1)
                {
                    MessageBox.Show("Maximum number of guests must be greater then 0.");
                    return;
                }
            }

            Language language = new Language(Lang);
            _languageRepository.Save(language);

            TourRequest newTourRequest = new TourRequest(location.Id, location, Description, language.Id, language, MaximumGuests,
             Interval, DateTime.Now);
            _tourRequestRepository.Save(newTourRequest);

            Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BackgroundComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public bool CheckIntervals(string intervals)
        {
            if (intervals == null)
            {
                MessageBox.Show("At least 1 interval is required.");
                return false;
            }

            return true;
        }

        DateInterval Interval;

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Interval = new DateInterval(DateTime.Parse(Start), DateTime.Parse(End));
        }
    }
}
