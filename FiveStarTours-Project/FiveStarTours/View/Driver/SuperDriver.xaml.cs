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
        
        public SuperDriver()
        {
            InitializeComponent();
            DataContext = this;
            
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
                            SuperDriverComboBox.Items.Add(value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }

        private void CheckHereButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = SuperDriverComboBox.SelectedItem.ToString();
            string[] values = selectedItem.Split('|');
            string lastValue = values[values.Length - 1];

            // Assuming the last value is a number, you can convert it to an integer or any other numeric type if needed
            int lastNumber = int.Parse(lastValue);

            if (lastNumber >= 15)
            {
                TextBoxMessage.Text = "Your fast drive number is :" + lastNumber.ToString() + Environment.NewLine + "Yes! You are a super-driver!";
            }
            else
            {
                TextBoxMessage.Text = "Your fast drive number is :" + lastNumber.ToString() + Environment.NewLine + "NO! You are NOT a super-driver!";
            }
        }
    }
}
