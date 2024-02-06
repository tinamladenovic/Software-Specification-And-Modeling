using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FiveStarTours.Model;
using FiveStarTours.Services;
using FiveStarTours.ViewModel.Command;

namespace FiveStarTours.ViewModel
{
    public class TourRegistrationFormViewModel : INotifyPropertyChanged
    {
        public event EventHandler RequestClose;
        public ICommand AddDateCommand { get; }
        public ICommand CloseWindowCommand { get; }
        public ICommand SaveCommand { get; }

        public User LoggedInUser { get; set; }

        private readonly ToursService _toursRepository;
        private readonly LanguagesService _languagesRepository;
        private readonly LocationsService _locationsRepository;
        private readonly KeyPointsService _keyPointsRepository;

        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        List<Language> LanguageList = new List<Language>();
        List<DateTime> DateTimes = new List<DateTime>();
        List<KeyPoints> KeyPointsList = new List<KeyPoints>();
        List<string> ImageUrlsList = new List<string>();

        private string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged();
                }
            }
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

        private string _languages;
        public string Languages
        {
            get => _languages;
            set
            {
                if (value != _languages)
                {
                    _languages = value;
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

        private string _keyPoints;
        public string KeyPoints
        {

            get => _keyPoints;
            set
            {
                if (value != _keyPoints)
                {
                    _keyPoints = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _duration;
        public string Duration
        {

            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _imageUrls;
        public string ImageUrls
        {
            get => _imageUrls;
            set
            {
                if (value != _imageUrls)
                {
                    _imageUrls = value;
                    OnPropertyChanged();
                }
            }
        }

        // Adding state and city trough combobox:

        private string _selectedState;
        public string SelectedState
        {
            get => _selectedState;
            set
            {
                _selectedState = value;
                SelectedStateChanged();
                OnPropertyChanged(nameof(SelectedState));
            }
        }

        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }

        private void SelectedStateChanged()
        {
            List<string> cities = _locationsRepository.GetCitiesInState(SelectedState);
            Cities = new ObservableCollection<string>(cities);
            OnPropertyChanged(nameof(Cities));
        }

        private Location GetSelectedLocation()
        {
            foreach (var location in _locationsRepository.GetAll())
            {
                if (location.City == SelectedCity)
                {
                    return location;
                }
            }
            return null;
        }

        // Adding dates and times into list

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private string _selectedTime;
        public string SelectedTime
        {
            get => _selectedTime;
            set
            {
                if (value != _selectedTime)
                {
                    _selectedTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> dateTimeCollection;
        public ObservableCollection<string> DateTimeCollection
        {
            get { return dateTimeCollection; }
            set
            {
                dateTimeCollection = value;
                OnPropertyChanged(nameof(DateTimeCollection));
            }
        }

        private void AddDateTime()
        {
            TimeSpan time = TimeSpan.Parse(SelectedTime);
            DateTime newDate = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, time.Hours, time.Minutes, time.Seconds);
            DateTimes.Add(newDate);
            DateTimeCollection.Add(newDate.ToString());
        }

        public TourRegistrationFormViewModel(User user)
        {
            _toursRepository = new ToursService();
            _languagesRepository = new LanguagesService();
            _locationsRepository = new LocationsService();
            _keyPointsRepository = new KeyPointsService();

            LoggedInUser = user;
            SelectedTime = "12:00:00";

            AddDateCommand = new RelayCommand(AddDateTime);
            CloseWindowCommand = new RelayCommand(CloseWindow);
            SaveCommand = new RelayCommand(Save);

            // Adding state and city trough combobox:

            States = new ObservableCollection<string>(_locationsRepository.GetAllStates());
            Cities = new ObservableCollection<string>();
            DateTimeCollection = new ObservableCollection<string>();
        }

        // Save tour
        private void Save()
        {
            Location location = new Location();
            location = GetSelectedLocation();

            LanguageList = MakeLanguagesList(Languages);
            List<int> LanguageIds = GetLanguagesIds(LanguageList);

            int MaximumGuests = int.Parse(MaxGuests);

            KeyPointsList = MakeKeyPointsList(KeyPoints);
            List<int> KeyPointsIds = GetKeyPointsIds(KeyPointsList);

            int DurationTime = int.Parse(Duration);

            ImageUrlsList = MakeUrlsList(ImageUrls);

            Tour newTour = new Tour(TourName, LoggedInUser, location.Id, location, Description, LanguageIds, LanguageList, MaximumGuests, KeyPointsIds, KeyPointsList, DateTimes, DurationTime, ImageUrlsList);
            _toursRepository.Save(newTour);

            CloseWindow();
        }

        // Close Window
        private void CloseWindow()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        // Convert from string to list of Language objects
        public List<Language> MakeLanguagesList(string languages)
        {
            List<Language> result = new List<Language>();

            Array _languages = languages.Split(", ");

            foreach (string language in _languages)
            {

                Language l = new Language(language);
                _languagesRepository.Save(l);
                result.Add(l);

            }

            return result;
        }

        public List<int> GetLanguagesIds(List<Language> languages)
        {
            List<int> result = new List<int>();

            foreach (Language lang in languages)
            {
                result.Add(lang.Id);
            }

            return result;
        }

        // Convert from string to list of KeyPoints objects
        public List<KeyPoints> MakeKeyPointsList(string keyPoints)
        {
            List<KeyPoints> result = new List<KeyPoints>();

            Array _keyPoints = keyPoints.Split(", ");

            foreach (string keyPoint in _keyPoints)
            {

                KeyPoints kp = new KeyPoints(keyPoint);
                _keyPointsRepository.Save(kp);
                result.Add(kp);

            }

            return result;
        }

        public List<int> GetKeyPointsIds(List<KeyPoints> keyPoints)
        {
            List<int> result = new List<int>();

            foreach (KeyPoints keyPoint in keyPoints)
            {
                result.Add(keyPoint.Id);
            }

            return result;
        }

        // Convert from string to list of strings
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
