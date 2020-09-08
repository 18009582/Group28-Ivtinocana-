using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vehlution.ViewModels
{
    public class CarPartsClass
    {
        public int ID { get; set; }
        public string PartName { get; set; }
        public int ReorderPoint { get; set; }
        public int StockOnHand { get; set; }
    }
}