using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vehlution.ViewModels
{
    public class PurchasesClass
    {
        public int ID { get; set; }
        public string ClientName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double AcceptedOffer { get; set; }
        public string Car { get; set; }
        public string Colour { get; set; } 
    }

    public class Advanced
    {
        public List<IGrouping<String, PurchasesClass>> results { get; set; }
    }
}