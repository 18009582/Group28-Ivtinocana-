//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace D5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchase
    {
        public int PURCHASE_ID { get; set; }
        public int CLIENT_ID { get; set; }
        public Nullable<System.DateTime> PURCHASEDATE_ { get; set; }
        public Nullable<double> COST_ { get; set; }
        public Nullable<int> CAR_ID { get; set; }
    
        public virtual CAR CAR { get; set; }
        public virtual CLIENT CLIENT { get; set; }
    }
}