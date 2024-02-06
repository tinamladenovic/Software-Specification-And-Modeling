using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FiveStarTours.Model;
using FiveStarTours.Services;
using FiveStarTours.ViewModel.Command;

namespace FiveStarTours.ViewModel.Guide
{
    public class RequestsStatisticVIewModel : FiveStarTours.ViewModel.BindableBase.BindableBase
    {
        private readonly LocationsService _locationsService;
        private readonly TourRequestService _tourRequestService;

        public ICommand SearchCommand { get; }

        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        public User LoggedInUser { get; set; }
        public List<int> Years { get; } = Enumerable.Range(2019, DateTime.Now.Year - 2019 + 1).ToList();

        private int _selectedYear;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                SetProperty(ref _selectedYear, value);
                SearchByMoths();
            }
        }

        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    SetProperty(ref _language, value);
                }
            }
        }

        private string _year2019;
        public string Year2019
        {
            get => _year2019;
            set
            {
                SetProperty(ref _year2019, value);
            }
        }

        private string _year2020;
        public string Year2020
        {
            get => _year2020;
            set
            {
                SetProperty(ref _year2020, value);
            }
        }

        private string _year2021;
        public string Year2021
        {
            get => _year2021;
            set
            {
                SetProperty(ref _year2021, value);
            }
        }

        private string _year2022;
        public string Year2022
        {
            get => _year2022;
            set
            {
                SetProperty(ref _year2022, value);
            }
        }

        private string _year2023;
        public string Year2023
        {
            get => _year2023;
            set
            {
                SetProperty(ref _year2023, value);
            }
        }

        private string _jaunary;
        public string January
        {
            get => _jaunary;
            set
            {
                SetProperty(ref _jaunary, value);
            }
        }

        private string _february;
        public string February
        {
            get => _february;
            set
            {
                SetProperty(ref _february, value);
            }
        }

        private string _march;
        public string March
        {
            get => _march;
            set
            {
                SetProperty(ref _march, value);
            }
        }

        private string _april;
        public string April
        {
            get => _april;
            set
            {
                SetProperty(ref _april, value);
            }
        }

        private string _may;
        public string May
        {
            get => _may;
            set
            {
                SetProperty(ref _may, value);
            }
        }

        private string _jun;
        public string Jun
        {
            get => _jun;
            set
            {
                SetProperty(ref _jun, value);
            }
        }

        private string _july;
        public string July
        {
            get => _july;
            set
            {
                SetProperty(ref _july, value);
            }
        }

        private string _august;
        public string August
        {
            get => _august;
            set
            {
                SetProperty(ref _august, value);
            }
        }

        private string _september;
        public string September
        {
            get => _september;
            set
            {
                SetProperty(ref _september, value);
            }
        }

        private string _october;
        public string October
        {
            get => _october;
            set
            {
                SetProperty(ref _october, value);
            }
        }

        private string _november;
        public string November
        {
            get => _november;
            set
            {
                SetProperty(ref _november, value);
            }
        }

        private string _december;
        public string December
        {
            get => _december;
            set
            {
                SetProperty(ref _december, value);
            }
        }


        private string _selectedState;
        public string SelectedState
        {
            get => _selectedState;
            set
            {
                if (_selectedCity == " ")
                {
                    value = null;
                    _selectedState = value;
                    OnPropertyChanged(nameof(SelectedState));
                }
                else
                {
                    _selectedState = value;
                    SelectedStateChanged();
                    OnPropertyChanged(nameof(SelectedState));
                }
            }
        }

        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if(_selectedCity == " ")
                {
                    value = null;
                }
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }

        private void SelectedStateChanged()
        {
            List<string> cities = _locationsService.GetCitiesInState(SelectedState);
            Cities = new ObservableCollection<string>(cities);
            OnPropertyChanged(nameof(Cities));
        }

        private Location GetSelectedLocation()
        {
            Location result = new Location();
            foreach (var location in _locationsService.GetAll())
            {
                if (location.City == SelectedCity)
                {
                    return result = location;
                }
            }
            return result;
        }
        public RequestsStatisticVIewModel(User user)
        {
            _locationsService = new LocationsService();
            _tourRequestService = new TourRequestService();

            LoggedInUser = user;

            SearchCommand = new RelayCommand(SearchYears);

            States = new ObservableCollection<string>(_locationsService.GetAllStates());
            Cities = new ObservableCollection<string>();
            States.Add("");
            Cities.Add("");
        }

        private void SearchYears()
        {
            Location location = GetSelectedLocation();
            Year2019 = _tourRequestService.GetByYear("2019", location, Language);
            Year2020 = _tourRequestService.GetByYear("2020", location, Language);
            Year2021 = _tourRequestService.GetByYear("2021", location, Language);
            Year2022 = _tourRequestService.GetByYear("2022", location, Language);
            Year2023 = _tourRequestService.GetByYear("2023", location, Language);
        }

        private void SearchByMoths()
        {
            Location location = GetSelectedLocation();
            January = _tourRequestService.GetByMonth("01", SelectedYear, location, Language);
            February = _tourRequestService.GetByMonth("02", SelectedYear, location, Language);
            March = _tourRequestService.GetByMonth("03", SelectedYear, location, Language);
            April = _tourRequestService.GetByMonth("04", SelectedYear, location, Language);
            May = _tourRequestService.GetByMonth("05", SelectedYear, location, Language);
            Jun = _tourRequestService.GetByMonth("06", SelectedYear, location, Language);
            July = _tourRequestService.GetByMonth("07", SelectedYear, location, Language);
            August = _tourRequestService.GetByMonth("08", SelectedYear, location, Language);
            September = _tourRequestService.GetByMonth("09", SelectedYear, location, Language);
            October = _tourRequestService.GetByMonth("10", SelectedYear, location, Language);
            November = _tourRequestService.GetByMonth("11", SelectedYear, location, Language);
            December = _tourRequestService.GetByMonth("12", SelectedYear, location, Language);
        }
    }
}
