using FiveStarTours.Model;
using FiveStarTours.Repository;
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
using System.Xml.Linq;

namespace FiveStarTours.View.Visitor
{
    /// <summary>
    /// Interaction logic for TourRating.xaml
    /// </summary>
    public partial class TourRatingView : Window, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }

        private readonly TourRatingService _tourRatingRepository;
        private readonly AttendanceService _attendanceRepository;
        public readonly ToursService _toursRepository;
        public Tour SelectedTour { get; set; }
        public List<string> tours = new List<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourRatingView(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            _tourRatingRepository = new TourRatingService();
            _attendanceRepository = new AttendanceService();
            _toursRepository = new ToursService();

            tours = GetAllTours();
            TourComboBox.ItemsSource = tours;
            TourComboBox.SelectedValuePath = ".";
            TourComboBox.DisplayMemberPath = ".";
        }

        public int guidesKnowledge;
        public int guidesSpeaking;
        public int levelOfInterest;
        public string photo;
        public string review;

        public string selectedTour;
        public int tourId;
        private void TourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TourComboBox.SelectedItem != null)
            {
                selectedTour = TourComboBox.SelectedItem as string;
                tourId = _toursRepository.GetByName(selectedTour);
            }
        }
        public string Photo
        {
            get => photo;
            set
            {
                if (value != photo)
                {
                    photo = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Review
        {
            get => review;
            set
            {
                if (value != review)
                {
                    review = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string > GetAllTours()
        {
            List<string> result;
            List<int> ids = _attendanceRepository.GetVisitedTours(LoggedInUser.Id);
            result = _toursRepository.GetNamesById(ids);
            return result;
        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            string knowledge = GuidesKnowledgeComboBox.Text;
            if (knowledge.Equals("1-very bad"))
            {
                guidesKnowledge = 1;
            }
            else if (knowledge.Equals("2-bad"))
            {
                guidesKnowledge = 2;
            }
            else if (knowledge.Equals("3-average"))
            {
                guidesKnowledge = 3;
            }
            else if (knowledge.Equals("4-good"))
            {
                guidesKnowledge = 4;
            }
            else if (knowledge.Equals("5-very good"))
            {
                guidesKnowledge = 5;
            }

            string speaking = GuidesSpeakingComboBox.Text;
            if (speaking.Equals("1-very bad"))
            {
                guidesSpeaking = 1;
            }
            else if (speaking.Equals("2-bad"))
            {
                guidesSpeaking = 2;
            }
            else if (speaking.Equals("3-average"))
            {
                guidesSpeaking = 3;
            }
            else if (speaking.Equals("4-good"))
            {
                guidesSpeaking = 4;
            }
            else if (speaking.Equals("5-very good"))
            {
                guidesSpeaking = 5;
            }

            string interest = GuidesSpeakingComboBox.Text;
            if (interest.Equals("1-very bad"))
            {
                levelOfInterest = 1;
            }
            else if (interest.Equals("2-bad"))
            {
                levelOfInterest = 2;
            }
            else if (interest.Equals("3-average"))
            {
                levelOfInterest = 3;
            }
            else if (interest.Equals("4-good"))
            {
                levelOfInterest = 4;
            }
            else if (interest.Equals("5-very good"))
            {
                levelOfInterest = 5;
            }
            TourRating rating = new TourRating(tourId, guidesKnowledge, guidesSpeaking, levelOfInterest, photo, review, LoggedInUser.Id);
            _tourRatingRepository.Save(rating);
            MessageBox.Show("Thank you for your rate.");
            Close();
        }

        private void InformationsButton_Click(object sender, RoutedEventArgs e)
        {
            ToursListingView toursListing = new ToursListingView(LoggedInUser);
            toursListing.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            VehicleSearchView vehicleSearch = new VehicleSearchView(LoggedInUser);
            vehicleSearch.Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ReservedToursView reservedTours = new ReservedToursView(LoggedInUser);
            reservedTours.Show();
        }
    }
}
