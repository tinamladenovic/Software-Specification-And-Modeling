using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using FiveStarTours.View.Owner;
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
    /// Interaction logic for OwnerMainPageView.xaml
    /// </summary>
    public partial class OwnerMainPageView : Window
    {
        public User LoggedInUser { get; set; }

        private readonly AccommodationReservationService _service;
        public OwnerMainPageView(User user)
        {
            InitializeComponent();
            DataContext = this;

            LoggedInUser = user;

            _service = new AccommodationReservationService();
            _service.NotifyAboutUnratedGuests();
        }

        private void ActionBar_ButtonClick(object sender, RoutedEventArgs e)
        {
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
            Close();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
