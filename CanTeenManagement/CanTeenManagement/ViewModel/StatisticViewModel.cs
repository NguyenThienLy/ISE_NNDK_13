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
using System.Text.RegularExpressions;

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
        private string _g_sv_StaticFood_Choice;
        private string _g_txt_StaticFood_BestSeller;
        private List<ItemFood> _g_is_StaticFood_Source;
        private ObservableCollection<KeyValuePair<string, int>> _g_dc_StaticFood_Chart;

        public DateTime? g_sd_StaticFood_FromTime { get => _g_sd_StaticFood_FromTime; set { _g_sd_StaticFood_FromTime = value; OnPropertyChanged(); } }
        public DateTime? g_sd_StaticFood_ToTime { get => _g_sd_StaticFood_ToTime; set { _g_sd_StaticFood_ToTime = value; OnPropertyChanged(); } }
        public string g_sv_StaticFood_Choice { get => _g_sv_StaticFood_Choice; set { _g_sv_StaticFood_Choice = value; OnPropertyChanged(); } }
        public string g_txt_StaticFood_BestSeller { get => _g_txt_StaticFood_BestSeller; set { _g_txt_StaticFood_BestSeller = value; OnPropertyChanged(); } }
        public List<ItemFood> g_is_StaticFood_Source { get => _g_is_StaticFood_Source; set { _g_is_StaticFood_Source = value; OnPropertyChanged(); } }
        public List<ItemFood> StaticFood_Value { get; private set; }
        public ObservableCollection<KeyValuePair<string, int>> g_dc_StaticFood_Chart { get => _g_dc_StaticFood_Chart; set { _g_dc_StaticFood_Chart = value; OnPropertyChanged(); } }
        public ObservableCollection<KeyValuePair<string, int>> StaticFood_ChartValue { get; private set; }

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
        }

        private void staticChart(StatisticView p)
        {
            this.StaticChart_Value = new ObservableCollection<KeyValuePair<string, int>>();

            if (_g_sv_StaticChart_Choice == "Tuần hiện tại")
            {
                staticChartThisWeek();
            }
            else if (_g_sv_StaticChart_Choice == "Tháng hiện tại")
            {
                staticChartThisMonth();
            }
            else if (_g_sv_StaticChart_Choice == "Năm hiện tại")
            {
                staticChartThisYear();
            }
            else
            {
                if (!_g_sd_StaticChart_FromTime.HasValue || !_g_sd_StaticChart_ToTime.HasValue)    // khi người dùng quên chọn ngày hoặc kiểu thống kê thì chương trình ko bị crash
                {
                    MessageBox.Show("Hãy chọn khoảng thời gian!", "Error", 0, 0);
                    return;
                }

                staticChartByTime();
            }

            g_dc_StaticChart_Chart = StaticChart_Value;
        }

        private void staticFood(StatisticView p)
        {
            if (_g_sv_StaticFood_Choice == "Tuần hiện tại")
            {
                DateTime? dtFirstDayOfWeek = getLastWeekDay(DayOfWeek.Monday);
                DateTime? dtLastDayOfWeek = dtFirstDayOfWeek.Value.AddDays(6);

                staticFoodBetweenTime(dtFirstDayOfWeek, dtLastDayOfWeek);
            }
            else if (_g_sv_StaticFood_Choice == "Tháng hiện tại")
            {
                DateTime? dtFirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime? dtLastDayOfMonth = dtFirstDayOfMonth.Value.AddMonths(1).AddDays(-1);

                staticFoodBetweenTime(dtFirstDayOfMonth, dtLastDayOfMonth);
            }
            else if (_g_sv_StaticFood_Choice == "Năm hiện tại")
            {
                DateTime? dtFirstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);
                DateTime? dtLastDayOfYear = dtFirstDayOfYear.Value.AddYears(1).AddDays(-1);

                staticFoodBetweenTime(dtFirstDayOfYear, dtLastDayOfYear);
            }
            else
            {
                if (!_g_sd_StaticFood_FromTime.HasValue || !_g_sd_StaticFood_ToTime.HasValue)    // khi người dùng quên chọn ngày hoặc kiểu thống kê thì chương trình ko bị crash
                {
                    MessageBox.Show("Hãy chọn khoảng thời gian!", "Error", 0, 0);
                    return;
                }

                staticFoodBetweenTime(_g_sd_StaticFood_FromTime, _g_sd_StaticFood_ToTime);
            }
        }

        private void staticChartByTime()
        {
            int MAX_COLUMN = 20;
            int iTimeSpan = (_g_sd_StaticChart_ToTime - _g_sd_StaticChart_FromTime).Value.Days;
            int iDayNum = iTimeSpan + 1;

            if (iTimeSpan > 366)
            {
                MessageBox.Show("Thời gian thống kê quá dài!", "Error", 0, 0);

                return;
            }

            if (iDayNum <= 31)
            {
                DateTime? dtDateTime = _g_sd_StaticChart_FromTime;

                while (dtDateTime <= _g_sd_StaticChart_ToTime)
                {
                    var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                               where oi.ORDERDATE == dtDateTime
                               select new { oi.TOTALMONEY };

                    int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                    string date = dtDateTime.Value.Day.ToString() + "/" + dtDateTime.Value.Month.ToString();

                    StaticChart_Value.Add(new KeyValuePair<string, int>(date, money));

                    dtDateTime = dtDateTime.Value.AddDays(1);
                }
            }
            else if (iDayNum > 31 && iDayNum <= 240)
            {
                int iDayPerColumn = iDayNum / MAX_COLUMN;

                if (iDayNum % MAX_COLUMN != 0)
                {
                    iDayPerColumn++;
                }

                DateTime? dtBeginTime = _g_sd_StaticChart_FromTime;
                DateTime? dtEndTime = dtBeginTime.Value.AddDays(iDayPerColumn);

                while (dtBeginTime <= _g_sd_StaticChart_ToTime)
                {
                    if (dtEndTime > _g_sd_StaticChart_ToTime)
                    {
                        dtEndTime = _g_sd_StaticChart_ToTime;
                    }

                    var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                               where oi.ORDERDATE >= dtBeginTime && oi.ORDERDATE <= dtEndTime
                               select new { oi.TOTALMONEY };

                    int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                    string date = getIndependentValue(dtBeginTime, dtEndTime);

                    StaticChart_Value.Add(new KeyValuePair<string, int>(date, money));

                    dtBeginTime = dtEndTime.Value.AddDays(1);
                    dtEndTime = dtBeginTime.Value.AddDays(iDayPerColumn);
                }
            }
            else if (iDayNum > 240)     // Thời gian quá dài nên thống kê theo tháng
            {
                DateTime? dtBeginTime = _g_sd_StaticChart_FromTime;
                DateTime? dtEndTime = new DateTime(dtBeginTime.Value.Year, dtBeginTime.Value.Month, 1).AddMonths(1).AddDays(-1);

                while (dtBeginTime <= _g_sd_StaticChart_ToTime)
                {
                    var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                               where oi.ORDERDATE >= dtBeginTime && oi.ORDERDATE <= dtEndTime
                               select new { oi.TOTALMONEY };

                    int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                    string date = "Tháng " + dtBeginTime.Value.Month.ToString();

                    StaticChart_Value.Add(new KeyValuePair<string, int>(date, money));

                    dtBeginTime = dtEndTime.Value.AddDays(1);
                    dtEndTime = dtBeginTime.Value.AddMonths(1).AddDays(-1);
                }
            }
        }

        private void staticChartThisWeek()
        {
            DateTime? dtFirstDayOfWeek = getLastWeekDay(DayOfWeek.Monday);
            DateTime? dtLastDayOfWeek = dtFirstDayOfWeek.Value.AddDays(6);
            DateTime? dtDateTime = dtFirstDayOfWeek;

            while (dtDateTime <= dtLastDayOfWeek)
            {
                var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                           where oi.ORDERDATE == dtDateTime
                           select new { oi.TOTALMONEY };

                int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                string date = getDayNameOfWeek(dtDateTime);

                StaticChart_Value.Add(new KeyValuePair<string, int>(date, money));

                dtDateTime = dtDateTime.Value.AddDays(1);
            }
        }

        private void staticChartThisMonth()
        {
            DateTime? dtFirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime? dtLastDayOfMonth = dtFirstDayOfMonth.Value.AddMonths(1).AddDays(-1);
            DateTime? dtDateTime = dtFirstDayOfMonth;

            {
                var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                           where oi.ORDERDATE == dtDateTime
                           select new { oi.TOTALMONEY };

                int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                string date = dtDateTime.Value.Day.ToString() + "/" + dtDateTime.Value.Month.ToString();

                StaticChart_Value.Add(new KeyValuePair<string, int>(date, money));

                dtDateTime = dtDateTime.Value.AddDays(1);
            }

            while (dtDateTime < dtLastDayOfMonth)
            {
                var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                           where oi.ORDERDATE == dtDateTime
                           select new { oi.TOTALMONEY };

                int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                string date = dtDateTime.Value.Day.ToString();

                StaticChart_Value.Add(new KeyValuePair<string, int>(date, money));

                dtDateTime = dtDateTime.Value.AddDays(1);
            }

            {
                var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                           where oi.ORDERDATE == dtDateTime
                           select new { oi.TOTALMONEY };

                int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                string date = dtDateTime.Value.Day.ToString() + "/" + dtDateTime.Value.Month.ToString();

                StaticChart_Value.Add(new KeyValuePair<string, int>(date, money));
            }
        }

        private void staticChartThisYear()
        {
            DateTime? dtFirstMonthOfYear = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime? dtLastMonthOfYear = dtFirstMonthOfYear.Value.AddYears(1).AddMonths(-1);
            DateTime? dtCurrentMonth = dtFirstMonthOfYear;
            DateTime? dtFirstDayOfMonth = dtCurrentMonth;
            DateTime? dtLastDayOfMonth = dtFirstDayOfMonth.Value.AddMonths(1).AddDays(-1);

            while (dtCurrentMonth <= dtLastMonthOfYear)
            {
                var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                           where oi.ORDERDATE >= dtFirstDayOfMonth && oi.ORDERDATE <= dtLastDayOfMonth
                           select new { oi.TOTALMONEY };

                int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                string date = "Tháng " + dtCurrentMonth.Value.Month.ToString();

                StaticChart_Value.Add(new KeyValuePair<string, int>(date, money));

                dtCurrentMonth = dtCurrentMonth.Value.AddMonths(1);
                dtFirstDayOfMonth = dtCurrentMonth;
                dtLastDayOfMonth = dtFirstDayOfMonth.Value.AddMonths(1).AddDays(-1);
            }
        }

        private void staticFoodBetweenTime(DateTime? dtBeginTime, DateTime? dtEndTime)
        {
            this.StaticFood_Value = new List<ItemFood>();
            this.StaticFood_ChartValue = new ObservableCollection<KeyValuePair<string, int>>();

            var dataOrderDetail = dataProvider.Instance.DB.ORDERDETAILs;
            var dataFood = dataProvider.Instance.DB.FOODs;
            var dataOrderInfo = dataProvider.Instance.DB.ORDERINFOes;

            var result = from od in dataOrderDetail
                         group od by od.FOODID into g
                         join f in dataFood on g.FirstOrDefault().FOODID equals f.ID
                         join oi in dataOrderInfo on g.FirstOrDefault().ORDERID equals oi.ID
                         where oi.ORDERDATE >= dtBeginTime && oi.ORDERDATE <= dtEndTime
                         select new { Name = f.FOODNAME, Sale = g.Sum(a => a.QUANTITY) };

            int iFirst = 0, iSecond = 0, iThird = 0, iOthers = 0;
            string sFirst = "", sSecond = "", sThird = "", sOthers = "Các món còn lại";

            foreach (var i in result)
            {
                int temp = i.Sale ?? default(int);
                StaticFood_Value.Add(new ItemFood(i.Name, temp));

                if (iFirst < temp)
                {
                    iOthers += iThird;
                    iThird = iSecond;
                    sThird = sSecond;
                    iSecond = iFirst;
                    sSecond = sFirst;
                    iFirst = temp;
                    sFirst = i.Name;
                }
                else if (iSecond < temp)
                {
                    iOthers += iThird;
                    iThird = iSecond;
                    sThird = sSecond;
                    iSecond = temp;
                    sSecond = i.Name;
                }
                else if (iSecond < temp)
                {
                    iOthers += iThird;
                    iThird = temp;
                    sThird = i.Name;
                }
            }

            string[] strFirst = Regex.Split(sFirst, "   ");
            string[] strSecond = Regex.Split(sSecond, "   ");
            string[] strThird = Regex.Split(sThird, "   ");

            if (iFirst > 0)
            {
                StaticFood_ChartValue.Add(new KeyValuePair<string, int>(strFirst[0], iFirst));
            }
            if (iSecond > 0)
            {
                StaticFood_ChartValue.Add(new KeyValuePair<string, int>(strSecond[0], iSecond));
            }
            if (iThird > 0)
            {
                StaticFood_ChartValue.Add(new KeyValuePair<string, int>(strThird[0], iThird));
            }

            StaticFood_ChartValue.Add(new KeyValuePair<string, int>(sOthers, iOthers));

            g_txt_StaticFood_BestSeller = "Bán chạy nhất:  " + strFirst[0];
            g_is_StaticFood_Source = StaticFood_Value;
            g_dc_StaticFood_Chart = StaticFood_ChartValue;
        }

        private string getIndependentValue(DateTime? dtBeginTime, DateTime? dtEndTime)
        {
            int iBeginDay = dtBeginTime.Value.Day;
            int iBeginMonth = dtBeginTime.Value.Month;
            int iBeginYear = dtBeginTime.Value.Year;
            int iEndDay = dtEndTime.Value.Day;
            int iEndMonth = dtEndTime.Value.Month;
            int iEndYear = dtEndTime.Value.Year;

            string result = iBeginDay.ToString() + "/" + iBeginMonth.ToString() + "-" + iEndDay.ToString() + "/" + iEndMonth.ToString();

            return result;
        }

        private DateTime? getLastWeekDay(DayOfWeek day)
        {
            DateTime? result = DateTime.Today;

            while (result.Value.DayOfWeek != day)
            {
                result = result.Value.AddDays(-1);
            }

            return result;
        }

        private string getDayNameOfWeek(DateTime? dtDay)
        {
            string result = dtDay.Value.Day.ToString() + "/" + dtDay.Value.Month.ToString();

            switch (dtDay.Value.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    result += "\nThứ hai";
                    break;
                case DayOfWeek.Tuesday:
                    result += "\nThứ ba";
                    break;
                case DayOfWeek.Wednesday:
                    result += "\nThứ tư";
                    break;
                case DayOfWeek.Thursday:
                    result += "\nThứ năm";
                    break;
                case DayOfWeek.Friday:
                    result += "\nThứ sáu";
                    break;
                case DayOfWeek.Saturday:
                    result += "\nThứ bảy";
                    break;
                case DayOfWeek.Sunday:
                    result += "\nChủ nhật";
                    break;
                default:
                    break;
            }

            return result;
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
    }
}

/*
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

                //g_t_StaticChart_Title = "Số lượng";
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

                //g_t_StaticChart_Title = "Doanh số";
                g_dc_StaticChart_Chart = StaticChart_Value;
            }

*/
