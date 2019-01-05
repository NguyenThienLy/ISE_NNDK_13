using CanTeenManagement.CO;
using CanTeenManagement.Model;
using CanTeenManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CanTeenManagement.ViewModel
{
    public class StatisticViewModel : BaseViewModel
    {

        #region declare StatisticChart
        private DateTime? _g_sd_StatChart_FromTime;
        private DateTime? _g_sd_StatChart_ToTime;
        private string _g_sv_StatChart_Choice;
        private string _g_t_StatChart_Title;
        private ObservableCollection<KeyValuePair<string, int>> _g_dc_StatChart_Chart;

        public DateTime? g_sd_StatChart_FromTime { get => _g_sd_StatChart_FromTime; set { _g_sd_StatChart_FromTime = value; OnPropertyChanged(); } }
        public DateTime? g_sd_StatChart_ToTime { get => _g_sd_StatChart_ToTime; set { _g_sd_StatChart_ToTime = value; OnPropertyChanged(); } }
        public string g_sv_StatChart_Choice { get => _g_sv_StatChart_Choice; set { _g_sv_StatChart_Choice = value; OnPropertyChanged(); } }
        public string g_t_StatChart_Title { get => _g_t_StatChart_Title; set { _g_t_StatChart_Title = value; OnPropertyChanged(); } }
        public ObservableCollection<KeyValuePair<string, int>> g_dc_StatChart_Chart { get => _g_dc_StatChart_Chart; set { _g_dc_StatChart_Chart = value; OnPropertyChanged(); } }
        public ObservableCollection<KeyValuePair<string, int>> StatChart_Value { get; private set; }
        #endregion

        #region declare StatisticFood
        private DateTime? _g_sd_StatFood_FromTime;
        private DateTime? _g_sd_StatFood_ToTime;
        private string _g_sv_StatFood_Choice;
        private string _g_txt_StatFood_BestSeller;
        private CollectionViewSource _g_is_StatFood_Source;
        private ObservableCollection<KeyValuePair<string, int>> _g_dc_StatFood_Chart;

        public DateTime? g_sd_StatFood_FromTime { get => _g_sd_StatFood_FromTime; set { _g_sd_StatFood_FromTime = value; OnPropertyChanged(); } }
        public DateTime? g_sd_StatFood_ToTime { get => _g_sd_StatFood_ToTime; set { _g_sd_StatFood_ToTime = value; OnPropertyChanged(); } }
        public string g_sv_StatFood_Choice { get => _g_sv_StatFood_Choice; set { _g_sv_StatFood_Choice = value; OnPropertyChanged(); } }
        public string g_txt_StatFood_BestSeller { get => _g_txt_StatFood_BestSeller; set { _g_txt_StatFood_BestSeller = value; OnPropertyChanged(); } }
        public ListCollectionView g_is_StatFood_Source { get => (ListCollectionView)_g_is_StatFood_Source.View; /*set { _g_is_StatFood_Source.Source = value; OnPropertyChanged(); } */}
        //public CollectionViewSource g_is_StatFood_Source { get => _g_is_StatFood_Source; set { _g_is_StatFood_Source = value; OnPropertyChanged(); } }
        public ListCollectionView StatFood_Value { get; private set; }
        public ObservableCollection<KeyValuePair<string, int>> g_dc_StatFood_Chart { get => _g_dc_StatFood_Chart; set { _g_dc_StatFood_Chart = value; OnPropertyChanged(); } }
        public ObservableCollection<KeyValuePair<string, int>> StatFood_ChartValue { get; private set; }
        #endregion

        private bool _g_b_isChart;
        public bool g_b_isChart
        {
            get => _g_b_isChart;
            set
            {
                _g_b_isChart = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_mode;
        public string g_str_mode
        {
            get => _g_str_mode;
            set
            {
                _g_str_mode = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_visibilityChart;
        public string g_str_visibilityChart
        {
            get => _g_str_visibilityChart;
            set
            {
                _g_str_visibilityChart = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_visibilityFood;
        public string g_str_visibilityFood
        {
            get => _g_str_visibilityFood;
            set
            {
                _g_str_visibilityFood = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_visibilityEmpty;
        public string g_str_visibilityEmpty
        {
            get => _g_str_visibilityEmpty;
            set
            {
                _g_str_visibilityEmpty = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_visibilityChartChart;
        public string g_str_visibilityChartChart
        {
            get => _g_str_visibilityChartChart;
            set
            {
                _g_str_visibilityChartChart = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_visibilityChartFood;
        public string g_str_visibilityChartFood
        {
            get => _g_str_visibilityChartFood;
            set
            {
                _g_str_visibilityChartFood = value;
                OnPropertyChanged();
            }
        }

        #region commands.
        public ICommand g_iCm_StatChart { get; set; }
        public ICommand g_iCm_StatFood { get; set; }
        public ICommand g_iCm_Sort { get; set; }
        public ICommand g_iCm_clickChart { get; set; }
        public ICommand g_iCm_clickFood { get; set; }
        #endregion

        public int MAX_COLUMN = 20;

        public StatisticViewModel()
        {
            this.initSupport();

            g_iCm_StatChart = new RelayCommand<StatisticView>((p) => { return true; }, (p) =>
            {
                this.clickChart();
            });

            g_iCm_StatFood = new RelayCommand<StatisticView>((p) => { return true; }, (p) =>
            {
                DataLV.Clear();
                this.clickFood();
            });

            g_iCm_Sort = new RelayCommand(sort);

            g_iCm_clickChart = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                this.clickChart();
            });

            g_iCm_clickFood = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                this.clickFood();
            });
        }

        private void initSupport()
        {
            this.g_b_isChart = true;
            this.g_str_mode = staticVarClass.mode_statisticChart;

            this.g_str_visibilityChart = staticVarClass.visibility_visible;
            this.g_str_visibilityChartChart = staticVarClass.visibility_visible;

            this.g_str_visibilityFood = staticVarClass.visibility_hidden;
            this.g_str_visibilityEmpty = staticVarClass.visibility_hidden;
            this.g_str_visibilityChartFood = staticVarClass.visibility_visible;

            this.statStartUp();
        }

        private void clickChart()
        {
            this.g_b_isChart = true;
            this.g_str_mode = staticVarClass.mode_statisticChart;

            this.g_str_visibilityChart = staticVarClass.visibility_visible;
            this.g_str_visibilityChartChart = staticVarClass.visibility_visible;

            this.g_str_visibilityFood = staticVarClass.visibility_hidden;
            this.g_str_visibilityEmpty = staticVarClass.visibility_hidden;
            this.g_str_visibilityChartFood = staticVarClass.visibility_visible;

            this.statChart();
            this.checkVisibilityChart();
        }

        private void clickFood()
        {
            this.g_b_isChart = false;
            this.g_str_mode = staticVarClass.mode_statisticFood;

            this.g_str_visibilityFood = staticVarClass.visibility_visible;
            this.g_str_visibilityChartFood = staticVarClass.visibility_visible;

            this.g_str_visibilityChart = staticVarClass.visibility_hidden;
            this.g_str_visibilityEmpty = staticVarClass.visibility_hidden;
            this.g_str_visibilityChartChart = staticVarClass.visibility_hidden;

            this.statFood();
            this.checkVisibilityFood();
        }

        private void statChart()
        {
            StatChart_Value = new ObservableCollection<KeyValuePair<string, int>>();

            if (_g_sv_StatChart_Choice == staticVarClass.timeStr_byWeek)
            {
                statChartByWeek();
            }
            else if (_g_sv_StatChart_Choice == staticVarClass.timeStr_thisWeek)
            {
                statChartThisWeek();
            }
            else if (_g_sv_StatChart_Choice == staticVarClass.timeStr_thisMoth)
            {
                statChartThisMonth();
            }
            else if (_g_sv_StatChart_Choice == staticVarClass.timeStr_thisYear)
            {
                statChartThisYear();
            }
            else if (g_sv_StatChart_Choice == staticVarClass.timeStr_manyYears)
            {
                statChartManyYears();
            }
            else
            {
                if (!_g_sd_StatChart_FromTime.HasValue || !_g_sd_StatChart_ToTime.HasValue)    // khi người dùng quên chọn ngày hoặc kiểu thống kê thì chương trình ko bị crash
                {
                    staticFunctionClass.showStatusView(false, "Hãy chọn khoảng thời gian!");
                    return;
                }

                statChartByTime();
            }

            g_dc_StatChart_Chart = StatChart_Value;
        }

        private void checkVisibilityChart()
        {
            if (g_dc_StatChart_Chart.Count == 0)
            {
                this.g_str_visibilityEmpty = staticVarClass.visibility_visible;

                this.g_str_visibilityChartChart = staticVarClass.visibility_hidden;
            }
            else
            {
                this.g_str_visibilityEmpty = staticVarClass.visibility_hidden;

                this.g_str_visibilityChartChart = staticVarClass.visibility_visible;
            }
        }

        private void statFood()
        {
            if (_g_sv_StatFood_Choice == staticVarClass.timeStr_thisWeek)
            {
                DateTime? dtFirstDayOfWeek = getLastWeekDay(DayOfWeek.Monday);
                DateTime? dtLastDayOfWeek = dtFirstDayOfWeek.Value.AddDays(6);

                statFoodBetweenTime(dtFirstDayOfWeek, dtLastDayOfWeek);
            }
            else if (_g_sv_StatFood_Choice == staticVarClass.timeStr_thisMoth)
            {
                DateTime? dtFirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime? dtLastDayOfMonth = dtFirstDayOfMonth.Value.AddMonths(1).AddDays(-1);

                statFoodBetweenTime(dtFirstDayOfMonth, dtLastDayOfMonth);
            }
            else if (_g_sv_StatFood_Choice == staticVarClass.timeStr_thisYear)
            {
                DateTime? dtFirstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);
                DateTime? dtLastDayOfYear = dtFirstDayOfYear.Value.AddYears(1).AddDays(-1);

                statFoodBetweenTime(dtFirstDayOfYear, dtLastDayOfYear);
            }
            else
            {
                if (!_g_sd_StatFood_FromTime.HasValue || !_g_sd_StatFood_ToTime.HasValue)    // khi người dùng quên chọn ngày hoặc kiểu thống kê thì chương trình ko bị crash
                {
                    MessageBox.Show("Hãy chọn khoảng thời gian!", "Error", 0, 0);
                    return;
                }

                statFoodBetweenTime(_g_sd_StatFood_FromTime, _g_sd_StatFood_ToTime);
            }

            sortSaleByDescending();
        }

        private void statChartByTime()
        {
            int iTimeSpan = (_g_sd_StatChart_ToTime - _g_sd_StatChart_FromTime).Value.Days;
            int iDayNum = iTimeSpan + 1;
            int l_i_money = 0;

            if (iTimeSpan > 366)
            {
                staticFunctionClass.showStatusView(false, "Thời gian thống kê quá dài!");

                return;
            }

            if (iDayNum <= 31)
            {
                DateTime? dtDateTime = _g_sd_StatChart_FromTime;

                while (dtDateTime <= _g_sd_StatChart_ToTime)
                {
                    using (var DB = new QLCanTinEntities())
                    {
                        var data = from oi in DB.ORDERINFOes
                                   where oi.ORDERDATE == dtDateTime
                                   select new { oi.TOTALMONEY };

                        l_i_money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                    }

                    string date = dtDateTime.Value.Day.ToString() + "/" + dtDateTime.Value.Month.ToString();

                    StatChart_Value.Add(new KeyValuePair<string, int>(date, l_i_money));

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

                DateTime? dtBeginTime = _g_sd_StatChart_FromTime;
                DateTime? dtEndTime = dtBeginTime.Value.AddDays(iDayPerColumn);

                while (dtBeginTime <= _g_sd_StatChart_ToTime)
                {
                    if (dtEndTime > _g_sd_StatChart_ToTime)
                    {
                        dtEndTime = _g_sd_StatChart_ToTime;
                    }

                    using (var DB = new QLCanTinEntities())
                    {
                        var data = from oi in DB.ORDERINFOes
                                   where oi.ORDERDATE >= dtBeginTime && oi.ORDERDATE <= dtEndTime
                                   select new { oi.TOTALMONEY };

                        l_i_money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                    }

                    string date = getDateMonth(dtBeginTime, dtEndTime);

                    StatChart_Value.Add(new KeyValuePair<string, int>(date, l_i_money));

                    dtBeginTime = dtEndTime.Value.AddDays(1);
                    dtEndTime = dtBeginTime.Value.AddDays(iDayPerColumn);
                }
            }
            else if (iDayNum > 240)     // Thời gian quá dài nên thống kê theo tháng
            {
                DateTime? dtBeginTime = _g_sd_StatChart_FromTime;
                DateTime? dtEndTime = new DateTime(dtBeginTime.Value.Year, dtBeginTime.Value.Month, 1).AddMonths(1).AddDays(-1);

                while (dtBeginTime <= _g_sd_StatChart_ToTime)
                {
                    using (var DB = new QLCanTinEntities())
                    {
                        var data = from oi in DB.ORDERINFOes
                                   where oi.ORDERDATE >= dtBeginTime && oi.ORDERDATE <= dtEndTime
                                   select new { oi.TOTALMONEY };

                        l_i_money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                    }

                    string date = "Tháng " + dtBeginTime.Value.Month.ToString();

                    StatChart_Value.Add(new KeyValuePair<string, int>(date, l_i_money));

                    dtBeginTime = dtEndTime.Value.AddDays(1);
                    dtEndTime = dtBeginTime.Value.AddMonths(1).AddDays(-1);
                }
            }
        }

        private void statChartByWeek()
        {
            DateTime? dtFirstDayOfWeek = g_sd_StatChart_FromTime;
            DateTime? dtLastDayOfWeek = dtFirstDayOfWeek.Value.AddDays(6);
            int l_i_money = 0;

            for (int i = 0; i < MAX_COLUMN; i++)
            {
                if (dtFirstDayOfWeek > g_sd_StatChart_ToTime)
                {
                    break;
                }

                using (var DB = new QLCanTinEntities())
                {
                    var data = from oi in DB.ORDERINFOes
                               where oi.ORDERDATE >= dtFirstDayOfWeek && oi.ORDERDATE <= dtLastDayOfWeek
                               select new { oi.TOTALMONEY };

                    l_i_money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                }

                string week = "Tuần " + getWeekOfYear(dtFirstDayOfWeek).ToString() + "\n" + getDateMonth(dtFirstDayOfWeek, dtLastDayOfWeek);

                StatChart_Value.Add(new KeyValuePair<string, int>(week, l_i_money));

                dtFirstDayOfWeek = dtLastDayOfWeek.Value.AddDays(1);
                dtLastDayOfWeek = dtFirstDayOfWeek.Value.AddDays(6);
            }
        }

        private void statChartThisWeek()
        {
            DateTime? dtFirstDayOfWeek = getLastWeekDay(DayOfWeek.Monday);
            DateTime? dtLastDayOfWeek = dtFirstDayOfWeek.Value.AddDays(6);
            DateTime? dtDateTime = dtFirstDayOfWeek;
            int l_i_money = 0;

            while (dtDateTime <= dtLastDayOfWeek)
            {
                using (var DB = new QLCanTinEntities())
                {
                    var data = from oi in DB.ORDERINFOes
                               where oi.ORDERDATE == dtDateTime
                               select new { oi.TOTALMONEY };

                    l_i_money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                }

                string date = getDayNameOfWeek(dtDateTime);

                StatChart_Value.Add(new KeyValuePair<string, int>(date, l_i_money));

                dtDateTime = dtDateTime.Value.AddDays(1);
            }
        }

        private void statChartThisMonth()
        {
            DateTime? dtFirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime? dtLastDayOfMonth = dtFirstDayOfMonth.Value.AddMonths(1).AddDays(-1);
            DateTime? dtDateTime = dtFirstDayOfMonth;
            int l_i_money = 0;

            {
                using (var DB = new QLCanTinEntities())
                {
                    var data = from oi in DB.ORDERINFOes
                               where oi.ORDERDATE == dtDateTime
                               select new { oi.TOTALMONEY };

                    l_i_money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                }

                string date = dtDateTime.Value.Day.ToString() + "/" + dtDateTime.Value.Month.ToString();

                StatChart_Value.Add(new KeyValuePair<string, int>(date, l_i_money));

                dtDateTime = dtDateTime.Value.AddDays(1);
            }

            while (dtDateTime < dtLastDayOfMonth)
            {
                using (var DB = new QLCanTinEntities())
                {
                    var data = from oi in DB.ORDERINFOes
                               where oi.ORDERDATE == dtDateTime
                               select new { oi.TOTALMONEY };

                    l_i_money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                }

                string date = dtDateTime.Value.Day.ToString();

                StatChart_Value.Add(new KeyValuePair<string, int>(date, l_i_money));

                dtDateTime = dtDateTime.Value.AddDays(1);
            }

            {
                using (var DB = new QLCanTinEntities())
                {
                    var data = from oi in DB.ORDERINFOes
                               where oi.ORDERDATE == dtDateTime
                               select new { oi.TOTALMONEY };

                    l_i_money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                }

                string date = dtDateTime.Value.Day.ToString() + "/" + dtDateTime.Value.Month.ToString();

                StatChart_Value.Add(new KeyValuePair<string, int>(date, l_i_money));
            }
        }

        private void statChartThisYear()
        {
            DateTime? dtFirstMonthOfYear = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime? dtLastMonthOfYear = dtFirstMonthOfYear.Value.AddYears(1).AddMonths(-1);
            DateTime? dtCurrentMonth = dtFirstMonthOfYear;
            DateTime? dtFirstDayOfMonth = dtCurrentMonth;
            DateTime? dtLastDayOfMonth = dtFirstDayOfMonth.Value.AddMonths(1).AddDays(-1);
            int l_i_money = 0;

            while (dtCurrentMonth <= dtLastMonthOfYear)
            {
                using (var DB = new QLCanTinEntities())
                {
                    var data = from oi in DB.ORDERINFOes
                               where oi.ORDERDATE >= dtFirstDayOfMonth && oi.ORDERDATE <= dtLastDayOfMonth
                               select new { oi.TOTALMONEY };

                    l_i_money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                }

                string date = "Tháng " + dtCurrentMonth.Value.Month.ToString();

                StatChart_Value.Add(new KeyValuePair<string, int>(date, l_i_money));

                dtCurrentMonth = dtCurrentMonth.Value.AddMonths(1);
                dtFirstDayOfMonth = dtCurrentMonth;
                dtLastDayOfMonth = dtFirstDayOfMonth.Value.AddMonths(1).AddDays(-1);
            }
        }

        private void statChartManyYears()
        {
            DateTime? dtLastYear = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime? dtFirstYear = dtLastYear.Value.AddYears(-10);
            DateTime? dtCurrentYear = dtFirstYear;
            int l_i_money = 0;

            while (dtCurrentYear <= dtLastYear)
            {
                DateTime? dtFirstDayOfYear = dtCurrentYear;
                DateTime? dtLastDayOfYear = dtFirstDayOfYear.Value.AddYears(1).AddDays(-1);

                using (var DB = new QLCanTinEntities())
                {
                    var data = from oi in DB.ORDERINFOes
                               where oi.ORDERDATE >= dtFirstDayOfYear && oi.ORDERDATE <= dtLastDayOfYear
                               select new { oi.TOTALMONEY };

                    l_i_money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                }

                string year = dtCurrentYear.Value.Year.ToString();

                StatChart_Value.Add(new KeyValuePair<string, int>(year, l_i_money));

                dtCurrentYear = dtCurrentYear.Value.AddYears(1);
            }
        }

        private void statFoodBetweenTime(DateTime? dtBeginTime, DateTime? dtEndTime)
        {
            StatFood_ChartValue = new ObservableCollection<KeyValuePair<string, int>>();
            int iFirst = 0, iSecond = 0, iThird = 0, iTotal = 0;
            string sFirst = string.Empty, sSecond = string.Empty, sThird = string.Empty;

            using (var DB = new QLCanTinEntities())
            {
                var dataOrderDetail = DB.ORDERDETAILs;
                var dataFood = DB.FOODs;
                var dataOrderInfo = DB.ORDERINFOes;

                var result = from od in dataOrderDetail
                             group od by od.FOODID into g
                             join f in dataFood on g.FirstOrDefault().FOODID equals f.ID
                             join oi in dataOrderInfo on g.FirstOrDefault().ORDERID equals oi.ID
                             where oi.ORDERDATE >= dtBeginTime && oi.ORDERDATE <= dtEndTime
                             select new { Name = f.FOODNAME, Sale = g.Sum(a => a.QUANTITY) };

                foreach (var i in result)
                {
                    int temp = i.Sale;
                    DataLV.Add(new ItemFood(i.Name, temp));
                    iTotal += temp;

                    if (iFirst < temp)
                    {
                        iThird = iSecond;
                        sThird = sSecond;
                        iSecond = iFirst;
                        sSecond = sFirst;
                        iFirst = temp;
                        sFirst = i.Name;
                    }
                    else if (iSecond < temp)
                    {
                        iThird = iSecond;
                        sThird = sSecond;
                        iSecond = temp;
                        sSecond = i.Name;
                    }
                    else if (iSecond < temp)
                    {
                        iThird = temp;
                        sThird = i.Name;
                    }
                }
            }

            string[] strFirst = Regex.Split(sFirst, "   ");
            string[] strSecond = Regex.Split(sSecond, "   ");
            string[] strThird = Regex.Split(sThird, "   ");
            int iOthers = iTotal - (iFirst + iSecond + iThird);

            if (iFirst > 0)
            {
                StatFood_ChartValue.Add(new KeyValuePair<string, int>(strFirst[0], iFirst));
            }
            if (iSecond > 0)
            {
                StatFood_ChartValue.Add(new KeyValuePair<string, int>(strSecond[0], iSecond));
            }
            if (iThird > 0)
            {
                StatFood_ChartValue.Add(new KeyValuePair<string, int>(strThird[0], iThird));
            }
            if (iOthers > 0)
            {
                StatFood_ChartValue.Add(new KeyValuePair<string, int>("Các món còn lại", iOthers));
            }

            g_txt_StatFood_BestSeller = strFirst[0];
            g_dc_StatFood_Chart = StatFood_ChartValue;
        }

        private void checkVisibilityFood()
        {
            if (g_dc_StatFood_Chart.Count == 0)
            {
                this.g_str_visibilityEmpty = staticVarClass.visibility_visible;

                this.g_str_visibilityChartFood = staticVarClass.visibility_hidden;
            }
            else
            {
                this.g_str_visibilityEmpty = staticVarClass.visibility_hidden;

                this.g_str_visibilityChartFood = staticVarClass.visibility_visible;
            }
        }

        private string getDateMonth(DateTime? dtBeginTime, DateTime? dtEndTime)
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

        private int getWeekOfYear(DateTime? date)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date.Value);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.Value.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private void statStartUp()
        {
            g_sd_StatChart_FromTime = new DateTime(DateTime.Today.Year, 1, 1);
            g_sd_StatChart_ToTime = DateTime.Today;
            g_sv_StatChart_Choice = staticVarClass.timeStr_instanceTime;
            g_sd_StatFood_FromTime = new DateTime(DateTime.Today.Year, 1, 1);
            g_sd_StatFood_ToTime = DateTime.Today;
            g_sv_StatFood_Choice = staticVarClass.timeStr_instanceTime;
            StatChart_Value = new ObservableCollection<KeyValuePair<string, int>>();
            DataLV = new ObservableCollection<ItemFood>();

            this.statChart();
            this.checkVisibilityChart();
        }

        #region Sort
        private string _sortColumn;
        private ListSortDirection _sortDirection;
        private ObservableCollection<ItemFood> _g_is_StatFood_DataLV;

        public ObservableCollection<ItemFood> DataLV
        {
            get
            {
                return _g_is_StatFood_DataLV;
            }
            set
            {
                _g_is_StatFood_DataLV = value;
                _g_is_StatFood_Source = new CollectionViewSource();
                _g_is_StatFood_Source.Source = _g_is_StatFood_DataLV;
            }
        }

        private void sort(object parameter)
        {
            string column = parameter as string;

            if (_sortColumn == column)
            {
                // Toggle sorting direction
                _sortDirection = _sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            else
            {
                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;
            }

            _g_is_StatFood_Source.SortDescriptions.Clear();
            _g_is_StatFood_Source.SortDescriptions.Add(new SortDescription(_sortColumn, _sortDirection));
        }

        private void sortSaleByDescending()
        {
            _g_is_StatFood_Source.SortDescriptions.Clear();
            _g_is_StatFood_Source.SortDescriptions.Add(new SortDescription("g_dmb_StatFood_Sale", ListSortDirection.Descending));
        }
        #endregion

        public class ItemFood
        {
            public ItemFood(string name, int sale)
            {
                g_dmb_StatFood_FoodName = name;
                g_dmb_StatFood_Sale = sale;
            }

            public string g_dmb_StatFood_FoodName { get; set; }
            public int g_dmb_StatFood_Sale { get; set; }
        }

        #region Relay Command
        public class RelayCommand : ICommand
        {
            private readonly Action<object> _Execute;
            private readonly Func<object, bool> _CanExecute;

            public RelayCommand(Action<object> execute)
                : this(execute, null)
            {

            }

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
            {
                if (execute == null)
                {
                    throw new ArgumentNullException("execute", "Execute cannot be null.");
                }

                _Execute = execute;
                _CanExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                _Execute(parameter);
            }

            public bool CanExecute(object parameter)
            {
                if (_CanExecute == null)
                {
                    return true;
                }

                return _CanExecute(parameter);
            }
        }
        #endregion
    }
}