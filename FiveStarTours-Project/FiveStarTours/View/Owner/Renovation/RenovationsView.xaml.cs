using FiveStarTours.Model;
using FiveStarTours.Services;
using FiveStarTours.View.Traveler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FiveStarTours.View.Owner.Renovation
{
    /// <summary>
    /// Interaction logic for RenovationsView.xaml
    /// </summary>
    public partial class RenovationsView : Window
    {
        public User LoggedInUser { get; set; }
        public static ObservableCollection<Renovations> RenovationsAcc { get; set; }

        public Renovations SelectedRenovation { get; set; }   //SELEKTOVANA
        private readonly RenovationService _renovationsService;
        public RenovationsView()
        {
            InitializeComponent();
            DataContext = this;

            _renovationsService = new RenovationService();

            RenovationsAcc = new ObservableCollection<Renovations>(_renovationsService.GetAll());

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRenovation != null)
            {

                if (_renovationsService.IsAbleToCancel(SelectedRenovation.Id))
                {
                    _renovationsService.Delete(SelectedRenovation);
                    RenovationsAcc.Remove(SelectedRenovation);
                }
                else
                {
                    MessageBox.Show("You are not able to cancel this renovation");
                }

                RenovationsView renovation = new RenovationsView();
                renovation.Show();
                Close();

            }
            else
            {
                MessageBox.Show("You must select renovation.");
            }
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationsView accommodations = new AccommodationsView();
            accommodations.Show();
            Close();
        }

        


        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
            Close();
        }
    }
}
