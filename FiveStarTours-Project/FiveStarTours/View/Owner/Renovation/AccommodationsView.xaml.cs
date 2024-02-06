using FiveStarTours.Model;
using FiveStarTours.Services;
using FiveStarTours.Services;
using FiveStarTours.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FiveStarTours.View.Owner.Renovation
{
    /// <summary>
    /// Interaction logic for RenovationAccommodations.xaml
    /// </summary>
    public partial class AccommodationsView : Window, INotifyPropertyChanged
    {
        public User LoggedInUser;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        private readonly AccommodationsService accommodationService;

        public AccommodationsView()
        {
            InitializeComponent();
            DataContext = this;

            accommodationService = new AccommodationsService();
            Accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());


        }

        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        private void ChooseButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedAccommodation!= null)
            {
                SchedulingView scheduling = new SchedulingView(SelectedAccommodation);
                scheduling.Show();
                Close();
            }
            else
            {
                return;
            }
            
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
            Close();
        }
    }
}
