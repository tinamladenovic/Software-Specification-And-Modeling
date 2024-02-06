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
    /// Interaction logic for TourView.xaml
    /// </summary>
    /// 
    public partial class TourView : Window
    {
        //public static ObservableCollection<Tour> Tours { get; set; }
        public User LoggedInUser { get; set; }

        public Tour SelectedTour { get; set; }
        private readonly ToursService _repository;
        public Tour tour { get; set; }

        public TourView(Tour selectedTour, User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            _repository = new ToursService();
            tour = new Tour();
            tour = _repository.GetById(selectedTour.Id);
            SelectedTour = selectedTour;
            //Tours = new ObservableCollection<Tour>(_repository.GetById(selectedTour.Id));
        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            ReservationView reservationView = new ReservationView(SelectedTour, LoggedInUser);
            reservationView.Show();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            ToursListingView toursListingView = new ToursListingView(LoggedInUser);
            //this.Visibility = Visibility.Hidden;
            toursListingView.Show();
        }
    }
}
