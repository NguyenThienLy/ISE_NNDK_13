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
        public StatisticView()
        {
            InitializeComponent();
            ShowChart();
        }

        private void ShowChart()
        {
            List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Tháng 6", 20),
                new KeyValuePair<string, int>("Tháng 7", 36),
                new KeyValuePair<string, int>("Tháng 8", 89),
                new KeyValuePair<string, int>("Tháng 9", 170),
                new KeyValuePair<string, int>("Tháng 10", 140)
            };
 
            lineChart.DataContext = MyValue;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
