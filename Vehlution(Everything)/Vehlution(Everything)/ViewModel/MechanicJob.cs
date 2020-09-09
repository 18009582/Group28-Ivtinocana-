using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vehlution_Everything_.ViewModels
{
    public class MechanicJobClass
    {
        public int ID { get; set; }
        public DateTime JobDate { get; set; }
        public DateTime JobTime { get; set; }
        public string Car { get; set; }
        public string MechanicName { get; set; }
    }
}