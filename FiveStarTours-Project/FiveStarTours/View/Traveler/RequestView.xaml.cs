using FiveStarTours.Model;
using FiveStarTours.Repository;
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
    /// Interaction logic for RequestView.xaml
    /// </summary>
    public partial class RequestView : Window, INotifyPropertyChanged
    {
        private AccommodationReservationService _accommodationReservationService;
        private ReservationChangeService _reservationChangeService;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ReservationChange SelectedChangedRes { get; set; }
        public ObservableCollection<ReservationChange> Changes{ get; set; }
        


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
   
        private string _startDate;
        public string StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        private string _endDate;
        public string EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public RequestView()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationReservationService =  new AccommodationReservationService();
            _reservationChangeService = new ReservationChangeService();
            NewStartDate = DateTime.Now;
            NewEndDate = DateTime.Now;
            Changes = new ObservableCollection<ReservationChange>(_reservationChangeService.GetAll());
        }

        private void back(object sender, RoutedEventArgs e)
        {
            TravelerViewandSearch tvs = new TravelerViewandSearch();
            tvs.Show();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        private void logout(object sender, RoutedEventArgs e)
        {
         
          foreach(Window window in Application.Current.Windows)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                window.Close();
            }
        }
    }
}
