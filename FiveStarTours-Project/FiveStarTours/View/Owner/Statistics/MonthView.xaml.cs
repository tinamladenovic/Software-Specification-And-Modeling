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

namespace FiveStarTours.View.Owner.Statistics
{
    /// <summary>
    /// Interaction logic for MonthView.xaml
    /// </summary>
    public partial class MonthView : Window
    {
        public User LoggedInUser { get; set; }
        public MonthView()
        {
            InitializeComponent();
            DataContext = this;

        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            ActionBarView action = new ActionBarView(LoggedInUser);
            action.Show();
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
