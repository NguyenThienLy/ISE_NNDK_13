using CanTeenManagement.Model;
using CanTeenManagement.View;
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

namespace CanTeenManagement.ViewModel
{
    public class EmployeesViewModel : BaseViewModel
    {
        private ObservableCollection<EMPLOYEE> _g_listEmloyee;
        public ObservableCollection<EMPLOYEE> g_listEmloyee { get => _g_listEmloyee; set { _g_listEmloyee = value; OnPropertyChanged(); } }

        private string _g_str_filter;
        public string g_str_filter
        {
            get { return _g_str_filter; }
            set
            {
                _g_str_filter = value;
                OnPropertyChanged();

                ICollectionView view = (ICollectionView)CollectionViewSource.GetDefaultView(_g_listEmloyee);
                view.Filter = filterIDEmployee;
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

                    if (g_selectedItem.POSITION == null)
                        g_str_position = string.Empty;
                    else g_str_position = g_selectedItem.POSITION.Trim();

                    g_str_role = g_selectedItem.ROLE.Trim();
                    g_str_status = g_selectedItem.STATUS.Trim();
                }
            }
        }

        #region Các thuộc tính của employee.
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

        public ICommand g_iCm_ClickAddInfoCommand { get; set; }

        public ICommand g_iCm_ClickEditInfoCommand { get; set; }

        public ICommand g_iCm_ClickSaveInfoCommand { get; set; }

        public ICommand g_iCm_TextChangedFilterCommand { get; set; }

        public ICommand g_iCm_ClickDetailCommand { get; set; }
        #endregion

        public EmployeesViewModel()
        {
            this.loadData();

            g_iCm_LoadedCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            g_iCm_ClickAddInfoCommand = new RelayCommand<EmployeesView>((p) => { return this.checkAdd(p); }, (p) =>
            {
                this.clickAdd(p);
            });

            g_iCm_ClickEditInfoCommand = new RelayCommand<EmployeesView>((p) => { return this.checkEdit(p); }, (p) =>
            {
                this.clickEdit(p);
            });

            g_iCm_ClickSaveInfoCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickSave(p);
            });

            g_iCm_TextChangedFilterCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.filterIDEmployee(p);
            });

            g_iCm_ClickDetailCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickDetail(p);
            });
        }

        private void loadData()
        {
            this.g_listEmloyee = new ObservableCollection<EMPLOYEE>(dataProvider.Instance.DB.EMPLOYEEs);
        }

        private void loaded(EmployeesView p)
        {
            if (p == null)
                return;

            //p.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private bool checkAdd(EmployeesView p)
        {
            if (p == null)
                return false;

            if (string.IsNullOrEmpty(g_str_id))
                return false;

            // check id.
            var l_employee = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_str_id);
            if (l_employee == null || l_employee.Count() != 0)
                return false;

            return true;
        }

        private void clickAdd(EmployeesView p)
        {
            //p.rDefTop.Height = new GridLength(40, GridUnitType.Star);

            var l_employee = new EMPLOYEE()
            {
                ID = g_str_id,
                PASSWORD = "123",
                FULLNAME = g_str_fullName,
                GENDER = g_str_gender,
                YEAROFBIRTH = g_i_yearOfBirth,
                PHONE = g_str_phone,
                EMAIL = g_str_email,
                POSITION = g_str_position,
                IMAGELINK = String.Empty,
                ROLE = g_str_role,
                STATUS = g_str_status
            };

            dataProvider.Instance.DB.EMPLOYEEs.Add(l_employee);
            dataProvider.Instance.DB.SaveChanges();

            g_listEmloyee.Add(l_employee);
        }

        private bool checkEdit(EmployeesView p)
        {
            if (p == null)
                return false;

            if (string.IsNullOrEmpty(g_str_id) || g_selectedItem == null)
                return false;

            // check id.
            var l_IDList = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_str_id);
            if (l_IDList == null || l_IDList.Count() == 0)
                return false;

            return true;
        }

        private void clickEdit(EmployeesView p)
        {
            var l_employee = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_selectedItem.ID).SingleOrDefault();
            l_employee.FULLNAME = g_str_fullName;
            l_employee.GENDER = g_str_gender;
            l_employee.YEAROFBIRTH = g_i_yearOfBirth;
            l_employee.PHONE = g_str_phone;
            l_employee.EMAIL = g_str_email;
            l_employee.POSITION = g_str_position;
            l_employee.ROLE = g_str_role;
            l_employee.STATUS = g_str_status;

            dataProvider.Instance.DB.SaveChanges();

            for (int i = 0; i < g_listEmloyee.Count(); i++)
            {
                if (g_listEmloyee[i].ID == g_selectedItem.ID)
                {
                    g_listEmloyee[i] = new EMPLOYEE()
                    {
                        ID = g_selectedItem.ID,
                        FULLNAME = g_str_fullName,
                        GENDER = g_str_gender,
                        YEAROFBIRTH = g_i_yearOfBirth,
                        PHONE = g_str_phone,
                        EMAIL = g_str_email,
                        POSITION = g_str_position,
                        ROLE = g_str_role,
                        STATUS = g_str_status
                    };
                    break;
                }
            }
        }

        private void clickSave(EmployeesView p)
        {
            if (p == null)
                return;

            //p.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private bool filterIDEmployee(object item)
        {
            if (string.IsNullOrEmpty(_g_str_filter))
                return true;
            else
                return ((item as EMPLOYEE).ID.IndexOf(_g_str_filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void filterIDEmployee(EmployeesView p)
        {
            if (p == null)
                return;

            CollectionViewSource.GetDefaultView(g_listEmloyee).Refresh();
        }

        private void clickDetail(EmployeesView p)
        {
            if (p == null)
                return;

            MainWindow mainWd = MainWindow.Instance;
            mainWd.Opacity = 0.5;
            p.Opacity = 0.5;

            DetailEmployeesView detailEmpView = new DetailEmployeesView();

            detailEmpView.ShowDialog();

            mainWd.Opacity = 100;
            p.Opacity = 100;
        }
    }
}
