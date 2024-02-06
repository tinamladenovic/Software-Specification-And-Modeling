using FiveStarTours.Model;
using FiveStarTours.Services;
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

namespace FiveStarTours.View.Traveler
{
    /// <summary>
    /// Interaction logic for GuestReviews.xaml
    /// </summary>
    public partial class GuestReviews : Window, INotifyPropertyChanged
    {
         public User LoggedInUser { get; set; }
         public AccommodationReservation _selectedReservation { get; set; }
        public GuestRating SelectedRatings { get; set; }
        public ObservableCollection<GuestRating> Ratings { get; set; }
        private GuestRaitingsService _rateService;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }
        public GuestRating guestRating { get; set; }
        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }
        public GuestReviews(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            DataContext = this;

            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _rateService = new GuestRaitingsService();

            guestRating = new GuestRating();
            Ratings = new ObservableCollection<GuestRating>(_rateService.GetAll());
            _selectedReservation = selectedReservation;
        }

        private void back(object sender, RoutedEventArgs e)
        {
            MainWindowLida mw=new MainWindowLida();
            mw.Show();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                window.Close();
            }
        }

        private void Request(object sender, RoutedEventArgs e)
        {
            RequestView requestView = new RequestView();
            requestView.Show();
            Close();
        }

        private void Super(object sender, RoutedEventArgs e)
        {
            SuperGuest superGuest= new SuperGuest();
            superGuest.Show();
            Close();
        }
    }
}
