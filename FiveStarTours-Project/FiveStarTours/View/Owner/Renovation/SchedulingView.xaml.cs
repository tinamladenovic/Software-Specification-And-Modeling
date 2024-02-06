using FiveStarTours.Services;
using FiveStarTours.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiveStarTours.View.Owner.Renovation;
using System.Collections.Generic;

namespace FiveStarTours.View.Owner
{
    /// <summary>
    /// Interaction logic for SchedulingRenovationsView.xaml
    /// </summary>
    public partial class SchedulingView : Window, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }

        private readonly AccommodationReservationService accommodationReservationService;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        public Accommodation SelectedAccommodation { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ExpectedDuration { get; set; }
        public string Description { get; set; }

 
        public SchedulingView(Accommodation selected)
        {
            InitializeComponent();
            DataContext = this;

            accommodationReservationService = new AccommodationReservationService();

            SelectedAccommodation = selected;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

    
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            List<DateInterval> dates = accommodationReservationService.GetFreeDateIntervals(SelectedAccommodation.AccommodationName, StartDate, EndDate, ExpectedDuration);
            FreeDatesView freeDates = new FreeDatesView(dates, SelectedAccommodation.AccommodationName, Description, ExpectedDuration);
            freeDates.Show();
            Close();

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationsView accommodations = new AccommodationsView();
            accommodations.Show();
            Close();
        }
    }
}
