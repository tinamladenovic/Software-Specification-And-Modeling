using FiveStarTours.Model;
using FiveStarTours.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using FiveStarTours.View.Traveler;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace FiveStarTours.View.Owner
{
    /// <summary>
    /// Interaction logic for SuperOwnerView.xaml
    /// </summary>
    public partial class SuperOwnerView : Window
    {

        private readonly AccommodationRatingService _rateService;
        private readonly UserService _userService;
        public AccommodationRating accommodationRating { get; set; }

        public User LoggedInUser { get; set; }

        public int NumberOfReviews { get; set; }

        public double AverageRate { get; set; }

        public bool IsSuperOwner { get; set; }


        public SuperOwnerView(User user)
        {
            InitializeComponent();
            DataContext = this;

            _userService = new UserService();

            _rateService = new AccommodationRatingService();

            accommodationRating = new AccommodationRating();

            LoggedInUser = user;

            NumberOfReviews = _rateService.CountRatings();
            AverageRate = _rateService.AverageOwnerRating();

            if (_rateService.CountRatings() > 50 && _rateService.AverageOwnerRating() > 4.5)
            {
                LoggedInUser.Super = true;
                _userService.Update(LoggedInUser);

            }
            else
            {
                LoggedInUser.Super = false;
                _userService.Update(LoggedInUser);
            }

            if (LoggedInUser.Super == true)
            {
                IsSuperOwner = true;
            }
            else
            {
                IsSuperOwner = false;
            }

            return;


        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
            Close();
        }
    }
}
