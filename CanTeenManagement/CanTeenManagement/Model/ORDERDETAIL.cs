//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CanTeenManagement.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ORDERDETAIL
    {
        public string ORDERID { get; set; }
        public string FOODID { get; set; }
        public Nullable<int> QUANTITY { get; set; }
        public Nullable<int> TOTALMONEY { get; set; }
        public string STATUS { get; set; }
    
        public virtual FOOD FOOD { get; set; }
        public virtual ORDERINFO ORDERINFO { get; set; }
    }
}
