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
    
    public partial class MAKE
    {
        public int MAKE_ID { get; set; }
        public Nullable<int> MODEL_ID { get; set; }
        public string MAKE_NAME { get; set; }
    
        public virtual MODEL MODEL { get; set; }
    }
}
