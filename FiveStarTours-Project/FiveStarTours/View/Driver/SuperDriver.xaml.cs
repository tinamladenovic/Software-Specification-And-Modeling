using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FiveStarTours.View.Driver
{
    /// <summary>
    /// Interaction logic for SuperDriver.xaml
    /// </summary>
    public partial class SuperDriver : Window
    {
        private readonly UserService _userService;
        private readonly VehicleOnAdressService _vehicleOnAdressService;


        public User LoggedInUser { get; set; }
        public int FastDriveNumber { get; set; }

        public bool IsSuperDriver { get; set; }
        public SuperDriver(User user)
        {
            InitializeComponent();
            DataContext = this;

            _userService = new UserService();

            LoggedInUser = user;

            VehicleOnAdressService _vehicleOnAdressService = new VehicleOnAdressService();

            FastDriveNumber = _vehicleOnAdressService.FastDriveCount();

            if (_vehicleOnAdressService.FastDriveCount() > 15)
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
                IsSuperDriver = true;
            }
            else
            {
                IsSuperDriver = false;
            }

            return;


        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
