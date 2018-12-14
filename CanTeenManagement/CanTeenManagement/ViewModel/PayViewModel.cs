using CanTeenManagement.Model;
using CanTeenManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CanTeenManagement.CO;
using System.Text.RegularExpressions;
using System.Windows;

namespace CanTeenManagement.ViewModel
{
    class PayViewModel : BaseViewModel
    {
        private ObservableCollection<PAYFOOD> _g_obCl_payFood;
        public ObservableCollection<PAYFOOD> g_obCl_payFood
        {
            get => _g_obCl_payFood;
            set
            {
                _g_obCl_payFood = value;
                OnPropertyChanged();
            }
        }

        private int _g_i_sumPrice;
        public int g_i_sumPrice
        {
            get => _g_i_sumPrice;

            set
            {
                _g_i_sumPrice = value;
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

        private string _g_str_customerImageLink;
        public string g_str_customerImageLink
        {
            get => _g_str_customerImageLink;

            set
            {
                _g_str_customerImageLink = value;
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

        private string _g_str_orderID;
        public string g_str_orderID
        {
            get => _g_str_orderID;

            set
            {
                _g_str_orderID = value;
            }
        }

        private bool _g_b_isPay;
        public bool g_b_isPay
        {
            get => _g_b_isPay;

            set
            {
                _g_b_isPay = value;
            }
        }

        #region command.
        public ICommand g_iCm_LoadedItemsControlCommand { get; set; }

        public ICommand g_iCm_ClickButtonPayCommand { get; set; }

        public ICommand g_iCm_ClickButtonUndoCommand { get; set; }

        public ICommand g_iCm_ClickCloseWindowCommand { get; set; }

        public ICommand g_iCm_ClickButtonDeleteCommand { get; set; }

        public ICommand g_iCm_ClickCheckBoxCommand { get; set; }

        public ICommand g_iCm_ClickButtonRemoveCommand { get; set; }

        public ICommand g_iCm_ClickButtonAddCommand { get; set; }

        public ICommand g_iCm_TextChangedTextBoxCustomerIDCommand { get; set; }

        public ICommand g_iCm_TextChangedTextBoxQuantityCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }

        #endregion

        public PayViewModel()
        {
            this.initSupport();

            g_iCm_ClickButtonPayCommand = new RelayCommand<Button>((p) => { return this.checkButtonPay(); }, (p) =>
            {
                this.clickButtonPay(p);
            });

            g_iCm_ClickButtonUndoCommand = new RelayCommand<Button>((p) => { return this.checkButtonUndo(); }, (p) =>
            {
                this.clickButtonUndo(p);
            });

            g_iCm_ClickCloseWindowCommand = new RelayCommand<PayView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            g_iCm_ClickButtonDeleteCommand = new RelayCommand<PAYFOOD>((p) => { return this.checkButtonDelete(); }, (p) =>
            {
                this.clickButtonDelete(p);
            });

            g_iCm_ClickCheckBoxCommand = new RelayCommand<PAYFOOD>((p) => { return this.checkCheckBox(); }, (p) =>
            {
                this.clickCheckBox(p);
            });

            g_iCm_ClickButtonRemoveCommand = new RelayCommand<PAYFOOD>((p) => { return this.checkButtonRemove(p); }, (p) =>
            {
                this.clickButtonRemove(p);
            });

            g_iCm_ClickButtonAddCommand = new RelayCommand<PAYFOOD>((p) => { return this.checkButtonAdd(p); }, (p) =>
            {
                this.clickButtonAdd(p);
            });

            g_iCm_TextChangedTextBoxCustomerIDCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.textChangedTextBoxCustomerID(p);
            });

            g_iCm_TextChangedTextBoxQuantityCommand = new RelayCommand<PAYFOOD>((p) => { return this.checkTextChangedTextBoxQuantity(); }, (p) =>
            {
                this.textChangedTextBoxQuantity(p);
            });

            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });
        }

        private void initSupport()
        {
            this.g_i_sumPrice = 0;          
            this.g_b_isPay = false;

            this.loadedItemsControl();
            this.resetCustomer();
        }

        private void resetCustomer()
        {
            this.g_b_isPay = false;
            this.g_i_customerStar = 0;

            this.g_str_customerID = "Trống";
            this.g_str_customerfullName = "Tên khách hàng trống";
            this.g_str_customerImageLink = @"\\127.0.0.1\CanteenManagement\avatar.default.png";
        }

        private void loadedItemsControl()
        {
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            this.g_obCl_payFood = new ObservableCollection<PAYFOOD>(l_orderVM.g_lst_orderFood);
            // Sum price in order food.
            this.g_i_sumPrice = this.getSumPrice();
        }

        private int getSumPrice()
        {
            int i = 0;
            int l_i_sumPrice = 0;

            for (i = 0; i < this.g_obCl_payFood.Count; i++)
            {
                if (this.g_obCl_payFood[i].ISCHECKED == true)
                {
                    l_i_sumPrice += this.g_obCl_payFood[i].QUANTITY * this.g_obCl_payFood[i].PRICESALE;
                }
            }

            return l_i_sumPrice;
        }

        #region Click pay.
        private bool checkButtonPay()
        {
            if (dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).Count() == 0
                || this.g_b_isPay == true)
                return false;

            return true;
        }

        private void clickButtonPay(Button p)
        {
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            // Reset affter pay in orderView.
            l_orderVM.g_lst_orderFood.Clear();
            l_orderVM.g_i_currOrderFood = 0;

            if (createNewOrderID())
            {
                this.saveOrderInfo();
                this.saveOrderDetail();
                this.addPointForCustomer();
                this.enableAllQuantityTextBox();

                this.g_b_isPay = true;

                staticFunctionClass.showStatusView(true, "Thêm thực đơn " + this.g_str_orderID
                    + " Cho khách hàng " + this.g_str_customerfullName.Trim()
                    + " phụ trách bởi " + staticVarClass.account_userName);
            }
            else
            {
                staticFunctionClass.showStatusView(true, "Thêm thực đơn " + this.g_str_orderID
                   + " Cho khách hàng " + this.g_str_customerfullName.Trim()
                   + " phụ trách bởi " + staticVarClass.account_userName);
            }
        }

        private void enableAllQuantityTextBox()
        {
            int i = 0;
            for (i = 0; i < this.g_obCl_payFood.Count(); i++)
            {
                this.g_obCl_payFood[i].ISENABLEQUANTITY = false;
            }
        }

        private bool createNewOrderID()
        {
            string l_str_CurrID = dataProvider.Instance.DB.ORDERINFOes
                .OrderByDescending(orderInfo => orderInfo.ID)
                .Select(orderInfo => orderInfo.ID).FirstOrDefault();

            //var resultString = dataProvider.Instance.DB.ORDERINFOes.OrderByDescending(id => id.ID).First();
            Match match = Regex.Match(l_str_CurrID, @"(\d+)");

            if (match.Success)
            {
                // Create new orderID.
                this.g_str_orderID = "ORD" + ((int.Parse(match.Groups[1].Value)) + 1).ToString();

                return true;
            }

            return false;
        }

        private void saveOrderInfo()
        {
            //Save in order info.
            var l_orderInfo = new ORDERINFO()
            {
                ID = this.g_str_orderID,
                CUSTOMERID = this.g_str_customerID,
                EMPLOYEEID = staticVarClass.account_userName,
                ORDERDATE = DateTime.Now,
                TOTALMONEY = this.g_i_sumPrice,
                STATUS = staticVarClass.status_waiting
            };

            dataProvider.Instance.DB.ORDERINFOes.Add(l_orderInfo);
            dataProvider.Instance.DB.SaveChanges();
        }

        private void saveOrderDetail()
        {
            int i = 0;
            for (i = 0; i < this.g_obCl_payFood.Count; i++)
            {
                //Save in order detail.
                var l_orderDetail = new ORDERDETAIL()
                {
                    ORDERID = this.g_str_orderID,
                    FOODID = this.g_obCl_payFood[i].ID,
                    QUANTITY = this.g_obCl_payFood[i].QUANTITY,
                    TOTALMONEY = this.g_obCl_payFood[i].QUANTITY * this.g_obCl_payFood[i].PRICESALE,
                    STATUS = staticVarClass.status_waiting
                };

                dataProvider.Instance.DB.ORDERDETAILs.Add(l_orderDetail);
                dataProvider.Instance.DB.SaveChanges();
            }
        }

        private void addPointForCustomer()
        {
            int l_i_addPoint = this.g_i_sumPrice / 10;

            //int l_str_currPoint = (int)dataProvider.Instance.DB.CUSTOMERs
            //   .Where(customer => customer.ID == this.g_str_customerID)
            //   .Select(customer => customer.POINT).FirstOrDefault();

            dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).ToList()
                                              .ForEach(customer => customer.POINT += l_i_addPoint);
            dataProvider.Instance.DB.SaveChanges();
        }
        #endregion

        #region Click undo.
        private bool checkButtonUndo()
        {
            if (this.g_b_isPay == true)
                return true;
            return false;
        }

        private void clickButtonUndo(Button p)
        {
            this.deleteOrderInfo();
            this.deleteOrderDetail();
            this.subPointCustomer();
        }

        private void deleteOrderInfo()
        {

            ORDERINFO l_orderInfo = dataProvider.Instance.DB.ORDERINFOes.Where(orderInfo => orderInfo.ID == this.g_str_orderID).SingleOrDefault();

            dataProvider.Instance.DB.ORDERINFOes.Remove(l_orderInfo);
            dataProvider.Instance.DB.SaveChanges();
        }

        private void deleteOrderDetail()
        {
            int i = 0;
            for (i = 0; i < this.g_obCl_payFood.Count(); i++)
            {
                ORDERDETAIL l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.Where(orderDetail => orderDetail.ORDERID == this.g_str_orderID && orderDetail.FOODID == this.g_obCl_payFood[i].ID).SingleOrDefault();

                dataProvider.Instance.DB.ORDERDETAILs.Remove(l_orderDetail);
                dataProvider.Instance.DB.SaveChanges();
            }
        }

        private void subPointCustomer()
        {
            int l_i_addPoint = this.g_i_sumPrice / 10;

            //int l_str_currPoint = (int)dataProvider.Instance.DB.CUSTOMERs
            //   .Where(customer => customer.ID == this.g_str_customerID)
            //   .Select(customer => customer.POINT).FirstOrDefault();

            dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).ToList()
                                              .ForEach(customer => customer.POINT -= l_i_addPoint);
            dataProvider.Instance.DB.SaveChanges();
        }
        #endregion

        private void clickCloseWindow(PayView p)
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

        #region Check button delete.
        private bool checkButtonDelete()
        {
            if (this.g_b_isPay == true)
                return false;

            return true;
        }

        private void clickButtonDelete(PAYFOOD p)
        {
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            l_orderVM.g_i_currOrderFood--;

            // sud price this in sum price.
            this.g_i_sumPrice -= p.QUANTITY * p.PRICESALE;

            this.g_obCl_payFood.Remove(p);
        }
        #endregion

        #region Check check box.
        private bool checkCheckBox()
        {
            if (this.g_b_isPay == true)
                return false;

            return true;
        }

        private void clickCheckBox(PAYFOOD p)
        {
            //p.IsChecked = false;
            if (p.ISCHECKED == false)
                p.ISENABLEQUANTITY = false;
            else
                p.ISENABLEQUANTITY = true;

            //sud price this in sum price.
            this.g_i_sumPrice = this.getSumPrice();
        }
        #endregion.

        #region Check button remove.
        private bool checkButtonRemove(PAYFOOD p)
        {
            if (p.QUANTITY == 1 || this.g_b_isPay == true || p.ISCHECKED == false)
                return false;

            return true;
        }

        private void clickButtonRemove(PAYFOOD p)
        {
            p.QUANTITY--;
            this.g_i_sumPrice -= p.PRICESALE;
        }
        #endregion

        #region Check button add.
        private bool checkButtonAdd(PAYFOOD p)
        {
            if (this.g_b_isPay == true || p.ISCHECKED == false)
                return false;

            return true;
        }

        private void clickButtonAdd(PAYFOOD p)
        {
            p.QUANTITY++;
            this.g_i_sumPrice += p.PRICESALE;
        }
        #endregion

        private void textChangedTextBoxCustomerID(TextBox p)
        {
            CUSTOMER l_customer = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).SingleOrDefault();

            if (l_customer != null)
            {
                this.g_str_customerfullName = l_customer.FULLNAME;
                this.g_str_customerImageLink = l_customer.IMAGELINK;
                this.g_i_customerStar = (int)l_customer.STAR;
            }
        }

        #region textChangedTextBoxQuantity.
        private bool checkTextChangedTextBoxQuantity()
        {
            if (this.g_b_isPay == true)
                return false;

            return true;
        }

        private void textChangedTextBoxQuantity(PAYFOOD p)
        {
            this.g_i_sumPrice = this.getSumPrice();
        }
        #endregion

        private void mouseLeftButtonDown(Window p)
        {
            if (p == null)
                return;

            p.DragMove();
        }
    }
}