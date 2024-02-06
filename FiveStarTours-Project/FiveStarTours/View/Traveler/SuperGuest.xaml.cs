using FiveStarTours.Model;
using FiveStarTours.Services;
using System;
using System.Collections.Generic;
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

namespace FiveStarTours.View.Traveler
{
    /// <summary>
    /// Interaction logic for SuperGuest.xaml
    /// </summary>
    public partial class SuperGuest : Window
    {
        private readonly AccommodationReservationService _reservations;
        private readonly UserService _userService;
        public AccommodationReservation accommodationReservation { get; set; }
        public User LoggedUser { get; set; }
        public int NumberOfReservations { get; set; }
        public int NumberOfPoints { get; set; }
        public bool IsSuperGuest { get; set; }

        public SuperGuest()
        {
            User user = MainWindow.LoggedUser;
            InitializeComponent();
            DataContext = this;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _userService = new UserService();
            _reservations = new AccommodationReservationService();
            LoggedUser = user;
            accommodationReservation = new AccommodationReservation();
            NumberOfReservations = _reservations.CountReservations();
            NumberOfPoints = _reservations.UpdateBonusPoints(_reservations.CountReservations(), 0);

            _userService.CheckIfSuperGuest(user);
            if (IsSuperGuest = user.Super)
            {
                CheckBoxSuper.IsChecked = true;
                IsSuperGuest = true;
            }
            else
            {
                CheckBoxSuper.IsChecked = false;
                IsSuperGuest = false;
            }

            



        }

        private void back(object sender, RoutedEventArgs e)
        {
            GuestReviews guestReviews = new GuestReviews(accommodationReservation);
            guestReviews.Show();
            Close();
        }
    }
}
