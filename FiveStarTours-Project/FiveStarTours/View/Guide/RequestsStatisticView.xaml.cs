﻿using System;
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
    /// Interaction logic for RequestsStatisticView.xaml
    /// </summary>
    public partial class RequestsStatisticView : Window
    {
        public RequestsStatisticView(User user)
        {
            InitializeComponent();
            DataContext = new RequestsStatisticVIewModel(user);
        }
    }
}
