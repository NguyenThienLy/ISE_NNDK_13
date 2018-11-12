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
    public class CustomersViewModel: BaseViewModel
    {
        private ObservableCollection<CUSTOMER> _listCustomer_g;
        public ObservableCollection<CUSTOMER> listCustomer_g { get=> _listCustomer_g; set { _listCustomer_g = value; OnPropertyChanged(); } }

        #region commands.
        public ICommand iCm_LoadedCommand_g { get; set; }

        public ICommand iCm_ClickAddCommand_g { get; set; }

        public ICommand iCm_ClickEditInfoCommand_g { get; set; }

        public ICommand iCm_ClickSaveInfoCommand_g { get; set; }

        public ICommand iCm_ClickDetailCommand_g { get; set; }
        #endregion

        public CustomersViewModel()
        {
            this.listCustomer_g = new ObservableCollection<CUSTOMER>(dataProvider.Instance.DB.CUSTOMERs);

            iCm_LoadedCommand_g = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.loaded(p);
            });

            iCm_ClickAddCommand_g = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickAdd(p);
            });

            iCm_ClickEditInfoCommand_g = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickEdit(p);
            });

            iCm_ClickSaveInfoCommand_g = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickSave(p);
            });

            iCm_ClickDetailCommand_g = new RelayCommand<CustomersView>((p) => { return true; }, (p) =>
            {
                this.clickDetail(p);
            });

        }


        private void loaded(CustomersView p)
        {
            p.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void clickAdd(CustomersView p)
        {
            p.rDefTop.Height = new GridLength(40, GridUnitType.Star);
        }

        private void clickEdit(CustomersView p)
        {
            p.rDefTop.Height = new GridLength(40, GridUnitType.Star);
        }

        private void clickSave(CustomersView p)
        {
            p.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void clickDetail(CustomersView p)
        {
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
