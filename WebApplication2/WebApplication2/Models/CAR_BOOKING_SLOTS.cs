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
    
    public partial class CAR_BOOKING_SLOTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAR_BOOKING_SLOTS()
        {
            this.EMPLOYEEs = new HashSet<EMPLOYEE>();
        }
    
        public int TIME_ID { get; set; }
        public int DAY_ { get; set; }
        public int STATUSID { get; set; }
        public Nullable<int> BOOKING_ID { get; set; }
    
        public virtual BOOKING_DATES BOOKING_DATES { get; set; }
        public virtual BOOKING_STATUS BOOKING_STATUS { get; set; }
        public virtual BOOKING_TIMES BOOKING_TIMES { get; set; }
        public virtual CAR_BOOKING CAR_BOOKING { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE> EMPLOYEEs { get; set; }
    }
}
