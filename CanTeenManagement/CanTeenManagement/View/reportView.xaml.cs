using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for reportView.xaml
    /// </summary>
    public partial class reportView : UserControl
    {
        public reportView()
        {
            InitializeComponent();

            List<User> items = new List<User>();
            items.Add(new User() { Name = "Nhập kho", Time = "01/01/2011", Note = "abc" });
            items.Add(new User() { Name = "Trả tiền điện", Time = "03/03/2002", Note = "def" });
            items.Add(new User() { Name = "Bla bla", Time = "10/10/1111", Note = "mnk"});
            LV_Report.ItemsSource = items;
        }

        public class User
        {
            public string Name { get; set; }

            public string Time { get; set; }

            public string Note { get; set; }

            public Popup Handle { get; set; }
        }

        private void Btn_AddReport_Click(object sender, RoutedEventArgs e)
        {
            // do something
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                //Do your stuff
            }
        }
    }
}
