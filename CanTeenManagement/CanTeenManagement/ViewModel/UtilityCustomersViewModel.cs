using CanTeenManagement.CO;
using CanTeenManagement.Model;
using CanTeenManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace CanTeenManagement.ViewModel
{
    class UtilityCustomersViewModel : BaseViewModel
    {
        private ObservableCollection<CASH> _g_obCl_million;
        public ObservableCollection<CASH> g_obCl_million
        {
            get => _g_obCl_million;
            set
            {
                _g_obCl_million = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_customerID;
        public string g_str_customerID
        {
            get => _g_str_customerID;

            set
            {
                _g_str_customerID = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_customerfullName;
        public string g_str_customerfullName
        {
            get => _g_str_customerfullName;

            set
            {
                _g_str_customerfullName = value;
                OnPropertyChanged();
            }
        }

        private int _g_i_customerStar;
        public int g_i_customerStar
        {
            get => _g_i_customerStar;

            set
            {
                _g_i_customerStar = value;
                OnPropertyChanged();
            }
        }

        private int _g_i_customerCash;
        public int g_i_customerCash
        {
            get => _g_i_customerCash;

            set
            {
                _g_i_customerCash = value;
                OnPropertyChanged();
            }
        }

        private int _g_i_customerPoint;
        public int g_i_customerPoint
        {
            get => _g_i_customerPoint;

            set
            {
                _g_i_customerPoint = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_sumPrice;
        public string g_str_sumPrice
        {
            get => _g_str_sumPrice;

            set
            {
                int i = 0;
                if (value != "")
                    if (!int.TryParse(value, out i))
                        value = g_str_sumPrice;
                    else if (int.Parse(value) > 1000000)
                        value = g_str_sumPrice;

                _g_str_sumPrice = value;
                OnPropertyChanged();
            }
        }

        private bool _g_b_isAddCash;
        public bool g_b_isAddCash
        {
            get => _g_b_isAddCash;

            set
            {
                _g_b_isAddCash = value;
            }
        }

        private string _g_str_Mode;
        public string g_str_Mode
        {
            get => _g_str_Mode;

            set
            {
                _g_str_Mode = value;
                OnPropertyChanged();
            }
        }

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

        #region command.
        public ICommand g_iCm_TextChangedTextBoxCustomerIDCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }

        public ICommand g_iCm_ClickCloseWindowCommand { get; set; }

        public ICommand g_iCm_MouseDoubleClickMillionCommand { get; set; }

        public ICommand g_iCm_MouseRightButtonUpMillionCommand { get; set; }

        public ICommand g_iCm_ChangeModeCommand { get; set; }

        public ICommand g_iCm_TextBoxGotFocusCommand { get; set; }

        public ICommand g_iCm_TextBoxLostFocusCommand { get; set; }
        #endregion

        public UtilityCustomersViewModel()
        {
            this.initSupport();

            g_iCm_TextChangedTextBoxCustomerIDCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.textChangedTextBoxCustomerID(p);
            });


            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });

            g_iCm_ClickCloseWindowCommand = new RelayCommand<UtilityCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            g_iCm_MouseDoubleClickMillionCommand = new RelayCommand<CASH>((p) => { return this.checkMouseDoubleClickMillion(p); }, (p) =>
            {
                this.mouseDoubleClickMillion(p);
            });

            g_iCm_MouseRightButtonUpMillionCommand = new RelayCommand<CASH>((p) => { return this.checkMouseRightButtonUpMillion(p); }, (p) =>
            {
                this.mouseRightButtonUpMillion(p);
            });

            g_iCm_ChangeModeCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) =>
            {
                this.changeMode(p);
            });

            g_iCm_TextBoxGotFocusCommand = new RelayCommand<UtilityCustomersView>((p) => { return true; }, (p) =>
            {
                this.gotFocus(p);
            });

            g_iCm_TextBoxLostFocusCommand = new RelayCommand<UtilityCustomersView>((p) => { return true; }, (p) =>
            {
                this.lostFocus(p);
            });
        }

        private void initSupport()
        {
            this.g_str_sumPrice = "0";
            this.g_obCl_million = new ObservableCollection<CASH>();
            this.g_b_isAddCash = true;
            this.g_str_Mode = staticVarClass.mode_addCash;

            this.loadedItemsControl();
            this.resetCustomer();
        }

        private void resetCustomer()
        {
            this.g_str_customerID = string.Empty;
            this.g_str_customerfullName = "Trống";
            this.g_imgSrc_customer = staticVarClass.imgSrc_empty;
            this.g_i_customerStar = 0;
            this.g_i_customerCash = 0;
            this.g_i_customerPoint = 0;
        }

        private void loadedItemsControl()
        {
            CASH cash100 = new CASH(100);
            this.g_obCl_million.Add(cash100);

            CASH cash200 = new CASH(200);
            this.g_obCl_million.Add(cash200);

            CASH cash500 = new CASH(500);
            this.g_obCl_million.Add(cash500);

            CASH cash1000 = new CASH(1000);
            this.g_obCl_million.Add(cash1000);

            CASH cash2000 = new CASH(2000);
            this.g_obCl_million.Add(cash2000);

            CASH cash5000 = new CASH(5000);
            this.g_obCl_million.Add(cash5000);

            CASH cash10000 = new CASH(10000);
            this.g_obCl_million.Add(cash10000);

            CASH cash20000 = new CASH(20000);
            this.g_obCl_million.Add(cash20000);

            CASH cash50000 = new CASH(50000);
            this.g_obCl_million.Add(cash50000);

            CASH cash100000 = new CASH(100000);
            this.g_obCl_million.Add(cash100000);

            CASH cash200000 = new CASH(200000);
            this.g_obCl_million.Add(cash200000);

            CASH cash500000 = new CASH(500000);
            this.g_obCl_million.Add(cash500000);
        }

        private void textChangedTextBoxCustomerID(TextBox p)
        {
            CUSTOMER l_customer = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).SingleOrDefault();

            if (l_customer != null)
            {
                this.g_str_customerfullName = l_customer.FULLNAME;
                this.g_imgSrc_customer = staticFunctionClass.LoadBitmap(l_customer.IMAGELINK);
                this.g_i_customerStar = (int)l_customer.STAR;
                this.g_i_customerCash = (int)l_customer.CASH;
                this.g_i_customerPoint = (int)l_customer.POINT;
            }
        }

        private void clickCloseWindow(UtilityCustomersView p)
        {
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            // Reset affter pay in orderView.
            l_orderVM.g_lst_orderFood.Clear();
            l_orderVM.g_i_currOrderFood = 0;

            this.resetCustomer();

            p.Close();
        }

        private void mouseLeftButtonDown(Window p)
        {
            if (p == null)
                return;

            p.DragMove();
        }

        #region Double click.
        private bool checkMouseDoubleClickMillion(CASH p)
        {
            // > 1.000.000.
            if (g_str_sumPrice == "")
                g_str_sumPrice = "0";

            if (int.Parse(g_str_sumPrice) + p.MILLION > 1000000)
                return false;

            return true;
        }

        private void mouseDoubleClickMillion(CASH p)
        {
            if (p == null)
                return;

            int l_i_sumPrice = int.Parse(this.g_str_sumPrice);
            l_i_sumPrice += p.MILLION;
            this.g_str_sumPrice = l_i_sumPrice.ToString();
        }
        #endregion

        #region Mouse right up.
        private bool checkMouseRightButtonUpMillion(CASH p)
        {
            if (g_str_sumPrice == "")
                g_str_sumPrice = "0";

            if (int.Parse(g_str_sumPrice) - p.MILLION <= 0)
                return false;

            return true;
        }

        private void mouseRightButtonUpMillion(CASH p)
        {
            if (p == null)
                return;

            int l_i_sumPrice = int.Parse(this.g_str_sumPrice);
            l_i_sumPrice -= p.MILLION;
            this.g_str_sumPrice = l_i_sumPrice.ToString();
        }
        #endregion

        private void changeMode(ToggleButton p)
        {
            if (p == null)
                return;

            if (this.g_b_isAddCash)
            {
                this.g_b_isAddCash = false;
                this.g_str_Mode = staticVarClass.mode_subCash;
            }
            else
            {
                this.g_b_isAddCash = true;
                this.g_str_Mode = staticVarClass.mode_addCash;
            }

        }

        private void gotFocus(UtilityCustomersView p)
        {
            if (p == null)
                return;

            if (this.g_str_sumPrice == "0")
                this.g_str_sumPrice = "";
        }

        private void lostFocus(UtilityCustomersView p)
        {
            if (p == null)
                return;

            if (this.g_str_sumPrice == "")
                this.g_str_sumPrice = "0";
        }
    }
}
