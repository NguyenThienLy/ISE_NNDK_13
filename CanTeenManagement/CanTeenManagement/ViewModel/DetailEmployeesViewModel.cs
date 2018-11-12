using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CanTeenManagement.View;
using CanTeenManagement.Model;

namespace CanTeenManagement.ViewModel
{
    public class DetailEmployeesViewModel : BaseViewModel
    {
        #region commands.
        public ICommand iCm_ClickCloseCommand_g { get; set; }

        public ICommand iCm_ClickEditInfoCommand_g { get; set; }

        public ICommand iCm_ClickSaveInfoCommand_g { get; set; }

        public ICommand iCm_ClickExportCommand_g { get; set; }

        public ICommand iCm_ClickSendMailCommand_g { get; set; }

        public ICommand iCm_MouseDownCommand_g { get; set; }
        #endregion

        public DetailEmployeesViewModel()
        {
            iCm_ClickCloseCommand_g = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickCloseWindow(p);
            });

            iCm_ClickEditInfoCommand_g = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickEditInfo(p);
            });

            iCm_ClickSaveInfoCommand_g = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                this.clickSaveInfo(p);
            });

            iCm_ClickExportCommand_g = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
            });

            iCm_ClickSendMailCommand_g = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
            });

            iCm_MouseDownCommand_g = new RelayCommand<DetailEmployeesView>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });
        }

        private void clickCloseWindow(DetailEmployeesView p)
        {
            p.Close();
        }

        private void clickEditInfo(DetailEmployeesView p)
        {
            p.grVEdit.Height = 350;
            p.grVInfo.Height = 0;
        }

        private void clickSaveInfo(DetailEmployeesView p)
        {
            p.grVInfo.Height = 350;
            p.grVEdit.Height = 0;
        }

        private void mouseDown(DetailEmployeesView p)
        {
            p.DragMove();
        }
    }
}
