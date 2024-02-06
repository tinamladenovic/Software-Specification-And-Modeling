using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using FiveStarTours.View.Guide;

namespace FiveStarTours.View
{
    /// <summary>
    /// Interaction logic for Tours.xaml
    /// </summary>
    public partial class Tours : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ToursService _toursRepository;
        private readonly TourReservationService _tourReservationRepository;
        private readonly GiftCardService _giftCardRepository;
        private readonly UserService _userRepository;
        public Tour SelectedTour { get; set; }
        public User LoggedInUser { get; set; }
        public ObservableCollection<Tour> ToursCollection { get; set; }
        public Tours(User user)
        {
            InitializeComponent();
            _toursRepository = new ToursService();
            _tourReservationRepository = new TourReservationService();
            _giftCardRepository = new GiftCardService();
            _userRepository = new UserService();
            DataContext = this;
            LoggedInUser = user;
            ToursCollection = new ObservableCollection<Tour>(_toursRepository.GetByUser(user));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            TourRegistrationForm tourRegistration = new TourRegistrationForm(LoggedInUser);
            tourRegistration.Show();
        }

        private void ToursDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ToursCollection = new ObservableCollection<Tour>(_toursRepository.GetAllByDate((DateTime)ToursDate.SelectedDate, LoggedInUser));
            DataGridTours.ItemsSource = ToursCollection;
        }

        private void StartTourButton_CLick(object sender, RoutedEventArgs e)
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
                foreach(Tour t in _toursRepository.GetAll())
                {
                    if(SelectedTour.Name == t.Name)
                    {
                        SelectedTour.Id = t.Id;
                    }
                }

                LiveTourTracking liveTourTracking = new LiveTourTracking(SelectedTour);
            }
        }

        private void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Choose tour first!");
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Do you want to cancel tour?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if(result == MessageBoxResult.Yes)
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

                    foreach (Tour t in _toursRepository.GetAll())
                    {
                        if (SelectedTour.Name == t.Name)
                        {
                            SelectedTour.Id = t.Id;
                        }
                    }

                    foreach (string visitor in _tourReservationRepository.GetAllVisitors(SelectedTour))
                    {
                        int Visitor = _userRepository.FindIdByName(visitor);
                        Visitors.Add(Visitor);
                    }

                    foreach (int id in Visitors)
                    {
                        GiftCard giftCard = new GiftCard(id, DateTime.Today.AddYears(1));
                        _giftCardRepository.Save(giftCard);
                    }

                    _toursRepository.DeleteByDate(SelectedTour);
                    _tourReservationRepository.DeleteById(SelectedTour);

                }
            }
            else
            {
                return;
            }
        }

        private void Statistic_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistic = new Statistics(LoggedInUser);
            statistic.Show();
        }

        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            Reviews reviews = new Reviews(LoggedInUser);
            reviews.Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            foreach (Window window in Application.Current.Windows)
            {
                if(window != mainWindow)
                {
                    window.Close();
                }      
            }
            
            mainWindow.Show();
        }
    }
}
