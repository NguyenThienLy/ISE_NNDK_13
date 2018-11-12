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
    public class LoginViewModel : BaseViewModel
    {
        public bool b_isLogin_g { get; set; }

        private string _str_userName_g;
        public string str_userName_g { get => _str_userName_g; set { _str_userName_g = value; OnPropertyChanged(); } }

        private string _str_password_g;
        public string str_password_g { get => _str_password_g; set { _str_password_g = value; OnPropertyChanged(); } }

        #region commands.
        public ICommand iCm_ClickCloseCommand_g { get; set; }

        public ICommand iCm_ClickLoginCommand_g { get; set; }

        public ICommand iCm_PasswordChangedCommand_g { get; set; }

        public ICommand iCm_MouseDownCommand_g { get; set; }
        #endregion

        public LoginViewModel()
        {
            this.b_isLogin_g = false;

            iCm_ClickCloseCommand_g = new RelayCommand<LoginView>((p) => { return true; }, (p) =>
            {
                this.clickClose(p);
            });

            iCm_ClickLoginCommand_g = new RelayCommand<LoginView>((p) => { return true; }, (p) =>
            {
                this.clickLogin(p);
            });

            iCm_PasswordChangedCommand_g = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                this.passwordChanged(p);
            });

            iCm_MouseDownCommand_g = new RelayCommand<LoginView>((p) => { return true; }, (p) =>
            {
                this.mouseDown(p);
            });

        }

        private void clickClose(LoginView p)
        {
            p.Close();
        }

        private void passwordChanged(PasswordBox p)
        {
            this.str_password_g = p.Password;
        }

        // Hàm hỗ trợ cho iCm_LoginWindowCommand_g. 
        private void clickLogin(LoginView p)
        {
            if (p == null)
                return;

            var quatityAccount_l = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == this.str_userName_g && employee.PASSWORD == this.str_password_g).Count();

            if (quatityAccount_l > 0)
            {
                this.b_isLogin_g = true;
                p.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void mouseDown(LoginView p)
        {
            p.DragMove();
        }
    }
}
