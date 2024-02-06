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
using FiveStarTours.Model;
using FiveStarTours.ViewModel.Guide;

namespace FiveStarTours.View.Guide
{
    /// <summary>
    /// Interaction logic for RequestsView.xaml
    /// </summary>
    public partial class RequestsView : Window
    {
        public RequestsViewModel ViewModel { get; set; }
        public RequestsView(User user)
        {
            InitializeComponent();
            ViewModel = new RequestsViewModel(user);
            DataContext = ViewModel;
            (DataContext as RequestsViewModel).RequestClose += CloseWindow;
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}
