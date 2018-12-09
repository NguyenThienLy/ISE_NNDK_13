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
    class OrderDoneViewModel : BaseViewModel
    {
        private ObservableCollection<ORDERQUEUE> _g_list_OrderDone;
        public ObservableCollection<ORDERQUEUE> g_list_OrderDone
        {
            get => _g_list_OrderDone;
            set { _g_list_OrderDone = value; OnPropertyChanged(); }
        }

        public ICommand g_iCm_LoadedItemsControlCommand { get; set; }
        public ICommand g_iCm_ClickCloseWindowCommand { get; set; }
        public ICommand g_iCm_MouseLeftButtonDownCommand { get; set; }
        public OrderDoneViewModel()
        {
            g_iCm_LoadedItemsControlCommand = new RelayCommand<OrderDoneView>((p) => { return true; }, (p) =>
            {
                this.loadData(p);
            });

            g_iCm_ClickCloseWindowCommand = new RelayCommand<OrderDoneView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            g_iCm_MouseLeftButtonDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                this.mouseLeftButtonDown(p);
            });
        }

        private void clickCloseWindow(OrderDoneView p)
        {
            if (g_list_OrderDone != null)
            {
                g_list_OrderDone.Clear();
            }

            p.Close();
        }

        private void loadData(OrderDoneView p)
        {
            SortFoodView sortFoodView = SortFoodView.Instance;

            if (sortFoodView.DataContext == null)
            {
                return;
            }

            var l_sortFoodVM = sortFoodView.DataContext as SortFoodViewModel;

            this.g_list_OrderDone = new ObservableCollection<ORDERQUEUE>(l_sortFoodVM.g_list_OrderComplete);

        }

        private void mouseLeftButtonDown(Window p)
        {
            if (p == null)
                return;

            p.DragMove();
        }
    }
}
