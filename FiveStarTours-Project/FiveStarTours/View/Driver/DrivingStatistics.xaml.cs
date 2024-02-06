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
using System.Windows.Forms.DataVisualization;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.VisualBasic.FileIO;


using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using System.ComponentModel;
using System.Data;
using System.Collections;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Runtime.CompilerServices;

namespace FiveStarTours.View.Driver
{
    /// <summary>
    /// Interaction logic for DrivingStatistics.xaml
    /// </summary>
    public partial class DrivingStatistics : Window
    {
        public  DrivingStatisticsRepository _drivingStatisticsRepository;
        public  DrivingStatisticsRepository2 _drivingStatisticsRepository2;
       

        public static List<DrivingStatisticsData> DrivingStatisticsData { get; set; }
        public static List<DrivingStatisticsData2> DrivingStatisticsData2 { get; set; }

        private string csvFilePath = "../../../Resources/Data/drivingstatistics2.csv"; // path to the CSV file
        private DataTable dataTable = new DataTable();

        private string _drivingYear2;
        public string DrivingYear2
        {

            get => _drivingYear2;
            set
            {
                if (value != _drivingYear2)
                {
                    _drivingYear2 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _drivingNDP;
        public string DrivingNDP
        {

            get => _drivingNDP;
            set
            {
                if (value != _drivingNDP)
                {
                    _drivingNDP = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _january;
        public string January
        {

            get => _january;
            set
            {
                if (value != _january)
                {
                    _january = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _february;
        public string February
        {

            get => _february;
            set
            {
                if (value != _february)
                {
                    _february = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _march;
        public string March
        {

            get => _march;
            set
            {
                if (value != _march)
                {
                    _march = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _april;
        public string April
        {

            get => _april;
            set
            {
                if (value != _april)
                {
                    _april = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _may;
        public string May
        {

            get => _may;
            set
            {
                if (value != _may)
                {
                    _may = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _june;
        public string June
        {

            get => _june;
            set
            {
                if (value != _june)
                {
                    _june = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _july;
        public string July
        {

            get => _july;
            set
            {
                if (value != _july)
                {
                    _july = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _august;
        public string August
        {

            get => _august;
            set
            {
                if (value != _august)
                {
                    _august = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _september;
        public string September
        {

            get => _september;
            set
            {
                if (value != _september)
                {
                    _september = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _october;
        public string October
        {

            get => _october;
            set
            {
                if (value != _october)
                {
                    _october = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _november;
        public string November
        {

            get => _november;
            set
            {
                if (value != _november)
                {
                    _november = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _december;
        public string December
        {

            get => _december;
            set
            {
                if (value != _december)
                {
                    _december = value;
                    OnPropertyChanged();
                }
            }
        }


        public DrivingStatistics()
        {
            InitializeComponent();
            // initialize the DataTable
            dataTable.Columns.Add("Year");
            dataTable.Columns.Add("NDP");
            dataTable.Columns.Add("January");
            dataTable.Columns.Add("February");
            dataTable.Columns.Add("March");
            dataTable.Columns.Add("April");
            dataTable.Columns.Add("May");
            dataTable.Columns.Add("June");
            dataTable.Columns.Add("July");
            dataTable.Columns.Add("August");
            dataTable.Columns.Add("September");
            dataTable.Columns.Add("October");
            dataTable.Columns.Add("November");
            dataTable.Columns.Add("December");
            // load data from CSV file into the DataTable
            LoadData();

            DataContext = this;

            _drivingStatisticsRepository = new DrivingStatisticsRepository();
            _drivingStatisticsRepository2 = new DrivingStatisticsRepository2();

            DrivingStatisticsData = _drivingStatisticsRepository.GetAll();
            DrivingStatisticsData2 = _drivingStatisticsRepository2.GetAll();

            

        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void LoadData()
        {
            try
            {
                using (var reader = new StreamReader(csvFilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        dataTable.Rows.Add(values);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data from CSV file: {ex.Message}");
            }
        }
    

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = ChooseYearComboBox.Text;
            DataView dataView = dataTable.DefaultView;
            dataView.RowFilter = $"Year LIKE '%{searchText}%'";
            DataGridStatistics2.ItemsSource = dataView;   
        }
    }
   
}
    

