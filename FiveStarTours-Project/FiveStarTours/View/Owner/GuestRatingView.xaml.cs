using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using MahApps.Metro.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;

namespace FiveStarTours.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerRatingGuests.xaml
    /// </summary>
    public partial class GuestRatingView : Window
    {
        User LoggedInUser { get; set; }
        public AccommodationReservation _selectedReservation { get; set; }

        private readonly AccommodationReservationService _service;
        private readonly GuestRaitingsService _ratingService;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        public GuestRatingView(AccommodationReservation selectedReservation, User user)
        {
            InitializeComponent();
            DataContext = this;

            LoggedInUser = user;

            _selectedReservation = selectedReservation;


            _service = new AccommodationReservationService();
            _ratingService = new GuestRaitingsService();
        }


        public int tidiness;

        public int rulesFollowed;

        public int quietness;

        public int friendliness;

        public int communication;

        public int respectTime;


        private string comment;
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
                OnPropertyChanged();
            }
        }

        private void SubmitRateButton_Click(object sender, RoutedEventArgs e)
        {
            //Tidiness
            string s = TidinessComboBox.Text;
            if (s.Equals("1 - very poor"))
            {
                tidiness = 1;
            }
            else if (s.Equals("2 - poor"))
            {
                tidiness = 2;
            }
            else if (s.Equals("3 - average"))
            {
                tidiness = 3;
            }
            else if (s.Equals("4 - good"))
            {
                tidiness = 4;
            }
            else if (s.Equals("5 - very good"))
            {
                tidiness = 5;
            }

            //Rules followed
            string r = RulesFollowedComboBox.Text;
            if (r.Equals("1 - very poor"))
            {
                rulesFollowed = 1;
            }
            else if (r.Equals("2 - poor"))
            {
                rulesFollowed = 2;
            }
            else if (r.Equals("3 - average"))
            {
                rulesFollowed = 3;
            }
            else if (r.Equals("4 - good"))
            {
                rulesFollowed = 4;
            }
            else if (r.Equals("5 - very good"))
            {
                rulesFollowed = 5;
            }

            //Quietness
            string q = QuietnessComboBox.Text;
            if (q.Equals("1 - very poor"))
            {
                quietness = 1;
            }
            else if (q.Equals("2 - poor"))
            {
                quietness = 2;
            }
            else if (q.Equals("3 - average"))
            {
                quietness = 3;
            }
            else if (q.Equals("4 - good"))
            {
                quietness = 4;
            }
            else if (q.Equals("5 - very good"))
            {
                quietness = 5;
            }

            //Friendliness
            string f = FriendlinessComboBox.Text;
            if (f.Equals("1 - very poor"))
            {
                friendliness = 1;
            }
            else if (f.Equals("2 - poor"))
            {
                friendliness = 2;
            }
            else if (f.Equals("3 - average"))
            {
                friendliness = 3;
            }
            else if (f.Equals("4 - good"))
            {
                friendliness = 4;
            }
            else if (f.Equals("5 - very good"))
            {
                friendliness = 5;
            }

            //Communication
            string c = CommunicationComboBox.Text;
            if (c.Equals("1 - very poor"))
            {
                communication = 1;
            }
            else if (c.Equals("2 - poor"))
            {
                communication = 2;
            }
            else if (c.Equals("3 - average"))
            {
                communication = 3;
            }
            else if (c.Equals("4 - good"))
            {
                communication = 4;
            }
            else if (c.Equals("5 - very good"))
            {
                communication = 5;
            }

            //RespectTime
            string t = RespectTimeComboBox.Text;
            if (t.Equals("1 - very poor"))
            {
                respectTime = 1;
            }
            else if (t.Equals("2 - poor"))
            {
                respectTime = 2;
            }
            else if (t.Equals("3 - average"))
            {
                respectTime = 3;
            }
            else if (t.Equals("4 - good"))
            {
                respectTime = 4;
            }
            else if (t.Equals("5 - very good"))
            {
                respectTime = 5;
            }

            
            

            AccommodationReservation reservation = _selectedReservation;

            GuestRating newGuestRate = new GuestRating(
                    reservation,
                    tidiness,
                    rulesFollowed,
                    quietness,
                    friendliness,
                    communication,
                    respectTime,
                    Comment
                );


            GuestsWithoutRateView guestsWithoutRateView = new GuestsWithoutRateView(LoggedInUser);

            if (IsValid(newGuestRate))
            {
                _ratingService.Save(newGuestRate);
                reservation.RatedByOwner = true;

                _service.Update(reservation);
                System.Threading.Thread.Sleep(1000);
                Close();
                System.Threading.Thread.Sleep(2000);


                guestsWithoutRateView.Show();
            }
            else
            {
                MessageBox.Show("You must provide all info on your accommodation.");
            }

           
            

        }

        public bool IsValid(GuestRating rating)
        {
            

            return true;
        }



        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            GuestsWithoutRateView guests = new GuestsWithoutRateView(LoggedInUser);
            guests.Show();
            Close();
        }



    }
}
