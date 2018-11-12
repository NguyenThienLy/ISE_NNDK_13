using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CanTeenManagement.View;

namespace CanTeenManagement.ViewModel
{
    public class DetailCustomersViewModel : BaseViewModel
    {
        #region commands.
        public ICommand g_iCm_ClickCloseCommand { get; set; }

        public ICommand g_iCm_ClickEditInfoCommand { get; set; }

        public ICommand g_iCm_ClickSaveInfoCommand { get; set; }

        public ICommand g_iCm_ClickExportCommand { get; set; }

        public ICommand g_iCm_ClickSendMailCommand { get; set; }

        public ICommand g_iCm_MouseDownCommand { get; set; }
        #endregion

        public DetailCustomersViewModel()
        {
            g_iCm_ClickCloseCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            g_iCm_ClickEditInfoCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickEditInfo(p);
            });

            g_iCm_ClickSaveInfoCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickSaveInfo(p);
            });

            g_iCm_ClickExportCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
            });

            g_iCm_ClickSendMailCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
            });

            g_iCm_MouseDownCommand = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });
        }

        private void clickCloseWindow(DetailCustomersView p)
        {
            p.Close();
        }

        private void clickEditInfo(DetailCustomersView p)
        {
            p.grVEdit.Height = 350;
            p.grVInfo.Height = 0;
        }

        private void clickSaveInfo(DetailCustomersView p)
        {
            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
        }

        private void mouseDown(DetailCustomersView p)
        {
            p.DragMove();
        }
    }
}
