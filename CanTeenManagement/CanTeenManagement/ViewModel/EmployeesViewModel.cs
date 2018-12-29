using CanTeenManagement.Model;
using CanTeenManagement.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using CanTeenManagement.CO;

namespace CanTeenManagement.ViewModel
{
    public class EmployeesViewModel : BaseViewModel
    {
        private ObservableCollection<EMPLOYEE> _g_listEmployees;
        public ObservableCollection<EMPLOYEE> g_listEmployees
        {
            get => _g_listEmployees;
            set
            {
                _g_listEmployees = value;
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

                ICollectionView view = (ICollectionView)CollectionViewSource.GetDefaultView(_g_listEmployees);
                view.Filter = filterIDEmployee;
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

        private EMPLOYEE _g_selectedItem;
        public EMPLOYEE g_selectedItem
        {
            get => _g_selectedItem;
            set
            {
                _g_selectedItem = value;
                OnPropertyChanged();
                if (g_selectedItem != null && g_i_addOrEdit == 0)
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

                    if (g_selectedItem.POSITION == null)
                        g_str_position = string.Empty;
                    else g_str_position = g_selectedItem.POSITION.Trim();

                    g_str_role = g_selectedItem.ROLE.Trim();
                    g_str_status = g_selectedItem.STATUS.Trim();

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

        private List<string> _g_listRoles;
        public List<string> g_listRoles
        {
            get => _g_listRoles;
            set
            {
                _g_listRoles = value;
                OnPropertyChanged();
            }
        }

        private List<string> _g_listStatus;
        public List<string> g_listStatus
        {
            get => _g_listStatus;
            set
            {
                _g_listStatus = value;
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

        #region Các thuộc tính của employee.
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
                    if (long.TryParse(value, out i))
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

        private string _g_str_position;
        public string g_str_position { get => _g_str_position; set { _g_str_position = value; OnPropertyChanged(); } }

        private string _g_str_role;
        public string g_str_role { get => _g_str_role; set { _g_str_role = value; OnPropertyChanged(); } }

        private string _g_str_status;
        public string g_str_status { get => _g_str_status; set { _g_str_status = value; OnPropertyChanged(); } }
        #endregion

        #region commands.
        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_ClickAddInfoCommand { get; set; }

        public ICommand g_iCm_ClickEditInfoCommand { get; set; }

        public ICommand g_iCm_ClickSaveInfoCommand { get; set; }

        public ICommand g_iCm_ClickExportCommand { get; set; }

        public ICommand g_iCm_TextChangedFilterCommand { get; set; }

        public ICommand g_iCm_ClickDetailCommand { get; set; }

        public ICommand g_iCm_ClickGoBackCommand { get; set; }
        #endregion

        public EmployeesViewModel()
        {
            this.loadData();

            g_iCm_LoadedCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.loaded();
            });

            g_iCm_ClickAddInfoCommand = new RelayCommand<EmployeesView>((p) => { return this.checkAdd(); }, (p) =>
            {
                this.clickAdd();
            });

            g_iCm_ClickEditInfoCommand = new RelayCommand<EmployeesView>((p) => { return this.checkEdit(); }, (p) =>
            {
                this.clickEdit();
            });

            g_iCm_ClickSaveInfoCommand = new RelayCommand<EmployeesView>((p) => { return this.checkSave(); }, (p) =>
            {
                this.clickSave();
            });

            g_iCm_ClickExportCommand = new RelayCommand<EmployeesView>((p) => { return this.checkExport(); }, (p) =>
            {
                this.clickExport();
            });

            g_iCm_TextChangedFilterCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.filterIDEmployee();
            });

            g_iCm_ClickDetailCommand = new RelayCommand<EMPLOYEE>((p) => { return true; }, (p) =>
            {
                this.clickDetail(p);
            });

            g_iCm_ClickGoBackCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickGoBack();
            });
        }

        private void loadData()
        {
            this.g_listEmployees = new ObservableCollection<EMPLOYEE>(dataProvider.Instance.DB.EMPLOYEEs);

            // Thêm danh sách gender.
            List<string> l_listGenders = new List<string>();
            l_listGenders.Add(staticVarClass.gender_feMale);
            l_listGenders.Add(staticVarClass.gender_male);
            l_listGenders.Add(staticVarClass.gender_different);
            g_listGenders = l_listGenders;
            g_str_gender = staticVarClass.gender_feMale; // mặc định.

            // Thêm danh sách role.
            List<string> l_listRoles = new List<string>();
            l_listRoles.Add(staticVarClass.role_member);
            l_listRoles.Add(staticVarClass.role_admin);
            g_listRoles = l_listRoles;
            g_str_role = l_listRoles[0]; // mặc định.

            // Thêm danh sách status.
            List<string> l_listStatus = new List<string>();
            l_listStatus.Add("Đang làm");
            l_listStatus.Add("Đã nghỉ");
            g_listStatus = l_listStatus;
            g_str_status = l_listStatus[0]; // mặc định.

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
            string l_dot = ".";
            int l_foundPos = id.LastIndexOf(l_dot);

            l_temp = id[0].ToString().ToUpper() + l_dot + id[l_foundPos + 1].ToString().ToUpper();

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
            g_str_position = string.Empty;
            g_str_role = g_listRoles[0];
            g_str_status = g_listStatus[0];
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
                var l_IDList = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_str_id);
                if (l_IDList == null || l_IDList.Count() != 0)
                    return false;
            }
            else if (g_i_addOrEdit == 2)
            {
                // check id.
                var l_IDList = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_str_id);
                if (l_IDList == null || l_IDList.Count() == 0)
                    return false;
            }

            return true;
        }

        private void clickSave()
        {
            if (g_i_addOrEdit == 1)
            {
                var l_employee = new EMPLOYEE()
                {
                    ID = g_str_id,
                    PASSWORD = "123456",
                    FULLNAME = g_str_fullName,
                    GENDER = g_str_gender,
                    YEAROFBIRTH = g_i_yearOfBirth,
                    PHONE = g_str_phone,
                    EMAIL = g_str_email,
                    POSITION = g_str_position,
                    IMAGELINK = staticVarClass.server_serverDirectory + g_str_id + staticVarClass.format_JPG,
                    ROLE = g_str_role,
                    STATUS = g_str_status
                };

                try
                {
                    dataProvider.Instance.DB.EMPLOYEEs.Add(l_employee);
                    dataProvider.Instance.DB.SaveChanges();

                    // Make image default.
                    staticFunctionClass.CreateProfilePicture(this.getNameForPicture(l_employee.ID), l_employee.ID, 80);
                    g_listEmployees.Add(l_employee);
                    staticFunctionClass.showStatusView(true, "Thêm thành viên " + g_str_id + " thành công!");
                }
                catch
                {
                    staticFunctionClass.showStatusView(false, "Thêm thành viên " + g_str_id + " thất bại!");
                }
            }
            else if (g_i_addOrEdit == 2)
            {
                var l_employee = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_selectedItem.ID).SingleOrDefault();
                l_employee.FULLNAME = g_str_fullName;
                l_employee.GENDER = g_str_gender;
                l_employee.YEAROFBIRTH = g_i_yearOfBirth;
                l_employee.PHONE = g_str_phone;
                l_employee.EMAIL = g_str_email;
                l_employee.POSITION = g_str_position;
                l_employee.IMAGELINK = g_str_imageLink;
                l_employee.ROLE = g_str_role;
                l_employee.STATUS = g_str_status;

                try
                {
                    dataProvider.Instance.DB.SaveChanges();
                    staticFunctionClass.showStatusView(true, "Sửa thông tin thành viên " + g_str_id + " thành công!");
                }
                catch
                {
                    staticFunctionClass.showStatusView(false, "Sửa thông tin thành viên " + g_str_id + " thất bại!");
                }

                g_b_isReadOnlyID = false;
                for (int i = 0; i < g_listEmployees.Count(); i++)
                {
                    if (g_listEmployees[i].ID.Trim() == g_selectedItem.ID.Trim())
                    {
                        g_listEmployees[i] = new EMPLOYEE()
                        {
                            ID = g_selectedItem.ID,
                            FULLNAME = g_str_fullName,
                            GENDER = g_str_gender,
                            YEAROFBIRTH = g_i_yearOfBirth,
                            PHONE = g_str_phone,
                            EMAIL = g_str_email,
                            POSITION = g_str_position,
                            IMAGELINK = g_str_imageLink,
                            ROLE = g_str_role,
                            STATUS = g_str_status
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
            if (g_i_addOrEdit != 0 || g_listEmployees.Count() == 0)
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
                List<string> l_listHeaders = new List<string> { "Mã nhân viên", "Họ và tên", "Giới tính", "Năm sinh", "Số điện thoại", "Email", "Chức vụ", "Quyền", "Trạng thái" };

                for (int x = 1; x < l_listHeaders.Count() + 1; x++)
                {
                    workSheet.Cells[1, x] = l_listHeaders[x - 1];
                    workSheet.Cells[1, x].Font.Bold = true;
                }

                for (int x = 2; x < g_listEmployees.Count() + 2; x++)
                {
                    workSheet.Cells[x, 1] = g_listEmployees[x - 2].ID.ToString().Trim();
                    workSheet.Cells[x, 2] = g_listEmployees[x - 2].FULLNAME.ToString().Trim();
                    workSheet.Cells[x, 3] = g_listEmployees[x - 2].GENDER.ToString().Trim();
                    workSheet.Cells[x, 4] = g_listEmployees[x - 2].YEAROFBIRTH.ToString().Trim();
                    workSheet.Cells[x, 5] = g_listEmployees[x - 2].PHONE.ToString().Trim();
                    workSheet.Cells[x, 6] = g_listEmployees[x - 2].EMAIL.ToString().Trim();
                    workSheet.Cells[x, 7] = g_listEmployees[x - 2].POSITION.ToString().Trim();
                    workSheet.Cells[x, 8] = g_listEmployees[x - 2].ROLE.ToString().Trim();
                    workSheet.Cells[x, 9] = g_listEmployees[x - 2].STATUS.ToString().Trim();
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

        private bool filterIDEmployee(object item)
        {
            if (string.IsNullOrEmpty(_g_str_filter))
                return true;
            else
                return ((item as EMPLOYEE).ID.IndexOf(_g_str_filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void filterIDEmployee()
        {
            CollectionViewSource.GetDefaultView(g_listEmployees).Refresh();
        }

        private void clickDetail(EMPLOYEE p)
        {
            // Lấy cái p này chính là dòng đang được chọn.
            // Tui có làm ở trong order view truyền vào pay view bà tham khảo đó nha.
            if (p == null)
                return;

            g_i_addOrEdit = 0;
            g_selectedItem = p;

            MainWindow mainWd = MainWindow.Instance;
            EmployeesView employeesV = EmployeesView.Instance;

            mainWd.Opacity = 0.5;
            employeesV.Opacity = 0.5;

            DetailEmployeesView detailEmpView = new DetailEmployeesView();
            detailEmpView.ShowDialog();

            mainWd.Opacity = 100;
            employeesV.Opacity = 100;
        }
    }
}
