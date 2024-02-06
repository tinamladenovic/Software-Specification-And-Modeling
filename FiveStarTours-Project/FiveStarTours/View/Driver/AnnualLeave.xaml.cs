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
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System;
using System.IO;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using FiveStarTours.View.Visitor;

namespace FiveStarTours.View.Driver
{
    /// <summary>
    /// Interaction logic for AnnualLeave.xaml
    /// </summary>
    /// 

    public partial class AnnualLeave : Window, INotifyPropertyChanged
    {


        private DateTime _startDate;
        public DateTime StartDate
        {

            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {

            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public AnnualLeave()
        {
            InitializeComponent();
            DataContext = this;


        }

        List<DateTime> DateTimes1 = new List<DateTime>();
        List<DateTime> DateTimes2 = new List<DateTime>();

        private void CheckTimelineButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select date first!");
                return;
            }
            DateTime now = DateTime.Now;
            DateTime startDate = (DateTime)StartDatePicker.SelectedDate;
            DateTime endDate = (DateTime)EndDatePicker.SelectedDate;
            DateTime newDate1 = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            DateTimes1.Add(newDate1);
            DateTime newDate2 = new DateTime(endDate.Year, endDate.Month, endDate.Day);
            DateTimes2.Add(newDate2);

            string csvPath = "../../../Resources/Data/reservedDrivings.csv";

            if (File.Exists(csvPath))
            {
                string[] lines = File.ReadAllLines(csvPath);
                foreach (string line in lines)
                {
                    string[] values = line.Split('|');

                    if (values.Length >= 5)
                    {
                        DateTime date;
                        if (DateTime.TryParse(values[4], out date))
                        {

                            TimeSpan difference = startDate - now;
                            if (difference.TotalHours < 48)
                            {
                                // Odmor se smatra hitnim
                                PorukaTextBox.Text = "Hitno tražite godišnji odmor. Vaše vožnje se automatski prebacuju slobodnim vozačima. Odmor je odobren.";
                            }
                            else
                            {
                                //ako nije hitno
                                if (date >= startDate && date <= endDate)
                                {
                                    PorukaTextBox.Text = "Imate zakazane voznje u ovom vremenskom intervalu. Vaše vožnje se šalju na odobrenje drugim vozačima.";
                                }
                                else
                                {
                                    PorukaTextBox.Text = "Nemate zakazane voznje u ovom vremenskom intervalu. Odmor je odobren.";
                                }
                            }


                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("CSV fajl ne postoji.");
            }

        }
    }
}
