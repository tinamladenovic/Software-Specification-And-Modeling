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
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;

namespace FiveStarTours.View.Traveler
{
    /// <summary>
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : Window, INotifyPropertyChanged
    {
        private readonly AccommodationReservationService accommodationReservationService;
        public static ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public Accommodation SelectedAccommodation { get; set; }
        public int VisitationDays { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
      



        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        public Reservation(Accommodation selectedAccommoodation)
        {
            InitializeComponent();
            DataContext = this;
            accommodationReservationService = new AccommodationReservationService();
            SelectedAccommodation = selectedAccommoodation;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
       

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
            if(VisitationDays < SelectedAccommodation.MinReservationDays)
            {
                MessageBox.Show("Min number of days for this accommodation is " + SelectedAccommodation.MinReservationDays + ".", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AvailableDates availableDates = new AvailableDates(accommodationReservationService.GetFreeDateIntervals(SelectedAccommodation.AccommodationName, StartDate, EndDate, VisitationDays), SelectedAccommodation.AccommodationName);
            availableDates.Show();
            Close();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            MainWindowLida mainWindowLida = new MainWindowLida();
            mainWindowLida.Show();
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            DateTime? selectedDate = datePicker.SelectedDate;

            
            if (selectedDate.HasValue && selectedDate.Value < DateTime.Today)
            {
                MessageBox.Show("You can't select dates in the past!");
                datePicker.SelectedDate = DateTime.Today;
            }
        }
    }
}

