using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanTeenManagement.View;
using CanTeenManagement.Model;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace CanTeenManagement.ViewModel
{
    public class CustomersViewModel : BaseViewModel
    {
        private ObservableCollection<CUSTOMER> _g_listCustomer;
        public ObservableCollection<CUSTOMER> g_listCustomer { get => _g_listCustomer; set { _g_listCustomer = value; OnPropertyChanged(); } }

        #region commands.
        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_ClickAddCommand { get; set; }

        public ICommand g_iCm_ClickEditCommand { get; set; }

        public ICommand g_iCm_ClickSaveCommand { get; set; }

        public ICommand g_iCm_ClickDetailCommand { get; set; }
        #endregion

        public CustomersViewModel()
        {
            //this.loadData();

            g_iCm_LoadedCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            g_iCm_ClickAddCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickAdd(p);
            });

            g_iCm_ClickEditCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickEdit(p);
            });

            g_iCm_ClickSaveCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickSave(p);
            });

            g_iCm_ClickDetailCommand = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickDetail(p);
            });
        }

        private void loadData()
        {
            this.g_listCustomer = new ObservableCollection<CUSTOMER>(dataProvider.Instance.DB.CUSTOMERs.ToList());
        }

        private void loaded(CustomersView p)
        {
            if (p == null)
                return;

            //p.rDefTop.Height = 0;

            p.rDefTop.Height = new GridLength(40, GridUnitType.Star);
        }

        private void clickAdd(CustomersView p)
        {
            if (p == null)
                return;

            //p.rDefTop.Height = 40;

            p.rDefTop.Height = new GridLength(40, GridUnitType.Star);
        }

        private void clickEdit(CustomersView p)
        {
            if (p == null)
                return;

            //p.rDefTop.Height = 40;
            p.rDefTop.Height = new GridLength(40, GridUnitType.Star);
        }

        private void clickSave(CustomersView p)
        {
            if (p == null)
                return;

            //p.rDefTop.Height = 0;
            p.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void clickDetail(CustomersView p)
        {
            if (p == null)
                return;

            MainWindow mainWd = MainWindow.Instance;
            mainWd.Opacity = 0.5;
            p.Opacity = 0.5;

            DetailCustomersView detailCusView = new DetailCustomersView();

            detailCusView.ShowDialog();

            mainWd.Opacity = 100;
            p.Opacity = 100;
        }
    }
}


