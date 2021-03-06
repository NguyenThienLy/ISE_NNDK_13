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
    /// Interaction logic for dashBoardView.xaml
    /// </summary>
    public partial class DashBoardView : UserControl
    {
        private static DashBoardView instance;

        public static DashBoardView Instance
        {
            get
            {
                if (instance == null) instance = new DashBoardView();
                return DashBoardView.instance;
            }

            set { DashBoardView.instance = value; }
        }

        private DashBoardView()
        {
            InitializeComponent();
        }
    }
}
