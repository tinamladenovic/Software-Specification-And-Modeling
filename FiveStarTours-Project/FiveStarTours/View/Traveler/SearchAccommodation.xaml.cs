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
    /// Interaction logic for SearchAccommodation.xaml
    /// </summary>
    public partial class SearchAccommodation : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectAccommodation { get; set; }

        private readonly AccommodationsService accommodationService;
        public SearchAccommodation()
        {

            InitializeComponent();
            accommodationService = new AccommodationsService();
            Accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());
            DataContext = this;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;



        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
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
                string length = LengthSearch.Text;

                List<Accommodation> seachedAccommodations = accommodationService.SearchAccomodations(name, state, city, guestNum, type, length);
                RefreshAccommodations(seachedAccommodations);
                MainWindowLida mainWindowLida=new MainWindowLida();
                mainWindowLida.Show();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid inputs");
            }


        }
        private void RefreshAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in accommodations)
            {
                Accommodations.Add(accommodation);
            }
        }

        private void gback(object sender, RoutedEventArgs e)
        {
            MainWindowLida mainWindowLida = new MainWindowLida();
            mainWindowLida.Show();
            Close();
        }

    }

}
