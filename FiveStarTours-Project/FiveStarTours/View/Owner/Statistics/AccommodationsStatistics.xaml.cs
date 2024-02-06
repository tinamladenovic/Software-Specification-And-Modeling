using FiveStarTours.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FiveStarTours.View.Traveler;

namespace FiveStarTours.View.Owner.Statistics
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class AccommodationsStatistics : Window
    {
        public User LoggedInUser { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }

        public Accommodation SelectedAccommodation { get; set; } 
        private readonly AccommodationsService _accommodationService;

        public AccommodationsStatistics(User user)
        {
            InitializeComponent();
            DataContext = this;

            _accommodationService = new AccommodationsService();
            LoggedInUser = user;

            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAll());
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            YearView yearView = new YearView(LoggedInUser, SelectedAccommodation);
            yearView.Show();
            Close();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
            Close();
        }
    }
}
