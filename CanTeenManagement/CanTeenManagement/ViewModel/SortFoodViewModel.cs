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
        public ICommand g_iCm_ClickSkipCommand { get; set; }
        public ICommand g_iCm_ClickSoldOutCommand { get; set; }

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

            #region Command card
            g_iCm_ClickDoneCommand = new RelayCommand<ORDERQUEUE>((p) => { return true; }, (p) =>
            {
                this.clickDone(p);
            });

            g_iCm_ClickSkipCommand = new RelayCommand<ORDERQUEUE>((p) => { return true; }, (p) =>
            {
                this.clickSkip(p);
            });

            g_iCm_ClickSoldOutCommand = new RelayCommand<ORDERQUEUE>((p) => { return true; }, (p) =>
            {
                this.clickSoldOut(p);
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
            if (g_list_OrderQueue != null)
            {
                g_list_OrderQueue.Clear();
            }

            this.g_list_OrderQueue = new ObservableCollection<ORDERQUEUE>();

            var l_orderInfoList = dataProvider.Instance.DB.ORDERINFOes.Where(order => order.STATUS == "Đang chờ").ToList();

            int l_i_count = l_orderInfoList.Count();

            if (l_i_count > 3)
                l_i_count = 3;
            for (int i = 0; i < l_i_count; i++)
            {
                ORDERQUEUE order = new ORDERQUEUE
                {
                    orderID = l_orderInfoList.ElementAt(i).ID
                };

                string t_customerID = l_orderInfoList.ElementAt(i).CUSTOMERID; //Biến tạm lưu customerID

                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(ord => ord.ORDERID == order.orderID);
                var l_food = dataProvider.Instance.DB.FOODs.First(ord => ord.ID == l_orderDetail.FOODID);
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(ord => ord.ID == t_customerID);

                order.foodName = l_food.FOODNAME.Trim();
                order.quantity = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();
                order.customerName = l_customer.FULLNAME.Trim();
                order.price = l_orderInfoList.ElementAt(0).TOTALMONEY.ToString() + " VND";
                order.time = "Thời gian: ";
                order.foodType = 1;
                order.status = "Đang lấy";

                g_list_OrderQueue.Add(order);

                l_orderInfoList.ElementAt(i).STATUS = order.status;
            }

            //dataProvider.Instance.DB.SaveChanges();

        }

        #region Click card
        private void clickDone(ORDERQUEUE p)
        {
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(ord => ord.ID == p.orderID);
            l_orderIsTaking.STATUS = "Xong";
            dataProvider.Instance.DB.SaveChanges();

            var l_orderInfoList = dataProvider.Instance.DB.ORDERINFOes.Where(ord => ord.STATUS == "Đang chờ").ToList();
            if (l_orderInfoList.Count() == 0)
            {
                MessageBox.Show("Chưa có món mới");
                return;
            }

            var l_orderInfo = dataProvider.Instance.DB.ORDERINFOes.First(ord => ord.STATUS == "Đang chờ");

            l_orderInfo.STATUS = "Đang lấy";
            dataProvider.Instance.DB.SaveChanges();


            var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(ord => ord.ORDERID == l_orderInfo.ID);

            var l_food = dataProvider.Instance.DB.FOODs.First(ord => ord.ID == l_orderDetail.FOODID);

            var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(ord => ord.ID == l_orderInfo.CUSTOMERID);

            int index = g_list_OrderQueue.IndexOf(p); 

            ORDERQUEUE order = new ORDERQUEUE
            {
                orderID = l_orderDetail.ORDERID,

                foodName = l_food.FOODNAME.Trim(),

                quantity = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim(),

                customerName = l_customer.FULLNAME.Trim(),

                price = l_orderInfo.TOTALMONEY.ToString() + " VND",

                time = "Thời gian: ",

                foodType = 1,

                status = "Đang lấy"
            };

            g_list_OrderQueue[index] = order;
        }

        private void clickSkip(ORDERQUEUE p)
        {
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(ord => ord.ID == p.orderID);
            l_orderIsTaking.STATUS = "Hết món";
            dataProvider.Instance.DB.SaveChanges();

            var l_orderInfoList = dataProvider.Instance.DB.ORDERINFOes.Where(ord => ord.STATUS == "Đang chờ").ToList();
            if (l_orderInfoList.Count() == 0)
            {
                MessageBox.Show("Chưa có món mới");
                return;
            }

            var l_orderInfo = dataProvider.Instance.DB.ORDERINFOes.First(ord => ord.STATUS == "Đang chờ");

            l_orderInfo.STATUS = "Đang lấy";
            dataProvider.Instance.DB.SaveChanges();


            var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(ord => ord.ORDERID == l_orderInfo.ID);

            var l_food = dataProvider.Instance.DB.FOODs.First(ord => ord.ID == l_orderDetail.FOODID);

            var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(ord => ord.ID == l_orderInfo.CUSTOMERID);

            int index = g_list_OrderQueue.IndexOf(p);

            ORDERQUEUE order = new ORDERQUEUE
            {
                orderID = l_orderDetail.ORDERID,

                foodName = l_food.FOODNAME.Trim(),

                quantity = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim(),

                customerName = l_customer.FULLNAME.Trim(),

                price = l_orderInfo.TOTALMONEY.ToString() + " VND",

                time = "Thời gian: ",

                foodType = 1,

                status = "Đang lấy"
            };

            g_list_OrderQueue[index] = order;
        }

        private void clickSoldOut(ORDERQUEUE p)
        {
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(ord => ord.ID == p.orderID);
            l_orderIsTaking.STATUS = "Xong";
            dataProvider.Instance.DB.SaveChanges();

            var l_orderInfoList = dataProvider.Instance.DB.ORDERINFOes.Where(ord => ord.STATUS == "Đang chờ").ToList();
            if (l_orderInfoList.Count() == 0)
            {
                MessageBox.Show("Chưa có món mới");
                return;
            }

            var l_orderInfo = dataProvider.Instance.DB.ORDERINFOes.First(ord => ord.STATUS == "Đang chờ");

            l_orderInfo.STATUS = "Đang lấy";
            dataProvider.Instance.DB.SaveChanges();


            var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(ord => ord.ORDERID == l_orderInfo.ID);

            var l_food = dataProvider.Instance.DB.FOODs.First(ord => ord.ID == l_orderDetail.FOODID);

            var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(ord => ord.ID == l_orderInfo.CUSTOMERID);

            int index = g_list_OrderQueue.IndexOf(p);

            ORDERQUEUE order = new ORDERQUEUE
            {
                orderID = l_orderDetail.ORDERID,

                foodName = l_food.FOODNAME.Trim(),

                quantity = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim(),

                customerName = l_customer.FULLNAME.Trim(),

                price = l_orderInfo.TOTALMONEY.ToString() + " VND",

                time = "Thời gian: ",

                foodType = 1,

                status = "Đang lấy"
            };

            g_list_OrderQueue[index] = order;
        }
        #endregion

        #region Click Button All 1
        private void clickDoneAll1(SortFoodView p)
        {
            //clickDone1(p);
        }

        private void clickSkipAll1(SortFoodView p)
        {
            //clickSkip1(p);
        }

        private void clickSoldOutAll1(SortFoodView p)
        {
            //clickSoldOut1(p);
        }
        #endregion
    }
}
