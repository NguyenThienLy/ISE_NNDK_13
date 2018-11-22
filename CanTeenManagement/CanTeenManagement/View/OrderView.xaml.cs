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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanTeenManagement.View
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        private static OrderView instance;

        public static OrderView Instance
        {
            get { if (instance == null) instance = new OrderView(); return OrderView.instance; }

            set { OrderView.instance = value; }
        }

        private OrderView()
        {
            InitializeComponent();
        }
    }
}
