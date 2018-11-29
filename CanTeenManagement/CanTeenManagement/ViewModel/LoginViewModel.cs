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
    public class LoginViewModel : BaseViewModel
    {
        public bool g_b_isLogin { get; set; }

        private string _g_str_userName;
        public string g_str_userName { get => _g_str_userName; set { _g_str_userName = value; OnPropertyChanged(); } }

        private string _g_str_password;
        public string g_str_password { get => _g_str_password; set { _g_str_password = value; OnPropertyChanged(); } }

        #region commands.
        public ICommand g_iCm_ClickCloseCommand { get; set; }

        public ICommand g_iCm_ClickLoginCommand { get; set; }

        public ICommand g_iCm_PasswordChangedCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }
        #endregion

        public LoginViewModel()
        {
            this.g_b_isLogin = false;

            g_iCm_ClickCloseCommand = new RelayCommand<LoginView>((p) => { return true; }, (p) =>
            {
                this.clickClose(p);
            });

            g_iCm_ClickLoginCommand = new RelayCommand<LoginView>((p) => { return true; }, (p) =>
            {
                this.clickLogin(p);
            });

            g_iCm_PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                this.passwordChanged(p);
            });

            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<LoginView>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });

        }

        private void clickClose(LoginView p)
        {
            if (p == null)
                return;

            p.Close();
        }

        private void passwordChanged(PasswordBox p)
        {
            this.g_str_password = p.Password;
        }

        // Hàm hỗ trợ cho iCm_LoginWindowCommand_g. 
        private void clickLogin(LoginView p)
        {
            if (p == null)
                return;

            var quatityAccount_l = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == this.g_str_userName && employee.PASSWORD == this.g_str_password).Count();

            if (quatityAccount_l > 0)
            {
                this.g_b_isLogin = true;

                staticVarClass.account_userName = this.g_str_userName;

                p.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void mouseLeftButtonDown(LoginView p)
        {
            if (p == null)
                return;

            p.DragMove();
        }
    }
}
