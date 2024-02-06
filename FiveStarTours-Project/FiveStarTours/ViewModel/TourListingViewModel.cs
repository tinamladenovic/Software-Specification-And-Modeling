using FiveStarTours.Model;
using FiveStarTours.Services;
using FiveStarTours.View.Visitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using FiveStarTours.ViewModel.Command;

namespace FiveStarTours.ViewModel
{
    public class TourListingViewModel : INotifyPropertyChanged
    {
        public event EventHandler RequestClose;
        public ICommand LogOutCommand { get; }
        public ICommand NotificationsCommand { get; }

        public User LoggedInUser { get; set; }
        public static ObservableCollection<Tour> Tours { get; set; }
        public static ObservableCollection<string> Languages { get; set; }

        public Tour SelectedTour { get; set; }
        private readonly ToursService _repository;
        public bool notificationReceived = false;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public TourListingViewModel(User user)
        {
            
            LoggedInUser = user;
            _repository = new ToursService();
            Tours = new ObservableCollection<Tour>(_repository.GetAll());
            Languages = new ObservableCollection<string>();

            //LogOutCommand = new RelayCommand(LogOut);

        }



        private void ShowTourView(object sender, RoutedEventArgs e)
        {

            if (SelectedTour != null)
            {
                TourView tourView = new TourView(SelectedTour, LoggedInUser);
                tourView.Show();

            }
        }

        private void RateClick(object sender, RoutedEventArgs e)
        {
            TourRatingView rate = new TourRatingView(LoggedInUser);
            rate.Show();
        }

        private void VehicleClick(object sender, RoutedEventArgs e)
        {
            VehicleSearchView vehicle = new VehicleSearchView(LoggedInUser);
            vehicle.Show();
        }

        private void LogOut(object sender, RoutedEventArgs e)
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

        private string selectedLanguage;
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void BackgroundComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NotificationsButtonClick(object sender, RoutedEventArgs e)
        {
            notificationReceived = Notification.SentNotification;

            // Check if there are any notifications
            if (!notificationReceived || Notification.User.Id != LoggedInUser.Id)
            {
                MessageBox.Show("There are no notifications.", "No Notifications", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                NotificationsView notificationWindow = new NotificationsView(LoggedInUser);
                notificationWindow.ShowDialog();

                if (notificationWindow.UserResponse == "yes")
                {
                    Notification.Answer = true;

                }
                else if (notificationWindow.UserResponse == "no")
                {
                    Notification.Answer = false;
                }

                Notification.SentNotification = false;
            }
        }
    }

}
