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
    public partial class CustomersView : UserControl
    {
        private static CustomersView instance;

        public static CustomersView Instance
        {
            get
            {
                if (instance == null) instance = new CustomersView();
                return CustomersView.instance;
            }

            set { CustomersView.instance = value; }
        }

        private CustomersView()
        {
            InitializeComponent();
        }
    }
}
