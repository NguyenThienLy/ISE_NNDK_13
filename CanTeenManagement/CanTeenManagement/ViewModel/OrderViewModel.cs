using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CanTeenManagement.View;
using CanTeenManagement.Model;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using CanTeenManagement.CO;
using System.Windows.Controls.Primitives;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System.Reflection;

namespace CanTeenManagement.ViewModel
{
    class OrderViewModel : BaseViewModel
    {
        #region collection.
        private ObservableCollection<ORDERFOOD> _g_obCl_orderFoodShow;
        public ObservableCollection<ORDERFOOD> g_obCl_orderFoodShow
        {
            get => _g_obCl_orderFoodShow;
            set { _g_obCl_orderFoodShow = value; OnPropertyChanged(); }
        }

        // Get list food from database.
        private ObservableCollection<FOOD> _g_obCl_food;
        public ObservableCollection<FOOD> g_obCl_food
        {
            get => _g_obCl_food;
            set
            {
                _g_obCl_food = value;
                OnPropertyChanged();
            }
        }

        // 
        private ObservableCollection<PAGE> _g_obCl_page { get; set; }
        public ObservableCollection<PAGE> g_obCl_page
        {
            get => _g_obCl_page;
            set
            {
                _g_obCl_page = value;
                OnPropertyChanged();
            }
        }

        //List order food.
        private ObservableCollection<PAYFOOD> _g_obCl_orderFood;
        public ObservableCollection<PAYFOOD> g_obCl_orderFood
        {
            get => _g_obCl_orderFood;
            set { _g_obCl_orderFood = value; }
        }

        private ORDERFOOD _g_ordF_orderFood;
        public ORDERFOOD g_ordF_orderFood
        {
            get => _g_ordF_orderFood;
            set { _g_ordF_orderFood = value; }
        }
        #endregion

        #region search.
        // curr Price in slider.
        private int _g_i_currPrice;
        public int g_i_currPrice
        {
            get => _g_i_currPrice;
            set { _g_i_currPrice = value; }
        }

        // curr star in rating bar.
        private int _g_i_currStar;
        public int g_i_currStar
        {
            get => _g_i_currStar;
            set { _g_i_currStar = value; }
        }

        // curr star in rating bar.
        private bool _g_b_isCheckedFoodCooked;
        public bool g_b_isCheckedFoodCooked
        {
            get => _g_b_isCheckedFoodCooked;
            set { _g_b_isCheckedFoodCooked = value; }
        }

        // curr star in rating bar.
        private bool _g_b_isCheckedNotFoodCooked;
        public bool g_b_isCheckedNotFoodCooked
        {
            get => _g_b_isCheckedNotFoodCooked;
            set { _g_b_isCheckedNotFoodCooked = value; }
        }

        private string _g_str_contentSearch;
        public string g_str_contentSearch
        {
            get => _g_str_contentSearch;
            set
            {
                _g_str_contentSearch = value;
            }
        }

        private string _g_str_contentSearchTemp;
        public string g_str_contentSearchTemp
        {
            get => _g_str_contentSearchTemp;
            set
            {
                _g_str_contentSearchTemp = value;
                OnPropertyChanged();
            }
        }

        //Curr order food.
        private int _g_i_currOrderFood;
        public int g_i_currOrderFood
        {
            get => _g_i_currOrderFood;
            set
            {
                _g_i_currOrderFood = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private bool _g_b_isAdd;
        public bool g_b_isAdd
        {
            get => _g_b_isAdd;
            set { _g_b_isAdd = value; }
        }

        #region break page.
        // Curr page.
        int g_i_currPage;
        int g_i_page;
        //Turn.
        int g_i_turn;
        //Curr turn.
        int g_i_currTurn;
        //Curr turn.
        int g_i_currIndex;
        // total food in page.
        int g_i_skipFood;
        #endregion

        #region mode.
        private bool _g_b_isViewToday;
        public bool g_b_isViewToday
        {
            get => _g_b_isViewToday;
            set
            {
                _g_b_isViewToday = value;
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
        #endregion

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

        int g_i_position;

        #region commands.
        public ICommand g_iCm_ClickPayViewCommand { get; set; }

        public ICommand g_iCm_ClickPreviousPageCommand { get; set; }

        public ICommand g_iCm_ClickNextPageCommand { get; set; }

        public ICommand g_iCm_ClickButtonPageCommand { get; set; }

        public ICommand g_iCm_ValueChangedSliderCommand { get; set; }

        public ICommand g_iCm_MouseDoubleClickRatingBarCommand { get; set; }

        public ICommand g_iCm_CheckedcheckBoxFoodCookedCommand { get; set; }

        public ICommand g_iCm_CheckedcheckBoxFoodNotCookedCommand { get; set; }

        public ICommand g_iCm_KeyUpTextSearchCommand { get; set; }

        public ICommand g_iCm_ClickCartCommand { get; set; }

        public ICommand g_iCm_ClickButtonAddCommand { get; set; }

        public ICommand g_iCm_ClickButtonExportCommand { get; set; }

        public ICommand g_iCm_ClickButtonUpdateCommand { get; set; }

        public ICommand g_iCm_ChangeModeCommand { get; set; }

        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_ClickButtonDeleteCommand { get; set; }

        public ICommand g_iCm_KeyTextSearchCommand { get; set; }

        public ICommand g_iCm_LostFocusTextSearchCommand { get; set; }

        public ICommand g_iCm_ClickButtonSearchCommand { get; set; }

        public ICommand g_iCm_ClickButtonRefreshCommand { get; set; }
        #endregion

        public OrderViewModel()
        {
            this.initSupport();

            g_iCm_LoadedCommand = new RelayCommand<OrderView>((p) => { return true; }, (p) =>
            {
                this.loaded();
            });

            g_iCm_ClickPayViewCommand = new RelayCommand<OrderView>((p) => { return this.checkButtonPay(); }, (p) =>
            {
                this.clickPayView(p);
            });

            #region break page.
            g_iCm_ClickPreviousPageCommand = new RelayCommand<Button>((p) => { return this.checkPreviousPage(); }, (p) =>
            {
                this.clickPreviousPage();
            });

            g_iCm_ClickNextPageCommand = new RelayCommand<Button>((p) => { return this.checkNextPage(); }, (p) =>
            {
                this.clickNextPage();
            });

            g_iCm_ClickButtonPageCommand = new RelayCommand<PAGE>((p) => { return true; }, (p) =>
            {
                this.clickButtonPage(p);
            });
            #endregion

            #region search.
            g_iCm_ValueChangedSliderCommand = new RelayCommand<Slider>((p) => { return true; }, (p) =>
            {
                this.valueChangedSlider(p);
            });

            g_iCm_MouseDoubleClickRatingBarCommand = new RelayCommand<RatingBar>((p) => { return true; }, (p) =>
            {
                this.mouseDoubleClickRatingBar(p);
            });

            g_iCm_CheckedcheckBoxFoodCookedCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                this.checkedcheckBoxFoodCooked(p);
            });

            g_iCm_CheckedcheckBoxFoodNotCookedCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                this.checkedcheckBoxFoodNotCooked(p);
            });

            g_iCm_LostFocusTextSearchCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.searchFood();
            });

            g_iCm_KeyTextSearchCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.searchFood();
            });

            g_iCm_ClickButtonSearchCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                this.searchFood();
            });
            #endregion

            g_iCm_ClickCartCommand = new RelayCommand<ORDERFOOD>((p) => { return this.checkClickCart(p); }, (p) =>
            {
                this.clickCart(p);
            });

            g_iCm_ClickButtonAddCommand = new RelayCommand<OrderView>((p) => { return this.checkClickButtonAdd(); }, (p) =>
            {
                this.clickButtonAdd();
            });

            g_iCm_ClickButtonExportCommand = new RelayCommand<OrderView>((p) => { return this.checkClickButtonExport(); }, (p) =>
            {
                this.clickButtonExport();
            });

            g_iCm_ClickButtonUpdateCommand = new RelayCommand<ORDERFOOD>((p) => { return this.checkClickButtonUpdate(); }, (p) =>
            {
                this.clickButtonUpdate(p);
            });

            g_iCm_ChangeModeCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) =>
            {
                this.changeMode();
            });

            g_iCm_ClickButtonDeleteCommand = new RelayCommand<ORDERFOOD>((p) => { return checkClickButtonDelete(p); }, (p) =>
            {
                this.clickButtonDelete(p);
            });

            g_iCm_ClickButtonRefreshCommand = new RelayCommand<Button>((p) => { return checkClickButtonRefresh(); }, (p) =>
            {
                this.clickButtonRefresh();
            });
        }

        FrameworkElement getWindowParent(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }

        private void initSupport()
        {
            #region collection.
            this.g_obCl_orderFood = new ObservableCollection<PAYFOOD>();

            //
            this.g_obCl_orderFoodShow = new ObservableCollection<ORDERFOOD>();

            //
            this.g_obCl_page = new ObservableCollection<PAGE>();
            #endregion

            #region mode.
            this.g_str_visibilityOrderFood = staticVarClass.visibility_hidden;
            //
            this.g_b_isViewToday = true;
            this.g_str_Mode = staticVarClass.mode_today;
            #endregion

            #region break page.
            this.g_i_currPage = 1;
            this.g_i_page = 0;
            //
            this.g_i_currIndex = 0;
            //
            this.g_i_currTurn = 0;
            this.g_i_turn = 0;
            //
            this.g_i_skipFood = 0;
            #endregion

            #region search.
            this.g_i_currPrice = 0;
            this.g_i_currStar = 1;
            this.g_b_isCheckedFoodCooked = true;
            this.g_b_isCheckedNotFoodCooked = true;
            this.g_str_contentSearch = string.Empty;
            //
            this.g_str_contentSearch = string.Empty;
            this.g_str_contentSearchTemp = string.Empty;
            #endregion

            //
            this.g_i_currOrderFood = 0;
            this.g_i_position = 0;
        }

        #region Loaded.
        private void authorize()
        {
            if (staticVarClass.position_user == staticVarClass.position_manager)
            {
                this.g_i_position = 1;
            }
            else if (staticVarClass.position_user == staticVarClass.position_cashier)
            {
                this.g_i_position = 2;
            }
            else
            {
                this.g_i_position = 3;
            }
        }

        private void loaded()
        {
            this.loadedData();
            this.authorize();
            this.checkVisibilityData();
            this.loadedPage();
            this.matchChoose();
        }

        //
        private int getQuantityOrder()
        {
            int l_count = 0;

            if (this.g_b_isViewToday == true)
            {
                using (var DB = new QLCanTinEntities())
                {
                    // Food type is 1 or 2 not 3.
                    if (this.g_b_isCheckedFoodCooked == true && this.g_b_isCheckedNotFoodCooked == false)
                        l_count = DB.FOODs
                            .Where(food => food.PRICE >= this.g_i_currPrice
                            && food.FOODNAME.Contains(this.g_str_contentSearch)
                            && food.STAR >= this.g_i_currStar
                            && (food.FOODTYPE == 1 || food.FOODTYPE == 2)
                            && food.STATUS == staticVarClass.status_still)
                            .OrderByDescending(food => food.PRICE).Count();
                    // Food type is 3 not 2 and 3.
                    else if (this.g_b_isCheckedFoodCooked == false && this.g_b_isCheckedNotFoodCooked == true)
                        l_count = DB.FOODs
                            .Where(food => food.PRICE >= this.g_i_currPrice
                            && food.FOODNAME
                            .Contains(this.g_str_contentSearch)
                            && food.STAR >= this.g_i_currStar
                            && food.FOODTYPE == 3
                            && food.STATUS == staticVarClass.status_still)
                            .OrderByDescending(food => food.PRICE).Count();
                    // Food type is 1 or 2 or 3.
                    else if (this.g_b_isCheckedFoodCooked == true && this.g_b_isCheckedNotFoodCooked == true)
                        l_count = DB.FOODs
                            .Where(food => food.PRICE >= this.g_i_currPrice
                            && food.FOODNAME.Contains(this.g_str_contentSearch)
                            && food.STAR >= this.g_i_currStar
                            && (food.FOODTYPE == 1 || food.FOODTYPE == 2 || food.FOODTYPE == 3)
                            && food.STATUS == staticVarClass.status_still)
                            .OrderByDescending(food => food.PRICE).Count();
                }
            }
            else
            {
                using (var DB = new QLCanTinEntities())
                {
                    // Food type is 1 or 2 not 3.
                    if (this.g_b_isCheckedFoodCooked == true && this.g_b_isCheckedNotFoodCooked == false)
                        l_count = DB.FOODs
                            .Where(food => food.PRICE >= this.g_i_currPrice
                            && food.FOODNAME.Contains(this.g_str_contentSearch)
                            && food.STAR >= this.g_i_currStar
                            && (food.FOODTYPE == 1 || food.FOODTYPE == 2))
                            .OrderByDescending(food => food.PRICE).Count();
                    // Food type is 3 not 2 and 3.
                    else if (this.g_b_isCheckedFoodCooked == false && this.g_b_isCheckedNotFoodCooked == true)
                        l_count = DB.FOODs
                            .Where(food => food.PRICE >= this.g_i_currPrice
                            && food.FOODNAME
                            .Contains(this.g_str_contentSearch)
                            && food.STAR >= this.g_i_currStar
                            && food.FOODTYPE == 3)
                            .OrderByDescending(food => food.PRICE).Count();
                    // Food type is 1 or 2 or 3.
                    else if (this.g_b_isCheckedFoodCooked == true && this.g_b_isCheckedNotFoodCooked == true)
                        l_count = DB.FOODs
                            .Where(food => food.PRICE >= this.g_i_currPrice
                            && food.FOODNAME.Contains(this.g_str_contentSearch)
                            && food.STAR >= this.g_i_currStar
                            && (food.FOODTYPE == 1 || food.FOODTYPE == 2 || food.FOODTYPE == 3))
                            .OrderByDescending(food => food.PRICE).Count();
                }
            }

            return l_count;
        }

        private void loadedPage()
        {
            if (this.g_obCl_page != null)
                this.g_obCl_page.Clear();

            int l_count = this.getQuantityOrder();
            int l_quantility = 0;
            int i = 0;

            if (l_count == 0)
                return;

            // Page not redundancy.
            if (l_count % staticVarClass.quantilityPage_order == 0)
                this.g_i_page = l_count / staticVarClass.quantilityPage_order;
            // Page redundancy.
            else
                this.g_i_page = l_count / staticVarClass.quantilityPage_order + 1;

            // Get quantility 5 pages on turn.
            this.g_i_turn = (this.g_i_page - 1) / staticVarClass.quantilityPage_turnOrder;
            this.g_i_currTurn = (this.g_i_currPage - 1) / staticVarClass.quantilityPage_turnOrder;

            // Get index for brush color.
            this.g_i_currIndex = (this.g_i_currPage - 1) % staticVarClass.quantilityPage_turnOrder;

            if (this.g_i_turn > this.g_i_currTurn)
                l_quantility = staticVarClass.quantilityPage_turnOrder;
            else if (this.g_i_turn == this.g_i_currTurn)
            {
                // Have # 5 pages.
                l_quantility = this.g_i_page % staticVarClass.quantilityPage_turnOrder;

                // Have 5 pages.
                if (l_quantility == 0)
                    l_quantility = staticVarClass.quantilityPage_turnOrder;
            }

            for (i = this.g_i_currTurn * staticVarClass.quantilityPage_turnOrder; i < this.g_i_currTurn * staticVarClass.quantilityPage_turnOrder + l_quantility; i++)
            {
                PAGE t_page = new PAGE(i + 1);
                this.g_obCl_page.Add(t_page);
            }

            this.changeCurrColor(this.g_i_currIndex, staticVarClass.color_indianRed);
        }

        private void loadedData()
        {
            if (this.g_obCl_orderFoodShow != null)
                this.g_obCl_orderFoodShow.Clear();
            if (this.g_obCl_food != null)
                this.g_obCl_food.Clear();

            if (this.g_b_isViewToday == true)
            {
                using (var DB = new QLCanTinEntities())
                {
                    // Food type is 1 or 2 not 3.
                    if (this.g_b_isCheckedFoodCooked == true && this.g_b_isCheckedNotFoodCooked == false)
                        g_obCl_food = new ObservableCollection<FOOD>(DB.FOODs
                        .Where(food => food.PRICE >= this.g_i_currPrice
                        && food.FOODNAME.Contains(this.g_str_contentSearch)
                        && food.STAR >= this.g_i_currStar
                        && (food.FOODTYPE == 1 || food.FOODTYPE == 2)
                        && food.STATUS == staticVarClass.status_still)
                        .OrderByDescending(food => food.PRICE)
                        .Skip(this.g_i_skipFood).Take(staticVarClass.quantilityPage_order));
                    // Food type is 3 not 1 and 2.
                    else if (this.g_b_isCheckedFoodCooked == false && this.g_b_isCheckedNotFoodCooked == true)
                        g_obCl_food = new ObservableCollection<FOOD>(DB.FOODs
                        .Where(food => food.PRICE >= this.g_i_currPrice
                        && food.FOODNAME.Contains(this.g_str_contentSearch)
                        && food.STAR >= this.g_i_currStar
                        && food.FOODTYPE == 3
                        && food.STATUS == staticVarClass.status_still)
                        .OrderByDescending(food => food.PRICE)
                        .Skip(this.g_i_skipFood).Take(staticVarClass.quantilityPage_order));
                    // Food type is 1 or 2 or 3.
                    else if (this.g_b_isCheckedFoodCooked == true && this.g_b_isCheckedNotFoodCooked == true)
                        g_obCl_food = new ObservableCollection<FOOD>(DB.FOODs
                       .Where(food => food.PRICE >= this.g_i_currPrice && food.FOODNAME
                       .Contains(this.g_str_contentSearch)
                        && food.STAR >= this.g_i_currStar
                        && (food.FOODTYPE == 1 || food.FOODTYPE == 2 || food.FOODTYPE == 3)
                        && food.STATUS == staticVarClass.status_still)
                       .OrderByDescending(food => food.PRICE)
                       .Skip(this.g_i_skipFood).Take(staticVarClass.quantilityPage_order));
                }
            }
            else
            {
                using (var DB = new QLCanTinEntities())
                {
                    // Food type is 1 or 2 not 3.
                    if (this.g_b_isCheckedFoodCooked == true && this.g_b_isCheckedNotFoodCooked == false)
                        g_obCl_food = new ObservableCollection<FOOD>(DB.FOODs
                        .Where(food => food.PRICE >= this.g_i_currPrice
                        && food.FOODNAME.Contains(this.g_str_contentSearch)
                        && food.STAR >= this.g_i_currStar
                        && (food.FOODTYPE == 1 || food.FOODTYPE == 2))
                        .OrderByDescending(food => food.PRICE)
                        .Skip(this.g_i_skipFood).Take(staticVarClass.quantilityPage_order));
                    // Food type is 3 not 1 and 2.
                    else if (this.g_b_isCheckedFoodCooked == false && this.g_b_isCheckedNotFoodCooked == true)
                        g_obCl_food = new ObservableCollection<FOOD>(DB.FOODs
                        .Where(food => food.PRICE >= this.g_i_currPrice
                        && food.FOODNAME.Contains(this.g_str_contentSearch)
                        && food.STAR >= this.g_i_currStar
                        && food.FOODTYPE == 3).OrderByDescending(food => food.PRICE)
                        .Skip(this.g_i_skipFood).Take(staticVarClass.quantilityPage_order));
                    // Food type is 1 or 2 or 3.
                    else if (this.g_b_isCheckedFoodCooked == true && this.g_b_isCheckedNotFoodCooked == true)
                        g_obCl_food = new ObservableCollection<FOOD>(DB.FOODs
                       .Where(food => food.PRICE >= this.g_i_currPrice && food.FOODNAME
                       .Contains(this.g_str_contentSearch)
                        && food.STAR >= this.g_i_currStar
                        && (food.FOODTYPE == 1 || food.FOODTYPE == 2 || food.FOODTYPE == 3))
                       .OrderByDescending(food => food.PRICE)
                       .Skip(this.g_i_skipFood).Take(staticVarClass.quantilityPage_order));
                }
            }

            foreach (FOOD food in g_obCl_food)
            {
                ORDERFOOD t_orderFood = new ORDERFOOD(food);
                g_obCl_orderFoodShow.Add(t_orderFood);
            }
        }

        private void checkVisibilityData()
        {
            if (this.g_obCl_food.Count == 0)
            {
                this.g_str_visibilityOrderFood = staticVarClass.visibility_visible;
            }
            else
            {
                this.g_str_visibilityOrderFood = staticVarClass.visibility_hidden;
            }
        }
        #endregion

        #region Pay.
        private bool checkButtonPay()
        {
            if (this.g_i_currOrderFood == 0)
                return false;

            return true;
        }

        private void clickPayView(OrderView p)
        {
            if (p == null)
                return;

            MainWindow mainWd = MainWindow.Instance;

            mainWd.Opacity = 0.5;
            p.Opacity = 0.5;

            PayView payV = new PayView();
            payV.ShowDialog();

            this.loaded();

            p.Opacity = 100;
            mainWd.Opacity = 100;
        }
        #endregion

        #region break page.
        #region Previous page.
        private bool checkPreviousPage()
        {
            if (this.g_i_currPage == 1)
                return false;

            return true;
        }

        private void clickPreviousPage()
        {
            this.changeCurrColor(this.g_i_currIndex, staticVarClass.color_mainColor);

            this.g_i_currPage--;
            this.g_i_skipFood = staticVarClass.quantilityPage_order * (this.g_i_currPage - 1);

            this.loaded();
        }
        #endregion

        #region Next page.
        private bool checkNextPage()
        {
            int l_count = this.getQuantityOrder();

            if (this.g_i_currPage * staticVarClass.quantilityPage_order >= l_count)
                return false;

            return true;
        }

        private void clickNextPage()
        {
            this.changeCurrColor(this.g_i_currIndex, staticVarClass.color_mainColor);

            this.g_i_currPage++;
            this.g_i_skipFood = staticVarClass.quantilityPage_order * (this.g_i_currPage - 1);

            this.loaded();
        }
        #endregion

        #region Shift page.
        private void clickButtonPage(PAGE p)
        {
            if (p == null)
                return;

            this.changeCurrColor(this.g_i_currIndex, staticVarClass.color_mainColor);

            this.g_i_currPage = p.CURRPAGE;
            this.g_i_skipFood = staticVarClass.quantilityPage_order * (p.CURRPAGE - 1);

            this.loaded();
        }

        private void changeCurrColor(int curr, string color)
        {
            this.g_obCl_page[curr].BORDERCOLOR = color;
        }
        #endregion
        #endregion

        #region Search.
        private void valueChangedSlider(Slider p)
        {
            if (p == null)
                return;

            this.g_i_currPrice = (int)p.Value * 5000;
            // reset curr page when value slider changed.
            this.g_i_currPage = 1;
            this.g_i_skipFood = 0;

            this.loaded();
        }

        private void mouseDoubleClickRatingBar(RatingBar p)
        {
            if (p == null)
                return;

            this.g_i_currStar = p.Value;

            // reset curr page when value slider changed.
            this.g_i_currPage = 1;
            this.g_i_skipFood = 0;

            this.loaded();
        }

        private void checkedcheckBoxFoodCooked(CheckBox p)
        {
            if (p == null)
                return;

            this.g_b_isCheckedFoodCooked = (bool)p.IsChecked;

            // reset curr page when value slider changed.
            this.g_i_currPage = 1;
            this.g_i_skipFood = 0;

            this.loaded();
        }

        private void checkedcheckBoxFoodNotCooked(CheckBox p)
        {
            if (p == null)
                return;

            this.g_b_isCheckedNotFoodCooked = (bool)p.IsChecked;

            // reset curr page when value slider changed.
            this.g_i_currPage = 1;
            this.g_i_skipFood = 0;

            this.loaded();
        }

        private void searchFood()
        {
            // Search by new text.
            this.g_str_contentSearch = this.g_str_contentSearchTemp;

            this.g_i_currPage = 1;
            this.g_i_skipFood = 0;

            this.loaded();
        }
        #endregion

        private void matchChoose()
        {
            int i = 0;

            for (i = 0; i < this.g_obCl_orderFood.Count; i++)
            {
                ORDERFOOD t_orderFood = this.g_obCl_orderFoodShow.Where(food => food.ID == this.g_obCl_orderFood[i].ID).FirstOrDefault();

                if (t_orderFood != null)
                {
                    t_orderFood.VISIBILITYCHOOSE = staticVarClass.visibility_visible;
                }
            }
        }

        // group quantity food.
        private void groupByFollowID(PAYFOOD p)
        {
            if (p == null)
                return;

            PAYFOOD l_payFood = this.g_obCl_orderFood.Where(food => food.ID == p.ID).FirstOrDefault();

            if (l_payFood != null)
            {
                l_payFood.QUANTITY++;
            }
            else
            {
                // Add quantity order food.
                this.g_obCl_orderFood.Add(p);
            }
        }

        #region Button cart.
        private bool checkClickCart(ORDERFOOD p)
        {
            if (this.g_i_position == 3)
                return false;

            // 10 order.
            if (this.g_i_currOrderFood + 1 > 10 || p.STATUS == staticVarClass.status_soldOut)
                return false;

            return true;
        }

        private void clickCart(ORDERFOOD p)
        {
            if (p == null)
                return;

            // Init quantity food.
            PAYFOOD l_payFood = new PAYFOOD(p);

            // Add quantity food.
            this.groupByFollowID(l_payFood);

            // Quantity order food.
            this.g_i_currOrderFood++;

            this.matchChoose();
        }
        #endregion

        #region Button add.
        private bool checkClickButtonAdd()
        {
            if (this.g_i_position != 1)
                return false;

            return true;
        }

        private void clickButtonAdd()
        {
            this.g_b_isAdd = true;

            MainWindow mainWd = MainWindow.Instance;
            OrderView orderV = OrderView.Instance;

            mainWd.Opacity = 0.5;
            orderV.Opacity = 0.5;

            FoodDetailView foodDetailV = new FoodDetailView();
            foodDetailV.ShowDialog();

            // Reload.
            this.loaded();

            mainWd.Opacity = 100;
            orderV.Opacity = 100;
        }
        #endregion

        #region Button export.
        private bool checkClickButtonExport()
        {
            if (this.g_obCl_orderFood == null || this.g_obCl_orderFood.Count() == 0)
                return false;

            return true;
        }

        private void clickButtonExport()
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
                List<string> l_listHeaders = new List<string> { "Mã món ăn", "Tên món ăn", "Loại món ăn", "Mô tả món ăn", "Đơn giá", "% giảm giá", "Số sao", "Trạng thái" };

                for (int x = 1; x < l_listHeaders.Count() + 1; x++)
                {
                    workSheet.Cells[1, x] = l_listHeaders[x - 1];
                    workSheet.Cells[1, x].Font.Bold = true;
                }

                for (int x = 2; x < this.g_obCl_food.Count() + 2; x++)
                {
                    workSheet.Cells[x, 1] = this.g_obCl_food[x - 2].ID.ToString().Trim();
                    workSheet.Cells[x, 2] = this.g_obCl_food[x - 2].FOODNAME.ToString().Trim();
                    workSheet.Cells[x, 3] = this.g_obCl_food[x - 2].FOODTYPE.ToString().Trim();
                    workSheet.Cells[x, 4] = this.g_obCl_food[x - 2].FOODDESCRIPTION.ToString().Trim();
                    workSheet.Cells[x, 5] = this.g_obCl_food[x - 2].PRICE.ToString().Trim();
                    workSheet.Cells[x, 6] = this.g_obCl_food[x - 2].SALE.ToString().Trim();
                    workSheet.Cells[x, 7] = this.g_obCl_food[x - 2].STAR.ToString().Trim();
                    workSheet.Cells[x, 8] = this.g_obCl_food[x - 2].STATUS.ToString().Trim();
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
                    staticFunctionClass.showStatusView(true, "Xuất file thất bại!");
                }

                workBook.Close();
                excel.Quit();
            }
        }
        #endregion

        #region Button update.
        private bool checkClickButtonUpdate()
        {
            if (this.g_i_position != 1)
                return false;

            return true;
        }

        private void clickButtonUpdate(ORDERFOOD p)
        {
            if (p == null)
                return;

            this.g_b_isAdd = false;

            this.g_ordF_orderFood = p;

            MainWindow mainWd = MainWindow.Instance;
            OrderView orderV = OrderView.Instance;

            mainWd.Opacity = 0.5;
            orderV.Opacity = 0.5;

            FoodDetailView foodDetailV = new FoodDetailView();
            foodDetailV.ShowDialog();

            // Reload.
            this.loaded();

            mainWd.Opacity = 100;
            orderV.Opacity = 100;
        }
        #endregion

        private void changeMode()
        {
            if (this.g_b_isViewToday)
            {
                this.g_b_isViewToday = false;
                this.g_str_Mode = staticVarClass.mode_all;
            }
            else
            {
                this.g_b_isViewToday = true;
                this.g_str_Mode = staticVarClass.mode_today;
            }

            this.g_i_currPage = 1;
            this.g_i_skipFood = 0;
            this.loaded();
        }

        #region Button delete.
        private bool checkClickButtonDelete(ORDERFOOD p)
        {
            if (p.VISIBILITYCHOOSE == staticVarClass.visibility_hidden)
                return false;

            return true;
        }

        private void clickButtonDelete(ORDERFOOD p)
        {
            if (p == null)
                return;

            p.VISIBILITYCHOOSE = staticVarClass.visibility_hidden;
            this.removeFood(p);
        }

        private void removeFood(ORDERFOOD p)
        {
            PAYFOOD l_payFood = this.g_obCl_orderFood.Where(food => food.ID == p.ID).FirstOrDefault();

            if (l_payFood != null)
            {
                this.g_obCl_orderFood.Remove(l_payFood);
            }

            // Quantity order food.
            this.g_i_currOrderFood -= l_payFood.QUANTITY;
        }
        #endregion

        #region button refresh.
        private bool checkClickButtonRefresh()
        {
            if (this.g_str_visibilityOrderFood == staticVarClass.visibility_visible)
                return false;

            return true;
        }

        private void clickButtonRefresh()
        {
            this.loaded();
        }
        #endregion
    }
}