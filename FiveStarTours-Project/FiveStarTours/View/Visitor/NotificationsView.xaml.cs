using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using FiveStarTours.View.Guide;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FiveStarTours.View.Visitor
{
    /// <summary>
    /// Interaction logic for NotificationsView.xaml
    /// </summary>
    public partial class NotificationsView : Window
    {
        public User LoggedInUser { get; set; }
        public readonly GiftCardService _repository;
        public Tour SelectedTour { get; set; }
        public string UserResponse { get; set; }
        public NotificationsView(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            GiftCardService repository = new GiftCardService();

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            UserResponse = "false";
            MessageBox.Show("OK");

        }

        private int yesCounter = 0;
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            UserResponse = "yes";
            yesCounter++;
            MessageBox.Show("OK");
            if (yesCounter == 5)
            {
                MessageBox.Show("Congratulation! You have just received a voucher that you can use for any tour. " +
                    "The voucher has been added to your list of vouchers");
                GiftCard giftCard = new GiftCard(LoggedInUser.Id, DateTime.Now.AddMonths(6));
                _repository.Save(giftCard);
                yesCounter = 0;
            }
            Close();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            VisitorMainWindow visitorMainView = new VisitorMainWindow(LoggedInUser);
            visitorMainView.Show();
        }

        private void GiftCardsButton_Click(object sender, RoutedEventArgs e)
        {
            GiftCardsListingView giftCards = new GiftCardsListingView(LoggedInUser);
            giftCards.Show();
        }

        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {
            ToursListingView toursListing = new ToursListingView(LoggedInUser);
            toursListing.Show();
        }
        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            foreach (Window window in Application.Current.Windows)
            {
                if (window != mainWindow)
                {
                    window.Close();
                }
            }
            mainWindow.Show();
        }

        private void ReservedToursButton_Click(object sender, RoutedEventArgs e)
        {
            ReservedToursView reservedTours = new ReservedToursView(LoggedInUser);
            reservedTours.Show();
        }
    }
}