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

namespace CanTeenManagement.ViewModel
{
    public class DetailEmployeesViewModel : BaseViewModel
    {
        private ObservableCollection<EMPLOYEE> _g_listEmail;
        public ObservableCollection<EMPLOYEE> g_listEmail
        {
            get => _g_listEmail;
            set
            {
                _g_listEmail = value;
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

        #region Các thuộc tính của employee.
        private string _g_str_imageLink;
        public string g_str_imageLink { get => _g_str_imageLink; set { _g_str_imageLink = value; OnPropertyChanged(); } }

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

            g_iCm_ClickEditInfoCommand = new RelayCommand<DetailEmployeesView>((p) => { return this.checkEditInfo(p); }, (p) =>
            {
                this.clickEditInfo(p);
            });

            g_iCm_ClickSaveInfoCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickSaveInfo(p);
            });

            g_iCm_ClickExportCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
            });

            g_iCm_ClickOpenMailCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickOpenMail(p);
            });

            g_iCm_ClickSendMailCommand = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
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
        }

        private void loaded(DetailEmployeesView p)
        {
            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 0;

            if (p == null)
                return;

            EmployeesView employeesView = EmployeesView.Instance;

            if (employeesView.DataContext == null)
                return;

            var l_employeesVM = employeesView.DataContext as EmployeesViewModel;

            g_listEmail = l_employeesVM.g_listEmloyee;
            foreach (var employee in g_listEmail)
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
            #endregion
        }

        private void clickCloseWindow(DetailEmployeesView p)
        {
            p.Close();

            EmployeesView employeesView = EmployeesView.Instance;

            if (employeesView.DataContext == null)
                return;

            var l_employeesVM = employeesView.DataContext as EmployeesViewModel;

            for (int i = 0; i < l_employeesVM.g_listEmloyee.Count(); i++)
            {
                if (l_employeesVM.g_listEmloyee[i].ID.Trim() == g_str_id)
                {
                    l_employeesVM.g_listEmloyee[i] = new EMPLOYEE()
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

                    l_employeesVM.g_selectedItem = l_employeesVM.g_listEmloyee[i];
                    break;
                }
            }
        }

        private bool checkEditInfo(DetailEmployeesView p)
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

        private void clickEditInfo(DetailEmployeesView p)
        {
            p.grVInfo.Height = 0;
            p.grVEdit.Height = 350;
            p.grVSendMail.Height = 0;
        }

        private void clickSaveInfo(DetailEmployeesView p)
        {
            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 0;

            var l_employee = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_str_id).SingleOrDefault();
            l_employee.FULLNAME = g_str_fullName;
            l_employee.GENDER = g_str_gender;
            l_employee.YEAROFBIRTH = g_i_yearOfBirth;
            l_employee.PHONE = g_str_phone;
            l_employee.EMAIL = g_str_email;
            l_employee.POSITION = g_str_position;
            l_employee.ROLE = g_str_role;
            l_employee.STATUS = g_str_status;

            dataProvider.Instance.DB.SaveChanges();
        }

        private void mouseLeftButtonDown(DetailEmployeesView p)
        {
            p.DragMove();
        }

        private void clickChangeImage(DetailEmployeesView p)
        {
            //myFTP ftp = new myFTP(staticVarClass.ftp_Server, staticVarClass.ftp_userName, staticVarClass.ftp_password);
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";

            // Display OpenFileDialog by calling ShowDialog method.
            Nullable<bool> b_result = openFileDialog.ShowDialog();

            // Get the selected file name and display in a TextBox. 
            if (b_result == true)
            {
                // Open document 
                string str_fullNameChosen = openFileDialog.FileName; // full name.

                // delete old image.
                //var path = g_str_imageLink;
                //g_str_imageLink = @"\\127.0.0.1\CanteenManagement\empty.jpg";
                //if (path != string.Empty && path.Contains("\\") && path.Contains("."))
                //{
                //    const string BackSlash = @"\";
                //    var foundPos = path.LastIndexOf(BackSlash);
                //    var fileNameCurrent = path.Substring(foundPos + 1, path.Length - foundPos - 1);

                //    myFTP ftp = new myFTP(staticVarClass.ftp_Server, staticVarClass.ftp_userName, staticVarClass.ftp_password);
                //    ftp.delete(fileNameCurrent);

                //}

                // upload new image.
                var path = str_fullNameChosen;
                if (path != string.Empty && path.Contains("\\") && path.Contains("."))
                {
                    const string Dot = ".";
                    var foundPos = path.LastIndexOf(Dot);
                    var extension = path.Substring(foundPos + 1, path.Length - foundPos - 1);
                    var newfileName = g_str_id + "1" + Dot + extension;
                    myFTP ftp = new myFTP(staticVarClass.ftp_Server, staticVarClass.ftp_userName, staticVarClass.ftp_password);
                    ftp.upload(newfileName, str_fullNameChosen);

                    // update image link in database.
                    g_str_imageLink = staticVarClass.server_serverDirectory + newfileName;
                    var l_employee = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_str_id).SingleOrDefault();
                    l_employee.IMAGELINK = g_str_imageLink;

                    dataProvider.Instance.DB.SaveChanges();
                }
            }
        }

        private void clickOpenMail(DetailEmployeesView p)
        {
            p.grVInfo.Height = 0;
            p.grVEdit.Height = 0;
            p.grVSendMail.Height = 350;
        }

        private void clickSendMail(DetailEmployeesView p)
        {
            try // Its a good practice to write your code in a try catch block 
            {
                SmtpClient client = new SmtpClient(staticVarClass.email_hostEmail, staticVarClass.email_portEmail);      //Connection Object.
                var message = new MailMessage("ise.nndk.13@gmail.com", g_selectedEmail.EMAIL); // Email Object.
                message.Body = g_str_contentMail + Environment.NewLine + "Sent from " + g_str_email;

                message.Subject = g_str_titleMail;

                client.Credentials = new System.Net.NetworkCredential("ise.nndk.13@gmail.com", "123456aA123456"); // Setting Credential of gmail account.
                client.EnableSsl = true;                // Enabling secured Connection.
                client.Send(message);
                message = null;                         // Free the memory

                p.grVInfo.Height = 350;
                p.grVEdit.Height = 0;
                p.grVSendMail.Height = 0;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
