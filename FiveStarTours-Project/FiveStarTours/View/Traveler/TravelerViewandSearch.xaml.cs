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
    /// Interaction logic for TravelerViewandSearch.xaml
    /// </summary>
    /// 

    public partial class TravelerViewandSearch : Window,INotifyPropertyChanged
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectAccommodation { get; set; }
   
        private readonly AccommodationsService accommodationService;
        public TravelerViewandSearch()
        {
            
            InitializeComponent();
            accommodationService = new AccommodationsService();
            Accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());
            DataContext = this;
            


        }


        private void RefreshAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach(Accommodation accommodation in accommodations)
            {
                Accommodations.Add(accommodation);
            }
        }


        private void Search_enter(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameSearch.Text;
                string state = StateSearch.Text;
                string city = CitySearch.Text;
                string guestNum = NumberSearch.Text;
                string type = TypeBox.Text;
                string length= LengthSearch.Text;

                List<Accommodation> seachedAccommodations = accommodationService.SearchAccomodations(name, state, city, guestNum,type, length);
                RefreshAccommodations(seachedAccommodations);

            }catch(Exception ex)
            {
                MessageBox.Show("Invalid inputs");
            }

            
        }
       


        private string _selectedReservation;
        public string SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            if (SelectAccommodation != null)
            {
                string messageBoxText = "Are you sure you want to reserve this?";    
                string caption = "Reservation";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                   
                    Reservation rs = new Reservation(SelectAccommodation);
                    rs.Show();

                }
                else
                {
                    return;
                }
            }
            Close();

        }

        private void ViewReservations(object sender, RoutedEventArgs e)
        {
            ReservationsView reservationsView = new ReservationsView();
            reservationsView.Show();
            Close();
        }
    }
}
