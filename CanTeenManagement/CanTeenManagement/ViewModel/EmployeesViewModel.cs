using CanTeenManagement.Model;
using CanTeenManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CanTeenManagement.ViewModel
{
    class EmployeesViewModel : BaseViewModel
    {
        private ObservableCollection<EMPLOYEE> _g_listEmloyee;
        public ObservableCollection<EMPLOYEE> g_listEmloyee { get => _g_listEmloyee; set { _g_listEmloyee = value; OnPropertyChanged(); } }

        private EMPLOYEE _g_selectedItem;
        private EMPLOYEE g_selectedItem { get => _g_selectedItem; set { _g_selectedItem = value; OnPropertyChanged(); } }

        #region commands.
        public ICommand g_iCm_LoadedCommand { get; set; }

        public ICommand g_iCm_ClickAddCommand { get; set; }

        public ICommand g_iCm_ClickEditInfoCommand { get; set; }

        public ICommand g_iCm_ClickSaveInfoCommand { get; set; }

        public ICommand g_iCm_ClickDetailCommand { get; set; }
        #endregion

        public EmployeesViewModel()
        {
            //this.loadData();

            g_iCm_LoadedCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.loaded(p);

            });

            g_iCm_ClickAddCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickAdd(p);
            });

            g_iCm_ClickEditInfoCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickEdit(p);
            });

            g_iCm_ClickSaveInfoCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickSave(p);
            });

            g_iCm_ClickDetailCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickDetail(p);
            });      
        }

        private void loadData()
        {
            this.g_listEmloyee = new ObservableCollection<EMPLOYEE>(dataProvider.Instance.DB.EMPLOYEEs.ToList());
        }


        private void loaded(EmployeesView p)
        {
            if (p == null)
                return;

            p.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void clickAdd(EmployeesView p)
        {
            if (p == null)
                return;

            p.rDefTop.Height = new GridLength(40, GridUnitType.Star);
        }

        private void clickEdit(EmployeesView p)
        {
            if (p == null)
                return;

            p.rDefTop.Height = new GridLength(40, GridUnitType.Star);
        }

        private void clickSave(EmployeesView p)
        {
            if (p == null)
                return;

            p.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private void clickDetail(EmployeesView p)
        {
            if (p == null)
                return;

            MainWindow mainWd = MainWindow.Instance;
            mainWd.Opacity = 0.5;
            p.Opacity = 0.5;

            DetailEmployeesView detailEmpView = new DetailEmployeesView();

            detailEmpView.ShowDialog();

            mainWd.Opacity = 100;
            p.Opacity = 100;
        }
    }
}
