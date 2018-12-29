using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CanTeenManagement.View;
using CanTeenManagement.Model;
using CanTeenManagement.CO;
using Microsoft.Win32;
using System.Net.Mail;
using System.Collections.ObjectModel;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;
using System.IO;

namespace CanTeenManagement.ViewModel
{
    public class DetailCustomersViewModel : BaseViewModel
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

        #region Các ô trong edit.
        private string _g_str_fullNameEdit;
        public string g_str_fullNameEdit { get => _g_str_fullNameEdit; set { _g_str_fullNameEdit = value; OnPropertyChanged(); } }

        private string _g_str_genderEdit;
        public string g_str_genderEdit { get => _g_str_genderEdit; set { _g_str_genderEdit = value; OnPropertyChanged(); } }

        private Nullable<int> _g_i_yearOfBirthEdit;
        public Nullable<int> g_i_yearOfBirthEdit { get => _g_i_yearOfBirthEdit; set { _g_i_yearOfBirthEdit = value; OnPropertyChanged(); } }

        private string _g_str_phoneEdit;
        public string g_str_phoneEdit { get => _g_str_phoneEdit; set { _g_str_phoneEdit = value; OnPropertyChanged(); } }

        private string _g_str_emailEdit;
        public string g_str_emailEdit { get => _g_str_emailEdit; set { _g_str_emailEdit = value; OnPropertyChanged(); } }
        #endregion

        #region Các thuộc tính của customer.
        private string _g_str_imageLink;
        public string g_str_imageLink { get => _g_str_imageLink; set { _g_str_imageLink = value; OnPropertyChanged(); } }

        private ImageSource _g_imgSrc_customer;
        public ImageSource g_imgSrc_customer
        {
            get => _g_imgSrc_customer;
            set
            {
                _g_imgSrc_customer = value;
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

        public ICommand g_iCm_ClickCloseCommand { get; set; }

        public ICommand g_iCm_ClickEditInfoCommand { get; set; }

        public ICommand g_iCm_ClickSaveInfoCommand { get; set; }

        public ICommand g_iCm_ClickExportCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }

        public ICommand g_iCm_ClickChangeImageCommand { get; set; }

        public ICommand g_iCm_TextChangedFilterCommand { get; set; }

        public ICommand g_iCm_ClickGoBackCommand { get; set; }
        #endregion

        public DetailCustomersViewModel()
        {
            this.loadData();

            g_iCm_LoadedCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            g_iCm_ClickCloseCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            g_iCm_ClickEditInfoCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickEditInfo(p);
            });

            g_iCm_ClickSaveInfoCommand = new RelayCommand<DetailCustomersView>((p) => { return this.checkSaveInfo(); }, (p) =>
            {
                this.clickSaveInfo(p);
            });

            g_iCm_ClickExportCommand = new RelayCommand<DetailCustomersView>((p) => { return checkExport(); }, (p) =>
            {
                this.clickExport();
            });

            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });

            g_iCm_ClickChangeImageCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickChangeImage();
            });

            g_iCm_TextChangedFilterCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.filterIDOrder(p);
            });

            g_iCm_ClickGoBackCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickGoBack(p);
            });
        }

        private void loadData()
        {
            CustomersView l_customersView = CustomersView.Instance;

            if (l_customersView.DataContext == null)
                return;

            var l_customersVM = l_customersView.DataContext as CustomersViewModel;

            // Thêm danh sách gender.
            List<string> l_listGenders = new List<string>();
            l_listGenders.Add(staticVarClass.gender_feMale);
            l_listGenders.Add(staticVarClass.gender_male);
            l_listGenders.Add(staticVarClass.gender_different);
            g_listGenders = l_listGenders;

            // Thêm danh sách năm sinh.
            List<int> l_listYearOfBirth = new List<int>();
            int l_i_EighteenYearLocal = DateTime.Now.Year - 2018 + 2000;

            for (int i = l_i_EighteenYearLocal; i >= l_i_EighteenYearLocal - (42 + DateTime.Now.Year - 2018); i--)
            {
                l_listYearOfBirth.Add(i);
            }
            g_listYearOfBirth = l_listYearOfBirth;

            #region gán giá trị cho các ô
            g_str_id = l_customersVM.g_str_id;
            g_str_fullName = l_customersVM.g_str_fullName;
            g_str_gender = l_customersVM.g_str_gender;
            g_i_yearOfBirth = l_customersVM.g_i_yearOfBirth;
            g_str_phone = l_customersVM.g_str_phone;
            g_str_email = l_customersVM.g_str_email;
            g_i_cash = l_customersVM.g_i_cash;
            g_i_point = l_customersVM.g_i_point;
            g_i_star = l_customersVM.g_i_star;
            g_str_imageLink = l_customersVM.g_str_imageLink;
            g_imgSrc_customer = staticFunctionClass.LoadBitmap(g_str_imageLink);
            #endregion

            #region đổ dữ liệu vào listview
            this.g_listOrders = new ObservableCollection<ORDERINFO>(dataProvider.Instance.DB.ORDERINFOes.Where(orderinfo => orderinfo.STATUS == staticVarClass.status_done && orderinfo.CUSTOMERID == g_str_id));
            #endregion
        }

        private void loaded(DetailCustomersView p)
        {
            if (p == null)
                return;

            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
        }

        private void clickCloseWindow(DetailCustomersView p)
        {
            p.Close();

            CustomersView l_customersView = CustomersView.Instance;

            if (l_customersView.DataContext == null)
                return;

            var l_customersVM = l_customersView.DataContext as CustomersViewModel;

            for (int i = 0; i < l_customersVM.g_listCustomers.Count(); i++)
            {
                if (l_customersVM.g_listCustomers[i].ID.Trim() == g_str_id)
                {
                    l_customersVM.g_listCustomers[i] = new CUSTOMER()
                    {
                        ID = g_str_id,
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

                    l_customersVM.g_selectedItem = l_customersVM.g_listCustomers[i];
                    break;
                }
            }
        }

        private void clickEditInfo(DetailCustomersView p)
        {
            if (p == null)
                return;

            p.grVInfo.Height = 0;
            p.grVEdit.Height = 350;

            g_str_fullNameEdit = g_str_fullName;
            g_str_genderEdit = g_str_gender;
            g_i_yearOfBirthEdit = g_i_yearOfBirth;
            g_str_phoneEdit = g_str_phone;
            g_str_emailEdit = g_str_email;
        }

        private bool checkSaveInfo()
        {
            if (string.IsNullOrEmpty(g_str_id))
                return false;

            // check id.
            var l_IDList = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == g_str_id);
            if (l_IDList == null || l_IDList.Count() == 0)
                return false;

            return true;
        }

        private void clickSaveInfo(DetailCustomersView p)
        {
            var l_customer = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == g_str_id).SingleOrDefault();
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
                staticFunctionClass.showStatusView(true, "Sửa thông tin khách hàng " + g_str_fullName + " thành công!");

                #region Cập nhật lại thông tin.
                g_str_fullName = g_str_fullNameEdit;
                g_str_gender = g_str_genderEdit;
                g_i_yearOfBirth = g_i_yearOfBirthEdit;
                g_str_phone = g_str_phoneEdit;
                g_str_email = g_str_emailEdit;
                #endregion
            }
            catch
            {
                staticFunctionClass.showStatusView(false, "Sửa thông tin khách hàng " + g_str_id + " thất bại!");
            }

            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
        }

        private bool checkExport()
        {
            if (g_listOrders == null || g_listOrders.Count() == 0)
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
                List<string> l_listHeaders = new List<string> { "Thời gian", "Mã đơn hàng", "Mã nhân viên", "Tổng tiền" };

                for (int x = 1; x < l_listHeaders.Count() + 1; x++)
                {
                    workSheet.Cells[1, x] = l_listHeaders[x - 1];
                    workSheet.Cells[1, x].Font.Bold = true;
                }

                for (int x = 2; x < g_listOrders.Count() + 2; x++)
                {
                    workSheet.Cells[x, 1] = g_listOrders[x - 2].ORDERDATE.ToString().Trim();
                    workSheet.Cells[x, 2] = g_listOrders[x - 2].ID.ToString().Trim();
                    workSheet.Cells[x, 3] = g_listOrders[x - 2].EMPLOYEEID.ToString().Trim();
                    workSheet.Cells[x, 4] = g_listOrders[x - 2].TOTALMONEY.ToString().Trim();
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

        private void mouseLeftButtonDown(DetailCustomersView p)
        {
            p.DragMove();
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
                    // upload new image.
                    string str_path = openFileDialog.FileName;

                    if (str_path != string.Empty)
                    {
                        if (staticFunctionClass.deleteFile(this.g_str_id + staticFunctionClass.getFormat(this.g_str_imageLink)))
                        {
                            string str_newfileName = this.g_str_id + staticFunctionClass.getFormat(str_path);

                            if (staticFunctionClass.uploadFile(str_newfileName, str_path))
                            {
                                // Update image link in database.
                                this.g_str_imageLink = staticVarClass.server_serverDirectory + str_newfileName;

                                // Update image source.
                                this.g_imgSrc_customer = staticFunctionClass.LoadBitmap(this.g_str_imageLink);

                                dataProvider.Instance.DB.EMPLOYEEs.Where(customer => customer.ID == this.g_str_id).ToList()
                                                                  .ForEach(customer => customer.IMAGELINK = g_str_imageLink);
                                dataProvider.Instance.DB.SaveChanges();

                                staticFunctionClass.showStatusView(true, "Đổi ảnh đại diện thành công!");
                            }
                        }
                    }
                }
                catch
                {
                    staticFunctionClass.showStatusView(false, "Đổi ảnh đại diện thất bại!");
                }
            }
        }

        private void clickGoBack(DetailCustomersView p)
        {
            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
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
            CollectionViewSource.GetDefaultView(g_listOrders).Refresh();
        }
    }
}
