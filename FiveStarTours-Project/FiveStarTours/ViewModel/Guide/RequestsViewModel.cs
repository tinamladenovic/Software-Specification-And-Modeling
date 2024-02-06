using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FiveStarTours.Model;
using FiveStarTours.Services;
using FiveStarTours.View;
using FiveStarTours.View.Guide;
using FiveStarTours.ViewModel.Command;

namespace FiveStarTours.ViewModel.Guide
{
    public class RequestsViewModel : FiveStarTours.ViewModel.BindableBase.BindableBase
    {
        public event EventHandler RequestClose;
        public ICommand SearchCommand { get; }
        public ICommand StatisticCommand { get; }
        public ICommand OpenLanguageCommand { get; }
        public ICommand OpenLocationCommand { get; }

        private readonly TourRequestService _tourRequestService;
        private readonly LocationsService _locationsService;
        public User LoggedInUser { get; set; }

        private ObservableCollection<TourRequest> _tourRequests;
        public ObservableCollection<TourRequest> TourRequests
        {
            get { return _tourRequests; }
            set
            {
                SetProperty(ref _tourRequests, value);
            }
        }

        private ObservableCollection<Location> _locations;
        public ObservableCollection<Location> Locations
        {
            get { return _locations; }
            set
            {
                SetProperty(ref _locations, value);
            }
        }

        private TourRequest _selectedRequest;
        public TourRequest SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                SetProperty(ref _selectedRequest, value);
                OpenAcceptWindow();
            }
        }

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                SetProperty(ref _selectedLocation, value);
            }
        }

        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                SetProperty(ref _language, value);
            }
        }

        private string _guestsNumber;
        public string GuestsNumber
        {
            get { return _guestsNumber; }
            set
            {
                SetProperty(ref _guestsNumber, value);
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public RequestsViewModel(User user)
        {
            LoggedInUser = user;

            _tourRequestService = new TourRequestService();
            _locationsService = new LocationsService();

            TourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetAllProcessing());
            Locations = new ObservableCollection<Location>(_locationsService.GetAll());

            SearchCommand = new RelayCommand(Search);
            StatisticCommand = new RelayCommand(ShowStatistic);
            OpenLanguageCommand = new RelayCommand(OpenLanguage);
            OpenLocationCommand = new RelayCommand(OpenLocation);

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            Location emptyLocation = new Location();
            Locations.Add(emptyLocation);
        }

        private void Search()
        {
            try
            {
                Language language = null;
                if (Language != null)
                {
                    language = new Language(Language);
                }
                if (GuestsNumber == null)
                {
                    GuestsNumber = "0";
                }

                DateInterval interval = new DateInterval(StartDate, EndDate);

                List<TourRequest> requests = _tourRequestService.SearchRequest(SelectedLocation, Convert.ToInt32(GuestsNumber), language, interval);
                TourRequests = new ObservableCollection<TourRequest>(requests);
                GuestsNumber = null;
                StartDate = DateTime.Now;
                EndDate = DateTime.Now;
                Language = null;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid inputs");
            }
        }

        private void ShowStatistic()
        {
            RequestsStatisticView view = new RequestsStatisticView(LoggedInUser);
            view.Show();
        }

        private void OpenLanguage()
        {
            Language language = _tourRequestService.GetMostWantedLanguage();
            if (language == null)
            {
                MessageBox.Show("There's no requests.");
                return;
            }
            MessageBoxResult result = MessageBox.Show($"Do you want to make tour for language: {language.Name} ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                TourRegistrationForm window = new TourRegistrationForm(LoggedInUser, language);
                window.Show();
            }
            else
            {
                return;
            }

        }

        private void OpenLocation()
        {
            Location location = _tourRequestService.GetMostWantedLocation();
            if (location == null)
            {
                MessageBox.Show("There's no requests.");
                return;
            }
            MessageBoxResult result = MessageBox.Show($"Do you want to make tour for location: {location.State}, {location.City} ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                TourRegistrationForm window = new TourRegistrationForm(LoggedInUser, location);
                window.Show();
            }
            else
            {
                return;
            }

        }

        private void OpenAcceptWindow()
        {
            Close();
            WholeTourAcceptView window = new WholeTourAcceptView(LoggedInUser, SelectedRequest);
            window.Show();
        }

        private void Close()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
