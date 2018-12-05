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
using CanTeenManagement.CO;

namespace CanTeenManagement.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private string _g_str_imageLink;
        public string g_str_imageLink
        {
            get => _g_str_imageLink;
            set
            { _g_str_imageLink = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_fullName;
        public string g_str_fullName
        {
            get => _g_str_fullName;
            set
            {
                _g_str_fullName = value;
                OnPropertyChanged();
            }
        }

        bool g_b_isLoaded { get; set; }

        #region commands.
        public ICommand g_iCm_ClickCloseWindowCommand { get; set; }

        public ICommand g_iCm_ClickMinimizeWindowCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }

        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_SelectedIndexListViewCommand { get; set; }

        public ICommand g_iCm_ClickSettingViewCommand { get; set; }

        public ICommand g_iCm_ClickUserViewCommand { get; set; }
        #endregion

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

            staticVarClass.screen_Top = (int)p.Top;
            staticVarClass.screen_Left = (int)p.Left + 220;
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
                    p.GridMainWindow.Children.Add(StatisticView.Instance);
                    break;
                case 4:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(ReportView.Instance);
                    break;
                case 5:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(CallFoodView.Instance);
                    break;
                case 6:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(EmployeesView.Instance);
                    break;
                default:
                    p.GridMainWindow.Children.Clear();
                    p.GridMainWindow.Children.Add(SortFoodView.Instance);
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

                this.loadUserInfo();

                // Load dash board.
                p.GridMainWindow.Children.Clear();
                p.GridMainWindow.Children.Add(DashBoardView.Instance);

                staticVarClass.screen_Top = (int)p.Top;
                staticVarClass.screen_Left = (int)p.Left + 220;
            }
            else
            {
                p.Close();
            }
        }

        // Load image link and full name.
        private void loadUserInfo()
        {
            var l_userInfo = dataProvider.Instance.DB.EMPLOYEEs
               .Where(user => user.ID == staticVarClass.account_userName)
               .Select(user => new { user.FULLNAME, user.IMAGELINK }).SingleOrDefault();

            if (l_userInfo != null)
            {
                this.g_str_fullName = l_userInfo.FULLNAME.Trim();
                this.g_str_imageLink = l_userInfo.IMAGELINK.Trim();
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
            p.GridCusor.Margin = new Thickness(0, (index * 60), 0, 0);
        }
    }
}
