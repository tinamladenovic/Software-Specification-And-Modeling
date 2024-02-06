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
    /// Interaction logic for GiftCardsListingView.xaml
    /// </summary>
    public partial class GiftCardsListingView : Window
    {     
        public User LoggedInUser { get; set; }
        public static ObservableCollection<GiftCard> GiftCards { get; set; }
        public GiftCard SelectedGiftCard { get; set; }

        private readonly GiftCardService _repository;

        public GiftCardsListingView(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            _repository = new GiftCardService();
            GiftCards = new ObservableCollection<GiftCard>(_repository.GetAll());
        }

        

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ToursListingView tourListing= new ToursListingView(LoggedInUser);
            tourListing.Show();
        }
    }
}
