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
    
    public partial class CAR_DEFECT_STATUS
    {
        public int STATUS_ID { get; set; }
        public int DEFECT_ID { get; set; }
        public int CAR_ID { get; set; }
        public Nullable<System.DateTime> DATE { get; set; }
    
        public virtual DEFECT_STATUS DEFECT_STATUS { get; set; }
        public virtual CAR_DEFECTS CAR_DEFECTS { get; set; }
    }
}
