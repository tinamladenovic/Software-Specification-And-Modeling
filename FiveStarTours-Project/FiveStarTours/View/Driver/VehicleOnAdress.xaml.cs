using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FiveStarTours.Repository;
using FiveStarTours.View;
using Microsoft.Win32;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using FiveStarTours.View.VehicleOnAdress;
using System.IO;
using System.Windows.Threading;
using FiveStarTours.Serializer;
using FiveStarTours.View.Driver;
using Microsoft.VisualBasic.FileIO;
using FiveStarTours.View.Visitor;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Controls;
using User = FiveStarTours.Model.User;

namespace FiveStarTours.View.VehicleOnAdress
{
    /// <summary>
    /// Interaction logic for VehicleOnAdress.xaml
    /// </summary>
    public partial class VehicleOnAdress : Window, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }

        private readonly VehicleOnAdressRepository _vehicleOnAddressRepository;
        private readonly DrivingsRepository _drivingsRepository;
        public static List<Drivings> Drivings { get; set; }
        public VisitorMainWindow secondWindow;
        
       

        private string _name;
        public string Name
        {

            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _fastDriving;
        public bool FastDriving
        {

            get => _fastDriving;
            set
            {
                if (value != _fastDriving)
                {
                    _fastDriving = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _onAdress;
        public bool OnAdress
        {

            get => _onAdress;
            set
            {
                if (value != _onAdress)
                {
                    _onAdress = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isDelay;
        public bool IsDelay
        {

            get => _isDelay;
            set
            {
                if (_onAdress == value) 
                { 
                    OnPropertyChanged();
                }
                if (value != _isDelay)
                {
                    _isDelay = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _enterDelay;
        public int EnterDelay
        {

            get => _enterDelay;
            set
            {
                if (value != _enterDelay)
                {
                    _enterDelay = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _drivingStarts;
        public bool DrivingStarts
        {

            get => _drivingStarts;
            set
            {
                if (value != _drivingStarts)
                {
                    _drivingStarts = value;
                    OnPropertyChanged();
                    
                }
            }
        }
        private int _enterStartPrice;
        public int EnterStartPrice
        {

            get => _enterStartPrice;
            set
            {
                if (value != _enterStartPrice)
                {
                    _enterStartPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public VehicleOnAdress()
        {

            InitializeComponent();
            DataContext = this;


            secondWindow = new VisitorMainWindow(LoggedInUser);


            _vehicleOnAddressRepository = new VehicleOnAdressRepository();
            _drivingsRepository = new DrivingsRepository();

            //from CSV file to SelectDriverComboBox
            string csvFilePath = "../../../Resources/Data/vehicles.csv";

            try
            {
                using (TextFieldParser parser = new TextFieldParser(csvFilePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        // Assuming the values are in the first column of each line
                        if (fields.Length > 0)
                        {
                            string value = fields[0];
                            SelectDriverComboBox.Items.Add(value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
            }
            //Reserved driving in Grid (Name)
            Drivings = _drivingsRepository.GetAll();

            //if OnAdress checked you can't check delay
            if (OnAdressCheckBox != null)
            {
                IsDelayCheckBox.IsReadOnly = true;
                EnterDelayTextBox.IsReadOnly = true;
            }
            else
            {
                IsDelayCheckBox.IsReadOnly = false;   
                EnterDelayTextBox.IsReadOnly = false;
            }
            //if FastDrive checked send notification
            if (FastDriving == true)
            {
                // Slanje poruke drugom prozoru
                string message = "Poruka koju želite poslati";
                //VisitorMainWindow secondWindow = new VisitorMainWindow(message);
                secondWindow.Show();
            }



        }
       
        private Drivings GetSelectedDriving()
        {
            return new Drivings();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            
                
               
            
            //Name
            Drivings name = GetSelectedDriving();
            //Accept fast drive
            bool fastDriving = Convert.ToBoolean(FastDriving);
            //On Adress
            bool isOnAdress = Convert.ToBoolean(OnAdress);
            //Delay
            bool isDelay = Convert.ToBoolean(IsDelay);
            int delay = Convert.ToInt32(EnterDelay);
            //Drivig Starts
            bool drivingStarts = Convert.ToBoolean(DrivingStarts);
            //Enter Start Price
            int enterStartPrice = Convert.ToInt32(EnterStartPrice);
            

            OnAdress newVehicleOnAdress = new OnAdress( name, fastDriving, isOnAdress, isDelay, delay , drivingStarts, enterStartPrice);
            _vehicleOnAddressRepository.Save(newVehicleOnAdress);
            
            
            System.Windows.MessageBox.Show("Data Saved");
            TaximeterWindow taximeterWindow = new TaximeterWindow();
            taximeterWindow.Show();
            

            Close();
        }

        private void SelectDriverComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
