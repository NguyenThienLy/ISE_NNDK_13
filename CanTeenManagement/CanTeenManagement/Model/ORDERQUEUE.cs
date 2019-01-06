using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanTeenManagement.CO;

namespace CanTeenManagement.Model
{
    public class ORDERQUEUE
    {
        public string ORDERID { get; set; }
        public string FOODNAME { get; set; }
        public string FOODID { get; set; }
        public int FOODTYPE { get; set; }

        public int _QUANTITY;
        public int QUANTITY
        {
            get => _QUANTITY;

            set
            {
                _QUANTITY = value;
                STRQUANTITY = string.Format("Số lượng {0} món", _QUANTITY);
            }
        }

        public string STRQUANTITY { get; set; }

        public int _TOTALMONEY;
        public int TOTALMONEY
        {
            get => _TOTALMONEY;

            set
            {
                _TOTALMONEY = value;
                STRTOTALMONEY = string.Format("Tổng số tiền {0:#,#} đ", _TOTALMONEY);
            }
        }

        public string STRTOTALMONEY { get; set; }

        public string CUSTOMERID { get; set; }

        public string CUSTOMERNAME { get; set; }

        private string _ORDERDATE;

        public string ORDERDATE
        {
            get => _ORDERDATE;
            set
            {
                _ORDERDATE = staticFunctionClass.TimeAgo(value);
            }
        }

        public string STATUS { get; set; }

        private Nullable<System.DateTime> _COMPLETIONDATE;
        public Nullable<System.DateTime> COMPLETIONDATE
        {
            get => _COMPLETIONDATE;

            set
            {
                _COMPLETIONDATE = value;
            }
        }

        public ORDERQUEUE() { }
    }
}
