using System;
using System.Windows;
using FiveStarTours.Model;
using FiveStarTours.ViewModel.Guide;

namespace FiveStarTours.View.Guide
{
    /// <summary>
    /// Interaction logic for WholeTourAcceptWindow.xaml
    /// </summary>
    public partial class WholeTourAcceptView : Window
    {
        public WholeTourAcceptView(User user, TourRequest request)
        {
            InitializeComponent();
            DataContext = new WholeTourAcceptViewModel(user,request);
            (DataContext as WholeTourAcceptViewModel).RequestClose += CloseWindow;
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}
