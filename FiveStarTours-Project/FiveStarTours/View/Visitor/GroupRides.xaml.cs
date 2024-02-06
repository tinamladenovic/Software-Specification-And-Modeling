using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FiveStarTours.View.Visitor
{
    /// <summary>
    /// Interaction logic for GroupRides.xaml
    /// </summary>
    public partial class GroupRides : Window
    {
        public User LoggedInUser { get; set; }
        public static ObservableCollection<GroupRide> RideRequest { get; set; }
        public GroupRide SelectedGroupRide { get; set; }


        private readonly GroupRideService _repository;
        public GroupRide groupRide { get; set; }
        public GroupRides(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            _repository = new GroupRideService();
            RideRequest = new ObservableCollection<GroupRide>(_repository.GetAll());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGroupRide != null)
            {
                GroupRideInfoView rideInfo = new GroupRideInfoView(SelectedGroupRide, LoggedInUser);
                rideInfo.Show();
                
            }
        }
    }
}
