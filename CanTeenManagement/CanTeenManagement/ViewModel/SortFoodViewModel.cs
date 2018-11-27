using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CanTeenManagement.View;
using CanTeenManagement.Model;
using System.Collections.ObjectModel;

namespace CanTeenManagement.ViewModel
{
    class SortFoodViewModel : BaseViewModel
    {
        private ObservableCollection<ORDERQUEUE> _g_list_OrderQueue;
        public ObservableCollection<ORDERQUEUE> g_list_OrderQueue {
            get => _g_list_OrderQueue;
            set { _g_list_OrderQueue = value; OnPropertyChanged(); }
        }

        #region Thuộc tính card 1
        private string _g_str_orderID1;
        public string g_str_orderID1 { get => _g_str_orderID1; set { _g_str_orderID1 = value; OnPropertyChanged(); } }

        private string _g_str_foodName1;
        public string g_str_foodName1 { get => _g_str_foodName1; set { _g_str_foodName1 = value; OnPropertyChanged(); } }

        private string _g_str_quantity1;
        public string g_str_quantity1 { get => _g_str_quantity1; set { _g_str_quantity1 = value; OnPropertyChanged(); } }

        private string _g_str_customerName1;
        public string g_str_customerName1 { get => _g_str_customerName1; set { _g_str_customerName1 = value; OnPropertyChanged(); } }

        private string _g_str_time1;
        public string g_str_time1 { get => _g_str_time1; set { _g_str_time1 = value; OnPropertyChanged(); } }

        private string _g_str_price1;
        public string g_str_price1 { get => _g_str_price1; set { _g_str_price1 = value; OnPropertyChanged(); } }
        #endregion

        #region commands.
        public ICommand g_iCm_LoadedItemsControlCommand { get; set; }

        public ICommand g_iCm_ClickDoneCommand { get; set; }
        public ICommand g_iCm_ClickSkipCommand1 { get; set; }
        public ICommand g_iCm_ClickSoldOutCommand1 { get; set; }

        public ICommand g_iCm_ClickDoneCommand2 { get; set; }
        public ICommand g_iCm_ClickSkipCommand2 { get; set; }
        public ICommand g_iCm_ClickSoldOutCommand2 { get; set; }

        public ICommand g_iCm_ClickDoneAllCommand1 { get; set; }
        public ICommand g_iCm_ClickSkipAllCommand1 { get; set; }
        public ICommand g_iCm_ClickSoldOutAllCommand1 { get; set; }
        #endregion

        public SortFoodViewModel()
        {
            g_iCm_LoadedItemsControlCommand = new RelayCommand<ItemsControl>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            #region Command card 1
            g_iCm_ClickDoneCommand = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickDone(p);
            });

            g_iCm_ClickSkipCommand1 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickSkip1(p);
            });

            g_iCm_ClickSoldOutCommand1 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickSoldOut1(p);
            });
            #endregion

            #region Command Button All
            g_iCm_ClickDoneAllCommand1 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickDoneAll1(p);
            });

            g_iCm_ClickSkipAllCommand1 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickSkipAll1(p);
            });

            g_iCm_ClickSoldOutAllCommand1 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickSoldOutAll1(p);
            });
            #endregion
        }

        private void loaded(ItemsControl p)
        {
            this.loadData();
        }

        private void loadData()
        {
            this.g_list_OrderQueue = new ObservableCollection<ORDERQUEUE>();

            var l_orderInfoList = dataProvider.Instance.DB.ORDERINFOes.Where(order => order.STATUS == "Đang chờ").ToList();

            int l_i_count = l_orderInfoList.Count();

            if (l_i_count > 3)
                l_i_count = 3;
            for (int i = 0; i < l_i_count; i++)
            {
                ORDERQUEUE order_test = new ORDERQUEUE
                {
                    orderID = l_orderInfoList.ElementAt(i).ID
                };

                string t_customerID = l_orderInfoList.ElementAt(i).CUSTOMERID; //Biến tạm lưu customerID

                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(order => order.ORDERID == order_test.orderID);
                var l_food = dataProvider.Instance.DB.FOODs.First(order => order.ID == l_orderDetail.FOODID);
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(order => order.ID == t_customerID);

                order_test.foodName = l_food.FOODNAME.Trim();
                order_test.quantity = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();
                order_test.customerName = l_customer.FULLNAME.Trim();
                order_test.price = l_orderInfoList.ElementAt(0).TOTALMONEY.ToString() + " VND";
                order_test.time = "Thời gian: ";
                order_test.foodType = 1;

                g_list_OrderQueue.Add(order_test);
            }

        }

        #region Click card 1
        private void clickDone(SortFoodView p)
        {
            //Lấy ID của order đang hiển thị trong card 1
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(order => order.ID == g_str_orderID1);
            //Cập nhật trạng thái của order
            l_orderIsTaking.STATUS = "Xong";
            dataProvider.Instance.DB.SaveChanges();

            //Kiểm tra có món mới không
            var l_orderInfoList = dataProvider.Instance.DB.ORDERINFOes.Where(order => order.STATUS == "Đang chờ").ToList();

            if (l_orderInfoList == null)
            {
                MessageBox.Show("Chưa có món mới");
                return;
            }

            //Lấy thông tin của order đang chờ tiếp theo
            var l_orderInfo = dataProvider.Instance.DB.ORDERINFOes.First(order => order.STATUS == "Đang chờ");
            l_orderInfo.STATUS = "Đang lấy";
            dataProvider.Instance.DB.SaveChanges();

            if (l_orderInfo == null)
            {
                MessageBox.Show("Chưa có món mới");
            }
            else
            {
                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(order => order.ORDERID == l_orderInfo.ID);

                var l_food = dataProvider.Instance.DB.FOODs.First(order => order.ID == l_orderDetail.FOODID);

                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(order => order.ID == l_orderInfo.CUSTOMERID);

                g_str_orderID1 = l_orderDetail.ORDERID;

                g_str_foodName1 = l_food.FOODNAME.Trim();

                g_str_quantity1 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();

                g_str_customerName1 = l_customer.FULLNAME.Trim();

                g_str_price1 = l_orderInfo.TOTALMONEY.ToString() + " VND";
            }
        }

        private void clickSkip1(SortFoodView p)
        {
            //Lấy ID của order đang hiển thị trong card 1
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(order => order.ID == g_str_orderID1);
            //Cập nhật trạng thái của order
            l_orderIsTaking.STATUS = "Bỏ qua";
            dataProvider.Instance.DB.SaveChanges();

            //Kiểm tra có món mới không
            var l_orderInfoList = dataProvider.Instance.DB.ORDERINFOes.Where(order => order.STATUS == "Đang chờ").ToList();

            if (l_orderInfoList == null)
            {
                MessageBox.Show("Chưa có món mới");
                return;
            }

            //Lấy thông tin của order đang chờ tiếp theo
            var l_orderInfo = dataProvider.Instance.DB.ORDERINFOes.First(order => order.STATUS == "Đang chờ");
            l_orderInfo.STATUS = "Đang lấy";
            dataProvider.Instance.DB.SaveChanges();

            if (l_orderInfo == null)
            {
                MessageBox.Show("Chưa có món mới");
            }
            else
            {
                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(order => order.ORDERID == l_orderInfo.ID);

                var l_food = dataProvider.Instance.DB.FOODs.First(order => order.ID == l_orderDetail.FOODID);

                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(order => order.ID == l_orderInfo.CUSTOMERID);

                g_str_orderID1 = l_orderDetail.ORDERID;

                g_str_foodName1 = l_food.FOODNAME.Trim();

                g_str_quantity1 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();

                g_str_customerName1 = l_customer.FULLNAME.Trim();

                g_str_price1 = l_orderInfo.TOTALMONEY.ToString() + " VND";
            }
        }

        private void clickSoldOut1(SortFoodView p)
        {
            //Lấy ID của order đang hiển thị trong card 1
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(order => order.ID == g_str_orderID1);
            //Cập nhật trạng thái của order
            l_orderIsTaking.STATUS = "Hết món";
            dataProvider.Instance.DB.SaveChanges();

            //Kiểm tra có món mới không
            var l_orderInfoList = dataProvider.Instance.DB.ORDERINFOes.Where(order => order.STATUS == "Đang chờ").ToList();

            if (l_orderInfoList == null)
            {
                MessageBox.Show("Chưa có món mới");
                return;
            }

            //Lấy thông tin của order đang chờ tiếp theo
            var l_orderInfo = dataProvider.Instance.DB.ORDERINFOes.First(order => order.STATUS == "Đang chờ");
            l_orderInfo.STATUS = "Đang lấy";
            dataProvider.Instance.DB.SaveChanges();

            if (l_orderInfo == null)
            {
                MessageBox.Show("Chưa có món mới");
            }
            else
            {
                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(order => order.ORDERID == l_orderInfo.ID);

                var l_food = dataProvider.Instance.DB.FOODs.First(order => order.ID == l_orderDetail.FOODID);

                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(order => order.ID == l_orderInfo.CUSTOMERID);

                g_str_orderID1 = l_orderDetail.ORDERID;

                g_str_foodName1 = l_food.FOODNAME.Trim();

                g_str_quantity1 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();

                g_str_customerName1 = l_customer.FULLNAME.Trim();

                g_str_price1 = l_orderInfo.TOTALMONEY.ToString() + " VND";
            }
        }
        #endregion

        #region Click Button All 1
        private void clickDoneAll1(SortFoodView p)
        {
            clickDone1(p);
        }

        private void clickSkipAll1(SortFoodView p)
        {
            clickSkip1(p);
        }

        private void clickSoldOutAll1(SortFoodView p)
        {
            clickSoldOut1(p);
        }
        #endregion
    }
}
