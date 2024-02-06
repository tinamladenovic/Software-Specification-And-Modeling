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
    /// Interaction logic for MainWindowLida.xaml
    /// </summary>
    public partial class MainWindowLida : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectAccommodation { get; set; }

        private readonly AccommodationsService accommodationService;
        public static ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }

        private readonly AccommodationReservationService accommodationReservationService;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        private List<ImageSource> _imageSources;
        public List<ImageSource> ImageSources
        {
            get => _imageSources;
            set
            {
                _imageSources = value;
                OnPropertyChanged(nameof(List<ImageSource>));
            }
        }


        public MainWindowLida()
        {
            InitializeComponent();
            accommodationService = new AccommodationsService();
            Accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());
            accommodationReservationService = new AccommodationReservationService();
            Reservations = new ObservableCollection<AccommodationReservation>(accommodationReservationService.GetAll());
            DataContext = this;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SetImages();
        }
        private void SetImages()
        {

            foreach (Accommodation accommodation in Accommodations)
            {
                try
                {
                    string imageUrl = accommodation.ImageURLs.FirstOrDefault();

                    if (imageUrl != null)
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imageUrl, UriKind.Absolute);
                        bitmap.EndInit();
                        accommodation.FrontImage = bitmap;
                    }
                }
                catch (Exception ex)
                {
                    
                    BitmapImage defaultImage = new BitmapImage(new Uri("/Resources/Images/save_hci.png", UriKind.Relative));
                    accommodation.FrontImage = defaultImage;
                }
            }
        }

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

        private string _visitationDays;
        public string VisitationDays
        {
            get => _visitationDays;
            set
            {
                _visitationDays = value;
                OnPropertyChanged();
            }
        }
        private string _imageUrls;
        public string ImageUrls
        {
            get => _imageUrls;
            set
            {
                if (value != _imageUrls)
                {
                    _imageUrls = value;
                    OnPropertyChanged();
                }
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
        private string _guestNumber;
        public string GuestNumber
        {
            get => _guestNumber;
            set
            {
                _guestNumber = value;
                OnPropertyChanged();
            }
        }


        private void Menage(object sender, RoutedEventArgs e)
        {
            ReservationsView reservationView = new ReservationsView();
            reservationView.Show();
            Close();
        }

        private void SerachAcc(object sender, RoutedEventArgs e)
        {
            SearchAccommodation searchAccommodation = new SearchAccommodation();
            searchAccommodation.Show();
            Close();
        }
        private void RefreshAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in accommodations)
            {
                Accommodations.Add(accommodation);
            }
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            if (SelectAccommodation != null)
            {
                ReserveAcc rs = new ReserveAcc(SelectAccommodation);
                rs.Show();
                Close();
            }
            else
                return;
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                window.Close();
            }
        }

        private void Account(object sender, RoutedEventArgs e)
        {
            GuestReviews reviews = new GuestReviews(SelectedReservation);
            reviews.Show();

        }
    }


}
