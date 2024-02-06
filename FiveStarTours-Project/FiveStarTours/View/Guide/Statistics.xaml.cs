using System.Windows;
using FiveStarTours.Model;
using FiveStarTours.ViewModel.Guide;

namespace FiveStarTours.View.Guide
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public Statistics(User user)
        {
            InitializeComponent();
            DataContext = new StatisticViewModel(user);
        }
    }
}