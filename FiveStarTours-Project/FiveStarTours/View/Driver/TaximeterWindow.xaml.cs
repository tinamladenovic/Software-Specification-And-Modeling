using FiveStarTours.Model;
using FiveStarTours.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Threading;
using System.Xml.Linq;


namespace FiveStarTours.View.Driver
{
    /// <summary>
    /// Interaction logic for TaximeterWindow.xaml
    /// </summary>
    public partial class TaximeterWindow : Window, INotifyPropertyChanged
    {
        private readonly TaximeterRepository _taximeterRepository;
        private readonly VehicleOnAdressRepository _vehicleOnAddressRepository;
        public static List<Taximeter> Taximeters { get; set; }

        private DispatcherTimer timer;
        private int timeInSeconds = 0;
        private double pricePerMinute = 0.25;

        private string _time;
        public string Time
        {

            get => _time;
            set
            {
                if (value != _time)
                {
                    _time = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _price;
        public string Price
        {

            get => _price;
            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public TaximeterWindow()
        {
            InitializeComponent();
            

            _vehicleOnAddressRepository = new VehicleOnAdressRepository();
            _taximeterRepository = new TaximeterRepository();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();


        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeInSeconds++;
            double price = timeInSeconds / 60.0 * pricePerMinute;
            TimeTextBox.Text = TimeSpan.FromSeconds(timeInSeconds).ToString(@"hh\:mm\:ss");
            PriceTextBox.Text = price.ToString("C");
        }


        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            /*
            string time = Convert.ToString(timer);
            double price = Convert.ToDouble(PriceTextBox.Text);

            Taximeter newTaximeter = new Taximeter(time , price);
            _taximeterRepository.Save(newTaximeter);
            */

            MessageBox.Show("TIME: " + TimeTextBox.Text + "\nPRICE: " + PriceTextBox.Text);
            Close();
        }

        

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
