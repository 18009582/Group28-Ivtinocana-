//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vehlution.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAR_BOOKING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAR_BOOKING()
        {
            this.CAR_BOOKING_SLOTS = new HashSet<CAR_BOOKING_SLOTS>();
        }
    
        public int CARBOOKING_ID { get; set; }
        public int CLIENT_ID { get; set; }
        public int CAR_ID { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> BOOKING_DATE { get; set; }
        public Nullable<System.DateTime> BOOKING_TIME { get; set; }
    
        public virtual CAR CAR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAR_BOOKING_SLOTS> CAR_BOOKING_SLOTS { get; set; }
        public virtual CLIENT CLIENT { get; set; }
    }
}
