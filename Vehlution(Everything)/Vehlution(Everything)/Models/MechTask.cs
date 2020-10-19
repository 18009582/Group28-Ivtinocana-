using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vehlution_Everything_.Models
{
    public class MechTask
    {
        public int PartID { get; set; }
        public String PartName { get; set; }
        public int Qty { get; set; }
        public int TaskId { get; set; }
        public String TaskName { get; set; }
    }
}