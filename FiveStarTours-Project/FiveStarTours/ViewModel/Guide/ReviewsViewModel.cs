using System.Collections.ObjectModel;
using System.Windows;
using FiveStarTours.Model;
using FiveStarTours.Services;

namespace FiveStarTours.ViewModel.Guide
{
    public class ReviewsViewModel : FiveStarTours.ViewModel.BindableBase.BindableBase
    {
        private readonly TourRatingService _tourRatingService;
        private readonly LiveTourService _liveTourService;
        private readonly ToursService _toursService;
        private readonly AttendanceService _attendanceService;
        private readonly KeyPointsService _keyPointsService;

        private ObservableCollection<TourRating> _reviewsCollection;
        public ObservableCollection<TourRating> ReviewsCollection
        {
            get { return _reviewsCollection; }
            set
            {
                SetProperty(ref _reviewsCollection, value);
            }
        }

        private ObservableCollection<Tour> _endedToursCollection;
        public ObservableCollection<Tour> EndedToursCollection
        {
            get { return _endedToursCollection; }
            set
            {
                SetProperty(ref _endedToursCollection, value);
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                SetProperty(ref _selectedTour, value);
                SelectTour();
            }
        }
        private TourRating _selectedReview;
        public TourRating SelectedReview
        {
            get { return _selectedReview; }
            set
            {
                SetProperty(ref _selectedReview, value);
            }
        }

        private bool _isReported;
        public bool Reported
        {
            get { return _isReported; }
            set
            {
                SetProperty(ref _isReported, value);
            }
        }
        public User LoggedInUser { get; set; }

        public ReviewsViewModel(User user)
        {
            LoggedInUser = user;
            _tourRatingService = new TourRatingService();
            _liveTourService = new LiveTourService();
            _toursService = new ToursService();
            _attendanceService = new AttendanceService();
            _keyPointsService = new KeyPointsService();

            ReviewsCollection = new ObservableCollection<TourRating>();
            EndedToursCollection = new ObservableCollection<Tour>(_liveTourService.GetEndedTours(_toursService.GetByUser(user)));
        }

        private void SelectTour()
        {
            ReviewsCollection = new ObservableCollection<TourRating>(_tourRatingService.GetAllByTour(SelectedTour.Id, _attendanceService.GetAll(), _keyPointsService.GetAll()));
        }
        private void Report()
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
                _tourRatingService.Replace(SelectedReview);
                ReviewsCollection = new ObservableCollection<TourRating>(_tourRatingService.GetAllByTour(SelectedTour.Id, _attendanceService.GetAll(), _keyPointsService.GetAll()));
            }
            else
            {
                return;
            }
        }
    }
}
