using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;

namespace FiveStarTours.View.Guide
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window, INotifyPropertyChanged
    {
        private readonly LiveTourService _liveTourRepository;
        private readonly AttendanceService _attendanceRepository;
        private readonly UserService _userRepository;
        private readonly TourReservationService _tourReservationRepository;
        private readonly ToursService _toursRepository;

        public User LoggedInUser { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public List<int> Years { get; } = Enumerable.Range(2000, DateTime.Now.Year - 2000 + 1).ToList();
        public DateTime SelectedYear { get; set; }
        public Statistics(User user)
        {
            InitializeComponent();
            _liveTourRepository = new LiveTourService();
            _attendanceRepository = new AttendanceService();
            _userRepository = new UserService();
            _tourReservationRepository = new TourReservationService();
            _toursRepository = new ToursService();
            DataContext = this;
            LoggedInUser = user;
            MostVisited.Text = GetMostVisitedAllTime();

            List<string> endedTours = _liveTourRepository.GetEndedTours(_toursRepository.GetByUser(user));
            EndedTours.ItemsSource = endedTours;
            EndedTours.SelectedValuePath = ".";
            EndedTours.DisplayMemberPath = ".";
        }

        private string selectedTour;
        public string selectedDate;

        private void EndedToursComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndedTours.SelectedItem != null)
            {

                string selectedTourComboBox = EndedTours.SelectedItem.ToString();
                List<string> dates = _liveTourRepository.GetDates(selectedTourComboBox);
                DateOfTour.ItemsSource = dates;
                selectedTour = EndedTours.SelectedItem as string;
            }
        }

        private void DateOfTour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateOfTour.SelectedItem != null)
            {
                selectedDate = Convert.ToString(DateOfTour.SelectedItem);
            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            LowerThan18.Text = GetLower();
            Between18and50.Text = GetBetween();
            GreaterThan50.Text = GetAbove();
            WithGitfCards.Text = GetWithGiftCard() + "%";
            WithoutGitfCards.Text = GetWithoutGiftCard() + "%";
        }
        public string GetLower()
        {
            LiveTour tour = _liveTourRepository.GetByNameAndDate(selectedTour, Convert.ToDateTime(selectedDate));
            int numberLower = _attendanceRepository.GetAllLower(tour.Id, _userRepository.GetAll());
            string Lower = Convert.ToString(numberLower / _attendanceRepository.GetAllById(tour.Id, _userRepository.GetAll()) * 100) + "%";
            return Lower;
        }

        public string GetBetween()
        {
            LiveTour tour = _liveTourRepository.GetByNameAndDate(selectedTour, Convert.ToDateTime(selectedDate));
            int numberBetween = _attendanceRepository.GetAllBetween(tour.Id, _userRepository.GetAll());
            string Between = Convert.ToString(numberBetween / _attendanceRepository.GetAllById(tour.Id, _userRepository.GetAll()) * 100) + "%";
            return Between;
        }
        public string GetAbove()
        {
            LiveTour tour = _liveTourRepository.GetByNameAndDate(selectedTour, Convert.ToDateTime(selectedDate));
            int numberAbove = _attendanceRepository.GetAllAbove(tour.Id, _userRepository.GetAll());
            string Above = Convert.ToString(numberAbove / _attendanceRepository.GetAllById(tour.Id, _userRepository.GetAll()) * 100) + "%";
            return Above;
        }

        public string GetWithGiftCard()
        {
            LiveTour tour = _liveTourRepository.GetByNameAndDate(selectedTour, Convert.ToDateTime(selectedDate));
            int number = _tourReservationRepository.GetWithGiftCard(tour, _attendanceRepository.GetAll(), _userRepository.GetAll());
            string WithGiftCard = Convert.ToString(number / _attendanceRepository.GetAllById(tour.Id, _userRepository.GetAll()) * 100) ;
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
            if(id == 0)
            {
                return "There's no visited tours.";
            }
            else
            {
                result = _toursRepository.GetById(id).Name;
                return result;
            }
        }
        private void Year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Year.SelectedItem != null)
            {
                int selected = int.Parse(Year.SelectedItem.ToString());
                SelectedYear = new DateTime(selected, 1, 1);
            }
        }
        private void SearchByYear_Click(object sender, RoutedEventArgs e)
        {
            string result = _attendanceRepository.GetMostVisitedByYear(SelectedYear, _toursRepository.GetAll());
            if(result == null)
            {
                MostVisited.Text = "There's no tour in this year.";
            }
            else
            {
                MostVisited.Text = result;
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
    }
}
