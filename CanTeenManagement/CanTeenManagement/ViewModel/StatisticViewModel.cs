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
    class StatisticViewModel : BaseViewModel
    {
        /* Static Chart */
        private DateTime? _g_sd_StaticChart_FromTime;
        private DateTime? _g_sd_StaticChart_ToTime;
        private string _g_sv_StaticChart_Choice;
        private string _g_t_StaticChart_Title;
        private ObservableCollection<KeyValuePair<string, int>> _g_dc_StaticChart_Chart;

        public DateTime? g_sd_StaticChart_FromTime { get => _g_sd_StaticChart_FromTime; set { _g_sd_StaticChart_FromTime = value; OnPropertyChanged(); } }
        public DateTime? g_sd_StaticChart_ToTime { get => _g_sd_StaticChart_ToTime; set { _g_sd_StaticChart_ToTime = value; OnPropertyChanged(); } }
        public string g_sv_StaticChart_Choice { get => _g_sv_StaticChart_Choice; set { _g_sv_StaticChart_Choice = value; OnPropertyChanged(); } }
        public string g_t_StaticChart_Title { get => _g_t_StaticChart_Title; set { _g_t_StaticChart_Title = value; OnPropertyChanged(); } }
        public ObservableCollection<KeyValuePair<string, int>> g_dc_StaticChart_Chart { get => _g_dc_StaticChart_Chart; set { _g_dc_StaticChart_Chart = value; OnPropertyChanged(); } }
        public ObservableCollection<KeyValuePair<string, int>> StaticChart_Value { get; private set; }

        /* Static Food */
        private DateTime? _g_sd_StaticFood_FromTime;
        private DateTime? _g_sd_StaticFood_ToTime;
        private string _g_txt_StaticFood_BestSeller;
        private List<ItemFood> _g_is_StaticFood_Source;

        public DateTime? g_sd_StaticFood_FromTime { get => _g_sd_StaticFood_FromTime; set { _g_sd_StaticFood_FromTime = value; OnPropertyChanged(); } }
        public DateTime? g_sd_StaticFood_ToTime { get => _g_sd_StaticFood_ToTime; set { _g_sd_StaticFood_ToTime = value; OnPropertyChanged(); } }
        public string g_txt_StaticFood_BestSeller { get => _g_txt_StaticFood_BestSeller; set { _g_txt_StaticFood_BestSeller = value; OnPropertyChanged(); } }
        public List<ItemFood> g_is_StaticFood_Source { get => _g_is_StaticFood_Source; set { _g_is_StaticFood_Source = value; OnPropertyChanged(); } }
        public List<ItemFood> StaticFood_Value { get; private set; }

        /* Static Revenue */
        private DateTime? _g_sd_StaticRevenue_FromTime;
        private DateTime? _g_sd_StaticRevenue_ToTime;
        private string _g_txt_StaticRevenue_TotalMoney;
        private List<ItemRevenue> _g_is_StaticRevenue_Source;

        public DateTime? g_sd_StaticRevenue_FromTime { get => _g_sd_StaticRevenue_FromTime; set { _g_sd_StaticRevenue_FromTime = value; OnPropertyChanged(); } }
        public DateTime? g_sd_StaticRevenue_ToTime { get => _g_sd_StaticRevenue_ToTime; set { _g_sd_StaticRevenue_ToTime = value; OnPropertyChanged(); } }
        public string g_txt_StaticRevenue_TotalMoney { get => _g_txt_StaticRevenue_TotalMoney; set { _g_txt_StaticRevenue_TotalMoney = value; OnPropertyChanged(); } }
        public List<ItemRevenue> g_is_StaticRevenue_Source { get => _g_is_StaticRevenue_Source; set { _g_is_StaticRevenue_Source = value; OnPropertyChanged(); } }
        public List<ItemRevenue> StaticRevenue_Value { get; private set; }

        /*--------------*/
        #region commands.
        public ICommand g_iCm_StaticChart { get; set; }
        public ICommand g_iCm_StaticFood { get; set; }
        public ICommand g_iCm_StaticRevenue { get; set; }
        #endregion


        public StatisticViewModel()
        {
            g_iCm_StaticChart = new RelayCommand<StatisticView>((p) => { return true; }, (p) =>
            {
                this.staticChart(p);
            });

            g_iCm_StaticFood = new RelayCommand<StatisticView>((p) => { return true; }, (p) =>
            {
                this.staticFood(p);
            });

            g_iCm_StaticRevenue = new RelayCommand<StatisticView>((p) => { return true; }, (p) =>
            {
                this.staticRevenue(p);
            });
        }

        private void staticChart(StatisticView p)
        {
            if (!_g_sd_StaticChart_FromTime.HasValue || !_g_sd_StaticChart_ToTime.HasValue || _g_sv_StaticChart_Choice == null)    // khi người dùng quên chọn ngày hoặc kiểu thống kê thì chương trình ko bị crash
            {
                return;
            }

            var dataOrderInfo = dataProvider.Instance.DB.ORDERINFOes;

            if (_g_sv_StaticChart_Choice == "Đặt món")
            {
                var countFirstDay = (from oi in dataOrderInfo where oi.ORDERDATE == _g_sd_StaticChart_FromTime select oi).Count();
                var countLastDay = (from oi in dataOrderInfo where oi.ORDERDATE == _g_sd_StaticChart_ToTime select oi).Count();
                var countOtherDay = from oi in dataOrderInfo
                                    where oi.ORDERDATE > _g_sd_StaticChart_FromTime && oi.ORDERDATE < _g_sd_StaticChart_ToTime
                                    group oi by oi.ORDERDATE into g
                                    select new { Date = g.Key, Count = g.Count() };

                this.StaticChart_Value = new ObservableCollection<KeyValuePair<string, int>>();

                string[] strFirstDay = _g_sd_StaticChart_FromTime.ToString().Split(' ');
                StaticChart_Value.Add(new KeyValuePair<string, int>(strFirstDay[0], countFirstDay));

                foreach (var i in countOtherDay)
                {
                    string day = i.Date.ToString();
                    string[] str = day.Split(' ');
                    StaticChart_Value.Add(new KeyValuePair<string, int>(str[0], i.Count));
                }

                string[] strLastDay = _g_sd_StaticChart_ToTime.ToString().Split(' ');
                StaticChart_Value.Add(new KeyValuePair<string, int>(strLastDay[0], countLastDay));

                g_t_StaticChart_Title = "Số lượng";
                g_dc_StaticChart_Chart = StaticChart_Value;

            }
            else if (_g_sv_StaticChart_Choice == "Doanh số")
            {
                var revenueFirstDay = (from oi in dataOrderInfo where oi.ORDERDATE == _g_sd_StaticChart_FromTime select oi.TOTALMONEY).Sum();
                var revenueLastDay = (from oi in dataOrderInfo where oi.ORDERDATE == _g_sd_StaticChart_ToTime select oi.TOTALMONEY).Sum();
                var revenueOtherDay = from oi in dataOrderInfo
                                      where oi.ORDERDATE > _g_sd_StaticChart_FromTime && oi.ORDERDATE < _g_sd_StaticChart_ToTime
                                      group oi by oi.ORDERDATE into g
                                      select new { Date = g.Key, TotalMoney = g.Sum(a => a.TOTALMONEY) };

                this.StaticChart_Value = new ObservableCollection<KeyValuePair<string, int>>();

                string[] strFirstDay = _g_sd_StaticChart_FromTime.ToString().Split(' ');
                StaticChart_Value.Add(new KeyValuePair<string, int>(strFirstDay[0], revenueFirstDay ?? default(int)));

                foreach (var i in revenueOtherDay)
                {
                    string day = i.Date.ToString();
                    string[] str = day.Split(' ');
                    StaticChart_Value.Add(new KeyValuePair<string, int>(str[0], i.TotalMoney ?? default(int)));
                }

                string[] strLastDay = _g_sd_StaticChart_ToTime.ToString().Split(' ');
                StaticChart_Value.Add(new KeyValuePair<string, int>(strLastDay[0], revenueLastDay ?? default(int)));

                g_t_StaticChart_Title = "Doanh số";
                g_dc_StaticChart_Chart = StaticChart_Value;
            }
        }

        private void staticFood(StatisticView p)
        {
            if (!_g_sd_StaticFood_FromTime.HasValue || !_g_sd_StaticFood_ToTime.HasValue)    // khi người dùng quên chọn ngày hoặc kiểu thống kê thì chương trình ko bị crash
            {
                return;
            }

            this.StaticFood_Value = new List<ItemFood>();

            var dataOrderDetail = dataProvider.Instance.DB.ORDERDETAILs;
            var dataFood = dataProvider.Instance.DB.FOODs;
            var dataOrderInfo = dataProvider.Instance.DB.ORDERINFOes;

            var result = from od in dataOrderDetail
                         group od by od.FOODID into g
                         join f in dataFood on g.FirstOrDefault().FOODID equals f.ID
                         join oi in dataOrderInfo on g.FirstOrDefault().ORDERID equals oi.ID
                         where oi.ORDERDATE >= _g_sd_StaticFood_FromTime && oi.ORDERDATE <= _g_sd_StaticFood_ToTime
                         select new { Name = f.FOODNAME, Sale = g.Sum(a => a.QUANTITY) };

            int max = 0;
            string bestSeller = "";

            foreach (var i in result)
            {
                int temp = i.Sale ?? default(int);
                StaticFood_Value.Add(new ItemFood(i.Name, temp));

                if (max < temp)
                {
                    max = temp;
                    bestSeller = i.Name;
                }
            }

            g_txt_StaticFood_BestSeller = "Bán chạy nhất:  " + bestSeller;
            g_is_StaticFood_Source = StaticFood_Value;
        }

        private void staticRevenue(StatisticView p)
        {
            if (!_g_sd_StaticRevenue_FromTime.HasValue || !_g_sd_StaticRevenue_ToTime.HasValue)    // khi người dùng quên chọn ngày hoặc kiểu thống kê thì chương trình ko bị crash
            {
                return;
            }

            this.StaticRevenue_Value = new List<ItemRevenue>();

            var dataOrderInfo = dataProvider.Instance.DB.ORDERINFOes;

            var result = from oi in dataOrderInfo
                         where oi.ORDERDATE >= _g_sd_StaticRevenue_FromTime && oi.ORDERDATE <= _g_sd_StaticRevenue_ToTime
                         group oi by oi.ORDERDATE into g
                         select new { Date = g.FirstOrDefault().ORDERDATE, Money = g.Sum(a => a.TOTALMONEY) };

            int sum = 0;

            foreach (var i in result)
            {
                string day = i.Date.ToString();
                string[] str = day.Split(' ');
                StaticRevenue_Value.Add(new ItemRevenue(str[0], i.Money ?? default(int)));

                sum += i.Money ?? default(int);
            }

            g_txt_StaticRevenue_TotalMoney = "Tổng doanh thu:   " + sum.ToString();
            g_is_StaticRevenue_Source = StaticRevenue_Value;
        }
    }

    public class ItemFood
    {
        public ItemFood(string name, int sale)
        {
            g_dmb_StaticFood_FoodName = name;
            g_dmb_StaticFood_Sale = sale;
        }

        public string g_dmb_StaticFood_FoodName { get; set; }
        public int g_dmb_StaticFood_Sale { get; set; }
    }

    public class ItemRevenue
    {
        public ItemRevenue(string date, int money)
        {
            g_dmb_StaticRevenue_Date = date;
            g_dmb_StaticRevenue_Money = money;
        }

        public string g_dmb_StaticRevenue_Date { get; set; }
        public int g_dmb_StaticRevenue_Money { get; set; }
    }
}

