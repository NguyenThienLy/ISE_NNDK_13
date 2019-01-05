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
using System.Windows.Threading;

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

        private Stack<int> _g_stck_undo;
        public Stack<int> g_stck_undo
        {
            get => _g_stck_undo;
            set
            {
                _g_stck_undo = value;
            }
        }

        private Stack<int> _g_stck_redo;
        public Stack<int> g_stck_redo
        {
            get => _g_stck_redo;
            set
            {
                _g_stck_redo = value;
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

        private int _g_i_sumPrice;
        public int g_i_sumPrice
        {
            get => _g_i_sumPrice;

            set
            {
                int i = 0;
                if (value != 0)
                {
                    if (!int.TryParse(value.ToString(), out i))
                        value = g_i_sumPrice;
                    else if (value < 0)
                        value = g_i_sumPrice;
                    else if (value > 1000000)
                        value = g_i_sumPrice;
                }

                _g_i_sumPrice = value;
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

        private bool _g_b_isAgree;
        public bool g_b_isAgree
        {
            get => _g_b_isAgree;

            set
            {
                _g_b_isAgree = value;
            }
        }

        private bool _g_b_isHaveCus;
        public bool g_b_isHaveCus
        {
            get => _g_b_isHaveCus;

            set
            {
                _g_b_isHaveCus = value;
            }
        }

        private string _g_str_isEnable;
        public string g_str_isEnable
        {
            get => _g_str_isEnable;

            set
            {
                _g_str_isEnable = value;
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

        private bool _g_b_isEnough;
        public bool g_b_isEnough
        {
            get => _g_b_isEnough;

            set
            {
                _g_b_isEnough = value;
            }
        }

        DispatcherTimer g_timer = null;

        #region command.
        public ICommand g_iCm_TextChangedTextBoxCustomerIDCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }

        public ICommand g_iCm_ClickCloseWindowCommand { get; set; }

        public ICommand g_iCm_MouseDoubleClickMillionCommand { get; set; }

        public ICommand g_iCm_MouseRightButtonUpMillionCommand { get; set; }

        public ICommand g_iCm_ChangeModeCommand { get; set; }

        public ICommand g_iCm_TextBoxGotFocusCommand { get; set; }

        public ICommand g_iCm_TextBoxLostFocusCommand { get; set; }

        public ICommand g_iCm_TextBoxKeyDownCommand { get; set; }

        public ICommand g_iCm_ClickButtonClearCommand { get; set; }

        public ICommand g_iCm_ClickButtonUndoCommand { get; set; }

        public ICommand g_iCm_ClickButtonRedoCommand { get; set; }

        public ICommand g_iCm_ClickButtonRemoveCommand { get; set; }

        public ICommand g_iCm_ClickButtonAddCommand { get; set; }

        public ICommand g_iCm_ClickButtonAgreeCommand { get; set; }

        public ICommand g_iCm_ClickButtonBackCommand { get; set; }

        public ICommand g_iCm_TextChangedPriceTextBoxCommand { get; set; }

        public ICommand g_iCm_LoadedWindowCommand { get; set; }
        #endregion

        public UtilityCustomersViewModel()
        {
            this.initSupport();

            g_iCm_LoadedWindowCommand = new RelayCommand<UtilityCustomersView>((p) => { return true; }, (p) =>
            {
                this.loaded();
            });

            g_iCm_TextChangedTextBoxCustomerIDCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.textChangedTextBoxCustomerID();
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
                this.changeMode();
            });

            g_iCm_TextBoxKeyDownCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.textBoxKeyDown();
            });

            g_iCm_ClickButtonClearCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonClear(); }, (p) =>
            {
                this.clickButtonClear();
            });

            g_iCm_ClickButtonRedoCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonRedo(); }, (p) =>
            {
                this.clickButtonRedo();
            });

            g_iCm_ClickButtonUndoCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonUndo(); }, (p) =>
            {
                this.clickButtonUndo();
            });

            g_iCm_ClickButtonRemoveCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonRemove(); }, (p) =>
            {
                this.clickButtonRemove();
            });

            g_iCm_ClickButtonAddCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonAdd(); }, (p) =>
            {
                this.clickButtonAdd();
            });

            g_iCm_ClickButtonBackCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonBack(); }, (p) =>
            {
                this.clickButtonBack();
            });

            g_iCm_ClickButtonAgreeCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonAgree(); }, (p) =>
            {
                this.clickButtonAgree();
            });

            g_iCm_TextChangedPriceTextBoxCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.textChangedPriceTextBox();
            });
        }

        private void initSupport()
        {
            this.g_timer = new DispatcherTimer();
            this.g_timer.Tick += (s, ev) => setIDCustomer();
            this.g_timer.Interval = new TimeSpan(0, 0, 1);

            //
            this.g_b_isAddCash = true;
            this.g_str_Mode = staticVarClass.mode_addCash;

            //
            this.g_i_sumPrice = 0;
            this.g_obCl_million = new ObservableCollection<CASH>();

            //
            this.g_str_isEnable = staticVarClass.str_true;

            //
            this.loadedItemsControl();
            this.resetCustomer();

            //
            this.g_stck_redo = new Stack<int>();
            this.g_stck_undo = new Stack<int>();
            this.g_stck_undo.Push(0);

            // 
            this.g_b_isAgree = false;
            this.g_b_isHaveCus = false;

            //
            this.g_str_visibitily = staticVarClass.visibility_hidden;

            // 
            this.g_b_isEnough = true;
        }

        private void loaded()
        {
            this.g_timer.Start();
        }

        private void resetCustomer()
        {
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

        private void textChangedTextBoxCustomerID()
        {
            CUSTOMER l_customer = dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).SingleOrDefault();

            if (l_customer != null)
            {
                this.g_b_isHaveCus = true;

                this.g_str_customerfullName = l_customer.FULLNAME;
                this.g_imgSrc_customer = staticFunctionClass.LoadBitmap(l_customer.IMAGELINK);
                this.g_i_customerStar = (int)l_customer.STAR;
                this.g_i_customerCash = (int)l_customer.CASH;
                this.g_i_customerPoint = (int)l_customer.POINT;
            }
            else
            {
                this.g_b_isHaveCus = false;

                this.resetCustomer();
            }
        }

        private void clickCloseWindow(UtilityCustomersView p)
        {
            this.g_timer.Start();

            //
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            // Reset affter pay in orderView.
            l_orderVM.g_obCl_orderFood.Clear();
            l_orderVM.g_i_currOrderFood = 0;

            this.resetCustomer();
            this.g_str_customerID = string.Empty;

            // 
            this.g_str_visibitily = staticVarClass.visibility_hidden;

            // 
            this.g_i_sumPrice = 0;

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
            if (g_i_sumPrice + p.MILLION > 1000000 || this.g_b_isAgree == true)
                return false;

            return true;
        }

        private void mouseDoubleClickMillion(CASH p)
        {
            if (p == null)
                return;

            int l_i_sumPrice = this.g_i_sumPrice;
            l_i_sumPrice += p.MILLION;
            this.g_i_sumPrice = l_i_sumPrice;

            this.g_stck_undo.Push(this.g_i_sumPrice);
            // When redo, add price then clear g_stck_redo.
            this.g_stck_redo.Clear();
        }
        #endregion

        #region Mouse right up.
        private bool checkMouseRightButtonUpMillion(CASH p)
        {
            if (g_i_sumPrice - p.MILLION <= 0 || this.g_b_isAgree == true)
                return false;

            return true;
        }

        private void mouseRightButtonUpMillion(CASH p)
        {
            if (p == null)
                return;

            int l_i_sumPrice = this.g_i_sumPrice;
            l_i_sumPrice -= p.MILLION;
            this.g_i_sumPrice = l_i_sumPrice;

            this.g_stck_undo.Push(this.g_i_sumPrice);
            // When redo, add price then clear g_stck_redo.
            this.g_stck_redo.Clear();
        }
        #endregion

        #region Mode.
        private void changeMode()
        {
            if (this.g_b_isAddCash)
            {
                this.g_b_isAddCash = false;
                this.g_str_Mode = staticVarClass.mode_subCash;

                //
                this.textChangedPriceTextBox();
            }
            else
            {
                this.g_b_isAddCash = true;
                this.g_str_Mode = staticVarClass.mode_addCash;

                //
                this.g_str_visibitily = staticVarClass.visibility_hidden;
                this.g_b_isEnough = true;
            }

            this.resetForMode();
        }

        private void resetForMode()
        {
            this.g_b_isAgree = false;
            this.g_i_sumPrice = 0;
            this.g_str_isEnable = staticVarClass.str_true;
        }
        #endregion

        #region Textbox price.
        private void textBoxKeyDown()
        {
            this.g_stck_undo.Push(this.g_i_sumPrice);
            // When redo, add price then clear g_stck_redo.
            this.g_stck_redo.Clear();
        }

        private void textChangedPriceTextBox()
        {
            this.g_i_sumPrice = this.g_i_sumPrice;

            if (this.g_b_isHaveCus == false || this.g_b_isAddCash == true)
                return;

            int l_currCash = (int)dataProvider.Instance.DB.CUSTOMERs
                .Where(cus => cus.ID == this.g_str_customerID)
                .Select(cus => cus.CASH).FirstOrDefault();

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
        #endregion

        #region Button clear.
        private bool checkClickButtonClear()
        {
            //if (this.g_i_sumPrice == "")
            //    this.g_i_sumPrice = "0";

            if (this.g_i_sumPrice == 0 || this.g_b_isAgree == true)
                return false;

            return true;
        }

        private void clickButtonClear()
        {
            this.g_i_sumPrice = 0;
        }
        #endregion

        #region Button redo.
        private bool checkClickButtonRedo()
        {
            if (this.g_stck_redo.Count() == 0 || this.g_b_isAgree == true || this.g_i_sumPrice == 0)
                return false;

            return true;
        }

        private void clickButtonRedo()
        {
            int l_temp = this.g_stck_redo.Pop();

            this.g_i_sumPrice = l_temp;
        }
        #endregion

        #region Button undo.
        private bool checkClickButtonUndo()
        {
            if (this.g_stck_undo.Count() <= 1 || this.g_b_isAgree == true || this.g_i_sumPrice == 0)
                return false;

            return true;
        }

        private void clickButtonUndo()
        {
            int l_temp = this.g_stck_undo.Pop();

            this.g_i_sumPrice = this.g_stck_undo.ElementAtOrDefault(0);

            this.g_stck_redo.Push(l_temp);
        }
        #endregion

        #region Button remove.
        private bool checkClickButtonRemove()
        {
            // < 10.000.
            if (g_i_sumPrice - 10000 < 0 || this.g_b_isAgree == true)
                return false;

            return true;
        }

        private void clickButtonRemove()
        {
            int l_i_sumPrice = this.g_i_sumPrice;
            l_i_sumPrice -= 10000;

            this.g_i_sumPrice = l_i_sumPrice;

            this.g_stck_undo.Push(this.g_i_sumPrice);
            // When redo, add price then clear g_stck_redo.
            this.g_stck_redo.Clear();
        }
        #endregion

        #region Button add.
        private bool checkClickButtonAdd()
        {
            // > 1.000.000.
            if (g_i_sumPrice + 10000 > 1000000 || this.g_b_isAgree == true)
                return false;

            return true;
        }

        private void clickButtonAdd()
        {
            int l_i_sumPrice = this.g_i_sumPrice;
            l_i_sumPrice += 10000;

            this.g_i_sumPrice = l_i_sumPrice;

            this.g_stck_undo.Push(this.g_i_sumPrice);
            // When redo, add price then clear g_stck_redo.
            this.g_stck_redo.Clear();
        }
        #endregion

        #region Button agree.
        private bool checkClickButtonAgree()
        {
            if (this.g_b_isHaveCus == false || this.g_i_sumPrice == 0
                || this.g_b_isAgree == true || this.g_b_isEnough == false)
                return false;

            return true;
        }

        private void clickButtonAgree()
        {
            // Bỏ nó chung để câu lệnh của status được nằm cuối.
            try
            {
                if (this.g_b_isAddCash)
                {
                    this.saveAddPriceForCustomer();

                    this.g_b_isAgree = true;
                    this.g_str_isEnable = staticVarClass.str_false;
                    this.updateCustomerCash();

                    staticFunctionClass.showStatusView(true, "Nạp " + this.g_i_sumPrice + " cho tài khoản " + this.g_str_customerID + " thành công!");
                }
                else
                {
                    this.saveSubPriceForCustomer();

                    this.g_b_isAgree = true;
                    this.g_str_isEnable = staticVarClass.str_false;
                    this.updateCustomerCash();

                    staticFunctionClass.showStatusView(true, "Rút " + this.g_i_sumPrice + " cho tài khoản " + this.g_str_customerID + " thành công!");
                }
            }
            catch
            {
                if (this.g_b_isAddCash)
                {
                    this.g_b_isAgree = false;

                    staticFunctionClass.showStatusView(false, "Nạp " + this.g_i_sumPrice + " cho tài khoản " + this.g_str_customerID + " thất bại!");
                }
                else
                {

                    this.g_b_isAgree = false;

                    staticFunctionClass.showStatusView(false, "Rút " + this.g_i_sumPrice + " cho tài khoản " + this.g_str_customerID + " thất bại!");
                }
            }
        }

        private void saveAddPriceForCustomer()
        {
            int i_cash = 0;

            if (this.g_b_isAddCash == true)
                i_cash = this.g_i_sumPrice;
            else
                i_cash = -this.g_i_sumPrice;

            dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).ToList()
                                              .ForEach(customer => customer.CASH += i_cash);
            dataProvider.Instance.DB.SaveChanges();
        }

        private void updateCustomerCash()
        {
            this.g_i_customerCash = (int)dataProvider.Instance.DB.CUSTOMERs
                .Where(customer => customer.ID == this.g_str_customerID)
                .Select(customer => customer.CASH).FirstOrDefault();
        }
        #endregion

        #region Button back.
        private bool checkClickButtonBack()
        {
            if (this.g_b_isAgree == false)
                return false;

            return true;
        }

        private void clickButtonBack()
        {
            try
            {
                if (this.g_b_isAddCash)
                {
                    this.saveSubPriceForCustomer();

                    //
                    this.resetForBack();
                    this.updateCustomerCash();

                    staticFunctionClass.showStatusView(true, "Hoàn tác nạp " + this.g_i_sumPrice + " cho tài khoản " + this.g_str_customerID + " thành công!");
                }
                else
                {
                    this.saveAddPriceForCustomer();

                    //
                    this.resetForBack();
                    this.updateCustomerCash();

                    staticFunctionClass.showStatusView(true, "Hoàn tác rút " + this.g_i_sumPrice + " cho tài khoản " + this.g_str_customerID + " thành công!");
                }


            }
            catch
            {
                if (this.g_b_isAddCash)
                {
                    this.g_b_isAgree = false;

                    staticFunctionClass.showStatusView(false, "Hoàn tác nạp " + this.g_i_sumPrice + " cho tài khoản " + this.g_str_customerID + " thất bại!");
                }
                else
                {
                    this.g_b_isAgree = false;

                    staticFunctionClass.showStatusView(false, "Hoàn tác rút " + this.g_i_sumPrice + " cho tài khoản " + this.g_str_customerID + " thất bại!");
                }
            }
        }

        private void saveSubPriceForCustomer()
        {
            int i_cash = 0;

            if (this.g_b_isAddCash == true)
                i_cash = -this.g_i_sumPrice;
            else
                i_cash = this.g_i_sumPrice;

            dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).ToList()
                                              .ForEach(customer => customer.CASH += i_cash);
            dataProvider.Instance.DB.SaveChanges();
        }

        private void resetForBack()
        {
            this.g_b_isAgree = false;
            this.g_str_isEnable = staticVarClass.str_true;
        }
        #endregion

        private void setIDCustomer()
        {
            string str_ID = staticFunctionClass.getIDFronExcel();
            if (str_ID != string.Empty)
                this.g_str_customerID = str_ID;
        }
    }
}
