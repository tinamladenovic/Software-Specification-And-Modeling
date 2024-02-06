using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace FiveStarTours.View.Owner
{
    /// <summary>
    /// Interaction logic for GuestReviewsView.xaml
    /// </summary>
    public partial class GuestReviewsView : Window
    {
        public User LoggedInUser { get; set; }
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }   //SELEKTOVANA
        private readonly AccommodationReservationService _service;

        public GuestReviewsView(User user)
        {
            InitializeComponent();
            DataContext = this;

            _service = new AccommodationReservationService();

            LoggedInUser = user;

            
            Reservations = new ObservableCollection<AccommodationReservation>(_service.GetRatesForOwner());

        }

        private void ViewReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                FullGuestReviewView fullReview = new FullGuestReviewView(SelectedReservation, LoggedInUser);
                fullReview.Show();
                Close();
            }
            else
            {
                MessageBox.Show("You must select the review.");
            }


        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
            Close();
        }
    }
}
