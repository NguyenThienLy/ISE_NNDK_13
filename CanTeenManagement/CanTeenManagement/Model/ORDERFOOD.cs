using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanTeenManagement.Model
{
    class ORDERFOOD
    {
        public string ID { get; set; }
        public string FOODNAME { get; set; }
        public int FOODTYPE { get; set; }
        public string FOODDESCRIPTION { get; set; }
        public int PRICE { get; set; }
        public int PRICESALE { get; set; }
        public int SALE { get; set; }
        public string IMAGELINK { get; set; }
        public int STAR { get; set; }
        public string STATUS { get; set; }


        public ORDERFOOD() { }

        public ORDERFOOD(FOOD food)
        {
            this.ID = food.ID;
            this.FOODNAME = food.FOODNAME;
            this.FOODTYPE = (int)food.FOODTYPE;
            this.FOODDESCRIPTION = food.FOODDESCRIPTION;
            this.PRICE = (int)food.PRICE;
            this.PRICESALE = (int)(food.PRICE * ((double)(100 - food.SALE) / 100));
            this.SALE = (int)food.SALE;
            this.IMAGELINK = food.IMAGELINK;
            this.STAR = (int)food.STAR;
            this.STATUS = food.STATUS;
        }
    }
}
