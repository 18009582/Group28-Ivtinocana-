//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Main.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class ORDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDER()
        {
            this.CAR_PARTS_ORDERED = new HashSet<CAR_PARTS_ORDERED>();
        }
    
        public int ORDER_ID { get; set; }
        public int SUPPLIER_ID { get; set; }
        public Nullable<System.DateTime> ORDER_DATE_ { get; set; }
        public Nullable<double> ORDER_PRICE_ { get; set; }
        public string ORDER_STATUS_ { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAR_PARTS_ORDERED> CAR_PARTS_ORDERED { get; set; }
        public virtual SUPPLIER SUPPLIER { get; set; }
    }
}
