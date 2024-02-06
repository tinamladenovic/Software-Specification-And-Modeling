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
    /// Interaction logic for Recommodation.xaml
    /// </summary>
    public partial class RecommodationForReservation : Window, INotifyPropertyChanged
    {
        public AccommodationReservation _selectedReservation { get; set; }
        private readonly AccommodationReservationService _reservationservice;
        private readonly RecommodationService _recommodationservice;
        public AccommodationReservation SelectedReservation { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<string> Necesities { get; set; }
        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        public RecommodationForReservation(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            DataContext = this;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _selectedReservation = selectedReservation;
            _reservationservice = new AccommodationReservationService();
            _recommodationservice = new RecommodationService();

            Necesities = new ObservableCollection<string>();
            Necesities.Add("LEVEL 1-NOT NECESSERY");
            Necesities.Add("LEVEL 2");
            Necesities.Add("LEVEL 3");
            Necesities.Add("LEVEL 4");
            Necesities.Add("LEVEL 5-VERY NECESSERY");
            
        }
        public int id { get; set; }

       private string _recommodationText;
        public string RecommodationText
        {
            get
            {
                return _recommodationText;
            }
            set
            {
                _recommodationText = value;
                OnPropertyChanged();
            }
        }
       
       
        public string Necessity { get; set; }

        private void Sumbit(object sender, RoutedEventArgs e)
        {
            

            AccommodationReservation reservation = _selectedReservation;
            Recommodation newrecommodation = new Recommodation(reservation,RecommodationText,Necessity);
            _recommodationservice.Save(newrecommodation);
            Close();

            
        }

        private void back(object sender, RoutedEventArgs e)
        {
           ReservationsView reservationsView = new ReservationsView();
            reservationsView.Show();
            Close();
        }
    }
}
