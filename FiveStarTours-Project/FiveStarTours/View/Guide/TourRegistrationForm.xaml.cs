// Modeled on CommentForm from InitialProject

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Services;
using FiveStarTours.ViewModel;

namespace FiveStarTours.View
{
    /// <summary>
    /// Interaction logic for TourRegistrationForm.xaml
    /// </summary>
    public partial class TourRegistrationForm : Window
    {
        TourRegistrationFormViewModel viewModel;
        public TourRegistrationForm(User user)
        {
            InitializeComponent();
            viewModel = new TourRegistrationFormViewModel(user);
            DataContext = viewModel;
            (DataContext as TourRegistrationFormViewModel).RequestClose += CloseWindow;
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}
