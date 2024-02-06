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
using FiveStarTours.View;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using FiveStarTours.View.Driver;

namespace FiveStarTours.View
{
    /// <summary>
    /// Interaction logic for DriverMainWindow.xaml
    /// </summary>
    public partial class DriverMainWindow : Window
    {
       
        public DriverMainWindow()
        {
            InitializeComponent();
            DataContext = this;
            
        }

        private void VehicleRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            VehicleRegistration.VehicleRegistration vehicleRegistration = new VehicleRegistration.VehicleRegistration();
            vehicleRegistration.Show();
        }

        private void VehicleOnAdressButton_Click(object sender, RoutedEventArgs e)
        {
           
            VehicleOnAdress.VehicleOnAdress vehicleOnAdress = new VehicleOnAdress.VehicleOnAdress();
            vehicleOnAdress.Show();
        }

        private void TaximeterButton_Click(object sender, RoutedEventArgs e)
        {
            TaximeterWindow taximeterWindow = new TaximeterWindow();
            taximeterWindow.Show();
            
        }

        private void DrivingStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            DrivingStatistics drivingStatistics = new DrivingStatistics();
            drivingStatistics.Show();
        }

        private void FastReservationDrivingButton_Click(object sender, RoutedEventArgs e)
        {
            FastReservationDriving fastReservationDriving = new FastReservationDriving();
            fastReservationDriving.Show();

        }

        private void SuperDriverButton_Click(object sender, RoutedEventArgs e)
        {
            SuperDriver superDriver = new SuperDriver();
            superDriver.Show();
        }
    }
}