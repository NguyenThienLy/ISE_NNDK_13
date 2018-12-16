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

namespace CanTeenManagement.ViewModel
{
    public class DetailEmployeesViewModel : BaseViewModel
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

        #region Các ô trong gửi mail.
        private EMPLOYEE _g_selectedEmployee;
        public EMPLOYEE g_selectedEmployee
        {
            get => _g_selectedEmployee;
            set
            {
                _g_selectedEmployee = value;
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

        public ICommand g_iCm_ClickCloseCommand { get; set; }

        public ICommand g_iCm_ClickEditInfoCommand { get; set; }

        public ICommand g_iCm_ClickSaveInfoCommand { get; set; }

        public ICommand g_iCm_ClickExportCommand { get; set; }

        public ICommand g_iCm_ClickSendMailCommand { get; set; }

        public ICommand g_iCm_ClickOpenMailCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }

        public ICommand g_iCm_ClickChangeImageCommand { get; set; }

        public ICommand g_iCm_TextChangedFilterCommand { get; set; }

        public ICommand g_iCm_ClickGoBackCommand { get; set; }
        #endregion

        public DetailEmployeesViewModel()
        {
            g_iCm_LoadedCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            g_iCm_ClickCloseCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            g_iCm_ClickEditInfoCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickEditInfo(p);
            });

            g_iCm_ClickSaveInfoCommand = new RelayCommand<DetailEmployeesView>((p) => { return this.checkSaveInfo(p); }, (p) =>
            {
                this.clickSaveInfo(p);
            });

            g_iCm_ClickExportCommand = new RelayCommand<DetailEmployeesView>((p) => { return checkExport(p); }, (p) =>
            {
                this.clickExport(p);
            });

            g_iCm_ClickOpenMailCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickOpenMail(p);
            });

            g_iCm_ClickSendMailCommand = new RelayCommand<DetailEmployeesView>((p) => { return this.checkSendMail(p); }, (p) =>
            {
                this.clickSendMail(p);
            });

            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });

            g_iCm_ClickChangeImageCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickChangeImage(p);
            });

            g_iCm_TextChangedFilterCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.filterIDOrder(p);
            });

            g_iCm_ClickGoBackCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickGoBack(p);
            });
        }

        private void loaded(DetailEmployeesView p)
        {
            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 0;

            if (p == null)
                return;

            EmployeesView l_employeesView = EmployeesView.Instance;

            if (l_employeesView.DataContext == null)
                return;

            var l_employeesVM = l_employeesView.DataContext as EmployeesViewModel;

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

            // Thêm danh sách email.
            g_listEmployees = l_employeesVM.g_listEmployees;
            foreach (var employee in g_listEmployees)
            {
                employee.EMAIL = employee.EMAIL.Trim();
            }

            #region gán giá trị cho các ô
            g_str_id = l_employeesVM.g_str_id;
            g_str_fullName = l_employeesVM.g_str_fullName;
            g_str_gender = l_employeesVM.g_str_gender;
            g_i_yearOfBirth = l_employeesVM.g_i_yearOfBirth;
            g_str_phone = l_employeesVM.g_str_phone;
            g_str_email = l_employeesVM.g_str_email;
            g_str_position = l_employeesVM.g_str_position;
            g_str_role = l_employeesVM.g_str_role;
            g_str_status = l_employeesVM.g_str_status;
            g_str_imageLink = l_employeesVM.g_str_imageLink;
            g_imgSrc_employee = staticFunctionClass.LoadBitmap(g_str_imageLink);
            #endregion

            #region đổ dữ liệu vào listview
            this.g_listOrders = new ObservableCollection<ORDERINFO>(dataProvider.Instance.DB.ORDERINFOes.Where(orderinfo => orderinfo.STATUS == staticVarClass.status_done && orderinfo.EMPLOYEEID == g_str_id));
            #endregion
        }

        private void clickCloseWindow(DetailEmployeesView p)
        {
            p.Close();

            EmployeesView l_employeesView = EmployeesView.Instance;

            if (l_employeesView.DataContext == null)
                return;

            var l_employeesVM = l_employeesView.DataContext as EmployeesViewModel;

            for (int i = 0; i < l_employeesVM.g_listEmployees.Count(); i++)
            {
                if (l_employeesVM.g_listEmployees[i].ID.Trim() == g_str_id)
                {
                    l_employeesVM.g_listEmployees[i] = new EMPLOYEE()
                    {
                        ID = g_str_id,
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

                    l_employeesVM.g_selectedItem = l_employeesVM.g_listEmployees[i];
                    break;
                }
            }
        }

        private void clickEditInfo(DetailEmployeesView p)
        {
            p.grVInfo.Height = 0;
            p.grVEdit.Height = 350;
            p.grVSendMail.Height = 0;
            g_str_fullNameEdit = g_str_fullName;
            g_str_genderEdit = g_str_gender;
            g_i_yearOfBirthEdit = g_i_yearOfBirth;
            g_str_phoneEdit = g_str_phone;
            g_str_emailEdit = g_str_email;
        }

        private bool checkSaveInfo(DetailEmployeesView p)
        {
            if (p == null)
                return false;

            if (string.IsNullOrEmpty(g_str_id))
                return false;

            // check id.
            var l_IDList = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_str_id);
            if (l_IDList == null || l_IDList.Count() == 0)
                return false;

            return true;
        }

        private void clickSaveInfo(DetailEmployeesView p)
        {
            var l_employee = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_str_id).SingleOrDefault();
            l_employee.FULLNAME = g_str_fullNameEdit;
            l_employee.GENDER = g_str_genderEdit;
            l_employee.YEAROFBIRTH = g_i_yearOfBirthEdit;
            l_employee.PHONE = g_str_phoneEdit;
            l_employee.EMAIL = g_str_emailEdit;
            l_employee.POSITION = g_str_position;
            l_employee.ROLE = g_str_role;
            l_employee.STATUS = g_str_status;
            l_employee.IMAGELINK = g_str_imageLink;

            dataProvider.Instance.DB.SaveChanges();

            staticFunctionClass.showStatusView(true, "Sửa thông tin của nhân viên " + g_str_fullName + " thành công!");

            #region Cập nhật lại thông tin.
            g_str_fullName = g_str_fullNameEdit;
            g_str_gender = g_str_genderEdit;
            g_i_yearOfBirth = g_i_yearOfBirthEdit;
            g_str_phone = g_str_phoneEdit;
            g_str_email = g_str_emailEdit;
            #endregion

            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 0;
        }

        private bool checkExport(DetailEmployeesView p)
        {
            if (p == null)
                return false;

            if (g_listOrders == null || g_listOrders.Count() == 0)
                return false;

            return true;
        }

        private void clickExport(DetailEmployeesView p)
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

                for (int x = 2; x < g_listOrders.Count() + 2; x++)
                {
                    workSheet.Cells[x, 1] = g_listOrders[x - 2].ORDERDATE.ToString().Trim();
                    workSheet.Cells[x, 2] = g_listOrders[x - 2].ID.ToString().Trim();
                    workSheet.Cells[x, 3] = g_listOrders[x - 2].CUSTOMERID.ToString().Trim();
                    workSheet.Cells[x, 4] = g_listOrders[x - 2].TOTALMONEY.ToString().Trim();
                }

                // AutoSet Cell Widths to Content Size
                workSheet.Cells.Select();
                workSheet.Cells.EntireColumn.AutoFit();

                workBook.SaveAs(str_fullNameChosen, Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                            Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                            Excel.XlSaveConflictResolution.xlUserResolution, true,
                            Missing.Value, Missing.Value, Missing.Value);
                workBook.Close();
                excel.Quit();
            }

        }

        private void mouseLeftButtonDown(DetailEmployeesView p)
        {
            p.DragMove();
        }

        private void clickChangeImage(DetailEmployeesView p)
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
                                this.g_imgSrc_employee = staticFunctionClass.LoadBitmap(this.g_str_imageLink);

                                dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == this.g_str_id).ToList()
                                                                  .ForEach(employee => employee.IMAGELINK = g_str_imageLink);
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

        private void clickOpenMail(DetailEmployeesView p)
        {
            p.grVInfo.Height = 0;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 350;
            g_selectedEmployee = null;
            g_str_titleMail = string.Empty;
            g_str_contentMail = string.Empty;
        }

        private bool checkSendMail(DetailEmployeesView p)
        {
            if (p == null)
                return false;

            if (string.IsNullOrEmpty(g_str_id))
                return false;

            // check có email của ng gửi chưa.
            if (string.IsNullOrEmpty(g_str_email))
                return false;

            // check có email của người nhận chưa.
            if (g_selectedEmployee == null)
                return false;

            return true;
        }

        private void clickSendMail(DetailEmployeesView p)
        {
            try // Its a good practice to write your code in a try catch block 
            {
                var l_from = "ise.nndk.13@gmail.com";
                var l_password = "123456aA123456";
                var l_to = g_selectedEmployee.EMAIL;
                SmtpClient client = new SmtpClient(staticVarClass.email_hostEmail, staticVarClass.email_portEmail);      //Connection Object.
                var message = new MailMessage(l_from, l_to); // Email Object.
                message.Body = g_str_contentMail + Environment.NewLine + "Sent from " + g_str_email;

                message.Subject = g_str_titleMail;

                client.Credentials = new System.Net.NetworkCredential(l_from, l_password); // Setting Credential of gmail account.
                client.EnableSsl = true;                // Enabling secured Connection.
                client.Send(message);
                message = null;                         // Free the memory

                staticFunctionClass.showStatusView(true, "Gửi email đến " + l_to + " thành công!");

                p.grVInfo.Height = 350;
                p.grVEdit.Height = 0;
                p.grVSendMail.Height = 0;
            }
            catch (Exception ex)
            {
                staticFunctionClass.showStatusView(false, "Gửi email đến " + g_selectedEmployee.EMAIL + " thất bại!");
            }
        }

        private void clickGoBack(DetailEmployeesView p)
        {
            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 0;
        }

        private bool filterIDOrder(object item)
        {
            if (string.IsNullOrEmpty(_g_str_filter))
                return true;
            else
                return ((item as ORDERINFO).ID.IndexOf(_g_str_filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void filterIDOrder(DetailEmployeesView p)
        {
            if (p == null)
                return;

            CollectionViewSource.GetDefaultView(g_listOrders).Refresh();
        }
    }
}
