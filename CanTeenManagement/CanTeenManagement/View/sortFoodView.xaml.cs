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
    /// Interaction logic for employeesView.xaml
    /// </summary>
    public partial class sortFoodView : UserControl
    {
        public sortFoodView()
        {
            InitializeComponent();

            List<sortFood> items = new List<sortFood>();

            items.Add(new sortFood() { ID = "TKT", Name = "Thịt kho trứng", Quantity = "1", Time = "12:04", Status = "Còn"});
            items.Add(new sortFood() { ID = "GCM", Name = "Gà chiên mắm", Quantity = "2", Time = "12:05", Status = "Hết"});
            items.Add(new sortFood() { ID = "P", Name = "Phở", Quantity = "1", Time = "12:06", Status = "Còn"});


        }

        private void gvMain_Loaded(object sender, RoutedEventArgs e)
        {
           //this.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSoldOut1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSkip1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNext2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSoldOut2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSkip2_Click(object sender, RoutedEventArgs e)
        {

        }       
    }

    public class sortFood
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
    }
}