using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;

namespace FiveStarTours.View.Guide
{
    /// <summary>
    /// Interaction logic for Reviews.xaml
    /// </summary>
    public partial class Reviews : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<TourRating> ReviewsCollection { get; set; }

        private readonly TourRatingService _tourRatingRepository;
        private readonly LiveTourService _liveTourRepository;
        private readonly ToursService _toursRepository;
        private readonly AttendanceService _attendanceRepository;
        private readonly KeyPointsService _keyPointsRepository;

        public string SelectedTour { get; set; }
        public TourRating SelectedReview { get; set; }
        private bool _isReported;
        public bool Reported
        {
            get { return Reported; }
            set
            {
                if (_isReported != value)
                {
                    _isReported = value;
                    OnPropertyChanged(nameof(Reported));
                }
            }
        }
        public User LoggedInUser { get; set; }
        public Reviews(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRatingRepository = new TourRatingService();
            _liveTourRepository = new LiveTourService();
            _toursRepository = new ToursService();
            _attendanceRepository = new AttendanceService();
            _keyPointsRepository = new KeyPointsService();
            ReviewsCollection = new ObservableCollection<TourRating>();

            List<string> endedTours = _liveTourRepository.GetEndedTours(_toursRepository.GetByUser(user));
            Tours.ItemsSource = endedTours;
            Tours.SelectedValuePath = ".";
            Tours.DisplayMemberPath = ".";
        }

        private void Tours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tours.SelectedItem != null)
            {
                SelectedTour = Tours.SelectedItem as string;
                int tour = _toursRepository.GetByName(SelectedTour);
                ReviewsCollection = new ObservableCollection<TourRating>(_tourRatingRepository.GetAllByTour(tour, _attendanceRepository.GetAll(), _keyPointsRepository.GetAll()));
                DataGridReviews.ItemsSource = ReviewsCollection;
            }
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReview == null)
            {
                MessageBox.Show("Choose review first.");
                return;
            }
            MessageBoxResult result = MessageBox.Show($"Do you want to report selected review?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SelectedReview.Reported = true;
                Reported = true;
                _tourRatingRepository.Replace(SelectedReview);
                int tour = _toursRepository.GetByName(SelectedTour);
                ReviewsCollection = new ObservableCollection<TourRating>(_tourRatingRepository.GetAllByTour(tour, _attendanceRepository.GetAll(), _keyPointsRepository.GetAll()));

            }
            else
            {
                return;
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
