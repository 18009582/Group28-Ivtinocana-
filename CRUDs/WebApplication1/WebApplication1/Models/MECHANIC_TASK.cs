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
    
    public partial class MECHANIC_TASK
    {
        public int CARPARTS_ID { get; set; }
        public int CARPARTSUSEDID { get; set; }
        public int SERVICE_ID { get; set; }
        public int MECHANICJOB_ID { get; set; }
        public int MECHANICTASKID { get; set; }
    
        public virtual MECHANIC_JOB MECHANIC_JOB { get; set; }
        public virtual TASK TASK { get; set; }
        public virtual RELATIONSHIP_12 RELATIONSHIP_12 { get; set; }
    }
}
