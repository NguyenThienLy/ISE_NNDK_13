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
    class StatisticViewModel : BaseViewModel
    {
        public ObservableCollection<KeyValuePair<string, int>> ValueList { get; private set; }

        private DateTime? _g_sd_StaticOrderFood_FromTime;
        private DateTime? _g_sd_StaticOrderFood_ToTime;
        private ObservableCollection<KeyValuePair<string, int>> _g_dc_ChartOrderFood;

        public DateTime? g_sd_StaticOrderFood_FromTime { get => _g_sd_StaticOrderFood_FromTime; set { _g_sd_StaticOrderFood_FromTime = value; OnPropertyChanged(); } }
        public DateTime? g_sd_StaticOrderFood_ToTime { get => _g_sd_StaticOrderFood_ToTime; set { _g_sd_StaticOrderFood_ToTime = value; OnPropertyChanged(); } }
        public ObservableCollection<KeyValuePair<string, int>> g_dc_ChartOrderFood { get => _g_dc_ChartOrderFood; set { _g_dc_ChartOrderFood = value; OnPropertyChanged(); } }

        #region commands.
        public ICommand g_iCm_StaticOrderFood { get; set; }
        #endregion

        public StatisticViewModel()
        {
            g_iCm_StaticOrderFood = new RelayCommand<StatisticView>((p) => { return true; }, (p) =>
            {
                this.staticOrderFood(p);
            });
        }

        private void staticOrderFood(StatisticView p)
        {
            string str_StaticOrderFood_FromTime;
            string str_StaticOrderFood_ToTime;

            if (_g_sd_StaticOrderFood_FromTime.HasValue)
            {
                str_StaticOrderFood_FromTime = _g_sd_StaticOrderFood_FromTime.Value.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (_g_sd_StaticOrderFood_ToTime.HasValue)
            {
                str_StaticOrderFood_ToTime = _g_sd_StaticOrderFood_ToTime.Value.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            var data = dataProvider.Instance.DB.ORDERINFOes;
            //var result = from od in data
            //             where od.ORDERDATE >= _g_sd_StaticOrderFood_FromTime && od.ORDERDATE <= _g_sd_StaticOrderFood_ToTime

            //             select new {date = od.ORDERDATE};
            //result.ToList();

            var countFirstDay = data.Where(x => x.ORDERDATE == _g_sd_StaticOrderFood_FromTime).Count();

            var countLastDay = data.Where(x => x.ORDERDATE == _g_sd_StaticOrderFood_ToTime).Count();

            var otherDay = data.Where(x => x.ORDERDATE > _g_sd_StaticOrderFood_FromTime && x.ORDERDATE < _g_sd_StaticOrderFood_ToTime)
                           .GroupBy(od => od.ORDERDATE)
                           .Select(group => new { Date = group.Key, Count = group.Count() });


            this.ValueList = new ObservableCollection<KeyValuePair<string, int>>();
            string[] strFirstDay = _g_sd_StaticOrderFood_FromTime.ToString().Split(' ');
            ValueList.Add(new KeyValuePair<string, int>(strFirstDay[0], countFirstDay));

            foreach (var i in otherDay)
            {
                string day = i.Date.ToString();
                string[] str = day.Split(' ');
                ValueList.Add(new KeyValuePair<string, int>(str[0], i.Count));
            }

            string[] strLastDay = _g_sd_StaticOrderFood_ToTime.ToString().Split(' ');
            ValueList.Add(new KeyValuePair<string, int>(strLastDay[0], countLastDay));

            g_dc_ChartOrderFood = ValueList;
        }
    }
}
