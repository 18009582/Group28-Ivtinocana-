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
    
    public partial class MODEL
    {
        public int MODEL_ID { get; set; }
        public int MAKE_ID { get; set; }
        public string MODEL_NAME { get; set; }
    
        public virtual MAKE MAKE { get; set; }
    }
}
