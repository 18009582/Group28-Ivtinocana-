using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vehlution.Models;


namespace Vehlution.ViewModels
{
    public class Sales
    {
        //Criteria 
        public IEnumerable<SelectListItem> Employees { get; set; }
        public int SelectedEmployeeID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        //Data 
        public EMPLOYEE employee { get; set; }
        public List<IGrouping< String, ReportRecord>> results { get; set; }
        public Dictionary<string, double> chartData { get; set; }
    }

    public class ReportRecord
    {
        public string orderDate { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Employee { get; set; }
        public int EmployeeID { get; set; }

    }
}