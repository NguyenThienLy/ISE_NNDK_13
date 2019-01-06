using CanTeenManagement.CO;
using CanTeenManagement.Model;
using CanTeenManagement.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using TableDependency.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace CanTeenManagement.ViewModel
{
    public class CustomersViewModel : BaseViewModel
    {
        private ObservableCollection<CUSTOMER> _g_listCustomers;
        public ObservableCollection<CUSTOMER> g_listCustomers
        {
            get => _g_listCustomers;
            set
            {
                _g_listCustomers = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_filter;
        public string g_str_filter
        {
            get { return _g_str_filter; }
            set
            {
                _g_str_filter = value;
                OnPropertyChanged();

                ICollectionView view = (ICollectionView)CollectionViewSource.GetDefaultView(this.g_listCustomers);
                view.Filter = filterIDCustomer;
            }
        }

        private string _g_str_mode;
        public string g_str_mode
        {
            get { return _g_str_mode; }
            set
            {
                _g_str_mode = value;
                OnPropertyChanged();
            }
        }

        private bool _g_b_isHitTestVisibleMode;
        public bool g_b_isHitTestVisibleMode
        {
            get { return _g_b_isHitTestVisibleMode; }
            set
            {
                _g_b_isHitTestVisibleMode = value;
                OnPropertyChanged();
            }
        }

        private int _g_i_height;
        public int g_i_height
        {
            get { return _g_i_height; }
            set
            {
                _g_i_height = value;
                OnPropertyChanged();
            }
        }

        private bool _g_b_isReadOnlyID;
        public bool g_b_isReadOnlyID
        {
            get { return _g_b_isReadOnlyID; }
            set
            {
                _g_b_isReadOnlyID = value;
                OnPropertyChanged();
            }
        }

        private CUSTOMER _g_selectedItem;
        public CUSTOMER g_selectedItem
        {
            get => _g_selectedItem;
            set
            {
                _g_selectedItem = value;
                OnPropertyChanged();
                if (this.g_selectedItem != null && this.g_i_addOrEdit == 0)
                {
                    // Binding giá trị đang chọn lên text box.
                    this.g_str_id = this.g_selectedItem.ID.Trim();

                    if (this.g_selectedItem.FULLNAME == null)
                        this.g_str_fullName = string.Empty;
                    else this.g_str_fullName = this.g_selectedItem.FULLNAME.Trim();

                    this.g_str_gender = this.g_selectedItem.GENDER.Trim();
                    this.g_i_yearOfBirth = this.g_selectedItem.YEAROFBIRTH;

                    if (this.g_selectedItem.PHONE == null)
                        this.g_str_phone = string.Empty;
                    else this.g_str_phone = this.g_selectedItem.PHONE.Trim();

                    if (this.g_selectedItem.EMAIL == null)
                        this.g_str_email = string.Empty;
                    else this.g_str_email = this.g_selectedItem.EMAIL.Trim();

                    if (this.g_selectedItem.CASH == null)
                        this.g_i_cash = 0;
                    else this.g_i_cash = this.g_selectedItem.CASH;

                    if (this.g_selectedItem.POINT == null)
                        this.g_i_point = 0;
                    else this.g_i_point = this.g_selectedItem.POINT;

                    if (this.g_selectedItem.STAR == null)
                        this.g_i_star = 0;
                    else this.g_i_star = this.g_selectedItem.STAR;

                    if (this.g_selectedItem.IMAGELINK == null)
                        this.g_str_imageLink = string.Empty;
                    else this.g_str_imageLink = this.g_selectedItem.IMAGELINK.Trim();
                }
            }
        }

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

        private string _g_str_visibilityCustomers;
        public string g_str_visibilityCustomers
        {
            get => _g_str_visibilityCustomers;
            set
            {
                _g_str_visibilityCustomers = value;
                OnPropertyChanged();
            }
        }

        int g_i_addOrEdit;
        bool g_b_groupGender;
        bool g_b_isRefreshed;
        bool g_b_isUnloaded;
        int g_i_position;

        CUSTOMER g_customer = null;

        #region Các thuộc tính của customer.
        private string _g_str_imageLink;
        public string g_str_imageLink
        {
            get => _g_str_imageLink;
            set
            {
                _g_str_imageLink = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_id;
        public string g_str_id
        {
            get => _g_str_id;
            set
            {
                long i = 0;
                if (value != string.Empty)
                    if (!long.TryParse(value, out i))
                        value = this.g_str_id;

                _g_str_id = value;
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

        private string _g_str_gender;
        public string g_str_gender
        {
            get => _g_str_gender;
            set
            {
                _g_str_gender = value;
                OnPropertyChanged();
            }
        }

        private Nullable<int> _g_i_yearOfBirth;
        public Nullable<int> g_i_yearOfBirth
        {
            get => _g_i_yearOfBirth;
            set
            {
                _g_i_yearOfBirth = value;
                OnPropertyChanged();
            }
        }

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
        public string g_str_email
        {
            get => _g_str_email;
            set
            {
                _g_str_email = value;
                OnPropertyChanged();
            }
        }

        private Nullable<int> _g_i_cash;
        public Nullable<int> g_i_cash
        {
            get => _g_i_cash;
            set
            {
                _g_i_cash = value;
                OnPropertyChanged();
            }
        }

        private Nullable<int> _g_i_point;
        public Nullable<int> g_i_point
        {
            get => _g_i_point;
            set
            {
                _g_i_point = value;
                OnPropertyChanged();
            }
        }

        private Nullable<int> _g_i_star;
        public Nullable<int> g_i_star
        {
            get => _g_i_star;
            set
            {
                _g_i_star = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region commands.
        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_UnloadedCommand { get; set; }

        public ICommand g_iCm_ClickAddCommand { get; set; }

        public ICommand g_iCm_ClickEditCommand { get; set; }

        public ICommand g_iCm_ClickSaveCommand { get; set; }

        public ICommand g_iCm_ClickExportCommand { get; set; }

        public ICommand g_iCm_TextChangedFilterCommand { get; set; }

        public ICommand g_iCm_ClickDetailCommand { get; set; }

        public ICommand g_iCm_ClickUtilityCommand { get; set; }

        public ICommand g_iCm_ClickGoBackCommand { get; set; }

        public ICommand g_iCm_ClickChangeModeCommand { get; set; }

        public ICommand g_iCm_ClickPreviousPageCommand { get; set; }

        public ICommand g_iCm_ClickNextPageCommand { get; set; }

        public ICommand g_iCm_ClickButtonPageCommand { get; set; }

        public ICommand g_iCm_ClickButtonRefreshCommand { get; set; }
        #endregion

        public CustomersViewModel()
        {
            this.initSupport();

            g_iCm_LoadedCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.loaded();
            });

            g_iCm_UnloadedCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.unloaded();
            });

            g_iCm_ClickAddCommand = new RelayCommand<CustomersView>((p) => { return this.checkAdd(); }, (p) =>
            {
                this.clickAdd();
            });

            g_iCm_ClickEditCommand = new RelayCommand<CustomersView>((p) => { return this.checkEdit(); }, (p) =>
            {
                this.clickEdit();
            });

            g_iCm_ClickSaveCommand = new RelayCommand<CustomersView>((p) => { return this.checkSave(); }, (p) =>
            {
                this.clickSave();
            });

            g_iCm_ClickExportCommand = new RelayCommand<CustomersView>((p) => { return this.checkExport(); }, (p) =>
            {
                this.clickExport();
            });

            g_iCm_TextChangedFilterCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.filterIDCustomer();
            });

            g_iCm_ClickDetailCommand = new RelayCommand<CUSTOMER>((p) => { return true; }, (p) =>
            {
                this.clickDetail(p);
            });

            g_iCm_ClickUtilityCommand = new RelayCommand<CustomersView>((p) => { return checkUtility(); }, (p) =>
            {
                this.clickUtility();
            });

            g_iCm_ClickGoBackCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickGoBack();
            });

            g_iCm_ClickChangeModeCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) =>
            {
                this.clickChangeModeGroup();
            });

            g_iCm_ClickButtonRefreshCommand = new RelayCommand<Button>((p) => { return checkClickButtonRefresh(); }, (p) =>
            {
                this.clickButtonRefresh();
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
            var tableName = "CUSTOMER";
            var tableDependency = new SqlTableDependency<CUSTOMER>(connectionString, tableName);

            tableDependency.OnChanged += OnNotificationReceived;
            tableDependency.Start();
        }

        public void StopTable()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EPOSEntities"].ConnectionString;
            var tableName = "CUSTOMER";
            var tableDependency = new SqlTableDependency<CUSTOMER>(connectionString, tableName);

            tableDependency.Stop();
        }

        private void OnNotificationReceived(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<CUSTOMER> e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                this.g_b_isRefreshed = true;
                this.loadData();
                this.authorize();

                if (this.g_customer != null)
                {
                    for (int i = 0; i < g_listCustomers.Count(); i++)
                    {
                        if (g_listCustomers[i].ID.Trim() == g_customer.ID)
                        {
                            g_selectedItem = g_listCustomers[i];
                            break;
                        }
                    }
                }
                this.sortID();
                this.clickChangeModeGroup();
            });
        }

        private void initSupport()
        {
            this.g_str_visibilityCustomers = staticVarClass.visibility_hidden;

            //
            this.g_listCustomers = new ObservableCollection<CUSTOMER>();
            this.g_selectedItem = null;
            this.g_i_addOrEdit = 0;
            this.g_b_groupGender = true;
            this.g_b_isRefreshed = false;
            this.g_b_isUnloaded = false;
            this.g_i_position = 0;
            this.g_b_isHitTestVisibleMode = true;
            this.g_str_mode = staticVarClass.mode_groupGender;
            this.loadCombobox();
        }

        #region load.
        private void loadCombobox()
        {
            // Thêm danh sách gender.
            List<string> l_listGenders = new List<string>();
            l_listGenders.Add(staticVarClass.gender_feMale);
            l_listGenders.Add(staticVarClass.gender_male);
            l_listGenders.Add(staticVarClass.gender_different);
            this.g_listGenders = l_listGenders;
            this.g_str_gender = staticVarClass.gender_feMale; // mặc định.

            // Thêm danh sách năm sinh.
            List<int> l_listYearOfBirth = new List<int>();
            int l_i_EighteenYear = DateTime.Now.Year - 2018 + 2000;

            for (int i = l_i_EighteenYear; i >= l_i_EighteenYear - (42 + DateTime.Now.Year - 2018); i--)
            {
                l_listYearOfBirth.Add(i);
            }
            this.g_listYearOfBirth = l_listYearOfBirth;
            this.g_i_yearOfBirth = l_listYearOfBirth[0];
        }

        private void authorize()
        {
            if (staticVarClass.position_user == staticVarClass.position_manager)
            {
                this.g_i_position = 1;
            }
            else if (staticVarClass.position_user == staticVarClass.position_cashier)
            {
                this.g_i_position = 2;
            }
            else
            {
                this.g_i_position = 3;
            }
        }

        private void unloaded()
        {
            this.g_b_isUnloaded = true;
            //   this.StopTable();
        }

        private void loaded()
        {
            this.loadData();
            this.authorize();
            this.checkVisibilityData();
            this.sortID();
            this.clickChangeModeGroup();
            // this.WatchTable();
        }

        private void loadData()
        {
            using (var DB = new QLCanTinEntities())
            {
                this.g_listCustomers = this.ToObservableCollection<CUSTOMER>
                ((from customer in DB.CUSTOMERs
                  select new
                  {
                      ID = customer.ID.Trim(),
                      PIN = customer.PIN.Trim(),
                      FULLNAME = customer.FULLNAME.Trim(),
                      GENDER = customer.GENDER.Trim(),
                      YEAROFBIRTH = customer.YEAROFBIRTH,
                      PHONE = customer.PHONE.Trim(),
                      EMAIL = customer.EMAIL.Trim(),
                      CASH = customer.CASH,
                      POINT = customer.POINT,
                      IMAGELINK = customer.IMAGELINK.Trim(),
                      STAR = customer.STAR

                  }).ToList().Select(x => new CUSTOMER
                  {
                      ID = x.ID.Trim(),
                      PIN = x.PIN.Trim(),
                      FULLNAME = x.FULLNAME.Trim(),
                      GENDER = x.GENDER.Trim(),
                      YEAROFBIRTH = x.YEAROFBIRTH,
                      PHONE = x.PHONE.Trim(),
                      EMAIL = x.EMAIL.Trim(),
                      CASH = x.CASH,
                      POINT = x.POINT,
                      IMAGELINK = x.IMAGELINK.Trim(),
                      STAR = x.STAR
                  }).ToList());
            }
        }

        private void checkVisibilityData()
        {
            if (this.g_listCustomers.Count == 0)
            {
                this.g_str_visibilityCustomers = staticVarClass.visibility_visible;
            }
            else
            {
                this.g_str_visibilityCustomers = staticVarClass.visibility_hidden;
            }
        }
        #endregion

        private void sortID()
        {
            ICollectionView view = (ICollectionView)CollectionViewSource.GetDefaultView(this.g_listCustomers);
            var sortDescription = new SortDescription("ID", ListSortDirection.Ascending);
            view.SortDescriptions.Add(sortDescription);
        }

        private void clickChangeModeGroup()
        {
            if (this.g_i_addOrEdit != 0 || this.g_b_isRefreshed == true) // click save.
            {
                this.g_b_groupGender = !this.g_b_groupGender;
            }

            if (this.g_b_isUnloaded == true)
            {
                this.g_b_groupGender = !this.g_b_groupGender;
            }

            ICollectionView view = (ICollectionView)CollectionViewSource.GetDefaultView(this.g_listCustomers);

            if (this.g_b_groupGender)
            {
                this.g_b_groupGender = false;

                var groupDescription = new PropertyGroupDescription("GENDER");
                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(groupDescription);
                this.g_str_mode = staticVarClass.mode_groupGender;
            }
            else
            {
                this.g_b_groupGender = true;

                var groupDescription = new PropertyGroupDescription("YEAROFBIRTH");
                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(groupDescription);
                this.g_str_mode = staticVarClass.mode_groupYearOfBirth;
            }

            this.g_b_isRefreshed = false;
            this.g_b_isUnloaded = false;
        }

        private string getNameForPicture(string id)
        {
            string l_temp = string.Empty;

            l_temp = id[4].ToString() + id[5].ToString() + id[6].ToString();

            return l_temp;
        }

        private bool clickGoBack()
        {
            this.g_i_height = 0;
            this.g_i_addOrEdit = 0;
            this.g_b_isReadOnlyID = false;
            this._g_b_isHitTestVisibleMode = true;
            return true;
        }

        #region btn add.
        private bool checkAdd()
        {
            if (this.g_i_position != 1)
                return false;

            if (this.g_i_addOrEdit != 0)
                return false;

            return true;
        }

        private void clickAdd()
        {
            this.g_i_height = 40;
            this.g_i_addOrEdit = 1;
            this.g_b_isHitTestVisibleMode = false;

            #region Làm trống text box.
            this.g_str_id = string.Empty;
            this.g_str_fullName = string.Empty;
            this.g_str_gender = this.g_listGenders[0];
            this.g_i_yearOfBirth = this.g_listYearOfBirth[0];
            this.g_str_phone = string.Empty;
            this.g_str_email = string.Empty;
            this.g_i_cash = 0;
            this.g_i_point = 0;
            #endregion
        }
        #endregion

        #region btn edit.
        private bool checkEdit()
        {
            if (this.g_i_position != 1)
                return false;

            if (this.g_selectedItem == null || this.g_i_addOrEdit != 0)
                return false;

            this.g_selectedItem = this.g_selectedItem;
            return true;
        }

        private void clickEdit()
        {
            this.g_i_height = 40;
            this.g_i_addOrEdit = 2;
            this.g_b_isReadOnlyID = true;
            this.g_b_isHitTestVisibleMode = false;
        }
        #endregion

        #region btn save.
        private bool checkSave()
        {
            if (this.g_i_addOrEdit == 0)
            {
                return false;
            }
            else if (this.g_i_addOrEdit == 1)
            {
                if (string.IsNullOrEmpty(this.g_str_id))
                    return false;

                if (this.g_str_id.Length < 7)
                    return false;

                using (var DB = new QLCanTinEntities())
                {
                    // check id.
                    var l_customer = DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_id);
                    if (l_customer == null || l_customer.Count() != 0)
                        return false;
                }

            }
            else if (this.g_i_addOrEdit == 2)
            {
                using (var DB = new QLCanTinEntities())
                {
                    // check id.
                    var l_IDList = DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_id);
                    if (l_IDList == null || l_IDList.Count() == 0)
                        return false;
                }
            }

            return true;
        }

        private void clickSave()
        {
            if (this.g_i_addOrEdit == 1)
            {
                var l_customer = new CUSTOMER()
                {
                    ID = this.g_str_id,
                    PIN = "123456",
                    FULLNAME = this.g_str_fullName,
                    GENDER = this.g_str_gender,
                    YEAROFBIRTH = this.g_i_yearOfBirth,
                    PHONE = this.g_str_phone,
                    EMAIL = this.g_str_email,
                    CASH = this.g_i_cash,
                    POINT = this.g_i_point,
                    STAR = 1,
                    IMAGELINK = staticVarClass.server_serverDirectory + this.g_str_id + staticVarClass.format_JPG
                };

                try
                {
                    using (var DB = new QLCanTinEntities())
                    {
                        DB.CUSTOMERs.Add(l_customer);
                        DB.SaveChanges();
                    }

                    // Make image default.
                    staticFunctionClass.CreateProfilePicture(this.getNameForPicture(l_customer.ID), l_customer.ID, 95);
                    //this.g_listCustomers.Add(l_customer);
                    staticFunctionClass.showStatusView(true, "Thêm khách hàng " + this.g_str_id + " thành công!");
                }
                catch
                {
                    staticFunctionClass.showStatusView(false, "Thêm khách hàng " + this.g_str_id + " thất bại!");
                }
            }
            else if (this.g_i_addOrEdit == 2)
            {
                try
                {
                    using (var DB = new QLCanTinEntities())
                    {
                        var l_customer = DB.CUSTOMERs.Where(customer => customer.ID == this.g_selectedItem.ID).SingleOrDefault();
                        l_customer.FULLNAME = this.g_str_fullName;
                        l_customer.GENDER = this.g_str_gender;
                        l_customer.YEAROFBIRTH = this.g_i_yearOfBirth;
                        l_customer.PHONE = this.g_str_phone;
                        l_customer.EMAIL = this.g_str_email;
                        l_customer.CASH = this.g_i_cash;
                        l_customer.POINT = this.g_i_point;
                        l_customer.STAR = this.g_i_star;
                        l_customer.IMAGELINK = this.g_str_imageLink;

                        DB.SaveChanges();
                    }

                    staticFunctionClass.showStatusView(true, "Sửa thông tin khách hàng " + this.g_str_id + " thành công!");
                }
                catch
                {
                    staticFunctionClass.showStatusView(false, "Sửa thông tin khách hàng " + this.g_str_id + " thất bại!");
                }

                this.g_b_isReadOnlyID = false;
                //for (int i = 0; i < this.g_listCustomers.Count(); i++)
                //{
                //    if (this.g_listCustomers[i].ID == this.g_selectedItem.ID)
                //    {
                //        this.g_listCustomers[i] = new CUSTOMER()
                //        {
                //            ID = this.g_selectedItem.ID,
                //            FULLNAME = this.g_str_fullName,
                //            GENDER = this.g_str_gender,
                //            YEAROFBIRTH = this.g_i_yearOfBirth,
                //            PHONE = this.g_str_phone,
                //            EMAIL = this.g_str_email,
                //            CASH = this.g_i_cash,
                //            POINT = this.g_i_point,
                //            STAR = this.g_i_star,
                //            IMAGELINK = this.g_str_imageLink
                //        };
                //        break;
                //    }
                //}
            }

            this.g_i_height = 0;
            this.g_i_addOrEdit = 0;
            this.g_b_isHitTestVisibleMode = true;
        }
        #endregion

        #region export.
        private bool checkExport()
        {
            if (this.g_i_addOrEdit != 0 || this.g_listCustomers.Count() == 0)
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
                List<string> l_listHeaders = new List<string> { "Mã số khách hàng", "Họ và tên", "Giới tính", "Năm sinh", "Số điện thoại", "Email", "Tài khoản", "Điểm", "Sao" };

                for (int x = 1; x < l_listHeaders.Count() + 1; x++)
                {
                    workSheet.Cells[1, x] = l_listHeaders[x - 1];
                    workSheet.Cells[1, x].Font.Bold = true;
                }

                for (int x = 2; x < this.g_listCustomers.Count() + 2; x++)
                {
                    workSheet.Cells[x, 1] = this.g_listCustomers[x - 2].ID.ToString().Trim();
                    workSheet.Cells[x, 2] = this.g_listCustomers[x - 2].FULLNAME.ToString().Trim();
                    workSheet.Cells[x, 3] = this.g_listCustomers[x - 2].GENDER.ToString().Trim();
                    workSheet.Cells[x, 4] = this.g_listCustomers[x - 2].YEAROFBIRTH.ToString().Trim();
                    workSheet.Cells[x, 5] = this.g_listCustomers[x - 2].PHONE.ToString().Trim();
                    workSheet.Cells[x, 6] = this.g_listCustomers[x - 2].EMAIL.ToString().Trim();
                    workSheet.Cells[x, 7] = this.g_listCustomers[x - 2].CASH.ToString().Trim();
                    workSheet.Cells[x, 8] = this.g_listCustomers[x - 2].POINT.ToString().Trim();
                    workSheet.Cells[x, 9] = this.g_listCustomers[x - 2].STAR.ToString().Trim();
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
                    staticFunctionClass.showStatusView(false, "Xuất file thất bại!");
                }

                workBook.Close();
                excel.Quit();
            }

        }
        #endregion

        #region fillter.
        private bool filterIDCustomer(object item)
        {
            if (string.IsNullOrEmpty(_g_str_filter))
                return true;
            else
                return ((item as CUSTOMER).ID.IndexOf(_g_str_filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void filterIDCustomer()
        {
            CollectionViewSource.GetDefaultView(this.g_listCustomers).Refresh();
        }
        #endregion

        private void clickDetail(CUSTOMER p)
        {
            if (p == null)
                return;

            //this.g_i_addOrEdit = 0;
            this.g_customer = p;
            this.g_selectedItem = p;

            MainWindow mainWd = MainWindow.Instance;
            CustomersView customersV = CustomersView.Instance;

            mainWd.Opacity = 0.5;
            customersV.Opacity = 0.5;

            DetailCustomersView detailCusView = new DetailCustomersView();
            detailCusView.ShowDialog();

            this.loaded();

            mainWd.Opacity = 100;
            customersV.Opacity = 100;
        }

        private bool checkUtility()
        {
            if (this.g_i_position == 3)
                return false;

            return true;
        }

        private void clickUtility()
        {
            MainWindow mainWd = MainWindow.Instance;
            CustomersView customersV = CustomersView.Instance;

            mainWd.Opacity = 0.5;
            customersV.Opacity = 0.5;

            UtilityCustomersView utilityCustomersView = new UtilityCustomersView();
            utilityCustomersView.ShowDialog();

            mainWd.Opacity = 100;
            customersV.Opacity = 100;
        }

        #region button refresh.
        private bool checkClickButtonRefresh()
        {
            //if (this.g_str_visibilityCustomers == staticVarClass.visibility_visible)
            //    return false;

            return true;
        }

        private void clickButtonRefresh()
        {
            this.g_b_isRefreshed = true;
            this.loaded();
        }
        #endregion
    }
}


