using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanTeenManagement.Model
{
    class CASH
    {
        public int MILLION { get; set; }
        public string STRMILLION { get; set; }
        public int QUANTITY { get; set; }

        public CASH(int million)
        {
            this.MILLION = million;
            this.STRMILLION = string.Format("{0:#,#} đ", million);
            this.QUANTITY = 0;
        }
    }
}
