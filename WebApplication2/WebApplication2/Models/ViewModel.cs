using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ViewModel
    {
        public List<dynamic> Orders { get; set; }
        public List<dynamic> PartsOrdered { get; set; }
    }
}