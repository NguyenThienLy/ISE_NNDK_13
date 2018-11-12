using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CanTeenManagement.View;
using CanTeenManagement.Model;
using System.Windows;
using System.Windows.Controls;

namespace CanTeenManagement.ViewModel
{
    class OrderViewModel : BaseViewModel
    {
        #region commands.
        public ICommand g_iCm_ClickPayViewCommand { get; set; }
        #endregion

        public OrderViewModel()
        {
            g_iCm_ClickPayViewCommand = new RelayCommand<OrderView>((p) => { return true; }, (p) =>
            {
                 this.clickPayView(p);
            });
        }

        FrameworkElement getWindowParent(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }

        private void clickPayView(OrderView p)
        {
            if (p == null)
                return;

            MainWindow mainWd = MainWindow.Instance;

            mainWd.Opacity = 0.5;
            p.Opacity = 0.5;

            PayView payV = new PayView();
            payV.ShowDialog();

            mainWd.Opacity = 100;
            p.Opacity = 100;
        }
    }
}
