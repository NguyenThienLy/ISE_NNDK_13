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
using CanTeenManagement.CO;

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

        private int _g_i_quantityDone1;
        public int g_i_quantityDone1
        {
            get => _g_i_quantityDone1;
            set { _g_i_quantityDone1 = value; OnPropertyChanged();}
        }

        private int _g_i_quantityDone2;
        public int g_i_quantityDone2
        {
            get => _g_i_quantityDone2;
            set { _g_i_quantityDone2 = value; OnPropertyChanged();}
        }

        private int _g_i_quantitySkip1;
        public int g_i_quantitySkip1
        {
            get => _g_i_quantitySkip1;
            set { _g_i_quantitySkip1 = value; OnPropertyChanged();}
        }

        private int _g_i_quantitySkip2;
        public int g_i_quantitySkip2
        {
            get => _g_i_quantitySkip2;
            set { _g_i_quantitySkip2 = value; OnPropertyChanged();}
        }

        private int _g_i_quantitySoldOut1;
        public int g_i_quantitySoldOut1
        {
            get => _g_i_quantitySoldOut1;
            set { _g_i_quantitySoldOut1 = value; OnPropertyChanged();}
        }

        private int _g_i_quantitySoldOut2;
        public int g_i_quantitySoldOut2
        {
            get => _g_i_quantitySoldOut2;
            set { _g_i_quantitySoldOut2 = value; OnPropertyChanged();}
        }
        // Curr page.
        private int _g_i_quantityFoodLoad;
        public int g_i_quantityFoodLoad
        {
            get => _g_i_quantityFoodLoad;
            set
            {
                _g_i_quantityFoodLoad = value;
            }
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
            this.initSupport();

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

        private void initSupport()
        {
            this.g_i_quantityFoodLoad = 3;

            this.g_list_OrderComplete = new ObservableCollection<ORDERQUEUE>();
        }

        private void loaded(ItemsControl p)
        {
            this.loadData1();
            this.loadData2();

            g_i_quantityDone1 = countAllOrder(1, 1);
            g_i_quantityDone2 = countAllOrder(1, 2);
            g_i_quantitySkip1 = countAllOrder(2, 1);
            g_i_quantitySkip2 = countAllOrder(2, 2);
            g_i_quantitySoldOut1 = countAllOrder(3, 1);
            g_i_quantitySoldOut2 = countAllOrder(3, 2);
        }

        public ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }

        //Load các món cơm
        private void loadData1()
        {
            if (this.g_list_OrderQueue1 != null)
            {
                this.g_list_OrderQueue1.Clear();
            }

            //Lấy danh sách các món có trạng thái đang chờ
            this.g_list_OrderQueue1 = this.ToObservableCollection<ORDERQUEUE>
                ((from orderInfo in dataProvider.Instance.DB.ORDERINFOes
                  join orderDetail in dataProvider.Instance.DB.ORDERDETAILs on orderInfo.ID equals orderDetail.ORDERID
                  join customer in dataProvider.Instance.DB.CUSTOMERs on orderInfo.CUSTOMERID equals customer.ID
                  join food in dataProvider.Instance.DB.FOODs on orderDetail.FOODID equals food.ID
                  where food.FOODTYPE == staticVarClass.foodType_one && orderDetail.STATUS == staticVarClass.status_waiting
                  orderby orderInfo.ORDERDATE ascending
                  select new ORDERQUEUE
                  {
                      ORDERID = orderDetail.ORDERID.Trim(),
                      FOODID = food.ID.Trim(),
                      FOODNAME = food.FOODNAME.Trim(),
                      FOODTYPE = (int)food.FOODTYPE,
                      QUANTITY = (int)orderDetail.QUANTITY,
                      TOTALMONEY = (int)orderDetail.TOTALMONEY,
                      CUSTOMERID = customer.ID.Trim(),
                      CUSTOMERNAME = customer.FULLNAME.Trim(),
                      ORDERDATE = orderInfo.ORDERDATE.ToString(),
                      STATUS = orderDetail.STATUS.Trim()
                  }).Take(this.g_i_quantityFoodLoad));
        }

        //Load các món nước
        private void loadData2()
        {
            if (this.g_list_OrderQueue2 != null)
            {
                this.g_list_OrderQueue2.Clear();
            }

            //Lấy danh sách các món có trạng thái đang chờ
            this.g_list_OrderQueue2 = this.ToObservableCollection<ORDERQUEUE>
                ((from orderInfo in dataProvider.Instance.DB.ORDERINFOes
                  join orderDetail in dataProvider.Instance.DB.ORDERDETAILs on orderInfo.ID equals orderDetail.ORDERID
                  join customer in dataProvider.Instance.DB.CUSTOMERs on orderInfo.CUSTOMERID equals customer.ID
                  join food in dataProvider.Instance.DB.FOODs on orderDetail.FOODID equals food.ID
                  where food.FOODTYPE == staticVarClass.foodType_two && orderDetail.STATUS == staticVarClass.status_waiting
                  orderby orderInfo.ORDERDATE ascending
                  select new ORDERQUEUE
                  {
                      ORDERID = orderDetail.ORDERID.Trim(),
                      FOODID = food.ID.Trim(),
                      FOODNAME = food.FOODNAME.Trim(),
                      FOODTYPE = (int)food.FOODTYPE,
                      QUANTITY = (int)orderDetail.QUANTITY,
                      TOTALMONEY = (int)orderDetail.TOTALMONEY,
                      CUSTOMERID = customer.ID.Trim(),
                      CUSTOMERNAME = customer.FULLNAME.Trim(),
                      ORDERDATE = orderInfo.ORDERDATE.ToString(),
                      STATUS = orderDetail.STATUS.Trim()
                  }).Take(this.g_i_quantityFoodLoad));
        }

        #region Click card
        private void updateStatusFoodDetail(string orderID, string foodID, string status)
        {
            dataProvider.Instance.DB.ORDERDETAILs
               .Where(ord => ord.ORDERID == orderID && ord.FOODID == foodID)
               .ToList().ForEach(ord => ord.STATUS = status);

            //Lưu thay đổi vào cơ sở dữ liệu
            dataProvider.Instance.DB.SaveChanges();
        }

        private void payBackCustomer(string customerID, int cash)
        {
            //Cộng lại tiền của món bỏ qua cho khách 
            dataProvider.Instance.DB.CUSTOMERs
           .Where(cus => cus.ID == customerID)
           .ToList().ForEach(cus => cus.CASH += cash);

            //Lưu thay đổi vào cơ sở dữ liệu
            dataProvider.Instance.DB.SaveChanges();
        }

        private void subPointCustomer(string customerID, int cash)
        {
            int l_i_addPoint = cash / 10;


            dataProvider.Instance.DB.CUSTOMERs
                .Where(customer => customer.ID == customerID).ToList()
                .ForEach(customer => customer.POINT -= l_i_addPoint);
            dataProvider.Instance.DB.SaveChanges();
        }

        private void updateStatusFood(string foodID, string status)
        {
            dataProvider.Instance.DB.FOODs
            .Where(food => food.ID == foodID)
            .ToList().ForEach(food => food.STATUS = status);

            //Lưu thay đổi vào cơ sở dữ liệu
            dataProvider.Instance.DB.SaveChanges();
        }

        private void clickDone(ORDERQUEUE p)
        {
            this.updateStatusFoodDetail(p.ORDERID, p.FOODID, staticVarClass.status_done);

            this.addOrder(p.FOODTYPE);

            addOneToAllOrder(1, p.FOODTYPE);

            staticFunctionClass.showStatusView(true, "Hoàn thành món " + p.FOODNAME);
        }

        private void clickSkip(ORDERQUEUE p)
        {
            this.updateStatusFoodDetail(p.ORDERID, p.FOODID, staticVarClass.status_skip);

            this.addOrder(p.FOODTYPE);

            addOneToAllOrder(2, p.FOODTYPE);

            staticFunctionClass.showStatusView(true, "Bỏ qua món " + p.FOODNAME);
        }

        private void clickSoldOut(ORDERQUEUE p)
        {
            this.updateStatusFoodDetail(p.ORDERID, p.FOODID, staticVarClass.status_soldOut);

            this.payBackCustomer(p.CUSTOMERID, p.TOTALMONEY);

            this.subPointCustomer(p.CUSTOMERID, p.TOTALMONEY);

            this.updateStatusFood(p.FOODID, staticVarClass.status_soldOut);

            this.addOrder(p.FOODTYPE);

            addOneToAllOrder(3, p.FOODTYPE);

            staticFunctionClass.showStatusView(true, "Hết món " + p.FOODNAME);
        }
        #endregion

        private void addOrder(int foodType)
        {
            if (foodType == 1)
            {
                this.loadData1();
            }
            else if (foodType == 2)
            {
                this.loadData2();
            }
        }

        #region Click Button All
        private void clickDoneAll1(SortFoodView p)
        {
            if (g_i_quantityDone1 == 0)
            {
                staticFunctionClass.showStatusView(true, "Chưa có đơn hàng món cơm nào xong trong ngày hôm nay");
                return;
            }

            loadAllOrder(p, 1, 1);
        }

        private void clickSkipAll1(SortFoodView p)
        {
            if (g_i_quantitySkip1 == 0)
            {
                staticFunctionClass.showStatusView(true, "Chưa có đơn hàng món cơm nào bị bỏ qua trong ngày hôm nay");
                return;
            }

            loadAllOrder(p, 2, 1);
        }

        private void clickSoldOutAll1(SortFoodView p)
        {
            if (g_i_quantitySoldOut1 == 0)
            {
                staticFunctionClass.showStatusView(true, "Chưa có đơn hàng món cơm nào hết món trong ngày hôm nay");
                return;
            }

            loadAllOrder(p, 3, 1);
        }

        private void clickDoneAll2(SortFoodView p)
        {
            if (g_i_quantityDone2 == 0)
            {
                staticFunctionClass.showStatusView(true, "Chưa có đơn hàng món nước nào xong trong ngày hôm nay");
                return;
            }

            loadAllOrder(p, 1, 2);
        }

        private void clickSkipAll2(SortFoodView p)
        {
            if (g_i_quantitySkip2 == 0)
            {
                staticFunctionClass.showStatusView(true, "Chưa có đơn hàng món nước nào bị bỏ qua trong ngày hôm nay");
                return;
            }

            loadAllOrder(p, 2, 2);
        }

        private void clickSoldOutAll2(SortFoodView p)
        {
            if (g_i_quantitySoldOut2 == 0)
            {
                staticFunctionClass.showStatusView(true, "Chưa có đơn hàng món nước nào hết món trong ngày hôm nay");
                return;
            }

            loadAllOrder(p, 3, 2);
        }
        #endregion

        private void loadAllOrder(SortFoodView p, int buttonID, int foodType)
        {
            if (g_list_OrderComplete != null)
            {
                g_list_OrderComplete.Clear();
            }
            this.g_list_OrderComplete = new ObservableCollection<ORDERQUEUE>();

            string l_status = "Xong";
            switch(buttonID)
            {
                case 1:
                    l_status = staticVarClass.status_done;
                    break;
                case 2:
                    l_status = staticVarClass.status_skip;
                    break;
                case 3:
                    l_status = staticVarClass.status_soldOut;
                    break;
            }

            this.g_list_OrderComplete = this.ToObservableCollection<ORDERQUEUE>
                ((from orderInfo in dataProvider.Instance.DB.ORDERINFOes
                  join orderDetail in dataProvider.Instance.DB.ORDERDETAILs on orderInfo.ID equals orderDetail.ORDERID
                  join customer in dataProvider.Instance.DB.CUSTOMERs on orderInfo.CUSTOMERID equals customer.ID
                  join food in dataProvider.Instance.DB.FOODs on orderDetail.FOODID equals food.ID
                  where food.FOODTYPE == foodType && orderDetail.STATUS == l_status && orderInfo.ORDERDATE == DateTime.Today
                  orderby orderInfo.ORDERDATE ascending
                  select new ORDERQUEUE
                  {
                      ORDERID = orderDetail.ORDERID.Trim(),
                      FOODID = food.ID.Trim(),
                      FOODNAME = food.FOODNAME.Trim(),
                      FOODTYPE = (int)food.FOODTYPE,
                      QUANTITY = (int)orderDetail.QUANTITY,
                      TOTALMONEY = (int)orderDetail.TOTALMONEY,
                      CUSTOMERID = customer.ID.Trim(),
                      CUSTOMERNAME = customer.FULLNAME.Trim(),
                      ORDERDATE = orderInfo.ORDERDATE.ToString(),
                      STATUS = orderDetail.STATUS.Trim()
                  }));

            MainWindow mainWd = MainWindow.Instance;

            mainWd.Opacity = 0.5;
            p.Opacity = 0.5;

            OrderDoneView orderDone = new OrderDoneView();
            orderDone.ShowDialog();

            mainWd.Opacity = 100;
            p.Opacity = 100;
        }

        private int countAllOrder(int buttonID, int foodType)
        {
            string l_status = "Xong";
            switch (buttonID)
            {
                case 1:
                    l_status = staticVarClass.status_done;
                    break;
                case 2:
                    l_status = staticVarClass.status_skip;
                    break;
                case 3:
                    l_status = staticVarClass.status_soldOut;
                    break;
            }

            int count = dataProvider.Instance.DB.ORDERDETAILs.Where(ord => ord.STATUS == l_status && ord.FOOD.FOODTYPE == foodType && ord.ORDERINFO.ORDERDATE == DateTime.Today).Count();
            return count;
        }

        private void addOneToAllOrder (int buttonID, int foodType)
        {
            switch (buttonID)
            {
                case 1:
                    if (foodType == 1)
                        g_i_quantityDone1++;
                    else
                        g_i_quantityDone2++;
                    break;
                case 2:
                    if (foodType == 1)
                        g_i_quantitySkip1++;
                    else
                        g_i_quantitySkip2++;
                    break;
                case 3:
                    if (foodType == 1)
                        g_i_quantitySoldOut1++;
                    else
                        g_i_quantitySoldOut2++;
                    break;
            }
        }
    }
}