//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace User.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAR_DEFECTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAR_DEFECTS()
        {
            this.CAR_DEFECT_STATUS = new HashSet<CAR_DEFECT_STATUS>();
        }
    
        public int DEFECT_ID { get; set; }
        public int CAR_ID { get; set; }
        public int PARTOFCAR_ID { get; set; }
        public Nullable<short> SEVERITY { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAR_DEFECT_STATUS> CAR_DEFECT_STATUS { get; set; }
        public virtual PART_OF_CAR PART_OF_CAR { get; set; }
        public virtual CAR CAR { get; set; }
        public virtual DEFECT DEFECT { get; set; }
    }
}
