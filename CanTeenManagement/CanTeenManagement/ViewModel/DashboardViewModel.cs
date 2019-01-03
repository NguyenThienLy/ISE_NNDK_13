using CanTeenManagement.Model;
using CanTeenManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CanTeenManagement.ViewModel
{
    class DashboardViewModel : BaseViewModel
    {
        private ObservableCollection<ORDERFOOD> _g_obCl_orderFood;
        public ObservableCollection<ORDERFOOD> g_obCl_orderFood
        {
            get => _g_obCl_orderFood;
            set
            {
                _g_obCl_orderFood = value;
                OnPropertyChanged();
            }
        }

        // Get list foods from database.
        private ObservableCollection<FOOD> _g_obCl_food { get; set; }
        public ObservableCollection<FOOD> g_obCl_food
        {
            get => _g_obCl_food;
            set
            {
                _g_obCl_food = value;
                OnPropertyChanged();
            }
        }

        private int _g_i_index { get; set; }
        public int g_i_index
        {
            get => _g_i_index;
            set
            {
                _g_i_index = value;
                OnPropertyChanged();
            }
        }

        DispatcherTimer g_timer = null;

        #region command.
        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_UnLoadedCommand { get; set; }

        public ICommand g_iCm_ClickButtonBackCommand { get; set; }

        public ICommand g_iCm_ClickButtonNextCommand { get; set; }

        public ICommand g_iCm_MouseEnterItemControlCommand { get; set; }

        public ICommand g_iCm_MouseLeaveItemControlCommand { get; set; }
        #endregion

        public DashboardViewModel()
        {
            this.inItSupport();

            g_iCm_LoadedCommand = new RelayCommand<DashBoardView>((p) => { return true; }, (p) =>
            {
                this.loaded();
            });

            g_iCm_UnLoadedCommand = new RelayCommand<DashBoardView>((p) => { return true; }, (p) =>
            {
                this.unloaded();
            });

            g_iCm_ClickButtonBackCommand = new RelayCommand<DashBoardView>((p) => { return true; }, (p) =>
            {
                this.clickBack();
            });

            g_iCm_ClickButtonNextCommand = new RelayCommand<DashBoardView>((p) => { return true; }, (p) =>
            {
                this.clickNext();
            });

            g_iCm_MouseEnterItemControlCommand = new RelayCommand<ItemsControl>((p) => { return true; }, (p) =>
            {
                this.mouseEnter();
            });

            g_iCm_MouseLeaveItemControlCommand = new RelayCommand<ItemsControl>((p) => { return true; }, (p) =>
            {
                this.mouseLeave();
            });
        }

        private void inItSupport()
        {
            this.g_i_index = 0;
            this.g_timer = new DispatcherTimer();
            this.g_timer.Tick += (s, ev) => clickNext();
            this.g_timer.Interval = new TimeSpan(0, 0, 5);
        }

        private void unloaded()
        {
            this.g_timer.Stop();
        }

        private void loaded()
        {
            this.loadData();
            this.g_timer.Start();
        }

        private void loadData()
        {
            if (this.g_obCl_orderFood != null)
                this.g_obCl_orderFood.Clear();
            if (this.g_obCl_food != null)
                this.g_obCl_food.Clear();

            this.g_obCl_orderFood = new ObservableCollection<ORDERFOOD>();
            this.g_obCl_food = new ObservableCollection<FOOD>(dataProvider.Instance.DB.FOODs);

            foreach (FOOD food in g_obCl_food)
            {
                ORDERFOOD t_orderFood = new ORDERFOOD(food);
                g_obCl_orderFood.Add(t_orderFood);
            }

        }

        private void clickBack()
        {
            int l_quantityFood = g_obCl_orderFood.Count();

            if (g_i_index > 0)
                this.g_i_index--;
            else if (g_i_index == 0)
                this.g_i_index = l_quantityFood - 1;
        }

        private void clickNext()
        {
            int l_quantityFood = g_obCl_orderFood.Count();

            if (g_i_index < l_quantityFood - 1)
                this.g_i_index++;
            else if (g_i_index == l_quantityFood - 1)
                this.g_i_index = 0;
        }

        private void mouseEnter()
        {
            //g_str_visible = "Visible";
        }

        private void mouseLeave()
        {
            //g_str_visible = "Hidden";
        }
    }
}
