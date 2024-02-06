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
using FiveStarTours.View;


namespace FiveStarTours.View.Traveler
{
    /// <summary>
    /// Interaction logic for TravelerMain.xaml
    /// </summary>
    public partial class TravelerMain : Window
    {
        public TravelerMain()
        {
            InitializeComponent();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            TravelerViewandSearch tvs = new TravelerViewandSearch();
            tvs.Show();
        }
    }
}
