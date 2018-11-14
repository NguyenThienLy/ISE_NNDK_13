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
    public class EmployeesViewModel : BaseViewModel
    {
        private ObservableCollection<EMPLOYEE> _g_listEmloyee;
        public ObservableCollection<EMPLOYEE> g_listEmloyee { get => _g_listEmloyee; set { _g_listEmloyee = value; OnPropertyChanged(); } }

        private EMPLOYEE _g_selectedItem;
        private EMPLOYEE g_selectedItem { get => _g_selectedItem; set { _g_selectedItem = value; OnPropertyChanged(); } }

        private string _g_ID;
        public string g_ID { get => _g_ID; set { _g_ID = value; OnPropertyChanged(); } }

        private string _g_Role;
        public string g_Role { get => _g_Role; set { _g_Role = value; OnPropertyChanged(); } }

        #region commands.
        public ICommand g_iCm_LoadingCommand { get; set; }

        public ICommand g_iCm_ClickAddInfoCommand { get; set; }

        public ICommand g_iCm_ClickEditInfoCommand { get; set; }

        public ICommand g_iCm_ClickSaveInfoCommand { get; set; }

        public ICommand g_iCm_ClickDetailCommand { get; set; }
        #endregion

        public EmployeesViewModel()
        {
            this.loadData();

            g_iCm_LoadingCommand = new RelayCommand<EmployeesView>((p) => { return true; }, (p) =>
            {
                this.loading(p);
            });

            g_iCm_ClickAddInfoCommand = new RelayCommand<EmployeesView>((p) => { return this.checkAdd(p); }, (p) =>
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
            this.g_listEmloyee = new ObservableCollection<EMPLOYEE>(dataProvider.Instance.DB.EMPLOYEEs);
        }

        private void loading(EmployeesView p)
        {
            if (p == null)
                return;

            p.rDefTop.Height = new GridLength(0, GridUnitType.Star);
        }

        private bool checkAdd(EmployeesView p)
        {
            if (p == null)
                return false;

            if (string.IsNullOrEmpty(g_ID))
                return false;

            // check id.
            var l_IDList = dataProvider.Instance.DB.EMPLOYEEs.Where(employee => employee.ID == g_ID);
            if (l_IDList == null || l_IDList.Count() != 0)
                return false;

            // check role.
            if (g_Role != "Admin" && g_Role != "Member")
                return false;

            return true;
        }

        private void clickAdd(EmployeesView p)
        {
            p.rDefTop.Height = new GridLength(40, GridUnitType.Star);

            var employee = new EMPLOYEE() { ID = g_ID, ROLE = g_Role };

            dataProvider.Instance.DB.EMPLOYEEs.Add(employee);
            dataProvider.Instance.DB.SaveChanges();

            g_listEmloyee.Add(employee);
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
