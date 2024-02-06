using FiveStarTours.Model;
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
using FiveStarTours.Repository;
using FiveStarTours.Services;

namespace FiveStarTours.View.Traveler
{
    /// <summary>
    /// Interaction logic for AvailableDates.xaml
    /// </summary>
    public partial class AvailableDates : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public static ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public ObservableCollection<DateInterval> FreeDates { get; set; }
        public DateInterval SelectedDate { get; set; }
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly AccommodationsService _accommodationService;
        public string AccommodationName { get; set; }

        public int GuestNumber { get; set; }

        private string _guestName;
        public string GuestName
        {
            get => _guestName;
            set
            {
                _guestName = value;
                OnPropertyChanged();
            }
        }
        private string _guestSurname;
        public string GuestSurname
        {
            get => _guestSurname;
            set
            {
                _guestSurname = value;
                OnPropertyChanged();
            }
        }


        public AvailableDates(List<DateInterval> freeIntervals, string accommodationName)
        {
            InitializeComponent();
            this.DataContext = this;

            FreeDates = new ObservableCollection<DateInterval>(freeIntervals);
            AccommodationName = accommodationName;
            _accommodationReservationService = new AccommodationReservationService();
            _accommodationService = new AccommodationsService();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;


        }



        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        private void Pick_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDate != null)
            {
                Accommodation accommodation = _accommodationService.GetAccommodationForAccommodationName(AccommodationName);
                if (accommodation.MaxGuestNum < GuestNumber)
                {
                    MessageBox.Show("Max guests for this accommodation is " + accommodation.MaxGuestNum + ".", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string messageBoxText = "Are you sure you want to pick this date?";
                string caption = "Date Picker";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    int days = (int)(SelectedDate.End.Date - SelectedDate.Start.Date).TotalDays;

                    GuestName = MainWindow.LoggedUser.Name;
                    AccommodationReservation accommodationReservation = new AccommodationReservation(GuestName, SelectedDate.Start, SelectedDate.End, days, AccommodationName, GuestNumber);
                    _accommodationReservationService.Save(accommodationReservation);

                    ReservationsView rs = new ReservationsView();
                    rs.Show();
                    Close();

                }
                else
                {
                    return;
                }
            }


        }

        private void goback(object sender, RoutedEventArgs e)
        {
            ReservationsView reservationsView = new ReservationsView();
            reservationsView.Show();

        }
    }
}
