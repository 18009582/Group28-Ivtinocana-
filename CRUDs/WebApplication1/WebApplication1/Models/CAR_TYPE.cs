//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAR_TYPE
    {
        public int CAR_ID { get; set; }
        public int CAR_TYPEID { get; set; }
        public string TYPE_NAME { get; set; }
    
        public virtual CAR CAR { get; set; }
    }
}
