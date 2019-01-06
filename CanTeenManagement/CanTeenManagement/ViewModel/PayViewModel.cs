using CanTeenManagement.CO;
using CanTeenManagement.Model;
using CanTeenManagement.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

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
       
        private string _g_str_visibitily;
        public string g_str_visibitily
        {
            get => _g_str_visibitily;

            set
            {
                _g_str_visibitily = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_visibilityOrderFood;
        public string g_str_visibilityOrderFood
        {
            get => _g_str_visibilityOrderFood;

            set
            {
                _g_str_visibilityOrderFood = value;
                OnPropertyChanged();
            }
        }
        
        string g_str_orderID;
        bool g_b_isPay;
        bool g_b_isHaveCus;
        bool g_b_isEnough;
        public bool g_b_addPriceNow;

        DispatcherTimer g_timer = null;

        #region command.
        public ICommand g_iCm_LoadedWindowCommand { get; set; }

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

        public ICommand g_iCm_TextChangedPriceTextBoxCommand { get; set; }

        public ICommand g_iCm_ClickAddPriceCommand { get; set; }
        #endregion

        public PayViewModel()
        {
            this.initSupport();

            g_iCm_LoadedWindowCommand = new RelayCommand<PayView>((p) => { return true; }, (p) =>
            {
                this.loaded();
            });

            g_iCm_ClickButtonPayCommand = new RelayCommand<Button>((p) => { return this.checkButtonPay(); }, (p) =>
            {
                this.clickButtonPay();
            });

            g_iCm_ClickButtonUndoCommand = new RelayCommand<Button>((p) => { return this.checkButtonUndo(); }, (p) =>
            {
                this.clickButtonUndo();
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
                this.textChangedTextBoxCustomerID();
            });

            g_iCm_TextChangedTextBoxQuantityCommand = new RelayCommand<PAYFOOD>((p) => { return this.checkTextChangedTextBoxQuantity(); }, (p) =>
            {
                this.textChangedTextBoxQuantity(p);
            });

            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });

            g_iCm_TextChangedPriceTextBoxCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.textChangedPriceTextBox();
            });

            g_iCm_ClickAddPriceCommand = new RelayCommand<PayView>((p) => { return true; }, (p) =>
            {
                this.clickAddPrice(p);
            });

        }

        private void initSupport()
        {
            this.g_str_visibilityOrderFood = staticVarClass.visibility_hidden;

            //
            this.g_b_addPriceNow = false;

            //
            this.g_timer = new DispatcherTimer();
            this.g_timer.Tick += (s, ev) => setIDCustomer();
            this.g_timer.Interval = new TimeSpan(0, 0, 1);

            //
            this.g_i_sumPrice = 0;
            this.g_b_isPay = false;
            this.g_b_isHaveCus = false;

            //
            this.resetCustomer();

            //
            this.g_str_visibitily = staticVarClass.visibility_hidden;

            // 
            this.g_b_isEnough = true;
        }

        private void resetCustomer()
        {
            this.g_b_isPay = false;
            this.g_i_customerStar = 0;

            //this.g_str_customerID = string.Empty;
            this.g_str_customerfullName = "Trống";
            this.g_imgSrc_customer = staticVarClass.imgSrc_empty;
        }

        private void loaded()
        {
            this.g_timer.Start();

            //
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            this.g_obCl_payFood = l_orderVM.g_obCl_orderFood;
            // Sum price in order food.
            this.g_i_sumPrice = this.getSumPrice();

            this.checkVisibilityOrderFood();
        }

        private void checkVisibilityOrderFood()
        {
            if (this.g_obCl_payFood.Count == 0)
            {
                this.g_str_visibilityOrderFood = staticVarClass.visibility_visible;
            }
            else
            {
                this.g_str_visibilityOrderFood = staticVarClass.visibility_hidden;
            }
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
            if (this.g_b_isHaveCus == false || this.g_b_isPay == true
                || this.g_b_isEnough == false || this.g_i_sumPrice == 0)
                return false;

            return true;
        }

        private void clickButtonPay()
        {
            if (createNewOrderID())
            {
                try
                {
                    this.saveOrderInfo();
                    this.saveOrderDetail();
                    this.subPriceCustomer();
                    this.addPointForCustomer();
                    this.enableAllQuantityTextBox(false);

                    //
                    this.g_b_isPay = true;

                    staticFunctionClass.showStatusView(true, "Thêm đơn hàng " + this.g_str_orderID + " thành công!");
                }
                catch
                {
                    staticFunctionClass.showStatusView(false, "Thêm đơn hàng " + this.g_str_orderID + " thất bại!");
                }

            }
            else
            {
                staticFunctionClass.showStatusView(false, "Thêm đơn hàng " + this.g_str_orderID + " thất bại!");
            }
        }

        private void enableAllQuantityTextBox(bool e)
        {
            int i = 0;
            for (i = 0; i < this.g_obCl_payFood.Count(); i++)
            {
                this.g_obCl_payFood[i].ISENABLEQUANTITY = e;
            }
        }

        private bool createNewOrderID()
        {
            using (var DB = new QLCanTinEntities())
            {
                string l_str_CurrID = DB.ORDERINFOes
                .OrderByDescending(orderInfo => orderInfo.ID)
                .Select(orderInfo => orderInfo.ID).FirstOrDefault();

                if (l_str_CurrID != null)
                {
                    //var resultString = dataProvider.Instance.DB.ORDERINFOes.OrderByDescending(id => id.ID).First();
                    Match match = Regex.Match(l_str_CurrID, @"(\d+)");

                    if (match.Success)
                    {
                        // Create new orderID.
                        this.g_str_orderID = "ORD" + ((int.Parse(match.Groups[1].Value)) + 1).ToString();

                        return true;
                    }
                }
                else
                {
                    this.g_str_orderID = "ORD1";

                    return true;
                }
            }

            return false;
        }

        private void saveOrderInfo()
        {
            using (var DB = new QLCanTinEntities())
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

                DB.ORDERINFOes.Add(l_orderInfo);
                DB.SaveChanges();
            }
        }

        private void saveOrderDetail()
        {
            int i = 0;
            for (i = 0; i < this.g_obCl_payFood.Count; i++)
            {
                using (var DB = new QLCanTinEntities())
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

                    DB.ORDERDETAILs.Add(l_orderDetail);
                    DB.SaveChanges();
                }
            }
        }

        private void subPriceCustomer()
        {
            int l_i_price = this.g_i_sumPrice;

            using (var DB = new QLCanTinEntities())
            {
                DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).ToList()
                                              .ForEach(customer => customer.CASH -= l_i_price);
                DB.SaveChanges();
            }
        }

        private void addPointForCustomer()
        {
            int l_i_addPoint = this.g_i_sumPrice / 10;

            using (var DB = new QLCanTinEntities())
            {
                DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).ToList()
                                              .ForEach(customer => customer.POINT += l_i_addPoint);
                DB.SaveChanges();
            }
        }
        #endregion

        #region Click undo.
        private bool checkButtonUndo()
        {
            if (this.g_b_isPay == false)
                return false;

            return true;
        }

        private void clickButtonUndo()
        {
            try
            {
                this.deleteOrderDetail();
                this.deleteOrderInfo();
                this.addPriceCustomer();
                this.subPointCustomer();
                this.enableAllQuantityTextBox(true);

                // 
                this.g_b_isPay = false;

                staticFunctionClass.showStatusView(true, "Hoàn tác đơn hàng " + this.g_str_orderID + " thành công!");
            }
            catch
            {
                staticFunctionClass.showStatusView(false, "Hoàn tác đơn hàng " + this.g_str_orderID + " thất bại!");
            }
        }

        private void deleteOrderInfo()
        {
            using (var DB = new QLCanTinEntities())
            {
                ORDERINFO l_orderInfo = DB.ORDERINFOes.Where(orderInfo => orderInfo.ID == this.g_str_orderID).SingleOrDefault();

                if (l_orderInfo != null)
                {
                    DB.ORDERINFOes.Remove(l_orderInfo);
                    //dataProvider.Instance.DB.Entry(l_orderInfo).State = System.Data.Entity.EntityState.Deleted;
                    DB.SaveChanges();
                }
            }
        }

        private void deleteOrderDetail()
        {
            int i = 0;
            for (i = 0; i < this.g_obCl_payFood.Count(); i++)
            {
                if (this.g_obCl_payFood[i].ISCHECKED == true)
                {
                    string t_id = this.g_obCl_payFood[i].ID;

                    using (var DB = new QLCanTinEntities())
                    {
                        //
                        ORDERDETAIL l_orderDetail = DB.ORDERDETAILs
                        .Where(orderDetail => orderDetail.ORDERID == this.g_str_orderID
                        && orderDetail.FOODID == t_id).SingleOrDefault();

                        //
                        if (l_orderDetail != null)
                        {
                            DB.ORDERDETAILs.Remove(l_orderDetail);
                            //dataProvider.Instance.DB.Entry(l_orderDetail).State = System.Data.Entity.EntityState.Deleted;
                            DB.SaveChanges();
                        }
                    }
                }
            }
        }

        private void addPriceCustomer()
        {
            int l_i_price = this.g_i_sumPrice;

            using (var DB = new QLCanTinEntities())
            {
                DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).ToList()
                                              .ForEach(customer => customer.CASH += l_i_price);
                DB.SaveChanges();
            }
        }

        private void subPointCustomer()
        {
            int l_i_addPoint = this.g_i_sumPrice / 10;

            using (var DB = new QLCanTinEntities())
            {
                DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).ToList()
                                              .ForEach(customer => customer.POINT -= l_i_addPoint);
                DB.SaveChanges();
            }
        }
        #endregion

        private void clickCloseWindow(PayView p)
        {
            if (p == null)
                return;

            this.g_timer.Stop();
            //
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            //Reset affter pay in orderView.
            l_orderVM.g_obCl_orderFood.Clear();
            l_orderVM.g_i_currOrderFood = 0;

            this.resetCustomer();
            this.g_str_customerID = string.Empty;

            //
            this.g_str_visibitily = staticVarClass.visibility_hidden;

            p.Close();
            p = null;
        }

        #region Button delete.
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

            this.checkVisibilityOrderFood();
        }
        #endregion

        #region Check box.
        private bool checkCheckBox()
        {
            if (this.g_b_isPay == true)
                return false;

            return true;
        }

        private void clickCheckBox(PAYFOOD p)
        {
            if (p == null)
                return;

            if (p.ISCHECKED == false)
                p.ISENABLEQUANTITY = false;
            else
                p.ISENABLEQUANTITY = true;

            //sud price this in sum price.
            this.g_i_sumPrice = this.getSumPrice();
        }
        #endregion.

        #region Button remove.
        private bool checkButtonRemove(PAYFOOD p)
        {
            if (p.QUANTITY <= 1 || this.g_b_isPay == true
                || p.ISCHECKED == false || p.QUANTITY > 10)
                return false;

            return true;
        }

        private void clickButtonRemove(PAYFOOD p)
        {
            p.QUANTITY--;
        }
        #endregion

        #region Button add.
        private bool checkButtonAdd(PAYFOOD p)
        {
            if (this.g_b_isPay == true || p.ISCHECKED == false
                || p.QUANTITY >= 10)
                return false;

            return true;
        }

        private void clickButtonAdd(PAYFOOD p)
        {
            p.QUANTITY++;
        }
        #endregion

        private void textChangedTextBoxCustomerID()
        {
            CUSTOMER l_customer = null;

            using (var DB = new QLCanTinEntities())
            {
                l_customer = DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).SingleOrDefault();
            }

            if (l_customer != null)
            {
                this.g_b_isHaveCus = true;

                this.g_str_customerfullName = l_customer.FULLNAME;
                this.g_imgSrc_customer = staticFunctionClass.LoadBitmap(l_customer.IMAGELINK);
                this.g_i_customerStar = (int)l_customer.STAR;
            }
            else
            {
                this.g_b_isHaveCus = false;

                this.g_str_visibitily = staticVarClass.visibility_hidden;

                this.resetCustomer();
            }

            this.textChangedPriceTextBox();
        }

        #region TextBox quantity.
        private bool checkTextChangedTextBoxQuantity()
        {
            if (this.g_b_isPay == true)
                return false;

            return true;
        }

        private void textChangedTextBoxQuantity(PAYFOOD p)
        {
            p.QUANTITY = p.QUANTITY;

            this.g_i_sumPrice = this.getSumPrice();
        }
        #endregion

        private void mouseLeftButtonDown(Window p)
        {
            if (p == null)
                return;

            p.DragMove();
        }

        private void textChangedPriceTextBox()
        {
            if (this.g_b_isHaveCus == false)
                return;

            int l_currCash = 0;

            using (var DB = new QLCanTinEntities())
            {
                l_currCash = (int)DB.CUSTOMERs
                .Where(cus => cus.ID == this.g_str_customerID)
                .Select(cus => cus.CASH).FirstOrDefault();
            }

            if (this.g_i_sumPrice > l_currCash)
            {
                this.g_b_isEnough = false;

                this.g_str_visibitily = staticVarClass.visibility_visible;
            }
            else
            {
                this.g_b_isEnough = true;

                this.g_str_visibitily = staticVarClass.visibility_hidden;
            }
        }

        private void setIDCustomer()
        {
            string str_ID = staticFunctionClass.getIDFronExcel();
            if (str_ID != string.Empty)
                this.g_str_customerID = str_ID;
        }

        private void clickAddPrice(PayView p)
        {
            p.Hide();
            g_timer.Stop();

            this.g_b_addPriceNow = true;

            UtilityCustomersView utilityCustomersView = new UtilityCustomersView();
            utilityCustomersView.ShowDialog();

            this.g_b_addPriceNow = false;

            this.textChangedPriceTextBox();
            p.ShowDialog();      
        }
    }
}