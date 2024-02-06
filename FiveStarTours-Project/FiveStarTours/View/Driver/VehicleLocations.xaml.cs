using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
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
using FiveStarTours.Model;

namespace FiveStarTours.View.Driver
{
    /// <summary>
    /// Interaction logic for VehicleLocations.xaml
    /// </summary>
    public partial class VehicleLocations : Window, INotifyPropertyChanged
    {
        string csvPath = "../../../Resources/Data/vehiclelocations.csv";
        

        int maxRides = 0;
        string addressWithMaxRides = "";
        int minRides = int.MaxValue;
        string addressWithMinRides = "";


        public VehicleLocations()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void CheckLocationsButton_Click(object sender, RoutedEventArgs e)
        {


            // Čitanje CSV fajla
            string[] lines = File.ReadAllLines(csvPath);

            // Prolazak kroz linije CSV fajla
            foreach (string line in lines)
            {
                // Razdvajanje linije na adresu i broj vožnji
                string[] parts = line.Split('|');

                if (parts.Length >= 2)
                {
                    string address = parts[0];
                    int rides;

                    // Provera da li je uspešno izvršena konverzija broja vožnji
                    if (int.TryParse(parts[1], out rides))
                    {
                        // Provera da li je broj vožnji veći od trenutnog maksimuma
                        if (rides > maxRides)
                        {
                            maxRides = rides;
                            addressWithMaxRides = address;
                        }
                    }
                    else
                    {
                        // Obrada ako konverzija nije uspela
                        Console.WriteLine($"Invalid rides value: {parts[1]}");
                    }
                }
            }

            // Prikaz rezultata u TextBox-u
            MaxPopLocTextBox.Text = addressWithMaxRides;


            // Prolazak kroz linije CSV fajla
            foreach (string line in lines)
            {
                // Razdvajanje linije na adresu i broj vožnji
                string[] parts = line.Split('|');

                if (parts.Length >= 2)
                {
                    string address = parts[0];
                    int rides;

                    // Provera da li je uspešno izvršena konverzija broja vožnji
                    if (int.TryParse(parts[1], out rides))
                    {
                        // Provera da li je broj vožnji manji od trenutnog minimuma
                        if (rides < minRides)
                        {
                            minRides = rides;
                            addressWithMinRides = address;
                        }
                    }
                    else
                    {
                        // Obrada ako konverzija nije uspela
                        Console.WriteLine($"Invalid rides value: {parts[1]}");
                    }
                }
            }

            // Prikaz rezultata u TextBox-u
            MinPopLocTextBox.Text = addressWithMinRides;


        }

        
    }
}
