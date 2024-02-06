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
    /// Interaction logic for ReserveAcc.xaml
    /// </summary>
    public partial class ReserveAcc : Window, INotifyPropertyChanged
    {
        private readonly AccommodationReservationService accommodationReservationService;
        private readonly AccommodationsService accommodationService;
        public static ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }
        public Accommodation SelectedAccommodation { get; set; }

        private string _minReservationDays;
        public string MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                _minReservationDays = value;
                OnPropertyChanged();
            }
        }
        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                _accommodationName = value;
                OnPropertyChanged();
            }
        }
        private string _maxGuestNumber;
        public string MaxGuestNumber
        {
            get => _maxGuestNumber;
            set
            {
                _maxGuestNumber = value;
                OnPropertyChanged();
            }
        }

        public ReserveAcc(Accommodation selectedAccommoodation)
        {
            InitializeComponent();
            DataContext = this;
            accommodationService= new AccommodationsService();
            SelectedAccommodation = selectedAccommoodation;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void ReserveThis(object sender, RoutedEventArgs e)
        {
            Reservation rs = new Reservation(SelectedAccommodation);
            rs.Show();
        }

        private void Goback(object sender, RoutedEventArgs e)
        {
            MainWindowLida mainWindowLida = new MainWindowLida();
            mainWindowLida.Show();
            Close();
        }

        private void startForum(object sender, RoutedEventArgs e)
        {

        }
    }
}
