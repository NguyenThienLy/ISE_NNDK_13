using CanTeenManagement.Model;
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

        private string _g_str_visible { get; set; }
        public string g_str_visible
        {
            get => _g_str_visible;
            set
            {
                _g_str_visible = value;
                OnPropertyChanged();
            }
        }

        #region command.
        public ICommand g_iCm_LoadedItemsControlCommand { get; set; }

        public ICommand g_iCm_ClickButtonBackCommand { get; set; }

        public ICommand g_iCm_ClickButtonNextCommand { get; set; }

        public ICommand g_iCm_MouseEnterItemControlCommand { get; set; }

        public ICommand g_iCm_MouseLeaveItemControlCommand { get; set; }
        #endregion

        public DashboardViewModel()
        {
            g_iCm_LoadedItemsControlCommand = new RelayCommand<ItemsControl>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            g_iCm_ClickButtonBackCommand = new RelayCommand<ORDERFOOD>((p) => { return true; }, (p) =>
            {
                this.clickBack(p);
            });

            g_iCm_ClickButtonNextCommand = new RelayCommand<ORDERFOOD>((p) => { return true; }, (p) =>
            {
                this.clickNext(p);
            });

            g_iCm_MouseEnterItemControlCommand = new RelayCommand<ORDERFOOD>((p) => { return true; }, (p) =>
            {
                this.mouseEnter(p);
            });

            g_iCm_MouseLeaveItemControlCommand = new RelayCommand<ORDERFOOD>((p) => { return true; }, (p) =>
            {
                this.mouseLeave(p);
            });
        }

        private void loaded(ItemsControl p)
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

            this.g_i_index = 0;
            this.g_str_visible = "Hidden";

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (s, ev) => clickNext(g_obCl_orderFood[0]);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        private void clickBack(ORDERFOOD p)
        {
            int l_quantityFood = g_obCl_orderFood.Count();

            if (g_i_index > 0)
                this.g_i_index--;
            else if (g_i_index == 0)
                this.g_i_index = l_quantityFood - 1;
        }

        private void clickNext(ORDERFOOD p)
        {
            int l_quantityFood = g_obCl_orderFood.Count();

            if (g_i_index < l_quantityFood - 1)
                this.g_i_index++;
            else if (g_i_index == l_quantityFood - 1)
                this.g_i_index = 0;
        }

        private void mouseEnter(ORDERFOOD p)
        {
            g_str_visible = "Visible";
        }

        private void mouseLeave(ORDERFOOD p)
        {
            g_str_visible = "Hidden";
        }
    }
}
