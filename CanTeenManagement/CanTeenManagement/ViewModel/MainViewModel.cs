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
        public ICommand iCm_ClickCloseWindowCommand_g { get; set; }

        public ICommand iCm_ClickMinimizeWindowCommand_g { get; set; }

        public ICommand iCm_MouseDownCommand_g { get; set; }

        public ICommand iCm_LoadedCommand_g{ get; set; }

        public ICommand iCm_SelectedIndexListViewCommand_g { get; set; }

        public ICommand iCm_ClickSettingViewCommand_g { get; set; }

        public ICommand iCm_ClickUserViewCommand_g { get; set; }
        #endregion

        bool b_isLoaded_g = false;

        public MainViewModel()
        {
            //CloseWindowCommand = new RelayCommand<MainWindow>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    FrameworkElement window = this.getWindowParent(p);
            //    var w = window as Window;
            //    if (w != null)
            //    {                
            //        w.Close();
            //    }
            //} );

            iCm_ClickCloseWindowCommand_g = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            iCm_ClickMinimizeWindowCommand_g = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.clickMinimizeWindow(p);
            });

            iCm_MouseDownCommand_g = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.mouseDown(p);
            });

            iCm_LoadedCommand_g= new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            iCm_SelectedIndexListViewCommand_g = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.selectedChange(p);
            });

            iCm_ClickSettingViewCommand_g = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                this.clickSettingsView(p);
            });

            iCm_ClickUserViewCommand_g = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
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
            p.Close();
        }

        private void clickMinimizeWindow(MainWindow p)
        {
            p.WindowState = WindowState.Minimized;
        }

        private void mouseDown(MainWindow p)
        {
            p.DragMove();
        }

        private void selectedChange(MainWindow p)
        {
            int i_Index = p.ListViewMenu.SelectedIndex;
            p.moveCusorMenu(i_Index);

            switch (i_Index)
            {
                case 0:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(new DashBoardView());
                    break;
                case 1:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(new OrderView());

                    break;
                case 2:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(new CustomersView());
                    break;
                case 3:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(new listView());
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
                    p.GridMainWindow.Children.Add(new EmployeesView());
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

            this.b_isLoaded_g = true;

            p.Hide();

            var loginV = new LoginView();
            loginV.ShowDialog();

            if (loginV.DataContext == null)
                return;

            var loginVM = loginV.DataContext as LoginViewModel;

            if (loginVM.b_isLogin_g)
            {
                p.Show();
            }
            else
            {
                p.Close();
            }
        }

        private void clickSettingsView(MainWindow p)
        {
            p.Opacity = 0.5;
            var settingV = new SettingsView();
            settingV.ShowDialog();
            p.Opacity = 100;
        }

        private void clickUserView(MainWindow p)
        {
            p.Opacity = 0.5;
            var detailEmployee = new DetailEmployeesView();
            detailEmployee.ShowDialog();
            p.Opacity = 100;
        }
    }
}
