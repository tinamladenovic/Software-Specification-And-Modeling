using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
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

namespace FiveStarTours.View.Visitor
{
    /// <summary>
    /// Interaction logic for TourRequestsListingView.xaml
    /// </summary>
    public partial class TourRequestsListingView : Window
    {
        public User LoggedInUser { get; set; }
        public static ObservableCollection<TourRequest> Requests { get; set; }
        public TourRequest SelectedRequest { get; set; }


        private readonly TourRequestService _repository;
        public TourRequest tourRequest { get; set; }
        public TourRequestsListingView(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            _repository = new TourRequestService();
            Requests = new ObservableCollection<TourRequest>(_repository.GetAll());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
