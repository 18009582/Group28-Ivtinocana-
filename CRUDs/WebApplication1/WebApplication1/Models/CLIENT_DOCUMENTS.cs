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
    
    public partial class CLIENT_DOCUMENTS
    {
        public int DOCUEMTN_ID { get; set; }
        public int CLIENT_ID { get; set; }
        public int DOCUMENTTYPE_ID { get; set; }
        public string DOCUMENT { get; set; }
    
        public virtual CLIENT CLIENT { get; set; }
        public virtual DOCUMENT_TYPE DOCUMENT_TYPE { get; set; }
    }
}
