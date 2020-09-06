using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vehlution.ViewModels
{
    public class AdminBookingsClass
    {
        public int ID { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Car { get; set; }
        public string Employee { get; set; }
    }
}