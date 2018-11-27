using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanTeenManagement.Model
{
    public class ORDERQUEUE
    {
        public string orderID { get; set; }
        public string foodName { get; set; }
        public int foodType { get; set; }
        public string quantity { get; set; }
        public string customerName { get; set; }
        public string time { get; set; }
        public string price { get; set; }
        public string status { get; set; }
    }
}
