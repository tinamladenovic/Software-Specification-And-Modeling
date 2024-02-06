using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FiveStarTours.View.Driver
{
    /// <summary>
    /// Interaction logic for SubstituteDriver.xaml
    /// </summary>
    public partial class SubstituteDriver : Window, INotifyPropertyChanged
    {

        
        private readonly DrivingsRepository _drivingsRepository;
        private ObservableCollection<Drivings> adrese = new ObservableCollection<Drivings>();


        public static List<Drivings> Drivings { get; set; }
        private int CounterPoints { get; set; } = 0;




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

        private bool _accept;
        public bool Accept
        {

            get => _accept;
            set
            {
                if (value != _accept)
                {
                    _accept = value;
                    OnPropertyChanged();
                }
            }
        }

        public User LoggedInUser { get; set; }
        public bool IfSuperDriver { get; set; }
        public SubstituteDriver(User user)
        {
            InitializeComponent();
            DataContext = this;

           
            _drivingsRepository = new DrivingsRepository();

            //Reserved driving in Grid (Name)
            //Drivings = _drivingsRepository.GetAll();

            

            LoggedInUser = user;

            LoggedInUser.Super = true;

            if(LoggedInUser.Super == true)

            {
                IfSuperDriver = true;
            } 
            else
            {
                IfSuperDriver = false;
            }

            
            adreseDataGrid.ItemsSource = adrese;
            adrese.Add(new Drivings("Bulevar Cara Lazara 101"));
            adrese.Add(new Drivings("Sentadrejski put 50"));
            adrese.Add(new Drivings("Temerinska 56"));
            adrese.Add(new Drivings("Rumenacka 345"));
            adrese.Add(new Drivings("Futoska ulica 222"));
            

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CounterPoints += 5;
            MessageBox.Show("Vožnja je prihvaćena. Dobili ste 5 poena.");

            Drivings selectedAddress = adreseDataGrid.SelectedItem as Drivings;
            if (selectedAddress != null)
            {
                adrese.Remove(selectedAddress);
            }

        }


    }
}
