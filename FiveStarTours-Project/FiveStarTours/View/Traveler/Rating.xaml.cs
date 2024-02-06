using FiveStarTours.Model;
using FiveStarTours.Services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Proba.xaml
    /// </summary>
    public partial class Rating : Window, INotifyPropertyChanged
    {
        public AccommodationReservation _selectedReservation { get; set; }
        private readonly AccommodationReservationService _reservationservice;
        private readonly AccommodationRatingService _rateservice;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

    

        public int ratingOwner;
        public int accCleanness;
        public int accAsInPicture;
        public int accCorectness;
        public int accExperience;

        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }
        private string _imageURLs;
        public string ImageURLs
        {
            get => _imageURLs;
            set
            {
                if (value != _imageURLs)
                {
                    _imageURLs = value;
                    OnPropertyChanged();
                }
            }
        }

        public int id;

        public Rating(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            DataContext = this;
            _selectedReservation = selectedReservation;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _reservationservice = new AccommodationReservationService();
            _rateservice = new AccommodationRatingService();
        }

        private void Recommodation(object sender, RoutedEventArgs e)
        {
            RecommodationForReservation recommodation = new RecommodationForReservation(_selectedReservation);
            recommodation.Show();
            Close();
        }
    }
}
