using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanTeenManagement.Model
{
    class PAYFOOD
    {
        public string ID { get; set; }
        public string FOODNAME { get; set; }
        public int FOODTYPE { get; set; }
        public int PRICE { get; set; }
        public int PRICESALE { get; set; }
        public int SALE { get; set; }
        public string IMAGELINK { get; set; }
        public int QUANTITY { get; set; }
        public bool ISCHECKED { get; set; }

        public PAYFOOD() { }

        public PAYFOOD(ORDERFOOD orderFood)
        {
            this.ID = orderFood.ID;
            this.FOODNAME = orderFood.FOODNAME;
            this.FOODTYPE = orderFood.FOODTYPE;
            this.PRICE = orderFood.PRICE;
            this.PRICESALE = orderFood.PRICESALE;
            this.SALE = orderFood.SALE;
            this.IMAGELINK = orderFood.IMAGELINK;

            // Default quantity  = 1.
            this.QUANTITY = 1;
            this.ISCHECKED = true;
        }

        public PAYFOOD(PAYFOOD payFood)
        {
            this.ID = payFood.ID;
            this.FOODNAME = payFood.FOODNAME;
            this.FOODTYPE = payFood.FOODTYPE;
            this.PRICE = payFood.PRICE;
            this.PRICESALE = payFood.PRICESALE;
            this.SALE = payFood.SALE;
            this.IMAGELINK = payFood.IMAGELINK;
            this.QUANTITY = payFood.QUANTITY;
            this.ISCHECKED = payFood.ISCHECKED;
        }
    }
}
