using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CanTeenManagement.View;
using CanTeenManagement.Model;

namespace CanTeenManagement.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region commands.
        public ICommand g_iCm_ClickCloseWindowCommand { get; set; }

        public ICommand g_iCm_ClickMinimizeWindowCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }

        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_SelectedIndexListViewCommand { get; set; }

        public ICommand g_iCm_ClickSettingViewCommand { get; set; }

        public ICommand g_iCm_ClickUserViewCommand { get; set; }
        #endregion

        bool g_b_isLoaded { get; set; }

        public MainViewModel()
        {
            this.g_b_isLoaded = false;

            //CloseWindowCommand = new RelayCommand<MainWindow>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    FrameworkElement window = this.getWindowParent(p);
            //    var w = window as Window;
            //    if (w != null)
            //    {                
            //        w.Close();
            //    }
            //} );

            g_iCm_ClickCloseWindowCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            g_iCm_ClickMinimizeWindowCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.clickMinimizeWindow(p);
            });

            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });

            g_iCm_LoadedCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            g_iCm_SelectedIndexListViewCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.selectedChange(p);
            });

            g_iCm_ClickSettingViewCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.clickSettingsView(p);
            });

            g_iCm_ClickUserViewCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.clickUserView(p);
            });
        }

        FrameworkElement getWindowParent(MainWindow p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }

        private void clickCloseWindow(MainWindow p)
        {
            if (p == null)
                return;

            MainWindow mainWD = MainWindow.Instance;
            mainWD.Close();

            p.Close();
        }

        private void clickMinimizeWindow(MainWindow p)
        {
            if (p == null)
                return;

            p.WindowState = WindowState.Minimized;
        }

        private void mouseLeftButtonDown(MainWindow p)
        {
            if (p == null)
                return;

            p.DragMove();
        }

        private void selectedChange(MainWindow p)
        {
            if (p == null)
                return;

            int i_Index = 0;

            i_Index = p.ListViewMenu.SelectedIndex;
            this.moveCusorMenu(i_Index, p);

            switch (i_Index)
            {
                case 0:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(DashBoardView.Instance);
                    break;
                case 1:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(OrderView.Instance);
                    break;
                case 2:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(CustomersView.Instance);
                    break;
                case 3:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(new StatisticView());
                    break;
                case 4:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(new ReportView());
                    break;
                case 5:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(new callFoodView());
                    break;
                case 6:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(EmployeesView.Instance);
                    break;
                default:
                    p.GridMainWindow.Children.Add(new SortFoodView());
                    break;
            }
        }

        private void loaded(MainWindow p)
        {
            if (p == null)
                return;

            if (this.g_b_isLoaded == true)
                return;

            this.g_b_isLoaded = true;

            p.Hide();

            var loginV = new LoginView();
            loginV.ShowDialog();

            if (loginV.DataContext == null)
                return;

            var loginVM = loginV.DataContext as LoginViewModel;

            if (loginVM.g_b_isLogin)
            {
                p.Show();

                // Load dash board.
                p.GridMainWindow.Children.Clear();
                p.GridMainWindow.Children.Add(DashBoardView.Instance);
            }
            else
            {
                p.Close();
            }
        }

        private void clickSettingsView(MainWindow p)
        {
            if (p == null)
                return;

            p.Opacity = 0.5;
            var settingV = new SettingsView();
            settingV.ShowDialog();
            p.Opacity = 100;
        }

        private void clickUserView(MainWindow p)
        {
            if (p == null)
                return;

            p.Opacity = 0.5;
            var detailEmployee = new DetailEmployeesView();
            detailEmployee.ShowDialog();
            p.Opacity = 100;
        }

        public void moveCusorMenu(int index, MainWindow p)
        {
            if (p == null)
                return;

            p.TransittionigContentSlide.OnApplyTemplate();
            p.GridCusor.Margin = new Thickness(0, (190 + index * 60), 0, 0);
        }
    }
}
