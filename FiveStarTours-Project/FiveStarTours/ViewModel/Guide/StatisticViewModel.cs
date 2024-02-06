using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FiveStarTours.Model;
using FiveStarTours.Services;
using FiveStarTours.ViewModel.Command;

namespace FiveStarTours.ViewModel.Guide
{
    public class StatisticViewModel : FiveStarTours.ViewModel.BindableBase.BindableBase
    {
        private readonly LiveTourService _liveTourRepository;
        private readonly AttendanceService _attendanceRepository;
        private readonly UserService _userRepository;
        private readonly TourReservationService _tourReservationRepository;
        private readonly ToursService _toursRepository;

        public ICommand SearchCommand { get; }
        public ICommand SearchByYearCommand { get; }

        public User LoggedInUser { get; set; }
        public List<int> Years { get; } = Enumerable.Range(2000, DateTime.Now.Year - 2000 + 1).ToList();

        private int _selectedYear;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                SetProperty(ref _selectedYear, value);
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                SetProperty(ref _selectedTour, value);
            }
        }

        private string _selectedDate;
        public string SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        private string _lowerThan18;
        public string LowerThan18
        {
            get { return _lowerThan18; }
            set
            {
                SetProperty(ref _lowerThan18, value);
            }
        }

        private string _between18and50;
        public string Between18and50
        {
            get { return _between18and50; }
            set
            {
                SetProperty(ref _between18and50, value);
            }
        }

        private string _greaterThan50;
        public string GreaterThan50
        {
            get { return _greaterThan50; }
            set
            {
                SetProperty(ref _greaterThan50, value);
            }
        }

        private string _withGitfCards;
        public string WithGitfCards
        {
            get { return _withGitfCards; }
            set
            {
                SetProperty(ref _withGitfCards, value);
            }
        }

        private string _withoutGitfCards;
        public string WithoutGitfCards
        {
            get { return _withoutGitfCards; }
            set
            {
                SetProperty(ref _withoutGitfCards, value);
            }
        }

        private ObservableCollection<Tour> _endedTours;
        public ObservableCollection<Tour> EndedTours
        {
            get { return _endedTours; }
            set
            {
                SetProperty(ref _endedTours, value);
                SelectedTourChange();
            }
        }

        private ObservableCollection<string> _dateTimes;
        public ObservableCollection<string> DateTimes
        {
            get { return _dateTimes; }
            set
            {
                SetProperty(ref _dateTimes, value);
            }
        }

        public string MostVisitedTour { get; set; }
        public StatisticViewModel(User user)
        {
            _liveTourRepository = new LiveTourService();
            _attendanceRepository = new AttendanceService();
            _userRepository = new UserService();
            _tourReservationRepository = new TourReservationService();
            _toursRepository = new ToursService();

            SearchByYearCommand = new RelayCommand(SearchByYear);
            SearchCommand = new RelayCommand(Search);

            LoggedInUser = user;

            MostVisitedTour = GetMostVisitedAllTime();

            EndedTours = new ObservableCollection<Tour>(_liveTourRepository.GetEndedTours(_toursRepository.GetByUser(user)));
        }

        private void SelectedTourChange()
         {
             if (SelectedTour != null)
             {
                 DateTimes = new ObservableCollection<string>( _liveTourRepository.GetDates(SelectedTour.Name));
             }
         }
         private void Search()
         {
            if(SelectedTour == null || SelectedDate == null)
            {
                MessageBox.Show("Choose tour and date.");
                return;
            }
             LowerThan18 = GetLower();
             Between18and50 = GetBetween();
             GreaterThan50 = GetAbove();
             WithGitfCards = GetWithGiftCard() + "%";
             WithoutGitfCards = GetWithoutGiftCard() + "%";
         }
         public string GetLower()
         {
             LiveTour tour = _liveTourRepository.GetByNameAndDate(SelectedTour.Name, Convert.ToDateTime(SelectedDate));
             int numberLower = _attendanceRepository.GetAllLower(tour.Id, _userRepository.GetAll());
             string Lower = Convert.ToString(numberLower / _attendanceRepository.GetAllById(tour.Id, _userRepository.GetAll()) * 100) + "%";
             return Lower;
         }

         public string GetBetween()
         {
             LiveTour tour = _liveTourRepository.GetByNameAndDate(SelectedTour.Name, Convert.ToDateTime(SelectedDate));
             int numberBetween = _attendanceRepository.GetAllBetween(tour.Id, _userRepository.GetAll());
             string Between = Convert.ToString(numberBetween / _attendanceRepository.GetAllById(tour.Id, _userRepository.GetAll()) * 100) + "%";
             return Between;
         }
         public string GetAbove()
         {
             LiveTour tour = _liveTourRepository.GetByNameAndDate(SelectedTour.Name, Convert.ToDateTime(SelectedDate));
             int numberAbove = _attendanceRepository.GetAllAbove(tour.Id, _userRepository.GetAll());
             string Above = Convert.ToString(numberAbove / _attendanceRepository.GetAllById(tour.Id, _userRepository.GetAll()) * 100) + "%";
             return Above;
         }

         public string GetWithGiftCard()
         {
             LiveTour tour = _liveTourRepository.GetByNameAndDate(SelectedTour.Name, Convert.ToDateTime(SelectedDate));
             int number = _tourReservationRepository.GetWithGiftCard(tour, _attendanceRepository.GetAll(), _userRepository.GetAll());
             string WithGiftCard = Convert.ToString(number / _attendanceRepository.GetAllById(tour.Id, _userRepository.GetAll()) * 100);
             return WithGiftCard;
         }

         public string GetWithoutGiftCard()
         {
             int number = 100 - Convert.ToInt32(GetWithGiftCard());
             return Convert.ToString(number);
         }
        
        public string GetMostVisitedAllTime()
        {
            int id;
            string result;
            id = _attendanceRepository.GetMostVisitedTour(_toursRepository.GetByUser(LoggedInUser));
            if (id == 0)
            {
                return "There's no visited tours.";
            }
            else
            {
                result = _toursRepository.GetById(id).Name;
                return result;
            }
        }
        
        private void SearchByYear()
        {
            DateTime dateTime = new DateTime(SelectedYear, 1, 1);
            string result = _attendanceRepository.GetMostVisitedByYear(dateTime, _toursRepository.GetAll());
            if (result == null)
            {
                MostVisitedTour = "There's no tour in this year.";
            }
            else
            {
                MostVisitedTour = result;
            }
        }
    }
}
