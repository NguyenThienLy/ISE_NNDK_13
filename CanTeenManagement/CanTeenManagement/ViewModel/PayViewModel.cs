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
        private ObservableCollection<PAYFOOD> _g_obCl_payFood;
        public ObservableCollection<PAYFOOD> g_obCl_payFood
        {
            get => _g_obCl_payFood;
            set
            {
                _g_obCl_payFood = value;
                OnPropertyChanged();
            }
        }

        private double _g_d_sumPrice;
        public double g_d_sumPrice
        {
            get => _g_d_sumPrice;

            set
            {
                _g_d_sumPrice = value;
                OnPropertyChanged();
            }
        }

        #region command.
        public ICommand g_iCm_LoadedItemsControlCommand { get; set; }

        public ICommand g_iCm_ClickPayButtonCommand { get; set; }

        public ICommand g_iCm_ClickCloseWindowCommand { get; set; }
        #endregion

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

            g_iCm_ClickCloseWindowCommand = new RelayCommand<PayView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });
        }

        private void initSupport()
        {
            this.g_d_sumPrice = 0.0;
            this.g_obCl_payFood = new ObservableCollection<PAYFOOD>();
        }

        private void loaded(ItemsControl p)
        {
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            this.g_obCl_payFood = l_orderVM.g_obCl_orderFood;
            // Sum price in order food.
            this.g_d_sumPrice = this.getSumPrice();
        }

        private double getSumPrice()
        {
            int i = 0;
            double l_d_sumPrice = 0.0;

            for (i = 0; i < this.g_obCl_payFood.Count; i++)
            {
                l_d_sumPrice += this.g_obCl_payFood[i].QUANTITY * this.g_obCl_payFood[i].PRICESALE;
            }

            return l_d_sumPrice;
        }

        private void clickPayButton(Button p)
        {
            OrderView orderView = OrderView.Instance;

            if (orderView.DataContext == null)
                return;

            var l_orderVM = orderView.DataContext as OrderViewModel;

            // Reset affter pay in orderView.
            l_orderVM.g_obCl_orderFood.Clear();
            l_orderVM.g_i_currOrderFood = 0;           
        }

        private void clickCloseWindow(PayView p)
        {
            p.Close();
        }
    }
}
