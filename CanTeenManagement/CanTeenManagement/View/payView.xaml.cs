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

namespace CanTeenManagement.View
{
    /// <summary>
    /// Interaction logic for payView.xaml
    /// </summary>
    public partial class PayView : Window
    {
        public PayView()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void gvMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}