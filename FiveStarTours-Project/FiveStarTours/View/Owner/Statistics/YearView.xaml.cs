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

namespace FiveStarTours.View.Owner.Statistics
{
    /// <summary>
    /// Interaction logic for StatisticsYearView.xaml
    /// </summary>
    public partial class YearView : Window
    {
        public User LoggedInUser { get; set; }

        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }

        public Accommodation SelectedAccommodation { get; set; }   //SELEKTOVANA

        private readonly AccommodationReservationService _service;

        public int NumberReservations { get; set; }
        public int NumberCancellations { get; set; }
        public int NumberSuggestions { get; set; }
        public string AccommodationName { get; set; }
        public bool Busiest { get; set; }

        //years

        public YearView(User user, Accommodation selected)
        {
            InitializeComponent();
            DataContext = this;

            SelectedAccommodation = selected;

            LoggedInUser = user;
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
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
