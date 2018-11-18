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

namespace CanTeenManagement.ViewModel
{
    class OrderViewModel : BaseViewModel
    {
        private ObservableCollection<FOOD> _g_listFood;
        public ObservableCollection<FOOD> g_listFood
        {
            get => _g_listFood;
            set { _g_listFood = value; OnPropertyChanged(); }
        }

        // Curr page.
        private int _g_i_currPage;
        public int g_i_currPage
        {
            get => _g_i_currPage;
            set { _g_i_currPage = value; OnPropertyChanged(); }
        }

        // total food in page.
        private int _g_i_totalFood;
        public int g_i_totalFood
        {
            get => _g_i_totalFood;
            set { _g_i_totalFood = value; }
        }

        // total food in page.
        private int _g_i_skipFood;
        public int g_i_skipFood
        {
            get => _g_i_skipFood;
            set { _g_i_skipFood = value; }
        }

        // curr Price in slider.
        private int _g_i_currPrice;
        public int g_i_currPrice
        {
            get => _g_i_currPrice;
            set { _g_i_currPrice = value; } }

        // curr star in rating bar.
        private int _g_i_currStar;
        public int g_i_currStar
        {
            get => _g_i_currStar;
            set { _g_i_currStar = value; }
        }

        // curr star in rating bar.
        private bool _g_b_isCheckedFoodCooked;
        public bool g_b_isCheckedFoodCooked {
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
            get { return _g_str_contentSearch; }
            set
            {
                _g_str_contentSearch = value;
            }
        }

        #region Các thuộc tính của food.
        private string _g_str_id;
        public string g_str_id { get => _g_str_id; set { _g_str_id = value; OnPropertyChanged(); } }

        private string _g_str_foodName;
        public string g_str_foodName { get => _g_str_foodName; set { _g_str_foodName = value; OnPropertyChanged(); } }

        private Nullable<int> _g_str_foodType;
        public Nullable<int> g_str_foodType { get => _g_str_foodType; set { _g_str_foodType = value; OnPropertyChanged(); } }

        private Nullable<int> _g_i_foodDescription;
        public Nullable<int> g_i_foodDescription { get => _g_i_foodDescription; set { _g_i_foodDescription = value; OnPropertyChanged(); } }

        private string _g_str_price;
        public string g_str_price { get => _g_str_price; set { _g_str_price = value; OnPropertyChanged(); } }

        private string _g_str_sale;
        public string g_str_sale { get => _g_str_sale; set { _g_str_sale = value; OnPropertyChanged(); } }

        private string _g_str_avatar;
        public string g_str_avatar { get => _g_str_avatar; set { _g_str_avatar = value; OnPropertyChanged(); } }

        private string _g_str_status;
        public string g_str_status { get => _g_str_status; set { _g_str_status = value; OnPropertyChanged(); } }
        #endregion

        #region commands.
        public ICommand g_iCm_ClickPayViewCommand { get; set; }

        public ICommand g_iCm_ClickPreviousPageCommand { get; set; }

        public ICommand g_iCm_ClickNextPageCommand { get; set; }

        public ICommand g_iCm_ClickNumericOneCommand { get; set; }

        public ICommand g_iCm_ClickNumericTwoCommand { get; set; }

        public ICommand g_iCm_ClickNumericThreeCommand { get; set; }

        public ICommand g_iCm_ClickNumericFourCommand { get; set; }

        public ICommand g_iCm_ClickNumericFiveCommand { get; set; }

        public ICommand g_iCm_LoadedItemsControlCommand { get; set; }

        public ICommand g_iCm_ValueChangedSliderCommand { get; set; }

        public ICommand g_iCm_MouseDoubleClickRatingBarCommand { get; set; }

        public ICommand g_iCm_CheckedcheckBoxFoodCookedCommand { get; set; }

        public ICommand g_iCm_CheckedcheckBoxFoodNotCookedCommand { get; set; }

        public ICommand g_iCm_KeyUpTextSearchCommand { get; set; }
    #endregion

    public OrderViewModel()
        {
            this.g_i_currPage = 1;
            this.g_i_totalFood = 8;
            this.g_i_currPrice = 30000;
            this.g_i_currStar = 5;
            this.g_b_isCheckedFoodCooked = true;
            this.g_b_isCheckedNotFoodCooked = false;
            this.g_str_contentSearch = string.Empty;

            g_iCm_LoadedItemsControlCommand = new RelayCommand<ItemsControl>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            g_iCm_ClickPayViewCommand = new RelayCommand<OrderView>((p) => { return true; }, (p) =>
            {
                this.clickPayView(p);
            });

            g_iCm_ClickPreviousPageCommand = new RelayCommand<Button>((p) => { return this.checkPreviousPage(); }, (p) =>
            {
                this.clickPreviousPage(p);
            });

            g_iCm_ClickNextPageCommand = new RelayCommand<Button>((p) => { return this.checkNextPage(); }, (p) =>
            {
                this.clickNextPage(p);
            });

            g_iCm_ClickNumericOneCommand = new RelayCommand<Button>((p) => { return checkShiftPage(1); }, (p) =>
            {
                this.shiftPage(p, 1);
            });

            g_iCm_ClickNumericTwoCommand = new RelayCommand<Button>((p) => { return checkShiftPage(2); }, (p) =>
            {
                this.shiftPage(p, 2);
            });

            g_iCm_ClickNumericThreeCommand = new RelayCommand<Button>((p) => { return checkShiftPage(3); }, (p) =>
            {
                this.shiftPage(p, 3);
            });

            g_iCm_ClickNumericFourCommand = new RelayCommand<Button>((p) => { return checkShiftPage(4); }, (p) =>
            {
                this.shiftPage(p, 4);
            });

            g_iCm_ClickNumericFiveCommand = new RelayCommand<Button>((p) => { return checkShiftPage(5); }, (p) =>
            {
                this.shiftPage(p, 5);
            });

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

            g_iCm_KeyUpTextSearchCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.keyUpTextSearch(p);
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

        private void loaded(ItemsControl p)
        {
            this.loadData();
        }

        private void loadData()
        {
            if (this.g_listFood != null)
                this.g_listFood.Clear();

            // Food type is 1 or 2 not 3.
            if (this.g_b_isCheckedFoodCooked == true && this.g_b_isCheckedNotFoodCooked == false)
                this.g_listFood = new ObservableCollection<FOOD>(dataProvider.Instance.DB.FOODs.Where(food => food.PRICE <= this.g_i_currPrice && food.FOODNAME.Contains(this.g_str_contentSearch) && food.STAR == this.g_i_currStar && (food.FOODTYPE == 1 || food.FOODTYPE == 2)).OrderByDescending(food => food.PRICE).Skip(this.g_i_skipFood).Take(this.g_i_totalFood));
            // Food type is 3 not 2 and 3.
            else if (this.g_b_isCheckedFoodCooked == false && this.g_b_isCheckedNotFoodCooked == true)
                this.g_listFood = new ObservableCollection<FOOD>(dataProvider.Instance.DB.FOODs.Where(food => food.PRICE <= this.g_i_currPrice && food.FOODNAME.Contains(this.g_str_contentSearch) && food.STAR == this.g_i_currStar && food.FOODTYPE == 3).OrderByDescending(food => food.PRICE).Skip(this.g_i_skipFood).Take(this.g_i_totalFood));
            // Food type is 1 or 2 or 3.
            else if (this.g_b_isCheckedFoodCooked == true && this.g_b_isCheckedNotFoodCooked == true)
                this.g_listFood = new ObservableCollection<FOOD>(dataProvider.Instance.DB.FOODs.Where(food => food.PRICE <= this.g_i_currPrice && food.FOODNAME.Contains(this.g_str_contentSearch) && food.STAR == this.g_i_currStar && (food.FOODTYPE == 1 || food.FOODTYPE == 2 || food.FOODTYPE == 3)) .OrderByDescending(food => food.PRICE).Skip(this.g_i_skipFood).Take(this.g_i_totalFood));
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

            mainWd.Opacity = 100;
            p.Opacity = 100;
        }

        #region previous page.
        private bool checkPreviousPage()
        {
            if (this.g_i_currPage == 1)
                return false;

            return true;
        }

        private void clickPreviousPage(Button p)
        {
            if (p == null)
                return;

            this.g_i_skipFood = g_i_totalFood * (this.g_i_currPage - 1);
            this.loadData();

            this.g_i_currPage--;
        }
        #endregion

        #region next page.
        private bool checkNextPage()
        {
            int l_i_maxFoodPage = dataProvider.Instance.DB.FOODs.Where(food => food.PRICE <= this.g_i_currPrice && food.STAR == this.g_i_currStar).OrderByDescending(food => food.PRICE).Count();

            if (this.g_i_currPage * this.g_i_totalFood > l_i_maxFoodPage)
                return false;

            return true;
        }

        private void clickNextPage(Button p)
        {
            if (p == null)
                return;

            this.g_i_skipFood = g_i_totalFood * (this.g_i_currPage);
            this.loadData();

            this.g_i_currPage++;
        }
        #endregion

        #region shift page.
        private bool checkShiftPage(int currPage)
        {
            int l_i_maxFoodPage = dataProvider.Instance.DB.FOODs.Where(food => food.PRICE <= this.g_i_currPrice && food.STAR == this.g_i_currStar).OrderByDescending(food => food.PRICE).Count();

            if (this.g_i_totalFood * (currPage - 1) >= l_i_maxFoodPage)
                return false;

            return true;
        }

        private void shiftPage(Button p, int currPage)
        {
            if (p == null)
                return;

            this.g_i_skipFood = g_i_totalFood * (currPage - 1);

            this.loadData();
            this.g_i_currPage = currPage;
        }
        #endregion

        private void valueChangedSlider(Slider p)
        {
            if (p == null)
                return;

            this.g_i_currPrice = (int)p.Value * 5000;

            // reset curr page when value slider changed.
            this.g_i_currPage = 1;

            this.loadData();
        }

        private void mouseDoubleClickRatingBar(RatingBar p)
        {
              if (p == null)
                return;

            this.g_i_currStar = p.Value;

            // reset curr page when value slider changed.
            this.g_i_currPage = 1;

            this.loadData();
        }

        private void checkedcheckBoxFoodCooked(CheckBox p)
        {
            if (p == null)
                return;

            this.g_b_isCheckedFoodCooked = (bool)p.IsChecked;

            // reset curr page when value slider changed.
            this.g_i_currPage = 1;

            this.loadData();
        }

        private void checkedcheckBoxFoodNotCooked(CheckBox p)
        {
            if (p == null)
                return;

            this.g_b_isCheckedNotFoodCooked = (bool)p.IsChecked;

            // reset curr page when value slider changed.
            this.g_i_currPage = 1;

            this.loadData();
        }

        private void keyUpTextSearch(TextBox p)
        {
            if (p == null)
                return;

            this.g_str_contentSearch = p.Text.Trim();

            // reset curr page when value slider changed.
            this.g_i_currPage = 1;

            this.loadData();
        }
    }
}
