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

        #region Thuộc tính card 2
        private string _g_str_orderID2;
        public string g_str_orderID2 { get => _g_str_orderID2; set { _g_str_orderID2 = value; OnPropertyChanged(); } }

        private string _g_str_foodName2;
        public string g_str_foodName2 { get => _g_str_foodName2; set { _g_str_foodName2 = value; OnPropertyChanged(); } }

        private string _g_str_quantity2;
        public string g_str_quantity2 { get => _g_str_quantity2; set { _g_str_quantity2 = value; OnPropertyChanged(); } }

        private string _g_str_customerName2;
        public string g_str_customerName2 { get => _g_str_customerName2; set { _g_str_customerName2 = value; OnPropertyChanged(); } }

        private string _g_str_time2;
        public string g_str_time2 { get => _g_str_time2; set { _g_str_time2 = value; OnPropertyChanged(); } }

        private string _g_str_price2;
        public string g_str_price2 { get => _g_str_price2; set { _g_str_price2 = value; OnPropertyChanged(); } }
        #endregion

        #region Thuộc tính card 3
        private string _g_str_orderID3;
        public string g_str_orderID3 { get => _g_str_orderID3; set { _g_str_orderID3 = value; OnPropertyChanged(); } }

        private string _g_str_foodName3;
        public string g_str_foodName3 { get => _g_str_foodName3; set { _g_str_foodName3 = value; OnPropertyChanged(); } }

        private string _g_str_quantity3;
        public string g_str_quantity3 { get => _g_str_quantity3; set { _g_str_quantity3 = value; OnPropertyChanged(); } }

        private string _g_str_customerName3;
        public string g_str_customerName3 { get => _g_str_customerName3; set { _g_str_customerName3 = value; OnPropertyChanged(); } }

        private string _g_str_time3;
        public string g_str_time3 { get => _g_str_time3; set { _g_str_time3 = value; OnPropertyChanged(); } }

        private string _g_str_price3;
        public string g_str_price3 { get => _g_str_price3; set { _g_str_price3 = value; OnPropertyChanged(); } }
        #endregion

        #region Thuộc tính card 4
        private string _g_str_orderID4;
        public string g_str_orderID4 { get => _g_str_orderID4; set { _g_str_orderID4 = value; OnPropertyChanged(); } }

        private string _g_str_foodName4;
        public string g_str_foodName4 { get => _g_str_foodName4; set { _g_str_foodName4 = value; OnPropertyChanged(); } }

        private string _g_str_quantity4;
        public string g_str_quantity4 { get => _g_str_quantity4; set { _g_str_quantity4 = value; OnPropertyChanged(); } }

        private string _g_str_customerName4;
        public string g_str_customerName4 { get => _g_str_customerName4; set { _g_str_customerName4 = value; OnPropertyChanged(); } }

        private string _g_str_time4;
        public string g_str_time4 { get => _g_str_time4; set { _g_str_time4 = value; OnPropertyChanged(); } }

        private string _g_str_price4;
        public string g_str_price4 { get => _g_str_price4; set { _g_str_price4 = value; OnPropertyChanged(); } }
        #endregion

        #region Thuộc tính card 5
        private string _g_str_orderID5;
        public string g_str_orderID5 { get => _g_str_orderID5; set { _g_str_orderID5 = value; OnPropertyChanged(); } }

        private string _g_str_foodName5;
        public string g_str_foodName5 { get => _g_str_foodName5; set { _g_str_foodName5 = value; OnPropertyChanged(); } }

        private string _g_str_quantity5;
        public string g_str_quantity5 { get => _g_str_quantity5; set { _g_str_quantity5 = value; OnPropertyChanged(); } }

        private string _g_str_customerName5;
        public string g_str_customerName5 { get => _g_str_customerName5; set { _g_str_customerName5 = value; OnPropertyChanged(); } }

        private string _g_str_time5;
        public string g_str_time5 { get => _g_str_time5; set { _g_str_time5 = value; OnPropertyChanged(); } }

        private string _g_str_price5;
        public string g_str_price5 { get => _g_str_price5; set { _g_str_price5 = value; OnPropertyChanged(); } }
        #endregion

        #region Thuộc tính card 6
        private string _g_str_orderID6;
        public string g_str_orderID6 { get => _g_str_orderID6; set { _g_str_orderID6 = value; OnPropertyChanged(); } }

        private string _g_str_foodName6;
        public string g_str_foodName6 { get => _g_str_foodName6; set { _g_str_foodName6 = value; OnPropertyChanged(); } }

        private string _g_str_quantity6;
        public string g_str_quantity6 { get => _g_str_quantity6; set { _g_str_quantity6 = value; OnPropertyChanged(); } }

        private string _g_str_customerName6;
        public string g_str_customerName6 { get => _g_str_customerName6; set { _g_str_customerName6 = value; OnPropertyChanged(); } }

        private string _g_str_time6;
        public string g_str_time6 { get => _g_str_time6; set { _g_str_time6 = value; OnPropertyChanged(); } }

        private string _g_str_price6;
        public string g_str_price6 { get => _g_str_price6; set { _g_str_price6 = value; OnPropertyChanged(); } }
        #endregion

        #region commands.
        public ICommand g_iCm_ClickDoneCommand1 { get; set; }
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
            this.loadData();

            #region Command card 1
            g_iCm_ClickDoneCommand1 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickDone1(p);
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

            #region Command card 2
            g_iCm_ClickDoneCommand2 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickDone2(p);
            });

            g_iCm_ClickSkipCommand2 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickSkip2(p);
            });

            g_iCm_ClickSoldOutCommand2 = new RelayCommand<SortFoodView>((p) => { return true; }, (p) =>
            {
                this.clickSoldOut2(p);
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

        private void loadData()
        {
            var l_orderInfoList = dataProvider.Instance.DB.ORDERINFOes.Where(order => order.STATUS == "Đang chờ").ToList();

            if (l_orderInfoList == null)
            {
                MessageBox.Show("Chưa có món mới");
                return;
            }

            int l_i_count = l_orderInfoList.Count();

            if (l_i_count > 6)
            {
                l_i_count = 6;
            }

            for (int i = 0; i < l_i_count; i++)
            {
                l_orderInfoList.ElementAt(i).STATUS = "Đang lấy";
            }

            dataProvider.Instance.DB.SaveChanges();

            if (l_i_count >= 1)
            {
                g_str_orderID1 = l_orderInfoList.ElementAt(0).ID;
                string t_customerID = l_orderInfoList.ElementAt(0).CUSTOMERID; //Biến tạm lưu customerID

                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(order => order.ORDERID == g_str_orderID1);
                var l_food = dataProvider.Instance.DB.FOODs.First(order => order.ID == l_orderDetail.FOODID);
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(order => order.ID == t_customerID);

                g_str_foodName1 = l_food.FOODNAME.Trim();
                g_str_quantity1 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();
                g_str_customerName1 = l_customer.FULLNAME.Trim();
                g_str_price1 = l_orderInfoList.ElementAt(0).TOTALMONEY.ToString() + " VND";
            }

            if (l_i_count >= 2)
            {
                g_str_orderID2 = l_orderInfoList.ElementAt(1).ID;
                string t_customerID = l_orderInfoList.ElementAt(1).CUSTOMERID; //Biến tạm lưu customerID

                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(order => order.ORDERID == g_str_orderID2);
                var l_food = dataProvider.Instance.DB.FOODs.First(order => order.ID == l_orderDetail.FOODID);
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(order => order.ID == t_customerID);

                g_str_foodName2 = l_food.FOODNAME.Trim();
                g_str_quantity2 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();
                g_str_customerName2 = l_customer.FULLNAME.Trim();
                g_str_price2 = l_orderInfoList.ElementAt(1).TOTALMONEY.ToString() + " VND";
            }

            if (l_i_count >= 3)
            {
                g_str_orderID3 = l_orderInfoList.ElementAt(2).ID;
                string t_customerID = l_orderInfoList.ElementAt(2).CUSTOMERID; //Biến tạm lưu customerID

                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(order => order.ORDERID == g_str_orderID3);
                var l_food = dataProvider.Instance.DB.FOODs.First(order => order.ID == l_orderDetail.FOODID);
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(order => order.ID == t_customerID);

                g_str_foodName3 = l_food.FOODNAME.Trim();
                g_str_quantity3 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();
                g_str_customerName3 = l_customer.FULLNAME.Trim();
                g_str_price3 = l_orderInfoList.ElementAt(2).TOTALMONEY.ToString() + " VND";
            }

            if (l_i_count >= 4)
            {
                g_str_orderID4 = l_orderInfoList.ElementAt(3).ID;
                string t_customerID = l_orderInfoList.ElementAt(3).CUSTOMERID; //Biến tạm lưu customerID

                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(order => order.ORDERID == g_str_orderID4);
                var l_food = dataProvider.Instance.DB.FOODs.First(order => order.ID == l_orderDetail.FOODID);
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(order => order.ID == t_customerID);

                g_str_foodName4 = l_food.FOODNAME.Trim();
                g_str_quantity4 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();
                g_str_customerName4 = l_customer.FULLNAME.Trim();
                g_str_price4 = l_orderInfoList.ElementAt(3).TOTALMONEY.ToString() + " VND";
            }

            if (l_i_count >= 5)
            {
                g_str_orderID5 = l_orderInfoList.ElementAt(4).ID;
                string t_customerID = l_orderInfoList.ElementAt(4).CUSTOMERID; //Biến tạm lưu customerID

                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(order => order.ORDERID == g_str_orderID5);
                var l_food = dataProvider.Instance.DB.FOODs.First(order => order.ID == l_orderDetail.FOODID);
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(order => order.ID == t_customerID);

                g_str_foodName5 = l_food.FOODNAME.Trim();
                g_str_quantity5 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();
                g_str_customerName5 = l_customer.FULLNAME.Trim();
                g_str_price5 = l_orderInfoList.ElementAt(4).TOTALMONEY.ToString() + " VND";
            }

            if (l_i_count >= 6)
            {
                g_str_orderID6 = l_orderInfoList.ElementAt(5).ID;
                string t_customerID = l_orderInfoList.ElementAt(5).CUSTOMERID; //Biến tạm lưu customerID

                var l_orderDetail = dataProvider.Instance.DB.ORDERDETAILs.First(order => order.ORDERID == g_str_orderID6);
                var l_food = dataProvider.Instance.DB.FOODs.First(order => order.ID == l_orderDetail.FOODID);
                var l_customer = dataProvider.Instance.DB.CUSTOMERs.First(order => order.ID == t_customerID);

                g_str_foodName6 = l_food.FOODNAME.Trim();
                g_str_quantity6 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();
                g_str_customerName6 = l_customer.FULLNAME.Trim();
                g_str_price6 = l_orderInfoList.ElementAt(5).TOTALMONEY.ToString() + " VND";
            }
        }

        #region Click card 1
        private void clickDone1(SortFoodView p)
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

        #region Click card 2
        private void clickDone2(SortFoodView p)
        {
            //Lấy ID của order đang hiển thị trong card 2
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(order => order.ID == g_str_orderID2);
            //Cập nhật trạng thái của order
            l_orderIsTaking.STATUS = "Xong";
            dataProvider.Instance.DB.SaveChanges();

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

                g_str_orderID2 = l_orderDetail.ORDERID;

                g_str_foodName2 = l_food.FOODNAME.Trim();

                g_str_quantity2 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();

                g_str_customerName2 = l_customer.FULLNAME.Trim();

                g_str_price2 = l_orderInfo.TOTALMONEY.ToString() + " VND";
            }
        }

        private void clickSkip2(SortFoodView p)
        {
            //Lấy ID của order đang hiển thị trong card 1
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(order => order.ID == g_str_orderID2);
            //Cập nhật trạng thái của order
            l_orderIsTaking.STATUS = "Bỏ qua";
            dataProvider.Instance.DB.SaveChanges();

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

                g_str_orderID2 = l_orderDetail.ORDERID;

                g_str_foodName2 = l_food.FOODNAME.Trim();

                g_str_quantity2 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();

                g_str_customerName2 = l_customer.FULLNAME.Trim();

                g_str_price2 = l_orderInfo.TOTALMONEY.ToString() + " VND";
            }
        }

        private void clickSoldOut2(SortFoodView p)
        {
            //Lấy ID của order đang hiển thị trong card 1
            var l_orderIsTaking = dataProvider.Instance.DB.ORDERINFOes.First(order => order.ID == g_str_orderID2);
            //Cập nhật trạng thái của order
            l_orderIsTaking.STATUS = "Hết món";
            dataProvider.Instance.DB.SaveChanges();

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

                g_str_orderID2 = l_orderDetail.ORDERID;

                g_str_foodName2 = l_food.FOODNAME.Trim();

                g_str_quantity2 = "Số lượng: " + l_orderDetail.QUANTITY.ToString().Trim();

                g_str_customerName2 = l_customer.FULLNAME.Trim();

                g_str_price2 = l_orderInfo.TOTALMONEY.ToString() + " VND";
            }
        }
        #endregion

        #region Click Button All 1
        private void clickDoneAll1(SortFoodView p)
        {
            clickDone1(p);
            clickDone2(p);
        }

        private void clickSkipAll1(SortFoodView p)
        {
            clickSkip1(p);
            clickSkip2(p);
        }

        private void clickSoldOutAll1(SortFoodView p)
        {
            clickSoldOut1(p);
            clickSoldOut2(p);
        }
        #endregion
    }
}
