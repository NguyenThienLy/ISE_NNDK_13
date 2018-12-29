using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanTeenManagement.View;
using CanTeenManagement.Model;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.Win32;
using CanTeenManagement.CO;

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

                ICollectionView view = (ICollectionView)CollectionViewSource.GetDefaultView(g_listCustomers);
                view.Filter = filterIDCustomer;
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
                if (g_selectedItem != null)
                {
                    // Binding giá trị đang chọn lên text box.
                    g_str_id = g_selectedItem.ID.Trim();

                    if (g_selectedItem.FULLNAME == null)
                        g_str_fullName = string.Empty;
                    else g_str_fullName = g_selectedItem.FULLNAME.Trim();

                    g_str_gender = g_selectedItem.GENDER.Trim();
                    g_i_yearOfBirth = g_selectedItem.YEAROFBIRTH;

                    if (g_selectedItem.PHONE == null)
                        g_str_phone = string.Empty;
                    else g_str_phone = g_selectedItem.PHONE.Trim();

                    if (g_selectedItem.EMAIL == null)
                        g_str_email = string.Empty;
                    else g_str_email = g_selectedItem.EMAIL.Trim();

                    if (g_selectedItem.CASH == null)
                        g_i_cash = 0;
                    else g_i_cash = g_selectedItem.CASH;

                    if (g_selectedItem.POINT == null)
                        g_i_point = 0;
                    else g_i_point = g_selectedItem.POINT;

                    if (g_selectedItem.STAR == null)
                        g_i_star = 0;
                    else g_i_star = g_selectedItem.STAR;

                    if (g_selectedItem.IMAGELINK == null)
                        g_str_imageLink = string.Empty;
                    else g_str_imageLink = g_selectedItem.IMAGELINK.Trim();
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

        int g_i_addOrEdit = 0;

        #region Các thuộc tính của customer.
        private string _g_str_imageLink;
        public string g_str_imageLink { get => _g_str_imageLink; set { _g_str_imageLink = value; OnPropertyChanged(); } }

        private string _g_str_id;
        public string g_str_id
        {
            get => _g_str_id;
            set
            {
                long i = 0;
                if (value != string.Empty)
                    if (!long.TryParse(value, out i))
                        value = g_str_id;

                _g_str_id = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_fullName;
        public string g_str_fullName { get => _g_str_fullName; set { _g_str_fullName = value; OnPropertyChanged(); } }

        private string _g_str_gender;
        public string g_str_gender { get => _g_str_gender; set { _g_str_gender = value; OnPropertyChanged(); } }

        private Nullable<int> _g_i_yearOfBirth;
        public Nullable<int> g_i_yearOfBirth { get => _g_i_yearOfBirth; set { _g_i_yearOfBirth = value; OnPropertyChanged(); } }

        private string _g_str_phone;
        public string g_str_phone { get => _g_str_phone; set { _g_str_phone = value; OnPropertyChanged(); } }

        private string _g_str_email;
        public string g_str_email { get => _g_str_email; set { _g_str_email = value; OnPropertyChanged(); } }

        private Nullable<int> _g_i_cash;
        public Nullable<int> g_i_cash { get => _g_i_cash; set { _g_i_cash = value; OnPropertyChanged(); } }

        private Nullable<int> _g_i_point;
        public Nullable<int> g_i_point { get => _g_i_point; set { _g_i_point = value; OnPropertyChanged(); } }

        private Nullable<int> _g_i_star;
        public Nullable<int> g_i_star { get => _g_i_star; set { _g_i_star = value; OnPropertyChanged(); } }
        #endregion

        #region commands.
        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_ClickAddCommand { get; set; }

        public ICommand g_iCm_ClickEditCommand { get; set; }

        public ICommand g_iCm_ClickSaveCommand { get; set; }

        public ICommand g_iCm_ClickExportCommand { get; set; }

        public ICommand g_iCm_TextChangedFilterCommand { get; set; }

        public ICommand g_iCm_ClickDetailCommand { get; set; }

        public ICommand g_iCm_ClickUtilityCommand { get; set; }

        public ICommand g_iCm_ClickGoBackCommand { get; set; }
        #endregion

        public CustomersViewModel()
        {
            this.loadData();

            g_iCm_LoadedCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.loaded();
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

            g_iCm_ClickUtilityCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickUtility();
            });

            g_iCm_ClickGoBackCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickGoBack();
            });
        }

        private void loadData()
        {
            this.g_listCustomers = new ObservableCollection<CUSTOMER>(dataProvider.Instance.DB.CUSTOMERs);

            // Thêm danh sách gender.
            List<string> l_listGenders = new List<string>();
            l_listGenders.Add(staticVarClass.gender_feMale);
            l_listGenders.Add(staticVarClass.gender_male);
            l_listGenders.Add(staticVarClass.gender_different);
            g_listGenders = l_listGenders;
            g_str_gender = staticVarClass.gender_feMale; // mặc định.

            // Thêm danh sách năm sinh.
            List<int> l_listYearOfBirth = new List<int>();
            int l_i_EighteenYearLocal = DateTime.Now.Year - 2018 + 2000;

            for (int i = l_i_EighteenYearLocal; i >= l_i_EighteenYearLocal - (42 + DateTime.Now.Year - 2018); i--)
            {
                l_listYearOfBirth.Add(i);
            }
            g_listYearOfBirth = l_listYearOfBirth;
            g_i_yearOfBirth = l_listYearOfBirth[0];
        }

        private void loaded()
        {
        }

        private string getNameForPicture(string id)
        {
            string l_temp = string.Empty;

            l_temp = id[4].ToString() + id[5].ToString() + id[6].ToString();

            return l_temp;
        }

        private bool clickGoBack()
        {
            g_i_height = 0;
            g_i_addOrEdit = 0;
            g_b_isReadOnlyID = false;
            return true;
        }

        private bool checkAdd()
        {
            if (g_i_addOrEdit != 0)
                return false;

            return true;
        }

        private void clickAdd()
        {
            g_i_height = 40;
            g_i_addOrEdit = 1;

            #region Làm trống text box.
            g_str_id = string.Empty;
            g_str_fullName = string.Empty;
            g_str_gender = g_listGenders[0];
            g_i_yearOfBirth = g_listYearOfBirth[0];
            g_str_phone = string.Empty;
            g_str_email = string.Empty;
            g_i_cash = 0;
            g_i_point = 0;
            #endregion
        }

        private bool checkEdit()
        {
            if (g_selectedItem == null || g_i_addOrEdit != 0)
                return false;

            g_selectedItem = g_selectedItem;
            return true;
        }

        private void clickEdit()
        {
            g_i_height = 40;
            g_i_addOrEdit = 2;
            g_b_isReadOnlyID = true;
        }

        private bool checkSave()
        {
            if (g_i_addOrEdit == 0)
            {
                return false;
            }
            else if (g_i_addOrEdit == 1)
            {
                if (string.IsNullOrEmpty(g_str_id))
                    return false;

                // check id.
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == g_str_id);
                if (l_customer == null || l_customer.Count() != 0)
                    return false;

            }
            else if (g_i_addOrEdit == 2)
            {
                // check id.
                var l_IDList = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == g_str_id);
                if (l_IDList == null || l_IDList.Count() == 0)
                    return false;
            }

            return true;
        }

        private void clickSave()
        {
            if (g_i_addOrEdit == 1)
            {
                var l_customer = new CUSTOMER()
                {
                    ID = g_str_id,
                    PIN = "123456",
                    FULLNAME = g_str_fullName,
                    GENDER = g_str_gender,
                    YEAROFBIRTH = g_i_yearOfBirth,
                    PHONE = g_str_phone,
                    EMAIL = g_str_email,
                    CASH = g_i_cash,
                    POINT = g_i_point,
                    STAR = 1,
                    IMAGELINK = staticVarClass.server_serverDirectory + g_str_id + staticVarClass.format_JPG
                };

                try
                {
                    dataProvider.Instance.DB.CUSTOMERs.Add(l_customer);
                    dataProvider.Instance.DB.SaveChanges();

                    // Make image default.
                    staticFunctionClass.CreateProfilePicture(this.getNameForPicture(l_customer.ID), l_customer.ID, 95);
                    g_listCustomers.Add(l_customer);
                    staticFunctionClass.showStatusView(true, "Thêm khách hàng " + g_str_id + " thành công!");
                }
                catch
                {
                    staticFunctionClass.showStatusView(false, "Thêm khách hàng " + g_str_id + " thất bại!");
                }
            }
            else if (g_i_addOrEdit == 2)
            {
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == g_selectedItem.ID).SingleOrDefault();
                l_customer.FULLNAME = g_str_fullName;
                l_customer.GENDER = g_str_gender;
                l_customer.YEAROFBIRTH = g_i_yearOfBirth;
                l_customer.PHONE = g_str_phone;
                l_customer.EMAIL = g_str_email;
                l_customer.CASH = g_i_cash;
                l_customer.POINT = g_i_point;
                l_customer.STAR = g_i_star;
                l_customer.IMAGELINK = g_str_imageLink;

                try
                {
                    dataProvider.Instance.DB.SaveChanges();
                    staticFunctionClass.showStatusView(true, "Sửa thông tin khách hàng " + g_str_id + " thành công!");
                }
                catch
                {
                    staticFunctionClass.showStatusView(false, "Sửa thông tin khách hàng " + g_str_id + " thất bại!");
                }

                g_b_isReadOnlyID = false;
                for (int i = 0; i < g_listCustomers.Count(); i++)
                {
                    if (g_listCustomers[i].ID == g_selectedItem.ID)
                    {
                        g_listCustomers[i] = new CUSTOMER()
                        {
                            ID = g_selectedItem.ID,
                            FULLNAME = g_str_fullName,
                            GENDER = g_str_gender,
                            YEAROFBIRTH = g_i_yearOfBirth,
                            PHONE = g_str_phone,
                            EMAIL = g_str_email,
                            CASH = g_i_cash,
                            POINT = g_i_point,
                            STAR = g_i_star,
                            IMAGELINK = g_str_imageLink
                        };
                        break;
                    }
                }
            }

            g_i_height = 0;
            g_i_addOrEdit = 0;
        }

        private bool checkExport()
        {
            if (g_i_addOrEdit != 0 || g_listCustomers.Count() == 0)
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

                for (int x = 2; x < g_listCustomers.Count() + 2; x++)
                {
                    workSheet.Cells[x, 1] = g_listCustomers[x - 2].ID.ToString().Trim();
                    workSheet.Cells[x, 2] = g_listCustomers[x - 2].FULLNAME.ToString().Trim();
                    workSheet.Cells[x, 3] = g_listCustomers[x - 2].GENDER.ToString().Trim();
                    workSheet.Cells[x, 4] = g_listCustomers[x - 2].YEAROFBIRTH.ToString().Trim();
                    workSheet.Cells[x, 5] = g_listCustomers[x - 2].PHONE.ToString().Trim();
                    workSheet.Cells[x, 6] = g_listCustomers[x - 2].EMAIL.ToString().Trim();
                    workSheet.Cells[x, 7] = g_listCustomers[x - 2].CASH.ToString().Trim();
                    workSheet.Cells[x, 8] = g_listCustomers[x - 2].POINT.ToString().Trim();
                    workSheet.Cells[x, 9] = g_listCustomers[x - 2].STAR.ToString().Trim();
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

        private bool filterIDCustomer(object item)
        {
            if (string.IsNullOrEmpty(_g_str_filter))
                return true;
            else
                return ((item as CUSTOMER).ID.IndexOf(g_str_filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void filterIDCustomer()
        {
            CollectionViewSource.GetDefaultView(g_listCustomers).Refresh();
        }

        private void clickDetail(CUSTOMER p)
        {
            if (p == null)
                return;

            g_selectedItem = p;

            MainWindow mainWd = MainWindow.Instance;
            CustomersView customersV = CustomersView.Instance;

            mainWd.Opacity = 0.5;
            customersV.Opacity = 0.5;

            DetailCustomersView detailCusView = new DetailCustomersView();
            detailCusView.ShowDialog();

            mainWd.Opacity = 100;
            customersV.Opacity = 100;
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
    }
}


