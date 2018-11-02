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

            items.Add(new sortFood() { Name = "Gà chiên mắm", Quantity = "1" });
            items.Add(new sortFood() { Name = "Thịt kho trứng", Quantity = "2" });

            this.lsVSortFood.ItemsSource = items;

            //this.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void gvMain_Loaded(object sender, RoutedEventArgs e)
        {
           //this.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSoldOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSkip_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class sortFood
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
    }
}