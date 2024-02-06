using System.Windows;
using FiveStarTours.Model;
using FiveStarTours.ViewModel;

namespace FiveStarTours.View
{
    /// <summary>
    /// Interaction logic for Tours.xaml
    /// </summary>
    public partial class Tours : Window
    {
        public Tours(User user)
        {
            InitializeComponent();
            DataContext = new ToursViewModel(user);
        }
    }
}