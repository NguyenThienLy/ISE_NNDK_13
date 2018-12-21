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
using System.Globalization;
using System.ComponentModel;
using System.Windows.Documents;

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
        private List<ItemFood> _g_is_StatFood_Source;
        private ObservableCollection<KeyValuePair<string, int>> _g_dc_StatFood_Chart;

        public DateTime? g_sd_StatFood_FromTime { get => _g_sd_StatFood_FromTime; set { _g_sd_StatFood_FromTime = value; OnPropertyChanged(); } }
        public DateTime? g_sd_StatFood_ToTime { get => _g_sd_StatFood_ToTime; set { _g_sd_StatFood_ToTime = value; OnPropertyChanged(); } }
        public string g_sv_StatFood_Choice { get => _g_sv_StatFood_Choice; set { _g_sv_StatFood_Choice = value; OnPropertyChanged(); } }
        public string g_txt_StatFood_BestSeller { get => _g_txt_StatFood_BestSeller; set { _g_txt_StatFood_BestSeller = value; OnPropertyChanged(); } }
        public List<ItemFood> g_is_StatFood_Source { get => _g_is_StatFood_Source; set { _g_is_StatFood_Source = value; OnPropertyChanged(); } }
        public List<ItemFood> StatFood_Value { get; private set; }
        public ObservableCollection<KeyValuePair<string, int>> g_dc_StatFood_Chart { get => _g_dc_StatFood_Chart; set { _g_dc_StatFood_Chart = value; OnPropertyChanged(); } }
        public ObservableCollection<KeyValuePair<string, int>> StatFood_ChartValue { get; private set; }
        #endregion

        #region commands.
        public ICommand g_iCm_StatChart { get; set; }
        public ICommand g_iCm_StatFood { get; set; }
        public ICommand g_iCm_StatRevenue { get; set; }
        #endregion

        public int MAX_COLUMN = 20;

        public StatisticViewModel()
        {
            g_sd_StatChart_FromTime = new DateTime(DateTime.Today.Year, 1, 1);
            g_sd_StatChart_ToTime = DateTime.Today;
            g_sv_StatChart_Choice = "Khoảng thời gian";
            g_sd_StatFood_FromTime = new DateTime(DateTime.Today.Year, 1, 1);
            g_sd_StatFood_ToTime = DateTime.Today;
            g_sv_StatFood_Choice = "Khoảng thời gian";

            g_iCm_StatChart = new RelayCommand<StatisticView>((p) => { return true; }, (p) =>
            {
                this.statChart(p);
            });

            g_iCm_StatFood = new RelayCommand<StatisticView>((p) => { return true; }, (p) =>
            {
                this.statFood(p);
            });
        }

        private void statChart(StatisticView p)
        {
            this.StatChart_Value = new ObservableCollection<KeyValuePair<string, int>>();

            if (_g_sv_StatChart_Choice == "Theo tuần")
            {
                statChartByWeek();
            }
            else if (_g_sv_StatChart_Choice == "Tuần hiện tại")
            {
                statChartThisWeek();
            }
            else if (_g_sv_StatChart_Choice == "Tháng hiện tại")
            {
                statChartThisMonth();
            }
            else if (_g_sv_StatChart_Choice == "Năm hiện tại")
            {
                statChartThisYear();
            }
            else if (g_sv_StatChart_Choice == "Nhiều năm")
            {
                statChartManyYears();
            }
            else
            {
                if (!_g_sd_StatChart_FromTime.HasValue || !_g_sd_StatChart_ToTime.HasValue)    // khi người dùng quên chọn ngày hoặc kiểu thống kê thì chương trình ko bị crash
                {
                    MessageBox.Show("Hãy chọn khoảng thời gian!", "Error", 0, 0);
                    return;
                }

                statChartByTime();
            }

            g_dc_StatChart_Chart = StatChart_Value;
        }

        private void statFood(StatisticView p)
        {
            if (_g_sv_StatFood_Choice == "Tuần hiện tại")
            {
                DateTime? dtFirstDayOfWeek = getLastWeekDay(DayOfWeek.Monday);
                DateTime? dtLastDayOfWeek = dtFirstDayOfWeek.Value.AddDays(6);

                statFoodBetweenTime(dtFirstDayOfWeek, dtLastDayOfWeek);
            }
            else if (_g_sv_StatFood_Choice == "Tháng hiện tại")
            {
                DateTime? dtFirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime? dtLastDayOfMonth = dtFirstDayOfMonth.Value.AddMonths(1).AddDays(-1);

                statFoodBetweenTime(dtFirstDayOfMonth, dtLastDayOfMonth);
            }
            else if (_g_sv_StatFood_Choice == "Năm hiện tại")
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
        }

        private void statChartByTime()
        {

            int iTimeSpan = (_g_sd_StatChart_ToTime - _g_sd_StatChart_FromTime).Value.Days;
            int iDayNum = iTimeSpan + 1;

            if (iTimeSpan > 366)
            {
                MessageBox.Show("Thời gian thống kê quá dài!", "Error", 0, 0);

                return;
            }

            if (iDayNum <= 31)
            {
                DateTime? dtDateTime = _g_sd_StatChart_FromTime;

                while (dtDateTime <= _g_sd_StatChart_ToTime)
                {
                    var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                               where oi.ORDERDATE == dtDateTime
                               select new { oi.TOTALMONEY };

                    int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                    string date = dtDateTime.Value.Day.ToString() + "/" + dtDateTime.Value.Month.ToString();

                    StatChart_Value.Add(new KeyValuePair<string, int>(date, money));

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

                    var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                               where oi.ORDERDATE >= dtBeginTime && oi.ORDERDATE <= dtEndTime
                               select new { oi.TOTALMONEY };

                    int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                    string date = getDateMonth(dtBeginTime, dtEndTime);

                    StatChart_Value.Add(new KeyValuePair<string, int>(date, money));

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
                    var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                               where oi.ORDERDATE >= dtBeginTime && oi.ORDERDATE <= dtEndTime
                               select new { oi.TOTALMONEY };

                    int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                    string date = "Tháng " + dtBeginTime.Value.Month.ToString();

                    StatChart_Value.Add(new KeyValuePair<string, int>(date, money));

                    dtBeginTime = dtEndTime.Value.AddDays(1);
                    dtEndTime = dtBeginTime.Value.AddMonths(1).AddDays(-1);
                }
            }
        }

        private void statChartByWeek()
        {
            DateTime? dtFirstDayOfWeek = g_sd_StatChart_FromTime;
            DateTime? dtLastDayOfWeek = dtFirstDayOfWeek.Value.AddDays(6);

            for (int i = 0; i < MAX_COLUMN; i++)
            {
                if (dtFirstDayOfWeek > g_sd_StatChart_ToTime)
                {
                    break;
                }

                var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                           where oi.ORDERDATE >= dtFirstDayOfWeek && oi.ORDERDATE <= dtLastDayOfWeek
                           select new { oi.TOTALMONEY };

                int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                string week = "Tuần " + getWeekOfYear(dtFirstDayOfWeek).ToString() + "\n" + getDateMonth(dtFirstDayOfWeek, dtLastDayOfWeek);

                StatChart_Value.Add(new KeyValuePair<string, int>(week, money));

                dtFirstDayOfWeek = dtLastDayOfWeek.Value.AddDays(1);
                dtLastDayOfWeek = dtFirstDayOfWeek.Value.AddDays(6);
            }
        }

        private void statChartThisWeek()
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

                StatChart_Value.Add(new KeyValuePair<string, int>(date, money));

                dtDateTime = dtDateTime.Value.AddDays(1);
            }
        }

        private void statChartThisMonth()
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

                StatChart_Value.Add(new KeyValuePair<string, int>(date, money));

                dtDateTime = dtDateTime.Value.AddDays(1);
            }

            while (dtDateTime < dtLastDayOfMonth)
            {
                var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                           where oi.ORDERDATE == dtDateTime
                           select new { oi.TOTALMONEY };

                int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                string date = dtDateTime.Value.Day.ToString();

                StatChart_Value.Add(new KeyValuePair<string, int>(date, money));

                dtDateTime = dtDateTime.Value.AddDays(1);
            }

            {
                var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                           where oi.ORDERDATE == dtDateTime
                           select new { oi.TOTALMONEY };

                int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                string date = dtDateTime.Value.Day.ToString() + "/" + dtDateTime.Value.Month.ToString();

                StatChart_Value.Add(new KeyValuePair<string, int>(date, money));
            }
        }

        private void statChartThisYear()
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

                StatChart_Value.Add(new KeyValuePair<string, int>(date, money));

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

            while (dtCurrentYear <= dtLastYear)
            {
                DateTime? dtFirstDayOfYear = dtCurrentYear;
                DateTime? dtLastDayOfYear = dtFirstDayOfYear.Value.AddYears(1).AddDays(-1);

                var data = from oi in dataProvider.Instance.DB.ORDERINFOes
                           where oi.ORDERDATE >= dtFirstDayOfYear && oi.ORDERDATE <= dtLastDayOfYear
                           select new { oi.TOTALMONEY };

                int money = data.AsEnumerable().Sum(oi => oi.TOTALMONEY) ?? default(int);
                string year = dtCurrentYear.Value.Year.ToString();

                StatChart_Value.Add(new KeyValuePair<string, int>(year, money));

                dtCurrentYear = dtCurrentYear.Value.AddYears(1);
            }
        }

        private void statFoodBetweenTime(DateTime? dtBeginTime, DateTime? dtEndTime)
        {
            this.StatFood_Value = new List<ItemFood>();
            this.StatFood_ChartValue = new ObservableCollection<KeyValuePair<string, int>>();

            var dataOrderDetail = dataProvider.Instance.DB.ORDERDETAILs;
            var dataFood = dataProvider.Instance.DB.FOODs;
            var dataOrderInfo = dataProvider.Instance.DB.ORDERINFOes;

            var result = from od in dataOrderDetail
                         group od by od.FOODID into g
                         join f in dataFood on g.FirstOrDefault().FOODID equals f.ID
                         join oi in dataOrderInfo on g.FirstOrDefault().ORDERID equals oi.ID
                         where oi.ORDERDATE >= dtBeginTime && oi.ORDERDATE <= dtEndTime
                         select new { Name = f.FOODNAME, Sale = g.Sum(a => a.QUANTITY) };

            int iFirst = 0, iSecond = 0, iThird = 0, iTotal = 0;
            string sFirst = "", sSecond = "", sThird = "";

            foreach (var i in result)
            {
                int temp = i.Sale ?? default(int);
                StatFood_Value.Add(new ItemFood(i.Name, temp));
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

            string[] strFirst = Regex.Split(sFirst, "   ");
            string[] strSecond = Regex.Split(sSecond, "   ");
            string[] strThird = Regex.Split(sThird, "   ");

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

            int iOthers = iTotal - (iFirst + iSecond + iThird);
            StatFood_ChartValue.Add(new KeyValuePair<string, int>("Các món còn lại", iOthers));

            g_txt_StatFood_BestSeller = "Bán chạy nhất:  " + strFirst[0];
            g_is_StatFood_Source = StatFood_Value;
            g_dc_StatFood_Chart = StatFood_ChartValue;
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

        #region Public attached properties

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(StatisticViewModel),
                new UIPropertyMetadata(
                    null,
                    (o, e) =>
                    {
                        ItemsControl listView = o as ItemsControl;
                        if (listView != null)
                        {
                            if (!GetAutoSort(listView)) // Don't change click handler if AutoSort enabled
                            {
                                if (e.OldValue != null && e.NewValue == null)
                                {
                                    listView.RemoveHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }
                                if (e.OldValue == null && e.NewValue != null)
                                {
                                    listView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }
                            }
                        }
                    }
                )
            );

        public static bool GetAutoSort(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoSortProperty);
        }

        public static void SetAutoSort(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoSortProperty, value);
        }

        // Using a DependencyProperty as the backing store for AutoSort.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoSortProperty =
            DependencyProperty.RegisterAttached(
                "AutoSort",
                typeof(bool),
                typeof(StatisticViewModel),
                new UIPropertyMetadata(
                    false,
                    (o, e) =>
                    {
                        ListView listView = o as ListView;
                        if (listView != null)
                        {
                            if (GetCommand(listView) == null) // Don't change click handler if a command is set
                            {
                                bool oldValue = (bool)e.OldValue;
                                bool newValue = (bool)e.NewValue;
                                if (oldValue && !newValue)
                                {
                                    listView.RemoveHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }
                                if (!oldValue && newValue)
                                {
                                    listView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                                }
                            }
                        }
                    }
                )
            );

        public static string GetPropertyName(DependencyObject obj)
        {
            return (string)obj.GetValue(PropertyNameProperty);
        }

        public static void SetPropertyName(DependencyObject obj, string value)
        {
            obj.SetValue(PropertyNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for PropertyName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.RegisterAttached(
                "PropertyName",
                typeof(string),
                typeof(StatisticViewModel),
                new UIPropertyMetadata(null)
            );

        public static bool GetShowSortGlyph(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowSortGlyphProperty);
        }

        public static void SetShowSortGlyph(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowSortGlyphProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowSortGlyph.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowSortGlyphProperty =
            DependencyProperty.RegisterAttached("ShowSortGlyph", typeof(bool), typeof(StatisticViewModel), new UIPropertyMetadata(true));

        public static ImageSource GetSortGlyphAscending(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(SortGlyphAscendingProperty);
        }

        public static void SetSortGlyphAscending(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(SortGlyphAscendingProperty, value);
        }

        // Using a DependencyProperty as the backing store for SortGlyphAscending.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SortGlyphAscendingProperty =
            DependencyProperty.RegisterAttached("SortGlyphAscending", typeof(ImageSource), typeof(StatisticViewModel), new UIPropertyMetadata(null));

        public static ImageSource GetSortGlyphDescending(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(SortGlyphDescendingProperty);
        }

        public static void SetSortGlyphDescending(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(SortGlyphDescendingProperty, value);
        }

        // Using a DependencyProperty as the backing store for SortGlyphDescending.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SortGlyphDescendingProperty =
            DependencyProperty.RegisterAttached("SortGlyphDescending", typeof(ImageSource), typeof(StatisticViewModel), new UIPropertyMetadata(null));

        #endregion

        #region Private attached properties

        private static GridViewColumnHeader GetSortedColumnHeader(DependencyObject obj)
        {
            return (GridViewColumnHeader)obj.GetValue(SortedColumnHeaderProperty);
        }

        private static void SetSortedColumnHeader(DependencyObject obj, GridViewColumnHeader value)
        {
            obj.SetValue(SortedColumnHeaderProperty, value);
        }

        // Using a DependencyProperty as the backing store for SortedColumn.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty SortedColumnHeaderProperty =
            DependencyProperty.RegisterAttached("SortedColumnHeader", typeof(GridViewColumnHeader), typeof(StatisticViewModel), new UIPropertyMetadata(null));

        #endregion

        #region Column header click event handler

        private static void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked != null && headerClicked.Column != null)
            {
                string propertyName = GetPropertyName(headerClicked.Column);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    ListView listView = GetAncestor<ListView>(headerClicked);
                    if (listView != null)
                    {
                        ICommand command = GetCommand(listView);
                        if (command != null)
                        {
                            if (command.CanExecute(propertyName))
                            {
                                command.Execute(propertyName);
                            }
                        }
                        else if (GetAutoSort(listView))
                        {
                            ApplySort(listView.Items, propertyName, listView, headerClicked);
                        }
                    }
                }
            }
        }

        #endregion

        #region Helper methods

        public static T GetAncestor<T>(DependencyObject reference) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(reference);
            while (!(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent != null)
                return (T)parent;
            else
                return null;
        }

        public static void ApplySort(ICollectionView view, string propertyName, ListView listView, GridViewColumnHeader sortedColumnHeader)
        {
            ListSortDirection direction = ListSortDirection.Ascending;
            if (view.SortDescriptions.Count > 0)
            {
                SortDescription currentSort = view.SortDescriptions[0];
                if (currentSort.PropertyName == propertyName)
                {
                    if (currentSort.Direction == ListSortDirection.Ascending)
                        direction = ListSortDirection.Descending;
                    else
                        direction = ListSortDirection.Ascending;
                }
                view.SortDescriptions.Clear();

                GridViewColumnHeader currentSortedColumnHeader = GetSortedColumnHeader(listView);
                if (currentSortedColumnHeader != null)
                {
                    RemoveSortGlyph(currentSortedColumnHeader);
                }
            }
            if (!string.IsNullOrEmpty(propertyName))
            {
                view.SortDescriptions.Add(new SortDescription(propertyName, direction));
                if (GetShowSortGlyph(listView))
                    AddSortGlyph(
                        sortedColumnHeader,
                        direction,
                        direction == ListSortDirection.Ascending ? GetSortGlyphAscending(listView) : GetSortGlyphDescending(listView));
                SetSortedColumnHeader(listView, sortedColumnHeader);
            }
        }

        private static void AddSortGlyph(GridViewColumnHeader columnHeader, ListSortDirection direction, ImageSource sortGlyph)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(columnHeader);
            adornerLayer.Add(
                new SortGlyphAdorner(
                    columnHeader,
                    direction,
                    sortGlyph
                    ));
        }

        private static void RemoveSortGlyph(GridViewColumnHeader columnHeader)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(columnHeader);
            Adorner[] adorners = adornerLayer.GetAdorners(columnHeader);
            if (adorners != null)
            {
                foreach (Adorner adorner in adorners)
                {
                    if (adorner is SortGlyphAdorner)
                        adornerLayer.Remove(adorner);
                }
            }
        }

        #endregion

        #region SortGlyphAdorner nested class

        private class SortGlyphAdorner : Adorner
        {
            private GridViewColumnHeader _columnHeader;
            private ListSortDirection _direction;
            private ImageSource _sortGlyph;

            public SortGlyphAdorner(GridViewColumnHeader columnHeader, ListSortDirection direction, ImageSource sortGlyph)
                : base(columnHeader)
            {
                _columnHeader = columnHeader;
                _direction = direction;
                _sortGlyph = sortGlyph;
            }

            private Geometry GetDefaultGlyph()
            {
                double x1 = _columnHeader.ActualWidth - 13;
                double x2 = x1 + 10;
                double x3 = x1 + 5;
                double y1 = _columnHeader.ActualHeight / 2 - 3;
                double y2 = y1 + 5;

                if (_direction == ListSortDirection.Ascending)
                {
                    double tmp = y1;
                    y1 = y2;
                    y2 = tmp;
                }

                PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();
                pathSegmentCollection.Add(new LineSegment(new Point(x2, y1), true));
                pathSegmentCollection.Add(new LineSegment(new Point(x3, y2), true));

                PathFigure pathFigure = new PathFigure(
                    new Point(x1, y1),
                    pathSegmentCollection,
                    true);

                PathFigureCollection pathFigureCollection = new PathFigureCollection();
                pathFigureCollection.Add(pathFigure);

                PathGeometry pathGeometry = new PathGeometry(pathFigureCollection);
                return pathGeometry;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);

                if (_sortGlyph != null)
                {
                    double x = _columnHeader.ActualWidth - 13;
                    double y = _columnHeader.ActualHeight / 2 - 5;
                    Rect rect = new Rect(x, y, 10, 10);
                    drawingContext.DrawImage(_sortGlyph, rect);
                }
                else
                {
                    drawingContext.DrawGeometry(Brushes.LightGray, new Pen(Brushes.Gray, 1.0), GetDefaultGlyph());
                }
            }
        }

        #endregion
    }
}