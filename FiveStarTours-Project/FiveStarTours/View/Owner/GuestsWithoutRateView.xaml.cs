using FiveStarTours.Model;
using FiveStarTours.Repository;
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

namespace FiveStarTours.View.Owner
{
    /// <summary>
    /// Interaction logic for GuestsWIthoutRateView.xaml
    /// </summary>
    public partial class GuestsWithoutRateView : Window
    {
        public User LoggedInUser { get; set; }
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }   //SELEKTOVANA
        private readonly AccommodationReservationService _reservationService;

        public GuestsWithoutRateView(User user)
        {
            InitializeComponent();
            DataContext = this;

            _reservationService = new AccommodationReservationService();

            LoggedInUser = user;

            Reservations = new ObservableCollection<AccommodationReservation>(_reservationService.GetUnratedAndLessThanFiveDaysAgo());

        }

        private void RateGuestsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                GuestRatingView guestRatingView = new GuestRatingView(SelectedReservation, LoggedInUser);
                guestRatingView.Show();
                Close();
                
            }
            else
            {
                MessageBox.Show("You must select the guest.");
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
