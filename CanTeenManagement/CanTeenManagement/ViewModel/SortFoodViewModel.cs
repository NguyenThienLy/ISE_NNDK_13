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

            //Lấy danh sách các món có trạng thái đang chờ
            var l_orderQueue = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Đang chờ").ToList();

            int l_i_count = l_orderQueue.Count(); //lấy số lượng các món trong danh sách các món đang chờ

            if (l_i_count > 3) //nếu danh sách nhiều hơn 3 phần tử thì đưa biến số lượng món về 3 để ItemControl chỉ hiển thị tối đa 3 món
                l_i_count = 3;

            for (int i = 0; i < l_i_count; i++)
            {
                ORDERQUEUE order = new ORDERQUEUE
                {
                    ORDERID = l_orderQueue.ElementAt(i).ORDERID //lấy mã ORDER
                };

                //Lấy thông tin của order(mã khách, mã nhân viên, thời gian, ...)
                var l_orderInfo = dataProvider.Instance.DB.ORDERINFOes.First(ord => ord.ID == order.ORDERID);

                //Lấy thông tin khách hàng
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(cus => cus.ID == l_orderInfo.CUSTOMERID);

                //Lấy thông tin món ăn
                string t_foodID = l_orderQueue.ElementAt(i).FOODID; //Lấy mã món ăn
                var l_food = dataProvider.Instance.DB.FOODs.First(f => f.ID == t_foodID);

                //Thiết lập giá trị cho biến order
                order.FOODNAME = l_food.FOODNAME.Trim();
                order.QUANTITY = "Số lượng: " + l_orderQueue.ElementAt(i).QUANTITY.ToString().Trim();
                order.CUSTOMERNAME = l_customer.FULLNAME.Trim();
                order.PRICE = l_orderQueue.ElementAt(i).TOTALMONEY.ToString() + " VND";
                order.TIME = "Thời gian: ";
                order.FOODTYPE = 1;
                order.STATUS = "Đang lấy";

                //Thêm biến order vào dnah sách g_list_OrderQueue để hiển thị lên màn hình
                g_list_OrderQueue.Add(order);

                //Cập nhật thuộc tính STATUS trong bảng ORDERDETAIL thành "Đang lấy"
                l_orderQueue.ElementAt(i).STATUS = order.STATUS;
            }

            dataProvider.Instance.DB.SaveChanges(); //Lưu thay đổi vào cơ sở dữ liệu
        }

        #region Click card
        private void clickDone(ORDERQUEUE p)
        {
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(ord => ord.ID == p.ORDERID);
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
            //dataProvider.Instance.DB.SaveChanges();


            var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(ord => ord.ORDERID == l_orderInfo.ID);

            var l_food = dataProvider.Instance.DB.FOODs.First(ord => ord.ID == l_orderDetail.FOODID);

            var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(ord => ord.ID == l_orderInfo.CUSTOMERID);

            int index = g_list_OrderQueue.IndexOf(p); 

            ORDERQUEUE order = new ORDERQUEUE
            {
                ORDERID = l_orderDetail.ORDERID,

                FOODNAME = l_food.FOODNAME.Trim(),

                QUANTITY = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim(),

                CUSTOMERNAME = l_customer.FULLNAME.Trim(),

                PRICE = l_orderInfo.TOTALMONEY.ToString() + " VND",

                TIME = "Thời gian: ",

                FOODTYPE = 1,

                STATUS = "Đang lấy"
            };

            g_list_OrderQueue[index] = order;
        }

        private void clickSkip(ORDERQUEUE p)
        {
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(ord => ord.ID == p.ORDERID);
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
                ORDERID = l_orderDetail.ORDERID,

                FOODNAME = l_food.FOODNAME.Trim(),

                QUANTITY = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim(),

                CUSTOMERNAME = l_customer.FULLNAME.Trim(),

                PRICE = l_orderInfo.TOTALMONEY.ToString() + " VND",

                TIME = "Thời gian: ",

                FOODTYPE = 1,

                STATUS = "Đang lấy"
            };

            g_list_OrderQueue[index] = order;
        }

        private void clickSoldOut(ORDERQUEUE p)
        {
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(ord => ord.ID == p.ORDERID);
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
                ORDERID = l_orderDetail.ORDERID,

                FOODNAME = l_food.FOODNAME.Trim(),

                QUANTITY = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim(),

                CUSTOMERNAME = l_customer.FULLNAME.Trim(),

                PRICE = l_orderInfo.TOTALMONEY.ToString() + " VND",

                TIME = "Thời gian: ",

                FOODTYPE = 1,

                STATUS = "Đang lấy"
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
