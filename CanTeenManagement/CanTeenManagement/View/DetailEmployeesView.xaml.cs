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
using System.Windows.Shapes;

namespace CanTeenManagement.View
{
    /// <summary>
    /// Interaction logic for detailEmployeeView.xaml
    /// </summary>
    public partial class DetailEmployeesView : Window
    {
        public DetailEmployeesView()
        {
            InitializeComponent();
            this.fillComboboxGender();
            this.fillComboboxYear();
        }

        private void fillComboboxYear()
        {
            this.cbbYearOfBirth.Items.Clear();
            int i_EighteenYearLocal = DateTime.Now.Year - 2018 + 2000;

            for (int i = i_EighteenYearLocal; i >= i_EighteenYearLocal - (42 + DateTime.Now.Year - 2018); i--)
            {
                this.cbbYearOfBirth.Items.Add(i);
            }

            this.cbbYearOfBirth.SelectedIndex = 0;
        }

        private void fillComboboxGender()
        {
            this.cbbGender.Items.Clear();
            this.cbbGender.Items.Add("Nữ");
            this.cbbGender.Items.Add("Nam");
            this.cbbGender.Items.Add("Khác");

            this.cbbGender.SelectedIndex = 0;
        }
    }
}
