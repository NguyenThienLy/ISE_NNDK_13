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
using CanTeenManagement.View;

namespace CanTeenManagement.View
{
    /// <summary>
    /// Interaction logic for customersView.xaml
    /// </summary>
    public partial class customersView : UserControl
    {
        public customersView()
        {
            InitializeComponent();

            List<customers> items = new List<customers>();

            items.Add(new customers() { ID = "1612365", Name = "Nguyễn Thiên Lý", Gmail = "nguyenmit2012@gmail.com", Phone =  "0344374834", PointSave= "20000", TypeCustomer = "4 sao", Password = "pepo2703", Status ="Đang sử dụng" });
            items.Add(new customers() { ID = "1612339", Name = "Trần Khánh Linh", Gmail = "trankhanhlinh98@gmail.com", Phone = "0344374834", PointSave = "20000", TypeCustomer = "3 sao", Password = "duyduy", Status = "Đang sử dụng" });

            this.lsVCustomer.ItemsSource = items;

            this.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.rDefTop.Height = new GridLength(40, GridUnitType.Star);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.rDefTop.Height = new GridLength(40, GridUnitType.Star);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void gvMain_Loaded(object sender, RoutedEventArgs e)
        {
            this.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWd = MainWindow.Instance;
            mainWd.Opacity = 0.5;
            this.Opacity = 0.5;

            detailCustomersView detailCusView = new detailCustomersView();

            detailCusView.ShowDialog();

            mainWd.Opacity = 100;
            this.Opacity = 100;
        }
    }

    public class customers
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Gmail { get; set; }
        public string Phone { get; set; }
        public string PointSave { get; set; }
        public string TypeCustomer { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
    }
}
