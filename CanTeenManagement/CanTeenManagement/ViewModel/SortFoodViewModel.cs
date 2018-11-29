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
        private ObservableCollection<ORDERQUEUE> _g_list_OrderQueue1;
        public ObservableCollection<ORDERQUEUE> g_list_OrderQueue1
        {
            get => _g_list_OrderQueue1;
            set { _g_list_OrderQueue1 = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ORDERQUEUE> _g_list_OrderQueue2;
        public ObservableCollection<ORDERQUEUE> g_list_OrderQueue2
        {
            get => _g_list_OrderQueue2;
            set { _g_list_OrderQueue2 = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ORDERQUEUE> _g_list_OrderComplete;
        public ObservableCollection<ORDERQUEUE> g_list_OrderComplete
        {
            get => _g_list_OrderComplete;
            set { _g_list_OrderComplete = value; OnPropertyChanged(); }
        }

        #region commands.
        public ICommand g_iCm_LoadedItemsControlCommand { get; set; }

        public ICommand g_iCm_ClickDoneCommand { get; set; }
        public ICommand g_iCm_ClickSkipCommand { get; set; }
        public ICommand g_iCm_ClickSoldOutCommand { get; set; }

        public ICommand g_iCm_ClickDoneAllCommand1 { get; set; }
        public ICommand g_iCm_ClickSkipAllCommand1 { get; set; }
        public ICommand g_iCm_ClickSoldOutAllCommand1 { get; set; }

        public ICommand g_iCm_ClickDoneAllCommand2 { get; set; }
        public ICommand g_iCm_ClickSkipAllCommand2 { get; set; }
        public ICommand g_iCm_ClickSoldOutAllCommand2 { get; set; }
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

            g_iCm_ClickDoneAllCommand2 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickDoneAll2(p);
            });

            g_iCm_ClickSkipAllCommand2 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickSkipAll2(p);
            });

            g_iCm_ClickSoldOutAllCommand2 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickSoldOutAll2(p);
            });
            #endregion
        }

        private void loaded(ItemsControl p)
        {
            this.loadData1();
            this.loadData2();
        }

        //Load các món cơm
        private void loadData1()
        {
            if (g_list_OrderQueue1 != null)
            {
                g_list_OrderQueue1.Clear();
            }

            this.g_list_OrderQueue1 = new ObservableCollection<ORDERQUEUE>();

            //Lấy danh sách các món có trạng thái đang chờ
            var l_orderQueue = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Đang chờ" && ord.FOOD.FOODTYPE == 1).ToList();

            int l_i_count = l_orderQueue.Count(); //lấy số lượng các món trong danh sách các món đang chờ

            if (l_i_count > 3)
            {
                l_i_count = 3;
            }
            for (int i = 0; i < l_i_count; i++)
            {
                ORDERQUEUE order = new ORDERQUEUE
                {
                    ORDERID = l_orderQueue.ElementAt(i).ORDERID, //lấy mã ORDER
                    FOODNAME = l_orderQueue.ElementAt(i).FOOD.FOODNAME.Trim(),
                    QUANTITY = "Số lượng: " + l_orderQueue.ElementAt(i).QUANTITY.ToString().Trim(),
                    CUSTOMERNAME = l_orderQueue.ElementAt(i).ORDERINFO.CUSTOMER.FULLNAME.Trim(),
                    CUSTOMERID = l_orderQueue.ElementAt(i).ORDERINFO.CUSTOMERID.Trim(),
                    PRICE = string.Format(("{0:0,0}đ"), l_orderQueue.ElementAt(i).TOTALMONEY.GetValueOrDefault()),
                    TIME = "Thời gian: ",
                    FOODTYPE = l_orderQueue.ElementAt(i).FOOD.FOODTYPE.GetValueOrDefault(),
                    STATUS = "Đang lấy"
                };

                //Thêm biến order vào danh sách g_list_OrderQueue1 để hiển thị lên màn hình
                g_list_OrderQueue1.Add(order);
                //Cập nhật thuộc tính STATUS trong bảng ORDERDETAIL thành "Đang lấy"
                l_orderQueue.ElementAt(i).STATUS = order.STATUS;
            }
            dataProvider.Instance.DB.SaveChanges(); //Lưu thay đổi vào cơ sở dữ liệu
        }

        //Load các món nước
        private void loadData2()
        {
            if (g_list_OrderQueue2 != null)
            {
                g_list_OrderQueue2.Clear();
            }

            this.g_list_OrderQueue2 = new ObservableCollection<ORDERQUEUE>();

            //Lấy danh sách các món có trạng thái đang chờ
            var l_orderQueue = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Đang chờ" && ord.FOOD.FOODTYPE == 2).ToList();

            int l_i_count = l_orderQueue.Count(); //lấy số lượng các món trong danh sách các món đang chờ

            if (l_i_count > 3)
            {
                l_i_count = 3;
            }
            for (int i = 0; i < l_i_count; i++)
            {
                ORDERQUEUE order = new ORDERQUEUE
                {
                    ORDERID = l_orderQueue.ElementAt(i).ORDERID, //lấy mã ORDER
                    FOODNAME = l_orderQueue.ElementAt(i).FOOD.FOODNAME.Trim(),
                    QUANTITY = "Số lượng: " + l_orderQueue.ElementAt(i).QUANTITY.ToString().Trim(),
                    CUSTOMERNAME = l_orderQueue.ElementAt(i).ORDERINFO.CUSTOMER.FULLNAME.Trim(),
                    CUSTOMERID = l_orderQueue.ElementAt(i).ORDERINFO.CUSTOMERID.Trim(),
                    PRICE = string.Format(("{0:0,0}đ"), l_orderQueue.ElementAt(i).TOTALMONEY.GetValueOrDefault()),
                    TIME = "Thời gian: ",
                    FOODTYPE = l_orderQueue.ElementAt(i).FOOD.FOODTYPE.GetValueOrDefault(),
                    STATUS = "Đang lấy"
                };

                //Thêm biến order vào danh sách g_list_OrderQueue1 để hiển thị lên màn hình
                g_list_OrderQueue2.Add(order);
                //Cập nhật thuộc tính STATUS trong bảng ORDERDETAIL thành "Đang lấy"
                l_orderQueue.ElementAt(i).STATUS = order.STATUS;
            }
            dataProvider.Instance.DB.SaveChanges(); //Lưu thay đổi vào cơ sở dữ liệu
        }


        #region Click card
        private void clickDone(ORDERQUEUE p)
        {
            //Cập nhật lại trạng thái món đang hiển thị
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERDETAILs.First(ord => ord.ORDERID == p.ORDERID);
            l_orderIsTaking.STATUS = "Xong";
            //Lưu thay đổi vào cơ sở dữ liệu
            dataProvider.Instance.DB.SaveChanges();

            if (p.FOODTYPE == 1)
            {
                int index = g_list_OrderQueue1.IndexOf(p);
                g_list_OrderQueue1.RemoveAt(index);
            }

            if (p.FOODTYPE == 2)
            {
                int index = g_list_OrderQueue2.IndexOf(p);
                g_list_OrderQueue2.RemoveAt(index);
            }

            if (checkNewOrder(p.FOODTYPE) == false)
            {
                return;
            }

            addOrder(p.FOODTYPE);
        }

        private void clickSkip(ORDERQUEUE p)
        {
            //Cập nhật lại trạng thái món đang hiển thị
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERDETAILs.First(ord => ord.ORDERID == p.ORDERID);
            l_orderIsTaking.STATUS = "Bỏ qua";
            dataProvider.Instance.DB.SaveChanges();
            //Cộng lại tiền của món bỏ qua cho khách 
            var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(cus => cus.ID == p.CUSTOMERID);
            l_customer.CASH = l_customer.CASH + l_orderIsTaking.TOTALMONEY;

            //Lưu thay đổi vào cơ sở dữ liệu
            dataProvider.Instance.DB.SaveChanges();

            if (p.FOODTYPE == 1)
            {
                int index = g_list_OrderQueue1.IndexOf(p);
                g_list_OrderQueue1.RemoveAt(index);
            }

            if (p.FOODTYPE == 2)
            {
                int index = g_list_OrderQueue2.IndexOf(p);
                g_list_OrderQueue2.RemoveAt(index);
            }

            if (checkNewOrder(p.FOODTYPE) == false)
            {
                return;
            }

            addOrder(p.FOODTYPE);
        }

        private void clickSoldOut(ORDERQUEUE p)
        {
            //Cập nhật lại trạng thái món đang hiển thị
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERDETAILs.First(ord => ord.ORDERID == p.ORDERID);
            l_orderIsTaking.STATUS = "Hết món";

            //Cộng lại tiền của món bỏ qua cho khách 
            var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(cus => cus.ID == p.CUSTOMERID);
            l_customer.CASH = l_customer.CASH + l_orderIsTaking.TOTALMONEY;

            //Cập nhật lại trạng thái món ăn trong database
            var l_food = dataProvider.Instance.DB.FOODs.First(f => f.ID == l_orderIsTaking.FOODID);
            l_food.STATUS = "Hết";

            //Lưu thay đổi vào cơ sở dữ liệu
            dataProvider.Instance.DB.SaveChanges();

            if (p.FOODTYPE == 1)
            {
                int index = g_list_OrderQueue1.IndexOf(p);
                g_list_OrderQueue1.RemoveAt(index);
            }

            if (p.FOODTYPE == 2)
            {
                int index = g_list_OrderQueue2.IndexOf(p);
                g_list_OrderQueue2.RemoveAt(index);
            }

            if (checkNewOrder(p.FOODTYPE) == false)
            {
                return;
            }

            addOrder(p.FOODTYPE);
        }
        #endregion

        private void addOrder(int foodType)
        {
            //Lấy thông tin order tiếp theo
            var l_orderNext = dataProvider.Instance.DB.ORDERDETAILs.First(ord => ord.STATUS == "Đang chờ" && ord.FOOD.FOODTYPE == 1);

            if (foodType == 2)
            {
                l_orderNext = dataProvider.Instance.DB.ORDERDETAILs.First(ord => ord.STATUS == "Đang chờ" && ord.FOOD.FOODTYPE == 2);
            }

            ORDERQUEUE order = new ORDERQUEUE
            {
                ORDERID = l_orderNext.ORDERID,
                FOODNAME = l_orderNext.FOOD.FOODNAME.Trim(),
                QUANTITY = "Số lượng: " + l_orderNext.QUANTITY.ToString().Trim(),
                CUSTOMERNAME = l_orderNext.ORDERINFO.CUSTOMER.FULLNAME.Trim(),
                CUSTOMERID = l_orderNext.ORDERINFO.CUSTOMERID.Trim(),
                PRICE = string.Format(l_orderNext.TOTALMONEY.ToString(), "{0:#,#}"),
                TIME = "Thời gian: ",
                FOODTYPE = l_orderNext.FOOD.FOODTYPE.GetValueOrDefault(),
                STATUS = "Đang lấy"
            };

            if (foodType == 1)
            {
                g_list_OrderQueue1.Add(order);
            }

            if (foodType == 2)
            {
                g_list_OrderQueue2.Add(order);
            }

            l_orderNext.STATUS = order.STATUS;
            dataProvider.Instance.DB.SaveChanges();
        }

        private bool checkNewOrder(int foodType)
        {
            var l_orderQueue = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Đang chờ" && ord.FOOD.FOODTYPE == 1).ToList();
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Đang lấy" && ord.FOOD.FOODTYPE == 1).ToList();

            if (foodType == 2)
            {
                l_orderQueue = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Đang chờ" && ord.FOOD.FOODTYPE == 2).ToList();
                l_orderIsTaking = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Đang lấy" && ord.FOOD.FOODTYPE == 2).ToList();
            }

            if (l_orderQueue.Count() == 0)
            {
                if (l_orderIsTaking.Count() == 0)
                {
                    MessageBox.Show("Chưa có món mới");
                }
                return false;
            }

            return true;
        }

        #region Click Button All
        private void clickDoneAll1(SortFoodView p)
        {
            var l_orderCompleteList = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Xong" && ord.FOOD.FOODTYPE == 1).OrderByDescending(ord => ord.ORDERID).ToList();
            
            int l_i_count = l_orderCompleteList.Count();

            if (l_i_count == 0)
            {
                MessageBox.Show("Chưa có món cơm nào xong");
                return;
            }

            loadAllOrder(l_orderCompleteList, l_i_count);
        }

        private void clickSkipAll1(SortFoodView p)
        {
            var l_orderCompleteList = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Bỏ qua" && ord.FOOD.FOODTYPE == 1).OrderByDescending(ord => ord.ORDERID).ToList();

            int l_i_count = l_orderCompleteList.Count();

            if (l_i_count == 0)
            {
                MessageBox.Show("Không có món cơm nào bị bỏ qua");
                return;
            }

            loadAllOrder(l_orderCompleteList, l_i_count);
        }

        private void clickSoldOutAll1(SortFoodView p)
        {
            var l_orderCompleteList = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Hết món" && ord.FOOD.FOODTYPE == 1).OrderByDescending(ord => ord.ORDERID).ToList();

            int l_i_count = l_orderCompleteList.Count();

            if (l_i_count == 0)
            {
                MessageBox.Show("Không có món cơm nào hết món");
                return;
            }

            loadAllOrder(l_orderCompleteList, l_i_count);
        }

        private void clickDoneAll2(SortFoodView p)
        {
            var l_orderCompleteList = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Xong" && ord.FOOD.FOODTYPE == 2).OrderByDescending(ord => ord.ORDERID).ToList();

            int l_i_count = l_orderCompleteList.Count();

            if (l_i_count == 0)
            {
                MessageBox.Show("Chưa có món nước nào xong");
                return;
            }

            loadAllOrder(l_orderCompleteList, l_i_count);
        }

        private void clickSkipAll2(SortFoodView p)
        {
            var l_orderCompleteList = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Bỏ qua" && ord.FOOD.FOODTYPE == 2).OrderByDescending(ord => ord.ORDERID).ToList();

            int l_i_count = l_orderCompleteList.Count();

            if (l_i_count == 0)
            {
                MessageBox.Show("Không có món nước nào bị bỏ qua");
                return;
            }

            loadAllOrder(l_orderCompleteList, l_i_count);
        }

        private void clickSoldOutAll2(SortFoodView p)
        {
            var l_orderCompleteList = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == "Hết món" && ord.FOOD.FOODTYPE == 2).OrderByDescending(ord => ord.ORDERID).ToList();

            int l_i_count = l_orderCompleteList.Count();

            if (l_i_count == 0)
            {
                MessageBox.Show("Không có món nước nào hết món");
                return;
            }

            loadAllOrder(l_orderCompleteList, l_i_count);
        }
        #endregion

        private void loadAllOrder(List<ORDERDETAIL> list, int count)
        {
            if (g_list_OrderComplete != null)
            {
                g_list_OrderComplete.Clear();
            }
            this.g_list_OrderComplete = new ObservableCollection<ORDERQUEUE>();

            for (int i = 0; i < count; i++)
            {
                ORDERQUEUE order = new ORDERQUEUE
                {
                    ORDERID = list.ElementAt(i).ORDERID, //lấy mã ORDER
                    FOODNAME = list.ElementAt(i).FOOD.FOODNAME.Trim(),
                    QUANTITY = "Số lượng: " + list.ElementAt(i).QUANTITY.ToString().Trim(),
                    CUSTOMERNAME = list.ElementAt(i).ORDERINFO.CUSTOMER.FULLNAME.Trim(),
                    CUSTOMERID = list.ElementAt(i).ORDERINFO.CUSTOMERID.Trim(),
                    PRICE = string.Format(("{0:0,0}đ"), list.ElementAt(i).TOTALMONEY.GetValueOrDefault()),
                    TIME = "",
                    FOODTYPE = list.ElementAt(i).FOOD.FOODTYPE.GetValueOrDefault(),
                    STATUS = list.ElementAt(i).STATUS
                };
                g_list_OrderComplete.Add(order);
            }

            OrderDoneView orderDone = new OrderDoneView();
            orderDone.ShowDialog();
        }
    }
}