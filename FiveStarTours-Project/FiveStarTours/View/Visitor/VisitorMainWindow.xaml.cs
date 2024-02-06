using FiveStarTours.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for VisitorMainWindow.xaml
    /// </summary>
    public partial class VisitorMainWindow : Window
    {
        public User LoggedInUser { get; set; }
        public bool notificationReceived = false;
        private string message;

        public VisitorMainWindow(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            
        }

        

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Block window from opening again until notification is received
            e.Cancel = !notificationReceived;
        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {
            //CC.Content = new ToursListingView();
            ToursListingView toursListing = new ToursListingView(LoggedInUser);
            this.Visibility = Visibility.Hidden;
            toursListing.Show();
       }

        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            notificationReceived = Notification.SentNotification;

            // Check if there are any notifications
            if (!notificationReceived || Notification.User.Id != LoggedInUser.Id)
            {
                MessageBox.Show("There are no notifications.", "No Notifications", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                NotificationsView notificationWindow = new NotificationsView(LoggedInUser);
                notificationWindow.ShowDialog();

                if (notificationWindow.UserResponse == "yes")
                {
                    Notification.Answer = true;

                }
                else if (notificationWindow.UserResponse == "no")
                {
                    Notification.Answer = false;
                }

                Notification.SentNotification = false;
            }
        }


        /*private class CC
        {
            public static ToursListingView Content { get; internal set; }
        }*/
    }
}
