using CanTeenManagement.CO;
using CanTeenManagement.Model;
using CanTeenManagement.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CanTeenManagement.ViewModel
{
    class FoodDetailViewModel : BaseViewModel
    {
        public const int DEFAULT_PRICE = 25000;
        //private ORDERFOOD _g_ordF_food;
        //public ORDERFOOD g_ordF_food
        //{
        //    get => _g_ordF_food;
        //    set
        //    {
        //        _g_ordF_food = value;
        //        OnPropertyChanged();
        //    }
        //}

        private string _g_str_id;
        public string g_str_id
        {
            get => _g_str_id;
            set
            {
                _g_str_id = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_foodName;
        public string g_str_foodName
        {
            get => _g_str_foodName;
            set
            {
                _g_str_foodName = value;
                OnPropertyChanged();
            }
        }

        private int _g_i_foodType;
        public int g_i_foodType
        {
            get => _g_i_foodType;
            set
            {
                _g_i_foodType = value; OnPropertyChanged();
            }
        }

        private string _g_str_foodDecription;
        public string g_str_foodDecription
        {
            get => _g_str_foodDecription;
            set
            {
                _g_str_foodDecription = value;
                OnPropertyChanged();
            }
        }

        private int _g_i_price;
        public int g_i_price
        {
            get => _g_i_price;
            set
            {
                int i = 0;
                if (value != 0)
                {
                    if (!int.TryParse(value.ToString(), out i))
                        value = g_i_price;
                    else if (value < 0)
                        value = g_i_price;
                    else if (value > 100000)
                        value = g_i_price;
                }

                _g_i_price = value;
                OnPropertyChanged();
            }
        }

        private int _g_i_priceSale;
        public int g_i_priceSale
        {
            get => _g_i_priceSale;
            set
            {
                _g_i_priceSale = value; OnPropertyChanged();
            }
        }

        private int _g_i_sale;
        public int g_i_sale
        {
            get => _g_i_sale;
            set
            {
                int i = 0;
                if (value != 0)
                {
                    if (!int.TryParse(value.ToString(), out i))
                        value = g_i_sale;
                    else if (value < 0)
                        value = g_i_sale;
                    else if (value > 100)
                        value = g_i_sale;
                }

                _g_i_sale = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_imageLink;
        public string g_str_imageLink
        {
            get => _g_str_imageLink;
            set
            {
                _g_str_imageLink = value;
                OnPropertyChanged();
            }
        }

        private int _g_i_star;
        public int g_i_star
        {
            get => _g_i_star;
            set
            {
                _g_i_star = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_status;
        public string g_str_status
        {
            get => _g_str_status;
            set
            {
                _g_str_status = value;
                OnPropertyChanged();
            }
        }

        private List<string> _g_lst_foodType;
        public List<string> g_lst_foodType
        {
            get => _g_lst_foodType;
            set
            {
                _g_lst_foodType = value;
                OnPropertyChanged();
            }
        }

        private List<string> _g_lst_status;
        public List<string> g_lst_status
        {
            get => _g_lst_status;
            set
            {
                _g_lst_status = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _g_imgSrc_currFood;
        public ImageSource g_imgSrc_currFood
        {
            get => _g_imgSrc_currFood;
            set
            {
                _g_imgSrc_currFood = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_visibility;
        public string g_str_visibility
        {
            get => _g_str_visibility;
            set
            {
                _g_str_visibility = value;
                OnPropertyChanged();
            }
        }

        private bool _g_b_isAdd;
        public bool g_b_isAdd
        {
            get => _g_b_isAdd;
            set
            {
                _g_b_isAdd = value;
                OnPropertyChanged();
            }
        }

        #region command.
        public ICommand g_iCm_LoadedWindowCommand { get; set; }

        public ICommand g_iCm_ClickCloseWindowCommand { get; set; }

        public ICommand g_iCm_ClickButtonSaveCommand { get; set; }

        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }

        public ICommand g_iCm_ClickChangeImageCommand { get; set; }

        public ICommand g_iCm_LoadedImageEditCommand { get; set; }

        public ICommand g_iCm_TextChangedTextBoxPriceCommand { get; set; }

        public ICommand g_iCm_TextChangedTextBoxSaleCommand { get; set; }

        public ICommand g_iCm_ClickButtonAddPriceCommand { get; set; }

        public ICommand g_iCm_ClickButtonRemovePriceCommand { get; set; }

        public ICommand g_iCm_ClickButtonAddSaleCommand { get; set; }

        public ICommand g_iCm_ClickButtonRemoveSaleCommand { get; set; }
        #endregion

        public FoodDetailViewModel()
        {
            g_iCm_LoadedWindowCommand = new RelayCommand<FoodDetailView>((p) => { return true; }, (p) =>
            {
                this.loaded();
            });

            g_iCm_ClickCloseWindowCommand = new RelayCommand<FoodDetailView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<FoodDetailView>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });

            g_iCm_ClickChangeImageCommand = new RelayCommand<FoodDetailView>((p) => { return true; }, (p) =>
            {
                this.clickChangeImage(p);
            });

            g_iCm_TextChangedTextBoxPriceCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.textChangedTextBoxPrice();
            });

            g_iCm_TextChangedTextBoxSaleCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                this.textChangedTextBoxSale();
            });

            g_iCm_ClickButtonAddPriceCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonAddPrice(); }, (p) =>
            {
                this.clickButtonAddPrice();
            });

            g_iCm_ClickButtonRemovePriceCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonRemovePrice(); }, (p) =>
            {
                this.clickButtonRemovePrice();
            });

            g_iCm_ClickButtonAddSaleCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonAddSale(); }, (p) =>
            {
                this.clickButtonAddSale();
            });

            g_iCm_ClickButtonRemoveSaleCommand = new RelayCommand<Button>((p) => { return this.checkClickButtonRemoveSale(); }, (p) =>
            {
                this.clickButtonRemoveSale();
            });

            g_iCm_ClickButtonSaveCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                this.clickButtonSave();
            });
        }

        #region loaded.
        private void loaded()
        {
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            //this.g_ordF_food = l_orderVM.g_ordF_orderFood;

            if (l_orderVM.g_b_isAdd)
            {
                this.g_b_isAdd = true;
                this.setPropetyOrderFood(this.emptyFood());
                this.g_i_priceSale = DEFAULT_PRICE;
            }
            else
            {
                this.g_b_isAdd = false;
                this.setPropetyOrderFood(l_orderVM.g_ordF_orderFood);
            }

            this.loadCbbFoodType();
            this.loadCbbStatus();
        }

        private void loadCbbFoodType()
        {
            // Thêm danh sách gender.
            List<string> l_lst_foodType = new List<string>();
            l_lst_foodType.Add(staticVarClass.foodTypeStr_one);
            l_lst_foodType.Add(staticVarClass.foodTypeStr_two);
            l_lst_foodType.Add(staticVarClass.foodTypeStr_three);

            this.g_lst_foodType = l_lst_foodType;
        }

        private void loadCbbStatus()
        {
            // Thêm danh sách gender.
            List<string> l_lst_status = new List<string>();
            l_lst_status.Add(staticVarClass.status_still);
            l_lst_status.Add(staticVarClass.status_soldOut);

            this.g_lst_status = l_lst_status;
        }

        private void setPropetyOrderFood(ORDERFOOD p)
        {
            if (p == null)
                return;

            this.g_str_id = p.ID;
            this.g_str_foodName = p.FOODNAME;
            this.g_i_foodType = p.FOODTYPE - 1;
            this.g_str_foodDecription = p.FOODDESCRIPTION;
            this.g_i_priceSale = p.PRICESALE;
            this.g_i_price = p.PRICE;
            this.g_i_sale = p.SALE;
            this.g_str_imageLink = p.IMAGELINK;
            this.g_imgSrc_currFood = p.IMAGESOURCE;
            this.g_i_star = p.STAR;
            this.g_str_status = p.STATUS;
            this.g_str_visibility = p.VISIBILITY;
        }

        private ORDERFOOD emptyFood()
        {
            ORDERFOOD orderFood = new ORDERFOOD
            {
                ID = this.createNewFoodID(),
                FOODNAME = string.Empty,
                FOODTYPE = 1,
                FOODDESCRIPTION = string.Empty,
                PRICE = DEFAULT_PRICE,
                SALE = 0,
                IMAGELINK = staticVarClass.linkImg_empty,
                IMAGESOURCE = staticFunctionClass.LoadBitmap(staticVarClass.linkImg_empty),
                STATUS = staticVarClass.status_still,
                VISIBILITY = staticVarClass.visibility_hidden
            };

            return orderFood;
        }

        private string createNewFoodID()
        {
            string l_str_CurrID = dataProvider.Instance.DB.FOODs
                .OrderByDescending(food => food.ID)
                .Select(food => food.ID).FirstOrDefault();

            //var resultString = dataProvider.Instance.DB.ORDERINFOes.OrderByDescending(id => id.ID).First();
            Match match = Regex.Match(l_str_CurrID, @"(\d+)");

            if (match.Success)
            {
                // Create new orderID.
                return "FOOD" + ((int.Parse(match.Groups[1].Value)) + 1).ToString();
            }

            return null; ;
        }
        #endregion

        private void clickCloseWindow(FoodDetailView p)
        {
            if (p == null)
                return;

            p.Close();
        }

        private void mouseLeftButtonDown(FoodDetailView p)
        {
            if (p == null)
                return;

            p.DragMove();
        }

        private void clickChangeImage(FoodDetailView p)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";

            Nullable<bool> b_result = openFileDialog.ShowDialog();

            if (b_result == true)
            {
                try
                {
                    // upload new image.
                    string str_path = openFileDialog.FileName;

                    if (str_path != string.Empty)
                    {
                        if (staticFunctionClass.deleteFile(this.g_str_id + staticFunctionClass.getFormat(this.g_str_imageLink)))
                        {
                            string str_newfileName = this.g_str_id + staticFunctionClass.getFormat(str_path);

                            if (staticFunctionClass.uploadFile(str_newfileName, str_path))
                            {
                                // Update image link in database.
                                this.g_str_imageLink = staticVarClass.server_serverDirectory + str_newfileName;

                                // Update image link in database.
                                this.g_str_imageLink = staticVarClass.server_serverDirectory + str_newfileName;

                                // Update image source.
                                this.g_imgSrc_currFood = staticFunctionClass.LoadBitmap(this.g_str_imageLink);

                                //Thread.Sleep(10000);

                                dataProvider.Instance.DB.FOODs.Where(food => food.ID == this.g_str_id).ToList()
                                                                  .ForEach(food => food.IMAGELINK = g_str_imageLink);
                                dataProvider.Instance.DB.SaveChanges();

                                staticFunctionClass.showStatusView(true, "Đổi ảnh đại diện thành công!");


                            }
                        }
                    }
                }
                catch
                {
                    staticFunctionClass.showStatusView(false, "Đổi ảnh đại diện thất bại!");
                }
            }


        }

        #region price.
        private bool checkClickButtonAddPrice()
        {
            // > 1.000.000 đ.
            if (this.g_i_price + 500 > 1000000)
                return false;

            return true;
        }

        private void clickButtonAddPrice()
        {
            this.g_i_price += 500;
        }

        private bool checkClickButtonRemovePrice()
        {
            // < 0 đ.
            if (this.g_i_price - 500 < 0)
                return false;

            return true;
        }

        private void clickButtonRemovePrice()
        {
            this.g_i_price -= 500;
        }

        private void textChangedTextBoxPrice()
        {
            this.g_i_price = this.g_i_price;

            this.g_i_priceSale = (int)(this.g_i_price * ((double)(100 - this.g_i_sale) / 100));
        }
        #endregion

        #region sale.
        private bool checkClickButtonAddSale()
        {
            // > 100%.
            if (this.g_i_sale + 1 > 100)
                return false;

            return true;
        }

        private void clickButtonAddSale()
        {
            this.g_i_sale++;
        }

        private bool checkClickButtonRemoveSale()
        {
            // < 0%.
            if (this.g_i_sale - 1 < 0)
                return false;

            return true;
        }

        private void clickButtonRemoveSale()
        {
            this.g_i_sale--;
        }

        private void textChangedTextBoxSale()
        {
            this.g_i_sale = this.g_i_sale;

            this.g_i_priceSale = (int)(this.g_i_price * ((double)(100 - this.g_i_sale) / 100));
        }

        private bool checkBeforeSave()
        {
            if (this.g_str_foodName == "" || this.g_str_foodName.Count() == 1)
            {
                staticFunctionClass.showStatusView(false, "Sai tên món ăn, vui lòng nhập lại!");
                return false;
            }

            if (dataProvider.Instance.DB.FOODs.Any(f => f.FOODNAME == g_str_foodName) == true && g_b_isAdd == true)
            {
                staticFunctionClass.showStatusView(false, "Món " + g_str_foodName + " đã có trong cơ sở dữ liệu");
                return false;
            }

            if (this.g_i_price < 1000)
            {
                staticFunctionClass.showStatusView(false, "Sai giá thành, vui lòng nhập lại!");
                return false;
            }

            if (this.g_str_status == "")
            {
                staticFunctionClass.showStatusView(false, "Chưa chọn trạng thái món ăn, vui lòng nhập lại!");
                return false;
            }

            if (this.g_i_star == 0)
            {
                staticFunctionClass.showStatusView(false, "Chưa đánh giá món ăn, vui lòng nhập lại!");
                return false;
            }

            if (this.g_i_foodType == -1)
            {
                staticFunctionClass.showStatusView(false, "Chưa chọn loại món ăn, vui lòng nhập lại!");
                return false;
            }

            //if (g_str_imageLink == @"\\127.0.0.1\CanteenManagement\empty.default.png")
            //{
            //    staticFunctionClass.showStatusView(false, "Chưa chọn hình ảnh, vui lòng nhập lại!");
            //    return false;
            //}

            if (dataProvider.Instance.DB.FOODs.Any(f => f.FOODNAME == g_str_foodName) == true && g_b_isAdd == false)
            {
                return true;
            }

            if (dataProvider.Instance.DB.FOODs.Any(f => f.FOODNAME == g_str_foodName) == false)
            {
                g_str_id = this.createNewFoodID();
                return true;
            }

            return true;
        }

        private void clickButtonSave()
        {
            if (checkBeforeSave() == true)
            {
                if (g_b_isAdd == true)
                {
                    addNewFood();
                }
                else
                {
                    editFood();
                }
            }
            else
            {
                return;
            }
        }

        private void addNewFood()
        {
            try
            {
                g_str_foodName = staticFunctionClass.StringNormalization(g_str_foodName);
                g_str_foodDecription = staticFunctionClass.StringNormalization(g_str_foodDecription);
                var l_food = new FOOD()
                {
                    ID = g_str_id,
                    FOODNAME = g_str_foodName,
                    FOODTYPE = g_i_foodType + 1,
                    FOODDESCRIPTION = g_str_foodDecription,
                    PRICE = g_i_price,
                    SALE = g_i_sale,
                    IMAGELINK = g_str_imageLink,
                    STAR = g_i_star,
                    STATUS = g_str_status
                };

                dataProvider.Instance.DB.FOODs.Add(l_food);
                dataProvider.Instance.DB.SaveChanges();

                staticFunctionClass.showStatusView(true, "Thêm món " + g_str_foodName + " thành công!");
            }
            catch
            {
                staticFunctionClass.showStatusView(false, "Thêm món " + g_str_foodName + " không thành công!");
            }
        }

        private void editFood()
        {
            try
            {
                g_str_foodName = staticFunctionClass.StringNormalization(g_str_foodName);
                g_str_foodDecription = staticFunctionClass.StringNormalization(g_str_foodDecription);

                var l_food = dataProvider.Instance.DB.FOODs.First(f => f.ID == g_str_id);
                l_food.FOODNAME = g_str_foodName;
                l_food.FOODTYPE = g_i_foodType + 1;
                l_food.FOODDESCRIPTION = g_str_foodDecription;
                l_food.PRICE = g_i_price;
                l_food.SALE = g_i_sale;
                l_food.IMAGELINK = g_str_imageLink;
                l_food.STAR = g_i_star;
                l_food.STATUS = g_str_status;

                dataProvider.Instance.DB.SaveChanges();

                staticFunctionClass.showStatusView(true, "Sửa món " + g_str_foodName + " thành công!");
            }
            catch
            {
                staticFunctionClass.showStatusView(false, "Sửa món " + g_str_foodName + " không thành công!");
            }
        }
        #endregion
    }
}
