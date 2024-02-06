using FiveStarTours.Model;
using FiveStarTours.Services;
using FiveStarTours.View.Owner.Renovation;
using FiveStarTours.View.Traveler;
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

namespace FiveStarTours.View.Owner.Renovation
{
    /// <summary>
    /// Interaction logic for RenovationDates.xaml
    /// </summary>
    public partial class FreeDatesView : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public static ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public ObservableCollection<Renovations> Renovations { get; set; }

        public ObservableCollection<DateInterval> FreeDates { get; set; }
        public DateInterval SelectedDate { get; set; }

        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly AccommodationsService _accommodationService;
        private readonly RenovationService _renovationService;
        public string AccommodationName { get; set; }

        public string Description { get; set; }
        public int ExpectedDuration { get; set; }

        public FreeDatesView(List<DateInterval> freeIntervals, string accommodationName, string description, int duration)
        {
            InitializeComponent();
            this.DataContext = this;

            FreeDates = new ObservableCollection<DateInterval>(freeIntervals);

            AccommodationName = accommodationName;
            Description = description;
            ExpectedDuration = duration;

            _accommodationService = new AccommodationsService();
            _accommodationReservationService = new AccommodationReservationService();
            _renovationService = new RenovationService();

        }



        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDate != null)
            {
                Accommodation accommodation = _accommodationService.GetAccommodationForAccommodationName(AccommodationName);

                string messageBoxText = "Are you sure you want to pick this date?";
                string caption = "Date Picker";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    int days = (int)(SelectedDate.End.Date - SelectedDate.Start.Date).TotalDays;


                    Renovations renovation = new Renovations(AccommodationName, SelectedDate.Start, SelectedDate.End, days, Description);
                    accommodation.RecentlyRenovated = true;
                    _accommodationService.Update(accommodation);

                    _renovationService.Save(renovation);
                    

                    RenovationsView renovations = new RenovationsView();
                    renovations.Show();
                    Close();

                }
                else
                {
                    return;
                }
            }


        }


        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationsView accommodations = new AccommodationsView();
            accommodations.Show();
            Close();
        }
    }
}
