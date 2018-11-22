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

namespace CanTeenManagement.ViewModel
{
    class PayViewModel : BaseViewModel
    {
        private List<QUANTITYFOOD> _g_lst_OrderFood;
        public List<QUANTITYFOOD> g_lst_OrderFood
        {
            get => _g_lst_OrderFood;
            set
            {
                _g_lst_OrderFood = value;
                OnPropertyChanged();
            }
        }

        private string _g_str_sumPrice;
        public string g_str_sumPrice
        {
            get => _g_str_sumPrice;

            set
            {
                _g_str_sumPrice = value;
                OnPropertyChanged();
            }
        }

        public ICommand g_iCm_LoadedItemsControlCommand { get; set; }

        public ICommand g_iCm_ClickPayButtonCommand { get; set; }

        public PayViewModel()
        {
            this.initSupport();

            g_iCm_LoadedItemsControlCommand = new RelayCommand<ItemsControl>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            g_iCm_ClickPayButtonCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                this.clickPayButton(p);
            });
        }

        private void initSupport()
        {
            this.g_lst_OrderFood = new List<QUANTITYFOOD>();
            this.g_str_sumPrice = "0 VNĐ";
        }

        private void loaded(ItemsControl p)
        {
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            this.g_lst_OrderFood = l_orderVM.g_lst_orderFood;
            // Sum price in order food.
            this.g_str_sumPrice = this.getSumPrice();
        }

        private string getSumPrice()
        {
            int i = 0;
            double l_i_sumPrice = 0;

            for (i = 0; i < this.g_lst_OrderFood.Count; i++)
            {
                // price food = price * quantity * sale;
                l_i_sumPrice += this.g_lst_OrderFood[i].PRICE * this.g_lst_OrderFood[i].QUANTITY * (double)((100 - this.g_lst_OrderFood[i].SALE) / 100);
            }

            string l_result = String.Format("{0:0.00}", l_i_sumPrice) + " VNĐ";

            return l_result;
        }

        private void clickPayButton(Button p)
        {
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            // Reset affter pay in orderView.
            l_orderVM.g_lst_orderFood.Clear();
            l_orderVM.g_i_currOrderFood = 0;           
        }
    }
}
