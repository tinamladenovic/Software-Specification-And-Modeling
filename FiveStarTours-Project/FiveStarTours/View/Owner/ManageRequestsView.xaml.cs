using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace FiveStarTours.View.Owner
{
    /// <summary>
    /// Interaction logic for ManageRequestsView.xaml
    /// </summary>
    public partial class ManageRequestsView : Window
    {
        public User LoggedInUser { get; set; }
        public static ObservableCollection<ReservationChange> Requests { get; set; }

        public ReservationChange SelectedRequest { get; set; }   //SELEKTOVANA

        private readonly ReservationChangeService _service;

        
        public ManageRequestsView(User user)
        {
            InitializeComponent();
            DataContext = this;

            LoggedInUser = user;

            _service = new ReservationChangeService();
            Requests = new ObservableCollection<ReservationChange>(_service.GetAllProcessing());
        }

        private void ViewRequestButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRequest != null)
            {
                FullRequestView fullRequest = new FullRequestView(SelectedRequest, LoggedInUser);
                fullRequest.Show();
                Close();
            }
            else
            {
                MessageBox.Show("You must select the request.");
            }


        }

        private void MaybeLaterButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
            Close();
        }
        
    }
}
