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
        public ICommand iCm_ClickCloseCommand_g { get; set; }

        public ICommand iCm_ClickEditInfoCommand_g { get; set; }

        public ICommand iCm_ClickSaveInfoCommand_g { get; set; }

        public ICommand iCm_ClickExportCommand_g { get; set; }

        public ICommand iCm_ClickSendMailCommand_g { get; set; }

        public ICommand iCm_MouseDownCommand_g { get; set; }
        #endregion

        public DetailCustomersViewModel()
        {
            iCm_ClickCloseCommand_g = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            iCm_ClickEditInfoCommand_g = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickEditInfo(p);
            });

            iCm_ClickSaveInfoCommand_g = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
                this.clickSaveInfo(p);
            });

            iCm_ClickExportCommand_g = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
            });

            iCm_ClickSendMailCommand_g = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
            {
            });

            iCm_MouseDownCommand_g = new RelayCommand<DetailCustomersView>((p) => { return true; }, (p) =>
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
