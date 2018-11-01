using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CanTeenManagement.View;

namespace CanTeenManagement.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        bool isLoaded = false;
        // mọi thứ xử lý sẽ nằm trong này
        public MainViewModel()
        {
            //if (!this.isLoaded)
            //{
            //    this.isLoaded = true;
            //    LoginView lgV = new LoginView();

            //    lgV.ShowDialog();
            //}
        }
    }
}
