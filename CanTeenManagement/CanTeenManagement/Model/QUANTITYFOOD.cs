using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanTeenManagement.Model
{
    class QUANTITYFOOD
    {
        public string ID { get; set; }
        public string FOODNAME { get; set; }
        public int FOODTYPE { get; set; }
        public int PRICE { get; set; }
        public int SALE { get; set; }
        public string IMAGELINK { get; set; }
        public int QUANTITY { get; set; }

        public QUANTITYFOOD() { }
            
        public QUANTITYFOOD(FOOD food)
        {
            this.ID = food.ID;
            this.FOODNAME = food.FOODNAME;
            this.FOODTYPE =  (int)food.FOODTYPE;
            this.PRICE = (int)food.PRICE;
            this.SALE = (int)food.SALE;
            this.IMAGELINK = food.IMAGELINK;

            // Default quantity  = 1.
            this.QUANTITY = 1;
        }
    }
}
