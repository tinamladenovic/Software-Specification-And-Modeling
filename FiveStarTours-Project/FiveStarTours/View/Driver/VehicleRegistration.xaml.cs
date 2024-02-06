using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;


namespace FiveStarTours.View.VehicleRegistration
{
    /// <summary>
    /// Interaction logic for VehicleRegistration.xaml
    /// </summary>
    public partial class VehicleRegistration : Window, INotifyPropertyChanged
    {

        private readonly VehicleRepository _vehicleRepository;
        private readonly LanguagesRepository _languagesRepository;
        private readonly LocationsRepository _locationsRepository;


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
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

        private string _maxPersonNum;
        public string MaxPersonNum
        {

            get => _maxPersonNum;
            set
            {
                if (value != _maxPersonNum)
                {
                    _maxPersonNum = value;
                    OnPropertyChanged();
                }
            }
        }

        
        private string selectedState;
        private string selectedCity;

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

        private string _selectedImagePath;

        public string SelectedImagePath
        {
            get { return _selectedImagePath; }
            set
            {
                _selectedImagePath = value;
                OnPropertyChanged("SelectedImagePath");
            }
        }

        private string _fastDriveNum;
        public string FastDriveNum
        {

            get => _fastDriveNum;
            set
            {
                if (value != _fastDriveNum)
                {
                    _fastDriveNum = value;
                    OnPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public VehicleRegistration()
        {
            InitializeComponent();
            DataContext = this;

            _vehicleRepository = new VehicleRepository();
            _languagesRepository = new LanguagesRepository();
            _locationsRepository = new LocationsRepository();

            // Adding state and city trough combobox:

            List<string> States = _locationsRepository.GetAllStates();
            stateComboBox.ItemsSource = States;
            stateComboBox.SelectedValuePath = ".";
            stateComboBox.DisplayMemberPath = ".";
        }

        // Adding state and city trough combobox:

        private void StateCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stateComboBox.SelectedItem != null)
            {

                string selectedStateComboBox = stateComboBox.SelectedItem.ToString();
                List<string> citiesInState = _locationsRepository.GetCitiesInState(selectedStateComboBox);
                cityComboBox.ItemsSource = citiesInState;
                selectedState = stateComboBox.SelectedItem as string;
            }
        }

        private void CityCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cityComboBox.SelectedItem != null)
            {
                selectedCity = cityComboBox.SelectedItem as string;
            }
        }
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Name
            

            //Location
            Location location = new Location();
            if (CheckLocation())
            {
                location = GetSelectedLocation();
            }
            else return;

            //Maximum Person Number
            int MaximumPersonNumber;
            if (MaxPersonNum == null)
            {
                MessageBox.Show("You must enter maximum guests number.");
                return;
            }
            else
            {
                MaximumPersonNumber = int.Parse(MaxPersonNum);
                if (MaximumPersonNumber < 1)
                {
                    MessageBox.Show("Maximum number of guests must be greater then 0.");
                    return;
                }
            }

            //Language
            List<Language> LanguageList = new List<Language>();
            if (CheckLanguages(Languages))
            {
                LanguageList = MakeLanguagesList(Languages);
            }
            else return;

            List<int> LanguageIds = GetLanguagesIds(LanguageList);

            //Img URLs
            List<string> ImageUrlsList = new List<string>();
            if (CheckUrls(ImageUrls))
            {
                ImageUrlsList = MakeUrlsList(ImageUrls);
            }
            else return;

            //Fast Drive Number
            string FastDriveNumber;
            if (FastDriveNum == null)
            {
                MessageBox.Show("You must enter number of fast drivings.");
                return;
            }
            else
            {
                FastDriveNumber = Convert.ToString(FastDriveNum);
                if (FastDriveNumber.Length < 1)
                {
                    MessageBox.Show("Maximum number of guests must be greater then 0.");
                    return;
                }
            }


            Vehicle newVehicle = new Vehicle( Name, location.Id, location, MaximumPersonNumber, LanguageList , LanguageIds, ImageUrlsList , FastDriveNumber);
            _vehicleRepository.Save(newVehicle);
            MessageBox.Show("Data saved successfully.");
            Close();
        }

        public bool CheckUrls(string urls)
        {
            if (urls == null)
            {
                MessageBox.Show("At least 1 URL is required.");
                return false;
            }

            return true;
        }
        public bool CheckLanguages(string languages)
        {
            if (languages == null)
            {
                MessageBox.Show("At least 1 language is required.");
                return false;
            }

            return true;
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

        private List<string> MakeUrlsList(string urls)
        {
            List<string> result = new List<string>();

            Array _urls = urls.Split(",");

            foreach (string url in _urls)
            {
                result.Add(url);
            }

            return result;
        }
        

        private List<int> GetLanguagesIds(List<Language> languages)
        {
            List<int> result = new List<int>();

            foreach (Language lang in languages)
            {
                result.Add(lang.Id);
            }

            return result;
        }

        private List<Language> MakeLanguagesList(string languages)
        {
            List<Language> result = new List<Language>();

            Array _languages = languages.Split(", ");

            foreach (string l in _languages)
            {
                Language lang = new Language();
                _languagesRepository.Save(lang);
                result.Add(lang);
            }

            return result;
        }

        private Location GetSelectedLocation()
        {
            return new Location();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }

}
