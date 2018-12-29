using CanTeenManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanTeenManagement.CO;

namespace CanTeenManagement.Model
{
    class PAGE : BaseViewModel
    {
        private int _CURRPAGE;
        public int CURRPAGE
        {
            get => _CURRPAGE;
            set
            {
                _CURRPAGE = value;
                OnPropertyChanged();
            }
        }

        private string _BORDERCOLOR;
        public string BORDERCOLOR
        {
            get => _BORDERCOLOR;
            set
            {
                _BORDERCOLOR = value;
                OnPropertyChanged();
            }
        }


        public PAGE() { }

        public PAGE(int p)
        {
            this.CURRPAGE = p;
            this.BORDERCOLOR = staticVarClass.color_mainColor;
        }

        public PAGE(int p, string color)
        {
            this.CURRPAGE = p;
            this.BORDERCOLOR = color;
        }
    }
}
