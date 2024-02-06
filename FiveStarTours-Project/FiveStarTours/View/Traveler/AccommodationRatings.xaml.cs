using FiveStarTours.Model;
using FiveStarTours.Repository;
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
using FiveStarTours.Services;

namespace FiveStarTours.View.Traveler
{
    /// <summary>
    /// Interaction logic for AccommodationRatings.xaml
    /// </summary>
    public partial class AccommodationRatings : Window
    {
        public AccommodationReservation _selectedReservation { get; set; }
        private readonly AccommodationReservationService _reservationservice;
        private readonly AccommodationRatingService _rateservice;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        public AccommodationRatings(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            DataContext = this;
            _selectedReservation = selectedReservation;


            _reservationservice = new AccommodationReservationService();
            _rateservice =new AccommodationRatingService();
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

        private void Submit(object sender, RoutedEventArgs e)
        {
            List<string> ImageURLsList = new List<string>();
            if (ImageURLs != null)
            {
                ImageURLsList = MakeUrlsList(ImageURLs);
            }
            else return;

            string s = RatingOwnerComboBox.Text;
            if(s.Equals("1"))
            {
                ratingOwner = 1;
            }
            else if(s.Equals("2"))
            {
                ratingOwner = 2;
            }
            else if(s.Equals("3"))
            {
                ratingOwner = 3;
            }
            else if(s.Equals("4"))
            {
                ratingOwner = 4;
            }
            else if(s.Equals("5"))
            {
                ratingOwner = 5;
            }

            string c = AccCleannessComboBox.Text;
            if(c.Equals("1"))
            {
                accCleanness = 1;
            }
            else if(c.Equals("2"))
            {
                accCleanness = 2;
            }
            else if (c.Equals("3"))
            {
                accCleanness = 3;
            }
            else if (c.Equals("4"))
            {
                accCleanness = 4;
            }
            else if (c.Equals("5"))
            {
                accCleanness = 5;
            }

            string a=AcAsInPictureComboBox.Text;
            if (a.Equals("1"))
            {
                accAsInPicture = 1;
            }
            else if (a.Equals("2"))
            {
                accAsInPicture = 2;
            }
            else if (a.Equals("3"))
            {
                accAsInPicture = 3;
            }
            else if (a.Equals("4"))
            {
                accAsInPicture = 4;
            }
            else if (a.Equals("5"))
            {
                accAsInPicture = 5;
            }

            string r = AccCorectnessComboBox.Text;
            if(r.Equals("1"))
            {
                accCorectness = 1;
            }
            else if(r.Equals("2"))
            {
                accCorectness = 2;
            }
            else if (r.Equals("3"))
            {
                accCorectness = 3;
            }
            else if (r.Equals("4"))
            {
                accCorectness = 4;
            }
            else if (r.Equals("5"))
            {
                accCorectness = 5;
            }

            string ex = AccExperienceComboBox.Text;
            if(ex.Equals("1"))
            {
                accExperience = 1;
            }
            else if(ex.Equals("2"))
            {
                accExperience = 2;
            }
            else if (ex.Equals("3"))
            {
                accExperience = 3;
            }
            else if (ex.Equals("4"))
            {
                accExperience = 4;
            }
            else if (ex.Equals("5"))
            {
                accExperience = 5;
            }

            AccommodationReservation reservation = _selectedReservation;
            AccommodationRating newAccommodationRate= new AccommodationRating(reservation,ratingOwner,accCleanness,accAsInPicture,accCorectness,accExperience,Comment, ImageURLsList);

            if (IsValid(newAccommodationRate))
            {
                _rateservice.Save(newAccommodationRate);
                reservation.RatedByGuest = true;

                _reservationservice.Update(reservation);
                Close();


            }
            else
            {
                MessageBox.Show("You must provide all info on your accommodation.");
            }

        }
        public List<string> MakeUrlsList(string urls)
        {
            List<string> result = new List<string>();

            Array _urls = urls.Split(", ");

            foreach (string url in _urls)
            {
                result.Add(url);
            }

            return result;
        }
        public static bool IsValidUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
        public bool IsValid(AccommodationRating rating)
        {


            return true;
        }

    }
}
