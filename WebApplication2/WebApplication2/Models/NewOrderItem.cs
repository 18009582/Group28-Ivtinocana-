using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class NewOrderItem
    {
        public int PartID { get; set; }
        public String PartName { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}