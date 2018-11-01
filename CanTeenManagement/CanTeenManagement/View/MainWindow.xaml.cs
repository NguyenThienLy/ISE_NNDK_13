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

namespace CanTeenManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShutDown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i_Index = ListViewMenu.SelectedIndex;
            this.moveCusorMenu(i_Index);

            switch(i_Index)
            {
                case 0:
                    this.GridMainWindow.Children.Clear();
                    this.GridMainWindow.Children.Add(new dashBoardView());
                    break;
                case 1:
                    this.GridMainWindow.Children.Clear();
                    this.GridMainWindow.Children.Add(new OrderView());
                  
                    break;
                case 2:
                    this.GridMainWindow.Children.Clear();
                    this.GridMainWindow.Children.Add(new customersView());
                    break;
                case 3:
                    this.GridMainWindow.Children.Clear();
                    this.GridMainWindow.Children.Add(new listView());
                    break;
                case 4:
                    this.GridMainWindow.Children.Clear();
                    this.GridMainWindow.Children.Add(new reportView());
                    break;
                case 5:
                    this.GridMainWindow.Children.Clear();
                    this.GridMainWindow.Children.Add(new callFoodView());
                    break;
                case 6:
                    this.GridMainWindow.Children.Clear();
                    this.GridMainWindow.Children.Add(new employeesView());
                    break;
                default:
                    this.GridMainWindow.Children.Add(new sortFoodView());
                    break;
            }
        }

        private void moveCusorMenu(int index)
        {
            this.TransittionigContentSlide.OnApplyTemplate();
            this.GridCusor.Margin = new Thickness(0 , (190 + index * 60), 0,0 );
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            usersView usersV = new usersView();
            usersV.ShowDialog();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            settingsView settingsV = new settingsView();
            settingsV.ShowDialog();
        }
    }
}
