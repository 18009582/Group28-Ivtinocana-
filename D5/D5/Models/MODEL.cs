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
    
    public partial class MODEL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MODEL()
        {
            this.CARS = new HashSet<CAR>();
            this.MAKEs = new HashSet<MAKE>();
        }
    
        public int MODEL_ID { get; set; }
        public int MAKE_ID { get; set; }
        public string MODEL_NAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAR> CARS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAKE> MAKEs { get; set; }
    }
}