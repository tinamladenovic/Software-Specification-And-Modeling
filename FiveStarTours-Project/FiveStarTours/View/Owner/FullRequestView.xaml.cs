using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using FiveStarTours.View.Traveler;
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

namespace FiveStarTours.View.Owner
{
    /// <summary>
    /// Interaction logic for FullRequestView.xaml
    /// </summary>
    public partial class FullRequestView : Window
    {
        User LoggedInUser { get; set; }

        private readonly ReservationChangeService _requestService;
        private readonly AccommodationReservationService _reservationService;
        public ReservationChange reservationChange { get; set; }
        public AccommodationReservation reservation { get; set; }



        public FullRequestView(ReservationChange selectedRequest, User user)
        {
            InitializeComponent();
            DataContext = this;

            LoggedInUser = user;

            _requestService = new ReservationChangeService();
            _reservationService = new AccommodationReservationService();


            reservationChange = selectedRequest;


            if (_requestService.IsBusy(reservationChange) == true)
            {
                reservationChange.IsBusy = true;
            }
            else
            {
                reservationChange.IsBusy = false;
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            reservationChange.AccommodationReservation.StartDate = reservationChange.NewStartDate;
            reservationChange.AccommodationReservation.EndDate = reservationChange.NewEndDate;
            _reservationService.Update(reservationChange.AccommodationReservation);

            reservationChange.Status = Model.Enums.ReservationChangeStatusType.Approved;
            _requestService.Update(reservationChange);

            ManageRequestsView manageRequestsView = new ManageRequestsView(LoggedInUser);
            manageRequestsView.Show();
            Close();
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            reservationChange.Status = Model.Enums.ReservationChangeStatusType.Rejected;
            _requestService.Update(reservationChange);

            ManageRequestsView manageRequestsView = new ManageRequestsView(LoggedInUser);
            manageRequestsView.Show();
            Close();
        }


        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
