using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using FiveStarTours.View;
using FiveStarTours.ViewModel;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using Application = System.Windows.Application;
using Image = System.Windows.Controls.Image;
using User = FiveStarTours.Model.User;

namespace FiveStarTours.View.Visitor
{
    /// <summary>
    /// Interaction logic for ToursListingView.xaml
    /// </summary>
    /// 

    public partial class ToursListingView : Window, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }
        public static ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        private readonly ToursService _repository;
        private readonly LanguagesService _languagesService;
        public bool notificationReceived = false;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        List<Language> Languages = new List<Language>();

        private List<ImageSource> _imageSources;
        public List<ImageSource> ImageSources
        {
            get => _imageSources;
            set
            {
                _imageSources = value;
                OnPropertyChanged(nameof(List<ImageSource>));
            }
        }
        public ToursListingView(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            _repository = new ToursService();
            Tours = new ObservableCollection<Tour>(_repository.GetAll());

            //tourListBox.Loaded += TourListBox_Loaded;
            SetImages();

        }
        private void TourListBox_Loaded(object sender, RoutedEventArgs e)
        {
            SetImages();
        }
        private void SetImages()
        {
            //tourListBox.UpdateLayout(); // Ensure all ListBoxItems are generated

            foreach (Tour tour in Tours)
            {
                try
                {
                    string imageUrl = tour.ImageUrls.FirstOrDefault();

                    if (imageUrl != null)
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imageUrl, UriKind.Absolute);
                        bitmap.EndInit();
                        tour.FrontImage = bitmap;
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception by setting the Source to a default image
                    BitmapImage defaultImage = new BitmapImage(new Uri("/Resources/Images/unavailable-image.jpg", UriKind.Relative));
                    tour.FrontImage = defaultImage;
                }
            }
        }



        private void ShowTourButton_Click(object sender, RoutedEventArgs e)
        {
            TourRequestView tourRequest = new TourRequestView(LoggedInUser);
            tourRequest.Show();

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

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
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



        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            notificationReceived = Notification.SentNotification;

            // Check if there are any notifications
            if (!notificationReceived || Notification.User.Id != LoggedInUser.Id)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("There are no notifications.", "No Notifications", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void GiftCardsButton_Click(object sender, RoutedEventArgs e)
        {
            GiftCardsListingView giftCards = new GiftCardsListingView(LoggedInUser);
            giftCards.Show();

        }

        private void ReservedToursButton_Click(object sender, RoutedEventArgs e)
        {
            ReservedToursView reservedToursView = new ReservedToursView(LoggedInUser);
            reservedToursView.Show();
        }

        private void BackgroundComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;

            if (button == null) return;

            int tourId = (int)button.Tag; // Assuming TourId is an integer

            // Perform the action you want to take for the selected tour
            // For example, you can open a new window with detailed information about the tour
            Tour selectedTour = Tours.FirstOrDefault(t => t.Id == tourId);
            if (selectedTour != null)
            {
                TourView tourView = new TourView(selectedTour, LoggedInUser);
                tourView.Show();
            }
        }

        private void GroupRideButton_Click(object sender, RoutedEventArgs e)
        {
            RideRequestView rideRequest = new RideRequestView(LoggedInUser);
            rideRequest.Show();
        }

        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            GroupRides groupRides = new GroupRides(LoggedInUser);
            groupRides.Show();
        }
    }
}