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

        private Stack<string> _g_stck_undo;
        public Stack<string> g_stck_undo
        {
            get => _g_stck_undo;
            set
            {
                _g_stck_undo = value;
            }
        }

        private Stack<string> _g_stck_redo;
        public Stack<string> g_stck_redo
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
        #endregion

        public UtilityCustomersViewModel()
        {
            this.initSupport();

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

            g_iCm_TextBoxGotFocusCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.textBoxGotFocus();
            });

            g_iCm_TextBoxLostFocusCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.textBoxLostFocus();
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
        }

        private void initSupport()
        {
            //
            this.g_b_isAddCash = true;
            this.g_str_Mode = staticVarClass.mode_addCash;

            //
            this.g_str_sumPrice = "0";
            this.g_obCl_million = new ObservableCollection<CASH>();

            //
            this.g_str_isEnable = staticVarClass.str_true;

            //
            this.loadedItemsControl();
            this.resetCustomer();

            //
            this.g_stck_redo = new Stack<string>();
            this.g_stck_undo = new Stack<string>();
            this.g_stck_undo.Push("0");

            // 
            this.g_b_isAgree = false;
            this.g_b_isHaveCus = false;
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
            if (int.Parse(g_str_sumPrice) + p.MILLION > 1000000 || this.g_b_isAgree == true)
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

            this.g_stck_undo.Push(this.g_str_sumPrice);
            // When redo, add price then clear g_stck_redo.
            this.g_stck_redo.Clear();
        }
        #endregion

        #region Mouse right up.
        private bool checkMouseRightButtonUpMillion(CASH p)
        {
            if (int.Parse(g_str_sumPrice) - p.MILLION <= 0 || this.g_b_isAgree == true)
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

            this.g_stck_undo.Push(this.g_str_sumPrice);
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
            }
            else
            {
                this.g_b_isAddCash = true;
                this.g_str_Mode = staticVarClass.mode_addCash;
            }

            this.resetForMode();
        }

        private void resetForMode()
        {
            this.g_b_isAgree = false;
            this.g_str_sumPrice = "0";
            this.g_str_isEnable = staticVarClass.str_true;
        }
        #endregion

        #region Textbox price.
        private void textBoxGotFocus()
        {
            if (this.g_str_sumPrice == "0")
                this.g_str_sumPrice = "";
        }

        private void textBoxLostFocus()
        {
            if (this.g_str_sumPrice == "")
                this.g_str_sumPrice = "0";
        }

        private void textBoxKeyDown()
        {
            this.g_stck_undo.Push(this.g_str_sumPrice);
            // When redo, add price then clear g_stck_redo.
            this.g_stck_redo.Clear();
        }
        #endregion

        #region Button clear.
        private bool checkClickButtonClear()
        {
            if (this.g_str_sumPrice == "")
                this.g_str_sumPrice = "0";

            if (int.Parse(this.g_str_sumPrice) == 0 || this.g_b_isAgree == true)
                return false;

            return true;
        }

        private void clickButtonClear()
        {
            this.g_str_sumPrice = "0";
        }
        #endregion

        #region Button redo.
        private bool checkClickButtonRedo()
        {
            if (this.g_stck_redo.Count() == 0 || this.g_b_isAgree == true || this.g_str_sumPrice == "0")
                return false;

            return true;
        }

        private void clickButtonRedo()
        {
            string l_temp = this.g_stck_redo.Pop();

            this.g_str_sumPrice = l_temp;
        }
        #endregion

        #region Button undo.
        private bool checkClickButtonUndo()
        {
            if (this.g_stck_undo.Count() <= 1 || this.g_b_isAgree == true || this.g_str_sumPrice == "0")
                return false;

            return true;
        }

        private void clickButtonUndo()
        {
            string l_temp = this.g_stck_undo.Pop();

            this.g_str_sumPrice = this.g_stck_undo.ElementAtOrDefault(0);

            this.g_stck_redo.Push(l_temp);
        }
        #endregion

        #region Button remove.
        private bool checkClickButtonRemove()
        {
            // < 10.000.
            if (int.Parse(g_str_sumPrice) - 10000 < 0 || this.g_b_isAgree == true)
                return false;

            return true;
        }

        private void clickButtonRemove()
        {
            int l_i_sumPrice = int.Parse(this.g_str_sumPrice);
            l_i_sumPrice -= 10000;

            this.g_str_sumPrice = l_i_sumPrice.ToString();

            this.g_stck_undo.Push(this.g_str_sumPrice);
            // When redo, add price then clear g_stck_redo.
            this.g_stck_redo.Clear();
        }
        #endregion

        #region Button add.
        private bool checkClickButtonAdd()
        {
            // > 1.000.000.
            if (int.Parse(g_str_sumPrice) + 10000 > 1000000 || this.g_b_isAgree == true)
                return false;

            return true;
        }

        private void clickButtonAdd()
        {
            int l_i_sumPrice = int.Parse(this.g_str_sumPrice);
            l_i_sumPrice += 10000;

            this.g_str_sumPrice = l_i_sumPrice.ToString();

            this.g_stck_undo.Push(this.g_str_sumPrice);
            // When redo, add price then clear g_stck_redo.
            this.g_stck_redo.Clear();
        }
        #endregion

        #region Button agree.
        private bool checkClickButtonAgree()
        {
            if (this.g_b_isHaveCus == false || this.g_str_sumPrice == "0" || this.g_b_isAgree == true)
                return false;

            return true;
        }

        private void clickButtonAgree()
        {
            try
            {
                if (this.g_b_isAddCash)
                {
                    this.saveAddPriceForCustomer();
                  
                    staticFunctionClass.showStatusView(true, "Nạp " + this.g_str_sumPrice + " cho tài khoản " + this.g_str_customerID + " thành công!");
                }
                else
                {
                    this.saveSubPriceForCustomer();

                    staticFunctionClass.showStatusView(true, "Rút " + this.g_str_sumPrice + " cho tài khoản " + this.g_str_customerID + " thành công!");
                }

                this.g_b_isAgree = true;
                this.g_str_isEnable = staticVarClass.str_false;
            }
            catch
            {
                if (this.g_b_isAddCash)
                {
                    staticFunctionClass.showStatusView(false, "Nạp " + this.g_str_sumPrice + " cho tài khoản " + this.g_str_customerID + " thất bại!");
                }
                else
                {
                    staticFunctionClass.showStatusView(false, "Rút " + this.g_str_sumPrice + " cho tài khoản " + this.g_str_customerID + " thất bại!");
                }

                this.g_b_isAgree = false;
            }
        }

        private void saveAddPriceForCustomer()
        {
            dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).ToList()
                                              .ForEach(customer => customer.CASH += int.Parse(this.g_str_sumPrice));
            dataProvider.Instance.DB.SaveChanges();
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

                    staticFunctionClass.showStatusView(true, "Hoàn tác nạp " + this.g_str_sumPrice + " cho tài khoản " + this.g_str_customerID + " thành công!");
                }
                else
                {
                    this.saveAddPriceForCustomer();

                    staticFunctionClass.showStatusView(true, "Hoàn tác rút " + this.g_str_sumPrice + " cho tài khoản " + this.g_str_customerID + " thành công!");
                }

                this.resetForBack();
            }
            catch
            {
                if (this.g_b_isAddCash)
                {
                    staticFunctionClass.showStatusView(false, "Hoàn tác nạp " + this.g_str_sumPrice + " cho tài khoản " + this.g_str_customerID + " thất bại!");
                }
                else
                {
                    staticFunctionClass.showStatusView(false, "Hoàn tác rút " + this.g_str_sumPrice + " cho tài khoản " + this.g_str_customerID + " thất bại!");
                }

                this.g_b_isAgree = false;
            }
        }

        private void saveSubPriceForCustomer()
        {
            dataProvider.Instance.DB.CUSTOMERs.Where(customer => customer.ID == this.g_str_customerID).ToList()
                                              .ForEach(customer => customer.CASH -= int.Parse(this.g_str_sumPrice));
            dataProvider.Instance.DB.SaveChanges();
        }

        private void resetForBack()
        {
            this.g_b_isAgree = false;
            this.g_str_isEnable = staticVarClass.str_true;
        }
        #endregion
    }
}
