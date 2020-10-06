using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vehlution_Everything_.Models
{
    public class NewDefect
    {
        public int defid { get; set; }
        public string defname { get; set; }
        public int PartID { get; set; }
        public String PartName { get; set; }
        public short severity { get; set; }
          

    }
}