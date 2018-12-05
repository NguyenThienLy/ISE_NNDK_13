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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanTeenManagement.View
{
    /// <summary>
    /// Interaction logic for listView.xaml
    /// </summary>
    public partial class StatisticView : UserControl
    {
        private static StatisticView instance;

        public static StatisticView Instance
        {
            get { if (instance == null) instance = new StatisticView(); return StatisticView.instance; }

            set { StatisticView.instance = value; }
        }

        private StatisticView()
        {
            InitializeComponent();
        }
    }
}
