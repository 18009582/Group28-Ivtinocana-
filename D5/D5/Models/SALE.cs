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
    
    public partial class SALE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SALE()
        {
            this.EMPLYEE_SALES = new HashSet<EMPLYEE_SALES>();
            this.OFFERS = new HashSet<OFFER>();
        }
    
        public int SALES_ID { get; set; }
        public int PAYMENT_ID { get; set; }
        public Nullable<System.DateTime> SALE_DATE_ { get; set; }
        public Nullable<double> ACCEPTED_OFFER { get; set; }
        public byte[] CAR_CONTRACT_ { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLYEE_SALES> EMPLYEE_SALES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OFFER> OFFERS { get; set; }
        public virtual PAYMENT PAYMENT { get; set; }
    }
}