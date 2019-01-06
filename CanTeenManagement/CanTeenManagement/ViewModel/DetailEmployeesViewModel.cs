using CanTeenManagement.CO;
using CanTeenManagement.Model;
using CanTeenManagement.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TableDependency.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace CanTeenManagement.ViewModel
{
    public class DetailEmployeesViewModel : BaseViewModel
    {
        private string _g_str_filter;
        public string g_str_filter
        {
            get { return _g_str_filter; }
            set
            {
                _g_str_filter = value;
                OnPropertyChanged();

                ICollectionView view = (ICollectionView)CollectionViewSource.GetDefaultView(_g_listOrders);
                view.Filter = filterIDOrder;
            }
        }

        private ObservableCollection<ORDERINFO> _g_listOrders;
        public ObservableCollection<ORDERINFO> g_listOrders
        {
            get => _g_listOrders;
            set
            {
                _g_listOrders = value;
                OnPropertyChanged();
            }
        }

        #region Các ô trong edit.
        private List<string> _g_listGenders;
        public List<string> g_listGenders
        {
            get => _g_listGenders;
            set
            {
                _g_listGenders = value;
                OnPropertyChanged();
            }
        }

        private List<int> _g_listYearOfBirth;
        public List<int> g_listYearOfBirth
        {
            get => _g_listYearOfBirth;
            set
            {
                _g_listYearOfBirth = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_fullNameEdit;
        public string g_str_fullNameEdit { get => _g_str_fullNameEdit; set { _g_str_fullNameEdit = value; OnPropertyChanged(); } }

        private string _g_str_genderEdit;
        public string g_str_genderEdit { get => _g_str_genderEdit; set { _g_str_genderEdit = value; OnPropertyChanged(); } }

        private Nullable<int> _g_i_yearOfBirthEdit;
        public Nullable<int> g_i_yearOfBirthEdit { get => _g_i_yearOfBirthEdit; set { _g_i_yearOfBirthEdit = value; OnPropertyChanged(); } }

        private string _g_str_phoneEdit;
        public string g_str_phoneEdit
        {
            get => _g_str_phoneEdit;
            set
            {
                long i = 0;
                if (value != string.Empty)
                    if (!long.TryParse(value, out i))
                        value = this.g_str_phoneEdit;

                _g_str_phoneEdit = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_emailEdit;
        public string g_str_emailEdit { get => _g_str_emailEdit; set { _g_str_emailEdit = value; OnPropertyChanged(); } }
        #endregion

        #region Các ô trong gửi mail.
        private List<string> _g_listEmails;
        public List<string> g_listEmails
        {
            get => _g_listEmails;
            set
            {
                _g_listEmails = value;
                OnPropertyChanged();
            }
        }

        private EMPLOYEE _g_selectedEmail;
        public EMPLOYEE g_selectedEmail
        {
            get => _g_selectedEmail;
            set
            {
                _g_selectedEmail = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_titleMail;
        public string g_str_titleMail
        {
            get => _g_str_titleMail;
            set
            {
                _g_str_titleMail = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_contentMail;
        public string g_str_contentMail
        {
            get => _g_str_contentMail;
            set
            {
                _g_str_contentMail = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Các ô trong đổi mật khẩu
        PasswordBox g_passBoxCurrPass = null;
        PasswordBox g_passBoxNewPass = null;
        PasswordBox g_passBoxConfirmNewPass = null;

        private int _g_i_widthButtonChangePassword;
        public int g_i_widthButtonChangePassword
        {
            get { return _g_i_widthButtonChangePassword; }
            set
            {
                _g_i_widthButtonChangePassword = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_currentPassword;
        public string g_str_currentPassword
        {
            get => _g_str_currentPassword;
            set
            {
                _g_str_currentPassword = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_newPassword;
        public string g_str_newPassword
        {
            get => _g_str_newPassword;
            set
            {
                _g_str_newPassword = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_confirmNewPassword;
        public string g_str_confirmNewPassword
        {
            get => _g_str_confirmNewPassword;
            set
            {
                _g_str_confirmNewPassword = value;
                OnPropertyChanged();
            }
        }

        private bool _g_b_isHitTestVisibleNewPass;
        public bool g_b_isHitTestVisibleNewPass
        {
            get { return _g_b_isHitTestVisibleNewPass; }
            set
            {
                _g_b_isHitTestVisibleNewPass = value;
                OnPropertyChanged();
            }
        }

        private bool _g_b_isHitTestVisibleConfirmNewPass;
        public bool g_b_isHitTestVisibleConfirmNewPass
        {
            get { return _g_b_isHitTestVisibleConfirmNewPass; }
            set
            {
                _g_b_isHitTestVisibleConfirmNewPass = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Các thuộc tính của employee.
        private string _g_str_imageLink;
        public string g_str_imageLink { get => _g_str_imageLink; set { _g_str_imageLink = value; OnPropertyChanged(); } }

        private ImageSource _g_imgSrc_employee;
        public ImageSource g_imgSrc_employee
        {
            get => _g_imgSrc_employee;
            set
            {
                _g_imgSrc_employee = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_id;
        public string g_str_id { get => _g_str_id; set { _g_str_id = value; OnPropertyChanged(); } }

        private string _g_str_fullName;
        public string g_str_fullName { get => _g_str_fullName; set { _g_str_fullName = value; OnPropertyChanged(); } }

        private string _g_str_gender;
        public string g_str_gender { get => _g_str_gender; set { _g_str_gender = value; OnPropertyChanged(); } }

        private Nullable<int> _g_i_yearOfBirth;
        public Nullable<int> g_i_yearOfBirth { get => _g_i_yearOfBirth; set { _g_i_yearOfBirth = value; OnPropertyChanged(); } }

        private string _g_str_phone;
        public string g_str_phone
        {
            get => _g_str_phone;
            set
            {
                long i = 0;
                if (value != string.Empty)
                    if (!long.TryParse(value, out i))
                        value = this.g_str_phone;

                _g_str_phone = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_email;
        public string g_str_email { get => _g_str_email; set { _g_str_email = value; OnPropertyChanged(); } }

        private string _g_str_position;
        public string g_str_position { get => _g_str_position; set { _g_str_position = value; OnPropertyChanged(); } }

        private string _g_str_status;
        public string g_str_status { get => _g_str_status; set { _g_str_status = value; OnPropertyChanged(); } }
        #endregion

        DispatcherTimer g_timer = null;
        bool g_b_isAdmin;

        #region commands.
        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_UnloadedCommand { get; set; }

        public ICommand g_iCm_ClickCloseCommand { get; set; }

        public ICommand g_iCm_ClickEditInfoCommand { get; set; }

        public ICommand g_iCm_ClickSaveInfoCommand { get; set; }

        public ICommand g_iCm_ClickExportCommand { get; set; }

        public ICommand g_iCm_ClickSendMailCommand { get; set; }

        public ICommand g_iCm_ClickOpenMailCommand { get; set; }

        public ICommand g_iCm_ClickChangePasswordCommand { get; set; }

        public ICommand g_iCm_ClickOpenChangePasswordCommand { get; set; }

        public ICommand g_iCm_PasswordChangedCurrentPasswordCommand { get; set; }

        public ICommand g_iCm_PasswordChangedNewPasswordCommand { get; set; }

        public ICommand g_iCm_PasswordChangedConfirmNewPasswordCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }

        public ICommand g_iCm_ClickChangeImageCommand { get; set; }

        public ICommand g_iCm_TextChangedFilterCommand { get; set; }

        public ICommand g_iCm_ClickGoBackCommand { get; set; }
        #endregion

        public DetailEmployeesViewModel()
        {
            this.inItSupport();

            g_iCm_LoadedCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            g_iCm_UnloadedCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.unloaded();
            });

            g_iCm_ClickCloseCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            g_iCm_ClickEditInfoCommand = new RelayCommand<DetailEmployeesView>((p) => { return this.checkEditInfo(); }, (p) =>
            {
                this.clickEditInfo(p);
            });

            g_iCm_ClickSaveInfoCommand = new RelayCommand<DetailEmployeesView>((p) => { return this.checkSaveInfo(); }, (p) =>
            {
                this.clickSaveInfo(p);
            });

            g_iCm_ClickExportCommand = new RelayCommand<DetailEmployeesView>((p) => { return checkExport(); }, (p) =>
            {
                this.clickExport();
            });

            g_iCm_ClickOpenMailCommand = new RelayCommand<DetailEmployeesView>((p) => { return checkOpenMail(); }, (p) =>
            {
                this.clickOpenMail(p);
            });

            g_iCm_ClickSendMailCommand = new RelayCommand<DetailEmployeesView>((p) => { return this.checkSendMail(); }, (p) =>
            {
                this.clickSendMail(p);
            });

            g_iCm_ClickOpenChangePasswordCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickOpenChangePassword(p);
            });

            g_iCm_ClickChangePasswordCommand = new RelayCommand<DetailEmployeesView>((p) => { return this.checkChangePassword(); }, (p) =>
            {
                this.clickChangePassword(p);
            });

            g_iCm_PasswordChangedCurrentPasswordCommand = new RelayCommand<System.Windows.Controls.PasswordBox>((p) => { return true; }, (p) =>
            {
                this.passwordChangedCurrentPassword(p);
            });

            g_iCm_PasswordChangedNewPasswordCommand = new RelayCommand<System.Windows.Controls.PasswordBox>((p) => { return true; }, (p) =>
            {
                this.passwordChangedNewPassword(p);
            });

            g_iCm_PasswordChangedConfirmNewPasswordCommand = new RelayCommand<System.Windows.Controls.PasswordBox>((p) => { return true; }, (p) =>
            {
                this.passwordChangedConfirmNewPassword(p);
            });

            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });

            g_iCm_ClickChangeImageCommand = new RelayCommand<DetailEmployeesView>((p) => { return this.checkChangeImage(); }, (p) =>
            {
                this.clickChangeImage();
            });

            g_iCm_TextChangedFilterCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.filterIDOrder();
            });

            g_iCm_ClickGoBackCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickGoBack(p);
            });


        }

        public ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new ObservableCollection<T>(source);
        }

        public void WatchTable()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EPOSEntities"].ConnectionString;
            var tableName = "EMPLOYEE";
            var tableDependency = new SqlTableDependency<EMPLOYEE>(connectionString, tableName);

            tableDependency.OnChanged += OnNotificationReceived;
            tableDependency.Start();
        }

        public void StopTable()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EPOSEntities"].ConnectionString;
            var tableName = "EMPLOYEE";
            var tableDependency = new SqlTableDependency<EMPLOYEE>(connectionString, tableName);

            tableDependency.Stop();
        }

        private void OnNotificationReceived(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<EMPLOYEE> e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                g_timer.Start();
            });
        }

        private void refresh()
        {
            this.loadDataEmployee();
            this.authorize();
            g_timer.Stop();
        }

        private void inItSupport()
        {
            this.g_timer = new DispatcherTimer();
            this.g_timer.Tick += (s, ev) => this.refresh();
            this.g_timer.Interval = new TimeSpan(0, 0, 1);

            this.g_selectedEmail = null;
            this.g_str_titleMail = string.Empty;
            this.g_str_contentMail = string.Empty;
            this.g_b_isAdmin = false;

            this.g_i_widthButtonChangePassword = 0;
            this.resetChangePassword();

            this.loadCombobox();
        }

        private void loadCombobox()
        {
            // Thêm danh sách gender.
            List<string> l_listGenders = new List<string>();
            l_listGenders.Add(staticVarClass.gender_feMale);
            l_listGenders.Add(staticVarClass.gender_male);
            l_listGenders.Add(staticVarClass.gender_different);
            this.g_listGenders = l_listGenders;

            // Thêm danh sách năm sinh.
            List<int> l_listYearOfBirth = new List<int>();
            int l_i_EighteenYearLocal = DateTime.Now.Year - 2018 + 2000;
            for (int i = l_i_EighteenYearLocal; i >= l_i_EighteenYearLocal - (42 + DateTime.Now.Year - 2018); i--)
            {
                l_listYearOfBirth.Add(i);
            }
            this.g_listYearOfBirth = l_listYearOfBirth;

            this.g_listEmails = new List<string>();
        }

        private void loadDataEmployee()
        {
            EmployeesView l_employeesView = EmployeesView.Instance;
            if (l_employeesView.DataContext == null)
                return;
            EmployeesViewModel l_employeesVM = l_employeesView.DataContext as EmployeesViewModel;

            MainWindow mainWd = MainWindow.Instance;
            if (mainWd.DataContext == null)
                return;
            MainViewModel l_mainVM = mainWd.DataContext as MainViewModel;

            // Thêm danh sách email.
            this.g_listEmails.Clear();
            for (int i = 0; i < l_employeesVM.g_listEmployees.Count(); i++)
            {
                string l_str_email = l_employeesVM.g_listEmployees[i].EMAIL.Trim();

                if (l_str_email != string.Empty)
                    this.g_listEmails.Add(l_str_email);
            }

            if (l_mainVM.g_b_detailFromMainWindow == true)
            {
                using (var DB = new QLCanTinEntities())
                {
                    l_employeesVM.g_listEmployees = this.ToObservableCollection<EMPLOYEE>
                    ((from employee in DB.EMPLOYEEs
                      select new
                      {
                          ID = employee.ID.Trim(),
                          PASSWORD = employee.PASSWORD.Trim(),
                          FULLNAME = employee.FULLNAME.Trim(),
                          GENDER = employee.GENDER.Trim(),
                          YEAROFBIRTH = employee.YEAROFBIRTH,
                          PHONE = employee.PHONE.Trim(),
                          EMAIL = employee.EMAIL.Trim(),
                          POSITION = employee.POSITION.Trim(),
                          IMAGELINK = employee.IMAGELINK.Trim(),
                          STATUS = employee.STATUS.Trim()

                      }).ToList().Select(x => new EMPLOYEE
                      {
                          ID = x.ID.Trim(),
                          PASSWORD = x.PASSWORD.Trim(),
                          FULLNAME = x.FULLNAME.Trim(),
                          GENDER = x.GENDER.Trim(),
                          YEAROFBIRTH = x.YEAROFBIRTH,
                          PHONE = x.PHONE.Trim(),
                          EMAIL = x.EMAIL.Trim(),
                          POSITION = x.POSITION.Trim(),
                          IMAGELINK = x.IMAGELINK.Trim(),
                          STATUS = x.STATUS.Trim()
                      }).ToList());
                }

                EMPLOYEE l_employee = null;
                for (int i = 0; i < l_employeesVM.g_listEmployees.Count; i++)
                {
                    if (l_employeesVM.g_listEmployees[i].ID == staticVarClass.account_userName)
                    {
                        l_employee = l_employeesVM.g_listEmployees[i];
                    }
                }

                #region gán giá trị cho các ô
                this.g_str_id = l_employee.ID;
                this.g_str_fullName = l_employee.FULLNAME;
                this.g_str_gender = l_employee.GENDER;
                this.g_i_yearOfBirth = l_employee.YEAROFBIRTH;
                this.g_str_phone = l_employee.PHONE;
                this.g_str_email = l_employee.EMAIL;
                this.g_str_position = l_employee.POSITION;
                this.g_str_status = l_employee.STATUS;
                this.g_str_imageLink = l_employee.IMAGELINK;
                this.g_imgSrc_employee = staticFunctionClass.LoadBitmap(this.g_str_imageLink);
                #endregion

                this.g_i_widthButtonChangePassword = 30;
            }
            else
            {
                #region gán giá trị cho các ô
                this.g_str_id = l_employeesVM.g_str_id;
                this.g_str_fullName = l_employeesVM.g_str_fullName;
                this.g_str_gender = l_employeesVM.g_str_gender;
                this.g_i_yearOfBirth = l_employeesVM.g_i_yearOfBirth;
                this.g_str_phone = l_employeesVM.g_str_phone;
                this.g_str_email = l_employeesVM.g_str_email;
                this.g_str_position = l_employeesVM.g_str_position;
                this.g_str_status = l_employeesVM.g_str_status;
                this.g_str_imageLink = l_employeesVM.g_str_imageLink;
                this.g_imgSrc_employee = staticFunctionClass.LoadBitmap(this.g_str_imageLink);
                #endregion

                if (this.g_str_id == staticVarClass.account_userName)
                    this.g_i_widthButtonChangePassword = 30;
            }
        }

        private void unloaded()
        {
            //this.StopTable();
        }

        private void loaded(DetailEmployeesView p)
        {
            if (p == null)
                return;

            this.g_i_widthButtonChangePassword = 0;
            this.loadDataEmployee();
            this.authorize();

            using (var DB = new QLCanTinEntities())
            {
                #region đổ dữ liệu vào listview
                this.g_listOrders = new ObservableCollection<ORDERINFO>(DB.ORDERINFOes
                    .Where(orderinfo => orderinfo.STATUS == staticVarClass.status_done
                    && orderinfo.EMPLOYEEID == this.g_str_id));
                #endregion
            }

            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 0;
            p.grVChangePassword.Height = 0;

            //  this.WatchTable();
        }

        private void authorize()
        {
            if (staticVarClass.position_user == staticVarClass.position_manager)
            {
                this.g_b_isAdmin = true;
            }
            else
            {
                this.g_b_isAdmin = false;
            }
        }

        private void clickCloseWindow(DetailEmployeesView p)
        {
            p.Close();

            //EmployeesView l_employeesView = EmployeesView.Instance;

            //if (l_employeesView.DataContext == null)
            //    return;

            //var l_employeesVM = l_employeesView.DataContext as EmployeesViewModel;

            //for (int i = 0; i < l_employeesVM.g_listEmployees.Count(); i++)
            //{
            //    if (l_employeesVM.g_listEmployees[i].ID.Trim() == this.g_str_id)
            //    {
            //        //System.Windows.Application.Current.Dispatcher.Invoke(() =>
            //        //{
            //        //    l_employeesVM.g_listEmployees[i] = new EMPLOYEE()
            //        //    {
            //        //        ID = this.g_str_id,
            //        //        FULLNAME = this.g_str_fullName,
            //        //        GENDER = this.g_str_gender,
            //        //        YEAROFBIRTH = this.g_i_yearOfBirth,
            //        //        PHONE = this.g_str_phone,
            //        //        EMAIL = this.g_str_email,
            //        //        POSITION = this.g_str_position,
            //        //        IMAGELINK = this.g_str_imageLink,
            //        //        STATUS = this.g_str_status
            //        //    };

            //        //    l_employeesVM.g_selectedItem = l_employeesVM.g_listEmployees[i];
            //        //});
            //        l_employeesVM.g_selectedItem = l_employeesVM.g_listEmployees[i];

            //        break;
            //    }
            //}
        }

        private bool checkEditInfo()
        {
            if (staticVarClass.account_userName != this.g_str_id && this.g_b_isAdmin == false)
                return false;
            else
                return true;
        }

        private void clickEditInfo(DetailEmployeesView p)
        {
            if (p == null)
                return;

            p.grVInfo.Height = 0;
            p.grVEdit.Height = 350;
            p.grVSendMail.Height = 0;
            p.grVChangePassword.Height = 0;

            this.g_str_fullNameEdit = this.g_str_fullName;
            this.g_str_genderEdit = this.g_str_gender;
            this.g_i_yearOfBirthEdit = this.g_i_yearOfBirth;
            this.g_str_phoneEdit = this.g_str_phone;
            this.g_str_emailEdit = this.g_str_email;
        }

        private bool checkSaveInfo()
        {
            if (string.IsNullOrEmpty(this.g_str_id))
                return false;

            using (var DB = new QLCanTinEntities())
            {
                // check id.
                var l_IDList = DB.EMPLOYEEs.Where(employee => employee.ID == this.g_str_id);
                if (l_IDList == null || l_IDList.Count() == 0)
                    return false;
            }

            return true;
        }

        private void clickSaveInfo(DetailEmployeesView p)
        {
            try
            {
                using (var DB = new QLCanTinEntities())
                {
                    var l_employee = DB.EMPLOYEEs.Where(employee => employee.ID == this.g_str_id).SingleOrDefault();
                    l_employee.FULLNAME = this.g_str_fullNameEdit;
                    l_employee.GENDER = this.g_str_genderEdit;
                    l_employee.YEAROFBIRTH = this.g_i_yearOfBirthEdit;
                    l_employee.PHONE = this.g_str_phoneEdit;
                    l_employee.EMAIL = this.g_str_emailEdit;
                    l_employee.POSITION = this.g_str_position;
                    l_employee.STATUS = this.g_str_status;
                    l_employee.IMAGELINK = this.g_str_imageLink;

                    DB.SaveChanges();
                }

                staticFunctionClass.showStatusView(true, "Sửa thông tin nhân viên " + this.g_str_id + " thành công!");

                #region Cập nhật lại thông tin.
                this.g_str_fullName = this.g_str_fullNameEdit;
                this.g_str_gender = this.g_str_genderEdit;
                this.g_i_yearOfBirth = this.g_i_yearOfBirthEdit;
                this.g_str_phone = this.g_str_phoneEdit;
                this.g_str_email = this.g_str_emailEdit;
                #endregion
            }
            catch
            {
                staticFunctionClass.showStatusView(false, "Sửa thông tin nhân viên " + this.g_str_id + " thất bại!");
            }

            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 0;
            p.grVChangePassword.Height = 0;
        }

        private bool checkExport()
        {
            if (this.g_listOrders == null || this.g_listOrders.Count() == 0)
                return false;

            return true;
        }

        private void clickExport()
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Title = "Choose a place to save",
                ValidateNames = true,
                Filter = "Excel (*.xlsx) | *.xlsx"
            };

            Nullable<bool> b_result = sfd.ShowDialog();

            // Nếu người dùng đã chọn được file excel.
            if (b_result == true)
            {
                string str_fullNameChosen = sfd.FileName;

                Excel.Application excel = new Excel.Application();
                //excel.Visible = true; 
                Excel.Workbook workBook = excel.Workbooks.Add(1);
                Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Worksheets[1];
                List<string> l_listHeaders = new List<string> { "Thời gian", "Mã đơn hàng", "Mã khách hàng", "Tổng tiền" };

                for (int x = 1; x < l_listHeaders.Count() + 1; x++)
                {
                    workSheet.Cells[1, x] = l_listHeaders[x - 1];
                    workSheet.Cells[1, x].Font.Bold = true;
                }

                for (int x = 2; x < this.g_listOrders.Count() + 2; x++)
                {
                    workSheet.Cells[x, 1] = this.g_listOrders[x - 2].ORDERDATE.ToString().Trim();
                    workSheet.Cells[x, 2] = this.g_listOrders[x - 2].ID.ToString().Trim();
                    workSheet.Cells[x, 3] = this.g_listOrders[x - 2].CUSTOMERID.ToString().Trim();
                    workSheet.Cells[x, 4] = this.g_listOrders[x - 2].TOTALMONEY.ToString().Trim();
                }

                // AutoSet Cell Widths to Content Size
                workSheet.Cells.Select();
                workSheet.Cells.EntireColumn.AutoFit();

                try
                {
                    workBook.SaveAs(str_fullNameChosen, Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                            Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                            Excel.XlSaveConflictResolution.xlUserResolution, true,
                            Missing.Value, Missing.Value, Missing.Value);
                    staticFunctionClass.showStatusView(true, "Xuất file thành công!");
                }
                catch
                {
                    staticFunctionClass.showStatusView(true, "Xuất file thất bại!");
                }

                workBook.Close();
                excel.Quit();
            }
        }

        private void mouseLeftButtonDown(DetailEmployeesView p)
        {
            p.DragMove();
        }

        private bool checkChangeImage()
        {
            if (staticVarClass.account_userName != this.g_str_id)
                return false;

            return true;
        }

        private void clickChangeImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";

            Nullable<bool> b_result = openFileDialog.ShowDialog();

            if (b_result == true)
            {
                try
                {
                    string str_path = openFileDialog.FileName;

                    if (str_path != string.Empty)
                    {
                        string str_oleFileName = this.g_str_id + staticFunctionClass.getFormatBefore(this.g_str_imageLink);

                        if (File.Exists(staticVarClass.server_serverDirectory + str_oleFileName))
                        {
                            if (!staticFunctionClass.deleteFile(str_oleFileName))
                            {
                                staticFunctionClass.showStatusView(false, "Đổi ảnh đại diện thất bại!");
                                return;
                            }
                        }

                        long t_tick = DateTime.Now.Ticks;
                        string str_newFileName = this.g_str_id + "_" + t_tick.ToString() + staticFunctionClass.getFormat(str_path);

                        if (staticFunctionClass.uploadFile(str_newFileName, str_path))
                        {
                            // Update image link in database.
                            this.g_str_imageLink = staticVarClass.server_serverDirectory + str_newFileName;

                            // Update image source.
                            this.g_imgSrc_employee = staticFunctionClass.LoadBitmap(this.g_str_imageLink);

                            using (var DB = new QLCanTinEntities())
                            {
                                DB.EMPLOYEEs.Where(employee => employee.ID == this.g_str_id).ToList()
                                                              .ForEach(customer => customer.IMAGELINK = this.g_str_imageLink);
                                DB.SaveChanges();
                            }

                            MainWindow mainWd = MainWindow.Instance;
                            if (mainWd.DataContext == null)
                                return;
                            MainViewModel l_mainVM = mainWd.DataContext as MainViewModel;
                            l_mainVM.loadUserInfo();

                            staticFunctionClass.showStatusView(true, "Đổi ảnh đại diện thành công!");
                        }
                        else
                        {
                            staticFunctionClass.showStatusView(false, "Đổi ảnh đại diện thất bại!");
                        }
                    }
                }
                catch
                {
                    staticFunctionClass.showStatusView(false, "Đổi ảnh đại diện thất bại!");
                }
            }
        }

        private bool checkOpenMail()
        {
            if (this.g_str_email == string.Empty || staticVarClass.account_userName != this.g_str_id)
                return false;

            return true;
        }

        private void clickOpenMail(DetailEmployeesView p)
        {
            p.grVInfo.Height = 0;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 350;
            p.grVChangePassword.Height = 0;
            //this.g_selectedEmail = null;
            //this.g_str_titleMail = string.Empty;
            //this.g_str_contentMail = string.Empty;
        }

        private bool checkSendMail()
        {
            if (string.IsNullOrEmpty(this.g_str_id))
                return false;

            // check có email của ng gửi chưa.
            if (string.IsNullOrEmpty(this.g_str_email))
                return false;

            // check có email của người nhận chưa.
            if (this.g_selectedEmail == null)
                return false;

            return true;
        }

        private void clickSendMail(DetailEmployeesView p)
        {
            try // Its a good practice to write your code in a try catch block 
            {
                var l_from = staticVarClass.gmail_user;
                var l_password = staticVarClass.gmail_password;
                var l_to = this.g_selectedEmail.EMAIL;
                SmtpClient client = new SmtpClient(staticVarClass.email_hostEmail, staticVarClass.email_portEmail);      //Connection Object.
                var message = new MailMessage(l_from, l_to); // Email Object.
                message.Body = this.g_str_contentMail + Environment.NewLine + "Sent from " + this.g_str_email;

                message.Subject = this.g_str_titleMail;

                client.Credentials = new System.Net.NetworkCredential(l_from, l_password); // Setting Credential of gmail account.
                client.EnableSsl = true;                // Enabling secured Connection.
                client.Send(message);
                message = null;                         // Free the memory

                staticFunctionClass.showStatusView(true, "Gửi email đến " + l_to + " thành công!");

                this.g_selectedEmail = null;
                this.g_str_titleMail = string.Empty;
                this.g_str_contentMail = string.Empty;

                //p.grVInfo.Height = 350;
                //p.grVEdit.Height = 0;
                //p.grVSendMail.Height = 0;
                //p.grVChangePassword.Height = 0;
            }
            catch
            {
                staticFunctionClass.showStatusView(false, "Gửi email đến " + this.g_selectedEmail.EMAIL + " thất bại!");
            }
        }

        private void clickOpenChangePassword(DetailEmployeesView p)
        {
            if (p == null)
                return;

            p.grVInfo.Height = 0;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 0;
            p.grVChangePassword.Height = 350;
        }

        private void resetChangePassword()
        {
            this.g_str_currentPassword = string.Empty;
            this.g_str_newPassword = string.Empty;
            this.g_str_confirmNewPassword = string.Empty;

            this.g_b_isHitTestVisibleNewPass = false;
            this.g_b_isHitTestVisibleConfirmNewPass = false;

            if (this.g_passBoxCurrPass != null)
            {
                this.g_passBoxCurrPass.Password = string.Empty;
            }
            if (this.g_passBoxNewPass != null)
            {
                this.g_passBoxNewPass.Password = string.Empty;
            }
            if (this.g_passBoxConfirmNewPass != null)
            {
                this.g_passBoxConfirmNewPass.Password = string.Empty;
            }
        }

        private void passwordChangedCurrentPassword(System.Windows.Controls.PasswordBox p)
        {
            this.g_passBoxCurrPass = p;
            this.g_str_currentPassword = this.g_passBoxCurrPass.Password;

            if (this.g_str_currentPassword != string.Empty)
            {
                this.g_b_isHitTestVisibleNewPass = true;
                this.g_b_isHitTestVisibleConfirmNewPass = true;
            }
            else
            {
                this.g_b_isHitTestVisibleNewPass = false;
                this.g_b_isHitTestVisibleConfirmNewPass = false;
            }
        }

        private void passwordChangedNewPassword(System.Windows.Controls.PasswordBox p)
        {
            this.g_passBoxNewPass = p;
            this.g_str_newPassword = this.g_passBoxNewPass.Password;

            if (this.g_str_newPassword != string.Empty)
            {
                this.g_b_isHitTestVisibleConfirmNewPass = true;
            }
            else
            {
                if (this.g_passBoxConfirmNewPass != null)
                    this.g_passBoxConfirmNewPass.Password = string.Empty;

                this.g_b_isHitTestVisibleConfirmNewPass = false;
            }
        }

        private void passwordChangedConfirmNewPassword(System.Windows.Controls.PasswordBox p)
        {
            this.g_passBoxConfirmNewPass = p;
            this.g_str_confirmNewPassword = this.g_passBoxConfirmNewPass.Password;
        }

        private bool checkChangePassword()
        {
            if (this.g_str_currentPassword == string.Empty)
                return false;

            if (this.g_str_newPassword == string.Empty)
                return false;

            if (this.g_str_confirmNewPassword == string.Empty)
                return false;

            return true;
        }

        private void clickChangePassword(DetailEmployeesView p)
        {
            if (staticVarClass.account_password != this.g_str_currentPassword)
            {
                staticFunctionClass.showStatusView(false, "Mật khẩu hiện tại không khớp. Vui lòng nhập lại!");
            }
            else
            {
                if (this.g_str_newPassword == this.g_str_confirmNewPassword)
                {
                    try
                    {
                        using (var DB = new QLCanTinEntities())
                        {
                            var l_employee = DB.EMPLOYEEs.Where(employee => employee.ID == this.g_str_id).SingleOrDefault();
                            l_employee.PASSWORD = this.g_str_confirmNewPassword;

                            DB.SaveChanges();
                        }

                        staticVarClass.account_password = this.g_str_confirmNewPassword;
                        staticFunctionClass.showStatusView(true, "Đổi mật khẩu thành công!");

                        this.resetChangePassword();
                        this.clickCloseWindow(p);

                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            MainWindow mainWd = MainWindow.Instance;
                            if (mainWd.DataContext == null)
                                return;
                            MainViewModel l_mainVM = mainWd.DataContext as MainViewModel;

                            l_mainVM.clickLogOutWindow(mainWd);
                        });
                    }
                    catch
                    {
                        staticFunctionClass.showStatusView(false, "Đổi mật khẩu thất bại!");
                    }
                }
                else
                {
                    staticFunctionClass.showStatusView(false, "Xác nhận mật khẩu mới không khớp. Vui lòng nhập lại!");
                }
            }
        }

        private void clickGoBack(DetailEmployeesView p)
        {
            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 0;
            p.grVChangePassword.Height = 0;
        }

        private bool filterIDOrder(object item)
        {
            if (string.IsNullOrEmpty(_g_str_filter))
                return true;
            else
                return ((item as ORDERINFO).ID.IndexOf(_g_str_filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void filterIDOrder()
        {
            CollectionViewSource.GetDefaultView(this.g_listOrders).Refresh();
        }
    }
}
