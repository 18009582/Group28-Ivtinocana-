//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BOOKING_TIMES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOKING_TIMES()
        {
            this.CAR_BOOKING_SLOTS = new HashSet<CAR_BOOKING_SLOTS>();
        }
    
        public int TIME_ID { get; set; }
        public Nullable<System.DateTime> START_TIME_ { get; set; }
        public Nullable<System.DateTime> END_TIME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAR_BOOKING_SLOTS> CAR_BOOKING_SLOTS { get; set; }
    }
}
