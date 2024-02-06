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
using System.Windows.Input;
using FiveStarTours.Model;
using FiveStarTours.Services;
using FiveStarTours.View;
using FiveStarTours.View.Guide;
using FiveStarTours.ViewModel.Command;

namespace FiveStarTours.ViewModel
{
    public class ToursViewModel : FiveStarTours.ViewModel.BindableBase.BindableBase
    {
        public ICommand NewTourCommand { get; }
        public ICommand StartTourCommand { get; }
        public ICommand CancelTourCommand { get; }
        public ICommand ShowStatisticCommand { get; }
        public ICommand ShowReviewsCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand ShowRequestsCommand { get; }

        private readonly ToursService _toursService;
        private readonly TourReservationService _tourReservationService;
        private readonly GiftCardService _giftCardService;
        private readonly UserService _userService;
        public Tour SelectedTour { get; set; }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);
                OnDateSelected();
            }
        }
        public User LoggedInUser { get; set; }

        private ObservableCollection<Tour> _toursCollection;
        public ObservableCollection<Tour> ToursCollection
        {
            get { return _toursCollection; }
            set
            {
                SetProperty(ref _toursCollection, value);
            }
        }

        public ToursViewModel(User user)
        {
            _toursService = new ToursService();
            _tourReservationService = new TourReservationService();
            _giftCardService = new GiftCardService();
            _userService = new UserService();

            NewTourCommand = new RelayCommand(NewTour);
            StartTourCommand = new RelayCommand(StartTour);
            CancelTourCommand = new RelayCommand(CancelTour);
            ShowStatisticCommand = new RelayCommand(ShowStatistic);
            ShowReviewsCommand = new RelayCommand(ShowReviews);
            CloseCommand = new RelayCommand(Close);
            ShowRequestsCommand = new RelayCommand(ShowRequests);

            LoggedInUser = user;
            SelectedDate = DateTime.Today;

            ToursCollection = new ObservableCollection<Tour>(_toursService.GetByUser(user));
        }
        private void OnDateSelected()
        {
            ToursCollection = new ObservableCollection<Tour>(_toursService.GetAllByDate(SelectedDate, LoggedInUser));
        }

        private void NewTour()
        {
            TourRegistrationForm tourRegistration = new TourRegistrationForm(LoggedInUser);
            tourRegistration.Show();
        }

        private void StartTour()
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Choose tour first!");
            }
            else if (SelectedTour.OneBeginningTime.Date != DateTime.Now.Date)
            {
                MessageBox.Show("This tour can not be started because it is not today.");
            }
            else
            {
                foreach (Tour t in _toursService.GetAll())
                {
                    if (SelectedTour.Name == t.Name)
                    {
                        SelectedTour.Id = t.Id;
                    }
                }

                LiveTourTracking liveTourTracking = new LiveTourTracking(SelectedTour);
            }
        }

        private void CancelTour()
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Choose tour first!");
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Do you want to cancel tour?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DateTime fixedDateTime = SelectedTour.OneBeginningTime;
                TimeSpan timeDifference = fixedDateTime - DateTime.Now;

                if (timeDifference.TotalHours < 48)
                {
                    MessageBox.Show("It is less than 48 hours from this tour.");
                    return;
                }
                else
                {
                    List<int> Visitors = new List<int>();

                    foreach (Tour t in _toursService.GetAll())
                    {
                        if (SelectedTour.Name == t.Name)
                        {
                            SelectedTour.Id = t.Id;
                        }
                    }

                    foreach (string visitor in _tourReservationService.GetAllVisitors(SelectedTour))
                    {
                        int Visitor = _userService.FindIdByName(visitor);
                        Visitors.Add(Visitor);
                    }

                    foreach (int id in Visitors)
                    {
                        GiftCard giftCard = new GiftCard(id, DateTime.Today.AddYears(1));
                        _giftCardService.Save(giftCard);
                    }

                    _toursService.DeleteByDate(SelectedTour);
                    _tourReservationService.DeleteById(SelectedTour);

                }
            }
            else
            {
                return;
            }
        }

        private void ShowStatistic()
        {
            Statistics statistic = new Statistics(LoggedInUser);
            statistic.Show();
        }

        private void ShowReviews()
        {
            Reviews reviews = new Reviews(LoggedInUser);
            reviews.Show();
        }

        private void Close()
        {
            MainWindow mainWindow = new MainWindow();
            foreach (Window window in Application.Current.Windows)
            {
                if (window != mainWindow)
                {
                    window.Close();
                }
            }

            mainWindow.Show();
        }

        private void ShowRequests()
        {
            RequestsView requestsView = new RequestsView(LoggedInUser);
            requestsView.Show();
        }
    }
}
