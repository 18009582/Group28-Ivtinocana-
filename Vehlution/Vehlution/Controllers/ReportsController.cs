using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vehlution.Models;
using Vehlution.ViewModels;

namespace Vehlution.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View("SalesReport");
        }


        //Returns advanced view with associated ViewModel
        //Called for initial loasding of the Advanced view
        [HttpGet]
        public ActionResult Advanced()
        {
            Sales vm = new Sales();

            //Retrives Employees dropdown
            vm.Employees = GetEmployees(0);

            //Set Defualt values for the FROM and TO dates 
            vm.DateFrom = new DateTime(2014, 12, 1);
            vm.DateTo = new DateTime(2014, 12, 31);

            return View(vm);
        }

        //Builds list of employees 
        public SelectList GetEmployees(int selected)
        {
            using (VehlutionEntities db = new VehlutionEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                //Create List of employees 
                var employees = db.EMPLOYEEs.Select(x => new SelectListItem
                {
                    Value = x.EMPLYEE_ID.ToString(),
                    Text = x.FULL_NAME
                }).ToList();

                //Choose employee based on selected paramemetr 
                if (selected == 0)
                    return new SelectList(employees, "Value", "Text");
                else
                    return new SelectList(employees, "Value", "Text", selected);
            }

        }

        [HttpPost]
        public ActionResult Advanced(Sales vm)
        {
            using (VehlutionEntities db = new VehlutionEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                vm.Employees = GetEmployees(vm.SelectedEmployeeID);
                vm.employee = db.EMPLOYEEs.Where(x => x.EMPLYEE_ID == vm.SelectedEmployeeID).FirstOrDefault();

                //   var list = db.SALES.Include("PaymentMethod").Where(pp => pp == vm.employee.EMPLYEE_ID
                //   && pp.SALE_DATE_ >= vm.DateFrom && pp.SALE_DATE_ <= vm.DateTo).ToList().Select(rr => new ReportRecord
                {
                    //       orderDate = rr.SALE.SALE_DATE_.ToString("dd-mmm-yyy"),
                    //       Amount = Convert.ToDouble(rr.SALE.ACCEPTED_OFFER),
                    //       PaymentMethod = rr.PaymentMethod.PaymentType,
                    //       Employee = db.EMPLOYEEs.Where(pp => pp.EMPLYEE_ID == rr.EMPLYEE_ID.Select(zz => zz.FullName).Fir)
                    //   });
                }
                return View();
            }

        }
    }
}