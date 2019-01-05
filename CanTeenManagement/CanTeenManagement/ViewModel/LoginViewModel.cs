using CanTeenManagement.CO;
using CanTeenManagement.Model;
using CanTeenManagement.View;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CanTeenManagement.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool g_b_isLogin { get; set; }

        private bool _g_b_isChecked;
        public bool g_b_isChecked { get => _g_b_isChecked; set { _g_b_isChecked = value; OnPropertyChanged(); } }

        private string _g_str_textUsername;
        public string g_str_textUsername
        {
            get => _g_str_textUsername;
            set
            {
                _g_str_textUsername = value;
                OnPropertyChanged();

                if (this.g_passwordBox != null)
                    this.g_passwordBox.Password = string.Empty;

                if (_g_str_textUsername != string.Empty)
                    this.getPassword(_g_str_textUsername);
            }
        }

        private string _g_str_username;
        public string g_str_username { get => _g_str_username; set { _g_str_username = value; OnPropertyChanged(); } }

        private string _g_str_password;
        public string g_str_password { get => _g_str_password; set { _g_str_password = value; OnPropertyChanged(); } }

        private List<string> _g_listUsernames;
        public List<string> g_listUsernames
        {
            get => _g_listUsernames;
            set
            {
                _g_listUsernames = value;
                OnPropertyChanged();
            }
        }

        bool g_b_isClickLogin = false;
        PasswordBox g_passwordBox = null;

        #region commands.
        public ICommand g_iCm_ClickCloseCommand { get; set; }

        public ICommand g_iCm_ClickLoginCommand { get; set; }

        public ICommand g_iCm_PasswordChangedCommand { get; set; }

        public ICommand g_iCm_SelectionChangedCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }

        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_LoadedPasswordBoxCommand { get; set; }
        #endregion

        public LoginViewModel()
        {
            this.inItSupport();

            g_iCm_LoadedPasswordBoxCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                this.g_passwordBox = p;
                this.loadUsername();
            });

            g_iCm_LoadedCommand = new RelayCommand<LoginView>((p) => { return true; }, (p) =>
            {
                this.loaded();
            });

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

            g_iCm_SelectionChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                this.selectedUsernameChanged(p);
            });

            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<LoginView>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });
        }

        private void inItSupport()
        {
            this.g_b_isLogin = false;
            this.g_b_isClickLogin = false;
            this.g_str_username = string.Empty;
            this.g_str_password = string.Empty;
            this.g_str_textUsername = string.Empty;
            //this.loadUsername();
        }

        private void loaded()
        {
            if (this.g_b_isClickLogin)
            {
                this.loadUsername();
                this.g_b_isClickLogin = false;
            }
        }

        private void saveAccount()
        {
            if (this.g_b_isChecked == true && g_str_password != string.Empty)
            {
                string str_FilePathTemp = staticVarClass.linkFile_account;

                if (System.IO.File.Exists(str_FilePathTemp))
                {
                    string str_IsCheckTemp = "Yes";
                    string str_AccountTemp = string.Empty;
                    string[] str_ArrValueTemp;
                    bool b_ExistTemp = false;
                    bool b_UpdateAccTemp = false;
                    string str_BeforeUpdatedAccTemp = string.Empty;

                    FileStream fs_FileTemp = new FileStream(str_FilePathTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    StreamReader strRe_ReaderTemp = new StreamReader(fs_FileTemp);
                    StreamWriter strWr_WriterTemp = new StreamWriter(fs_FileTemp);

                    // Kiểm tra username và password hiện tại đã lưu trong file chưa.
                    while ((str_AccountTemp = strRe_ReaderTemp.ReadLine()) != null)
                    {
                        if (str_AccountTemp.Trim() != string.Empty)
                        {
                            str_ArrValueTemp = str_AccountTemp.Trim().Split(' ');
                            if (str_ArrValueTemp.Count() == 2)
                            {
                                if (str_ArrValueTemp[0] == g_str_username)
                                {
                                    //i_LineLength = str_AccountTemp.Length + 2;
                                    b_UpdateAccTemp = true;
                                    break;
                                }

                                if (str_ArrValueTemp[0] == g_str_username && str_ArrValueTemp[1] == g_str_password)
                                {
                                    b_ExistTemp = true;
                                    break;
                                }
                            }

                            if (str_AccountTemp == "Yes" || str_AccountTemp == "No")
                                str_BeforeUpdatedAccTemp += str_AccountTemp; // Lấy chữ "Yes"/ "No".
                            else
                                str_BeforeUpdatedAccTemp += "\r\n" + str_AccountTemp;
                        }
                    }

                    if (b_UpdateAccTemp == true)
                    {
                        fs_FileTemp.Seek(0, SeekOrigin.Current);

                        string str_AfterUpdatedAccTemp = strRe_ReaderTemp.ReadToEnd();
                        string newLog = str_BeforeUpdatedAccTemp + "\r\n" + str_AfterUpdatedAccTemp;

                        fs_FileTemp.SetLength(0);
                        strWr_WriterTemp.Write(newLog);

                        strWr_WriterTemp.Close();
                        strRe_ReaderTemp.Close();
                        fs_FileTemp.Close();
                    }

                    if (b_UpdateAccTemp != true)
                    {
                        strWr_WriterTemp.Close();
                        strRe_ReaderTemp.Close();
                        fs_FileTemp.Close();
                    }

                    if (b_ExistTemp == false) // Nếu chưa tồn tại thì thêm vào file.
                    {
                        FileStream _fs_FileTemp = new FileStream(str_FilePathTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                        StreamReader _strRe_ReaderTemp = new StreamReader(_fs_FileTemp);
                        StreamWriter _strWr_WriterTemp = new StreamWriter(_fs_FileTemp);

                        _fs_FileTemp.Seek(0, SeekOrigin.Begin);
                        _strRe_ReaderTemp.ReadLine();
                        string newLog = str_IsCheckTemp + "\r\n" + g_str_textUsername + " " + g_str_password + "\r\n" + _strRe_ReaderTemp.ReadToEnd();
                        _fs_FileTemp.SetLength(0);
                        _strWr_WriterTemp.Write(newLog);

                        _strWr_WriterTemp.Close();
                        _strRe_ReaderTemp.Close();
                        _fs_FileTemp.Close();
                    }

                    staticVarClass.account_userName = g_str_username;
                    staticVarClass.account_password = g_str_password;
                }
                else
                {
                    MessageBox.Show("File không tồn tại!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (g_b_isChecked == false)
            {
                string str_FilePathTemp = staticVarClass.linkFile_account;

                if (System.IO.File.Exists(str_FilePathTemp))
                {
                    string str_IsCheckTemp = "No";

                    StreamWriter str_Wr = new StreamWriter(str_FilePathTemp);

                    str_Wr.Write(str_IsCheckTemp);

                    str_Wr.Flush();
                    str_Wr.Close();

                    staticVarClass.account_userName = g_str_username;
                    staticVarClass.account_password = g_str_password;
                }
                else
                {
                    MessageBox.Show("File không tồn tại!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void loadUsername()
        {
            string str_FilePathLocal = staticVarClass.linkFile_account;

            if (System.IO.File.Exists(str_FilePathLocal))
            {
                StreamReader strRd_Reader = new StreamReader(str_FilePathLocal);

                string str_RememberAccountLocal = string.Empty;
                string str_LineLocal = string.Empty;
                string[] str_ArrValueLocal;
                List<string> lstStr_UsernameLocal = new List<string>();

                str_RememberAccountLocal = strRd_Reader.ReadLine();

                // Xử lý check ô remember account.
                if (str_RememberAccountLocal == "Yes")
                    this.g_b_isChecked = true;
                else
                    this.g_b_isChecked = false;

                // Xử lý nạp ô combobox name.
                while ((str_LineLocal = strRd_Reader.ReadLine()) != null)
                {
                    if (str_LineLocal.Trim() != string.Empty)
                    {
                        str_ArrValueLocal = str_LineLocal.Trim().Split(' ');
                        if (str_ArrValueLocal.Count() == 2)
                        {
                            lstStr_UsernameLocal.Add(str_ArrValueLocal[0]);
                        }
                    }
                }

                strRd_Reader.Close();

                this.g_listUsernames = lstStr_UsernameLocal;
                if (this.g_listUsernames.Count > 0)
                    this.g_str_username = this.g_listUsernames[0];

                if (this.g_str_username != string.Empty)
                    this.getPassword(this.g_str_username);
                //this.cbbUsername.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //this.cbbUsername.AutoCompleteSource = AutoCompleteSource.ListItems;

                if (this.g_b_isClickLogin == true)
                {
                    if (staticVarClass.account_userName != string.Empty && this.g_b_isChecked == true)
                        this.g_str_username = this.g_listUsernames[this.g_listUsernames.IndexOf(staticVarClass.account_userName)];
                    else if (staticVarClass.account_userName != string.Empty && this.g_b_isChecked == false)
                    {
                        this.g_str_username = staticVarClass.account_userName;
                    }
                }
            }
            else
            {
                MessageBox.Show("File không tồn tại!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.g_listUsernames = null;
            }
        }

        private void getPassword(string username)
        {
            string str_FilePathLocal = staticVarClass.linkFile_account;

            if (System.IO.File.Exists(str_FilePathLocal))
            {
                StreamReader strRd_Reader = new StreamReader(staticVarClass.linkFile_account);

                string str_LineLocal = string.Empty;
                string[] strArr_ValueLocal;

                while ((str_LineLocal = strRd_Reader.ReadLine()) != null)
                {
                    if (str_LineLocal.Trim() != string.Empty)
                    {
                        strArr_ValueLocal = str_LineLocal.Split(' ');
                        if (strArr_ValueLocal.Count() == 2)
                        {
                            if (strArr_ValueLocal[0] == username)
                            {
                                if (this.g_passwordBox != null)
                                {
                                    this.g_passwordBox.Password = strArr_ValueLocal[1];
                                    this.g_str_password = this.g_passwordBox.Password;
                                }
                                break;
                            }
                        }
                    }
                }

                strRd_Reader.Close();
            }
            else
            {
                MessageBox.Show("File không tồn tại!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void selectedUsernameChanged(ComboBox p)
        {
            if (this.g_passwordBox != null)
                this.g_passwordBox.Password = string.Empty;

            if (this.g_str_username != string.Empty)
                this.getPassword(this.g_str_username);

        }

        // Hàm hỗ trợ cho iCm_LoginWindowCommand_g. 
        private void clickLogin(LoginView p)
        {
            if (p == null)
                return;

            this.saveAccount();
            this.g_b_isClickLogin = true;

            int l_quantityAccount = 0;

            using (var DB = new QLCanTinEntities())
            {
                l_quantityAccount = DB.EMPLOYEEs
                   .Where(employee => employee.ID == this.g_str_textUsername
                   && employee.PASSWORD == this.g_str_password).Count();
            }

            if (l_quantityAccount > 0)
            {
                this.g_b_isLogin = true;

                staticVarClass.account_userName = this.g_str_textUsername;
                staticVarClass.account_password = this.g_str_password;

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
