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
    /// Interaction logic for GroupRideInfoView.xaml
    /// </summary>
    public partial class GroupRideInfoView : Window
    {
        public User LoggedInUser { get; set; }

        public GroupRide SelectedRide { get; set; }
        private readonly GroupRideService _repository;
        public GroupRide groupRide { get; set; }
        public GroupRideInfoView(GroupRide selectedRide, User user)
        {
            InitializeComponent();
            DataContext = this;
            _repository = new GroupRideService();
            
            LoggedInUser = user;
            SelectedRide = selectedRide;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
