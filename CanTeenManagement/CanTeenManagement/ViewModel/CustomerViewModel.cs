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

namespace CanTeenManagement.ViewModel
{
    public class CustomersViewModel : BaseViewModel
    {
        private ObservableCollection<CUSTOMER> _g_listCustomer;
        public ObservableCollection<CUSTOMER> g_listCustomer
        { get => _g_listCustomer;
            set
            { _g_listCustomer = value;
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

                ICollectionView view = (ICollectionView)CollectionViewSource.GetDefaultView(g_listCustomer);
                view.Filter = filterIDCustomer;
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
                }
            }
        }

        #region Các thuộc tính của customer.
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
        #endregion

        #region commands.
        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_ClickAddCommand { get; set; }

        public ICommand g_iCm_ClickEditCommand { get; set; }

        public ICommand g_iCm_ClickSaveCommand { get; set; }

        public ICommand g_iCm_TextChangedFilterCommand { get; set; }

        public ICommand g_iCm_ClickDetailCommand { get; set; }
        #endregion

        public CustomersViewModel()
        {
            this.loadData();

            g_iCm_LoadedCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            g_iCm_ClickAddCommand = new RelayCommand<CustomersView>((p) => { return this.checkAdd(p); }, (p) =>
            {
                this.clickAdd(p);
            });

            g_iCm_ClickEditCommand = new RelayCommand<CustomersView>((p) => { return this.checkEdit(p); }, (p) =>
            {
                this.clickEdit(p);
            });

            g_iCm_ClickSaveCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickSave(p);
            });

            g_iCm_TextChangedFilterCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.filterIDCustomer(p);
            });

            g_iCm_ClickDetailCommand = new RelayCommand<CUSTOMER>((p) => { return true; }, (p) =>
            {
                this.clickDetail(p);
            });
        }

        private void loadData()
        {
            this.g_listCustomer = new ObservableCollection<CUSTOMER>(dataProvider.Instance.DB.CUSTOMERs);
        }

        private void loaded(CustomersView p)
        {
            if (p == null)
                return;

            //p.rDefTop.Height = 0;

            //p.rDefTop.Height = new GridLength(40, GridUnitType.Star);
        }

        private bool checkAdd(CustomersView p)
        {
            if (p == null)
                return false;

            if (string.IsNullOrEmpty(g_str_id))
                return false;

            // check id.
            var l_customer = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == g_str_id);
            if (l_customer == null || l_customer.Count() != 0)
                return false;

            return true;
        }

        private void clickAdd(CustomersView p)
        {
            //p.rDefTop.Height = new GridLength(40, GridUnitType.Star);

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
                IMAGELINK = ""
            };

            dataProvider.Instance.DB.CUSTOMERs.Add(l_customer);
            dataProvider.Instance.DB.SaveChanges();

            g_listCustomer.Add(l_customer);
        }

        private bool checkEdit(CustomersView p)
        {
            if (p == null)
                return false;

            if (string.IsNullOrEmpty(g_str_id) || g_selectedItem == null)
                return false;

            // check id.
            var l_IDList = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == g_str_id);
            if (l_IDList == null || l_IDList.Count() == 0)
                return false;

            return true;
        }

        private void clickEdit(CustomersView p)
        {
            var l_customer = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == g_selectedItem.ID).SingleOrDefault();
            l_customer.FULLNAME = g_str_fullName;
            l_customer.GENDER = g_str_gender;
            l_customer.YEAROFBIRTH = g_i_yearOfBirth;
            l_customer.PHONE = g_str_phone;
            l_customer.EMAIL = g_str_email;
            l_customer.CASH = g_i_cash;
            l_customer.POINT = g_i_point;

            dataProvider.Instance.DB.SaveChanges();

            for (int i = 0; i < g_listCustomer.Count(); i++)
            {
                if (g_listCustomer[i].ID == g_selectedItem.ID)
                {
                    g_listCustomer[i] = new CUSTOMER()
                    {
                        ID = g_selectedItem.ID,
                        FULLNAME = g_str_fullName,
                        GENDER = g_str_gender,
                        YEAROFBIRTH = g_i_yearOfBirth,
                        PHONE = g_str_phone,
                        EMAIL = g_str_email,
                        CASH = g_i_cash,
                        POINT = g_i_point,
                    };
                    break;
                }
            }
        }

        private void clickSave(CustomersView p)
        {
            if (p == null)
                return;

            //p.rDefTop.Height = 0;
            p.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private bool filterIDCustomer(object item)
        {
            if (string.IsNullOrEmpty(_g_str_filter))
                return true;
            else
                return ((item as CUSTOMER).ID.IndexOf(g_str_filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void filterIDCustomer(CustomersView p)
        {
            if (p == null)
                return;

            CollectionViewSource.GetDefaultView(g_listCustomer).Refresh();
        }

        private void clickDetail(CUSTOMER p)
        {
            // Lấy cái p này chính là dòng đang được chọn.
            // Tui có làm ở trong order view truyền vào pay view bà tham khảo đó nha.
            if (p == null)
                return;

            MainWindow mainWd = MainWindow.Instance;
            CustomersView customersV = CustomersView.Instance;

            mainWd.Opacity = .2;
            customersV.Opacity = .2;

            DetailCustomersView detailCusView = new DetailCustomersView();
            detailCusView.ShowDialog();

            mainWd.Opacity = 100;
            customersV.Opacity = 100;
        }
    }
}


