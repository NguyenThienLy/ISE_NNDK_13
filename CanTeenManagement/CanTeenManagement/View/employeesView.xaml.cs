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
    public partial class employeesView : UserControl
    {
        public employeesView()
        {
            InitializeComponent();
            List<employees> items = new List<employees>();

            items.Add(new employees() { ID = "1612365", Name = "Nguyễn Thiên Lý", Phone = "0344374834", Gender = "Nam", Birthday = "01/01/1998", Status = "Chính thức", Salary = "4.000.000 đ" });
            items.Add(new employees() { ID = "1612339", Name = "Trần Khánh Linh", Phone = "0344374834", Gender = "Nữ", Birthday = "01/01/1998", Status = "Bán thời gian", Salary = "2.000.000 đ" });
            items.Add(new employees() { ID = "1612352", Name = "Nguyễn Hà Hoàng Long", Phone = "0344374834", Gender = "Nam", Birthday = "01/01/1998", Status = "Chính thức", Salary = "4.000.000 đ" });

            this.lsVEmployees.ItemsSource = items;

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

        private void btnExport_Click(object sender, RoutedEventArgs e)
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
            detailCustomersView detailCusView = new detailCustomersView();

            detailCusView.ShowDialog();
        }

        private void btnPhanca_Click(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(10, 45, 0, 150);
        }

        private void btnLuong_Click(object sender, RoutedEventArgs e)
        {
            GridCursor.Margin = new Thickness(10 + 150, 45, 0, 150);
        }
        
    }

    public class employees
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Status { get; set; }
        public string Salary { get; set; }
    }
}