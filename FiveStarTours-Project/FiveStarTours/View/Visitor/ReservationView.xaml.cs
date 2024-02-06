using FiveStarTours.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FiveStarTours.Model;
using FiveStarTours.View.Traveler;
using System.Collections.ObjectModel;
using FiveStarTours.View.Owner;
using FiveStarTours.Services;

namespace FiveStarTours.View.Visitor
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationView : Window, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }

        private readonly TourReservationService _visitorRepository;
        private readonly KeyPointsService _keyPointsRepository;
        private readonly UserService _userRepository;
        private readonly GiftCardService _giftCardRepository;

        public ObservableCollection<User> Visitors { get; set; }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if(value != _phoneNumber)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _membersNumber;
        public string MembersNumber
        {
            get => _membersNumber;
            set
            {
                if( value != _membersNumber)
                {
                    _membersNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if(_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        List<DateTime> DateTimeTour = new List<DateTime>();
        List<DateTime> ExpiringDates = new List<DateTime>();
        List<string> Names = new List<string>();
        int freeSeats
        {
            get; set;
        }
        public ReservationView(Tour selectedTour, User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            freeSeats = selectedTour.MaxGuests;
            SelectedTour = selectedTour;

            _visitorRepository = new TourReservationService();
            _keyPointsRepository = new KeyPointsService();
            _userRepository = new UserService();
            _giftCardRepository = new GiftCardService();

            Visitors = new ObservableCollection<User>(_userRepository.GetAll());

            List<int> KeyPoints = selectedTour.IdKeyPoints;
            List<string> Points = GetName(KeyPoints);
            StartingKeyPoint.ItemsSource = Points;
            StartingKeyPoint.SelectedValuePath = ".";
            StartingKeyPoint.DisplayMemberPath = ".";

            DateTimeTour = selectedTour.Beginning;
            DateTimeComboBox.ItemsSource = DateTimeTour;
            DateTimeComboBox.SelectedValuePath = ".";
            DateTimeComboBox.DisplayMemberPath = ".";

            ExpiringDates = _giftCardRepository.GetAllDatesById(user.Id);
            GiftCardComboBox.ItemsSource = ExpiringDates;
            GiftCardComboBox.SelectedValuePath = ".";
            GiftCardComboBox.DisplayMemberPath = ".";

            freeSeats -= _visitorRepository.ReservedSeats(SelectedTour);

        }
        public List<string> GetName(List<int> keyPoints)
        {
            List<string> result  = new List<string>();
            foreach (int i in keyPoints)
            {
                result.Add(_keyPointsRepository.GetById(i));
            }
            return result;
        }
        public Tour SelectedTour;
        public bool giftCard;
        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            KeyPoints startingKeyPoint = new KeyPoints();
            startingKeyPoint.Name = selectedKeyPoint;
            if(freeSeats < Convert.ToInt32(MembersNumber))
            {
                MessageBox.Show($"No enough seats for this reservation. Left seats : {freeSeats}");
                return;
            }
            foreach(var keyPoint in _keyPointsRepository.GetAll())
            {
                if(startingKeyPoint.Name == keyPoint.Name)
                {
                    startingKeyPoint.Id = keyPoint.Id;
                }
            }
            DateTime dateTime = new DateTime();
            dateTime = selectedDateTime;
            
            TourReservation visitor = new TourReservation(Names, PhoneNumber, SelectedTour.Id, startingKeyPoint.Id, startingKeyPoint, dateTime, Convert.ToInt32(MembersNumber), Email, giftCard);
            _visitorRepository.Save(visitor);
            MessageBox.Show("Reservation has just made.");
            ReservedToursView reserved = new ReservedToursView(LoggedInUser);
            reserved.Show();
            Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private string selectedKeyPoint;
        private void StartingKeyPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StartingKeyPoint.SelectedItem != null)
            {
                selectedKeyPoint = StartingKeyPoint.SelectedItem as string;

            }
        }
        private DateTime selectedDateTime;
        private void DateTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DateTimeComboBox.SelectedItem != null)
            {
                selectedDateTime = (DateTime)DateTimeComboBox.SelectedItem;//as DateTime;
            }
        }
        private void GiftCardComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(GiftCardComboBox.SelectedItem != null)
            {
                giftCard = true;
                _giftCardRepository.Delete(GiftCardComboBox.SelectedItem.ToString());
            }
            else
            {
                giftCard = false;
            }
        }

        private void User_Checked(object sender, RoutedEventArgs e)
        {
            string item = (string)((CheckBox)sender).Content;
            Names.Add(item);
        }

      

       
    }
}
