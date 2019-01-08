using CanTeenManagement.CO;
using CanTeenManagement.Model;
using CanTeenManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using TableDependency.SqlClient;

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
            set { _g_i_quantityDone1 = value; OnPropertyChanged(); }
        }

        private int _g_i_quantityDone2;
        public int g_i_quantityDone2
        {
            get => _g_i_quantityDone2;
            set { _g_i_quantityDone2 = value; OnPropertyChanged(); }
        }

        private int _g_i_quantitySkip1;
        public int g_i_quantitySkip1
        {
            get => _g_i_quantitySkip1;
            set { _g_i_quantitySkip1 = value; OnPropertyChanged(); }
        }

        private int _g_i_quantitySkip2;
        public int g_i_quantitySkip2
        {
            get => _g_i_quantitySkip2;
            set { _g_i_quantitySkip2 = value; OnPropertyChanged(); }
        }

        private int _g_i_quantitySoldOut1;
        public int g_i_quantitySoldOut1
        {
            get => _g_i_quantitySoldOut1;
            set { _g_i_quantitySoldOut1 = value; OnPropertyChanged(); }
        }

        private int _g_i_quantitySoldOut2;
        public int g_i_quantitySoldOut2
        {
            get => _g_i_quantitySoldOut2;
            set { _g_i_quantitySoldOut2 = value; OnPropertyChanged(); }
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

        private string _g_str_visibility1;
        public string g_str_visibility1
        {
            get => _g_str_visibility1;
            set
            {
                _g_str_visibility1 = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_visibility2;
        public string g_str_visibility2
        {
            get => _g_str_visibility2;
            set
            {
                _g_str_visibility2 = value;
                OnPropertyChanged();
            }
        }

        DispatcherTimer g_timerRefresh = null;
        int g_i_position;

        #region commands.
        public ICommand g_iCm_LoadedWindowCommand { get; set; }
        public ICommand g_iCm_UnloadedWindowCommand { get; set; }

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

            g_iCm_LoadedWindowCommand = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.loaded();
            });

            g_iCm_UnloadedWindowCommand = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.unloaded();
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
            this.g_i_position = 0;

            this.g_list_OrderComplete = new ObservableCollection<ORDERQUEUE>();

            this.g_str_visibility1 = staticVarClass.visibility_hidden;
            this.g_str_visibility2 = staticVarClass.visibility_hidden;

            //
            this.g_timerRefresh = new DispatcherTimer();
            this.g_timerRefresh.Tick += (s, ev) => loadData();
            this.g_timerRefresh.Interval = new TimeSpan(0, 0, 2);
        }

        public ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }

        #region refresh.
        public void WatchTable()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EPOSEntities"].ConnectionString;
            var tableName = "ORDERDETAIL";
            var tableDependency = new SqlTableDependency<ORDERDETAIL>(connectionString, tableName);

            tableDependency.OnChanged += OnNotificationReceived;
            tableDependency.Start();
        }

        public void StopTable()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EPOSEntities"].ConnectionString;
            var tableName = "ORDERDETAIL";
            var tableDependency = new SqlTableDependency<ORDERDETAIL>(connectionString, tableName);

            tableDependency.Stop();
        }

        private void OnNotificationReceived(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<ORDERDETAIL> e)
        {
            using (var DB = new QLCanTinEntities())
            {
                this.g_list_OrderQueue1 = this.ToObservableCollection<ORDERQUEUE>
               ((from orderInfo in DB.ORDERINFOes
                 join orderDetail in DB.ORDERDETAILs on orderInfo.ID equals orderDetail.ORDERID
                 join customer in DB.CUSTOMERs on orderInfo.CUSTOMERID equals customer.ID
                 join food in DB.FOODs on orderDetail.FOODID equals food.ID
                 where food.FOODTYPE == staticVarClass.foodType_one && orderDetail.STATUS == staticVarClass.status_waiting
                 orderby orderInfo.ID ascending
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
                     STATUS = orderDetail.STATUS.Trim(),
                     COMPLETIONDATE = DateTime.Today
                 }).Take(this.g_i_quantityFoodLoad));

                //Lấy danh sách các món có trạng thái đang chờ
                this.g_list_OrderQueue2 = this.ToObservableCollection<ORDERQUEUE>
                    ((from orderInfo in DB.ORDERINFOes
                      join orderDetail in DB.ORDERDETAILs on orderInfo.ID equals orderDetail.ORDERID
                      join customer in DB.CUSTOMERs on orderInfo.CUSTOMERID equals customer.ID
                      join food in DB.FOODs on orderDetail.FOODID equals food.ID
                      where food.FOODTYPE == staticVarClass.foodType_two && orderDetail.STATUS == staticVarClass.status_waiting
                      orderby orderInfo.ID ascending
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
        }
        #endregion

        private void unloaded()
        {
            // this.StopTable();
            this.g_timerRefresh.Stop();
        }

        #region load.
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
            this.loadData();
            this.authorize();

            g_i_quantityDone1 = countAllOrder(1, 1);
            g_i_quantityDone2 = countAllOrder(1, 2);
            g_i_quantitySkip1 = countAllOrder(2, 1);
            g_i_quantitySkip2 = countAllOrder(2, 2);
            g_i_quantitySoldOut1 = countAllOrder(3, 1);
            g_i_quantitySoldOut2 = countAllOrder(3, 2);

            //this.WatchTable();
            this.g_timerRefresh.Start();
        }

        private void loadData()
        {
            this.loadData1();
            this.checkVisibilityData1();
            this.loadData2();
            this.checkVisibilityData2();
        }

        //Load các món cơm
        private void loadData1()
        {
            if (this.g_list_OrderQueue1 != null)
            {
                this.g_list_OrderQueue1.Clear();
            }

            using (var DB = new QLCanTinEntities())
            {
                //Lấy danh sách các món có trạng thái đang chờ
                this.g_list_OrderQueue1 = this.ToObservableCollection<ORDERQUEUE>
                ((from orderInfo in DB.ORDERINFOes
                  join orderDetail in DB.ORDERDETAILs on orderInfo.ID equals orderDetail.ORDERID
                  join customer in DB.CUSTOMERs on orderInfo.CUSTOMERID equals customer.ID
                  join food in DB.FOODs on orderDetail.FOODID equals food.ID
                  where food.FOODTYPE == staticVarClass.foodType_one && orderDetail.STATUS == staticVarClass.status_waiting
                  orderby orderInfo.ID ascending
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
                      STATUS = orderDetail.STATUS.Trim(),
                      COMPLETIONDATE = DateTime.Today
                  }).Take(this.g_i_quantityFoodLoad));
            }
        }

        private void checkVisibilityData1()
        {
            if (this.g_list_OrderQueue1.Count == 0)
            {
                this.g_str_visibility1 = staticVarClass.visibility_visible;
            }
            else
            {
                this.g_str_visibility1 = staticVarClass.visibility_hidden;
            }
        }

        //Load các món nước
        private void loadData2()
        {
            if (this.g_list_OrderQueue2 != null)
            {
                this.g_list_OrderQueue2.Clear();
            }

            using (var DB = new QLCanTinEntities())
            {
                //Lấy danh sách các món có trạng thái đang chờ
                this.g_list_OrderQueue2 = this.ToObservableCollection<ORDERQUEUE>
                ((from orderInfo in DB.ORDERINFOes
                  join orderDetail in DB.ORDERDETAILs on orderInfo.ID equals orderDetail.ORDERID
                  join customer in DB.CUSTOMERs on orderInfo.CUSTOMERID equals customer.ID
                  join food in DB.FOODs on orderDetail.FOODID equals food.ID
                  where food.FOODTYPE == staticVarClass.foodType_two && orderDetail.STATUS == staticVarClass.status_waiting
                  orderby orderInfo.ID ascending
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
        }

        private void checkVisibilityData2()
        {
            if (this.g_list_OrderQueue2.Count == 0)
            {
                this.g_str_visibility2 = staticVarClass.visibility_visible;
            }
            else
            {
                this.g_str_visibility2 = staticVarClass.visibility_hidden;
            }
        }
        #endregion

        #region Click card
        private void updateStatusFoodDetail(string orderID, string foodID, string status)
        {
            using (var DB = new QLCanTinEntities())
            {
                DB.ORDERDETAILs
               .Where(ord => ord.ORDERID == orderID && ord.FOODID == foodID)
               .ToList().ForEach(ord => ord.STATUS = status);

                DB.ORDERDETAILs
                   .Where(ord => ord.ORDERID == orderID && ord.FOODID == foodID)
                   .ToList().ForEach(ord => ord.COMPLETIONDATE = DateTime.Today);
                //Lưu thay đổi vào cơ sở dữ liệu
                DB.SaveChanges();
            }
        }

        private void payBackCustomer(string customerID, int cash)
        {
            using (var DB = new QLCanTinEntities())
            {
                //Cộng lại tiền của món bỏ qua cho khách 
                DB.CUSTOMERs
           .Where(cus => cus.ID == customerID)
           .ToList().ForEach(cus => cus.CASH += cash);

                //Lưu thay đổi vào cơ sở dữ liệu
                DB.SaveChanges();
            }
        }

        private void subPointCustomer(string customerID, int cash)
        {
            int l_i_addPoint = cash / 10;

            using (var DB = new QLCanTinEntities())
            {
                DB.CUSTOMERs
                .Where(customer => customer.ID == customerID).ToList()
                .ForEach(customer => customer.POINT -= l_i_addPoint);
                DB.SaveChanges();
            }
        }

        private void updateStatusFood(string foodID, string status)
        {
            using (var DB = new QLCanTinEntities())
            {
                DB.FOODs
               .Where(food => food.ID == foodID)
               .ToList().ForEach(food => food.STATUS = status);

                //Lưu thay đổi vào cơ sở dữ liệu
                DB.SaveChanges();
            }
        }

        #region Button done.
        private bool checkClickDone()
        {
            if (this.g_i_position != 3)
                return false;

            return true;
        }

        private void clickDone(ORDERQUEUE p)
        {
            try
            {
                this.updateStatusFoodDetail(p.ORDERID, p.FOODID, staticVarClass.status_done);

                this.addOrder(p.FOODTYPE);

                addOneToAllOrder(1, p.FOODTYPE);

                staticFunctionClass.showStatusView(true, "Hoàn thành món " + p.FOODNAME + " thành công!");
            }
            catch
            {
                staticFunctionClass.showStatusView(false, "Hoàn thành món " + p.FOODNAME + " thất bại!");
            }
        }
        #endregion

        #region Button skip.
        private bool checkClickSkip()
        {
            if (this.g_i_position != 3)
                return false;

            return true;
        }

        private void clickSkip(ORDERQUEUE p)
        {
            try
            {
                this.updateStatusFoodDetail(p.ORDERID, p.FOODID, staticVarClass.status_skip);

                this.addOrder(p.FOODTYPE);

                addOneToAllOrder(2, p.FOODTYPE);

                staticFunctionClass.showStatusView(true, "Bỏ qua món " + p.FOODNAME + " thành công!");
            }
            catch
            {
                staticFunctionClass.showStatusView(false, "Bỏ qua món " + p.FOODNAME + " thất bại!");
            }
        }
        #endregion

        #region Button sold out.
        private bool checkClickSoldOut()
        {
            if (this.g_i_position != 3)
                return false;

            return true;
        }

        private void clickSoldOut(ORDERQUEUE p)
        {
            try
            {
                this.updateStatusFoodDetail(p.ORDERID, p.FOODID, staticVarClass.status_soldOut);

                this.payBackCustomer(p.CUSTOMERID, p.TOTALMONEY);

                this.subPointCustomer(p.CUSTOMERID, p.TOTALMONEY);

                this.updateStatusFood(p.FOODID, staticVarClass.status_soldOut);

                this.addOrder(p.FOODTYPE);

                addOneToAllOrder(3, p.FOODTYPE);

                staticFunctionClass.showStatusView(true, "Báo hết món " + p.FOODNAME + "thành công!");
            }
            catch
            {
                staticFunctionClass.showStatusView(false, "Báo hết món " + p.FOODNAME + " thất bại!");
            }
        }
        #endregion
        #endregion

        private void addOrder(int foodType)
        {
            if (foodType == 1)
            {
                this.loadData1();
                this.checkVisibilityData1();
            }
            else if (foodType == 2)
            {
                this.loadData2();
                this.checkVisibilityData2();
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

            string l_status = string.Empty;

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

            using (var DB = new QLCanTinEntities())
            {
                this.g_list_OrderComplete = this.ToObservableCollection<ORDERQUEUE>
                ((from orderInfo in DB.ORDERINFOes
                  join orderDetail in DB.ORDERDETAILs on orderInfo.ID equals orderDetail.ORDERID
                  join customer in DB.CUSTOMERs on orderInfo.CUSTOMERID equals customer.ID
                  join food in DB.FOODs on orderDetail.FOODID equals food.ID
                  where food.FOODTYPE == foodType && orderDetail.STATUS == l_status && orderDetail.COMPLETIONDATE == DateTime.Today
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
                      STATUS = orderDetail.STATUS.Trim(),
                      COMPLETIONDATE = orderDetail.COMPLETIONDATE
                  }));
            }

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
            string l_status = staticVarClass.status_done;
            int l_count = 0;

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

            using (var DB = new QLCanTinEntities())
            {
                l_count = DB.ORDERDETAILs
                    .Where(ord => ord.STATUS == l_status && ord.FOOD.FOODTYPE == foodType
                    && ord.COMPLETIONDATE == DateTime.Today).Count();
            }
            return l_count;
        }

        private void addOneToAllOrder(int buttonID, int foodType)
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