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

namespace FiveStarTours.View.Visitor
{
    /// <summary>
    /// Interaction logic for ReservedToursView.xaml
    /// </summary>
    public partial class ReservedToursView : Window
    {
        public User LoggedInUser { get; set; }
        public static ObservableCollection<TourReservation> ReservedTours { get; set; }
        public Tour SelectedReservedTour { get; set; }


        private readonly TourReservationService _repository;
        public Tour tour { get; set; }
        public ReservedToursView(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            _repository = new TourReservationService();
            ReservedTours = new ObservableCollection<TourReservation>(_repository.GetAll());

        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            
                TourRatingView tourRatingView = new TourRatingView(LoggedInUser);
                tourRatingView.Show();

            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ToursListingView tourListing = new ToursListingView(LoggedInUser);
            tourListing.Show();
        }
    }
}
