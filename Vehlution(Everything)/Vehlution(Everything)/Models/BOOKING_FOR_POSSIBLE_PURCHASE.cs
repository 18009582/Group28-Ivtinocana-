//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vehlution_Everything_.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BOOKING_FOR_POSSIBLE_PURCHASE
    {
        public int BOOKING_ID { get; set; }
        public Nullable<int> CAR_ID { get; set; }
        public Nullable<int> USER_ID { get; set; }
        public Nullable<System.DateTime> BOOKING_DATE { get; set; }
        public Nullable<System.DateTime> BOOKING_TIME { get; set; }
        public string BOOKING_STATUS { get; set; }
    
        public virtual CAR CAR { get; set; }
        public virtual USER USER { get; set; }
    }
}