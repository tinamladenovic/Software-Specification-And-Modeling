using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using FiveStarTours.ViewModel.Guide;

namespace FiveStarTours.View.Guide
{
    /// <summary>
    /// Interaction logic for Reviews.xaml
    /// </summary>
    public partial class Reviews : Window
    {
        public Reviews(User user)
        {
            InitializeComponent();
            DataContext = new ReviewsViewModel(user);
        }
    }
}