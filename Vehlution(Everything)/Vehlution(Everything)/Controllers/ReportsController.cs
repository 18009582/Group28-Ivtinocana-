﻿using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Vehlution_Everything_.Models;
using Vehlution_Everything_.Reporting;
using Vehlution_Everything_.ViewModels;
using System.Dynamic;
using Newtonsoft.Json;

namespace Vehlution_Everything_.Controllers
{
    public class ReportsController : Controller
    {
        //Code for Sales Report
        VehlutionEntities db = new VehlutionEntities();
        // GET: Reports

        //Returns advanced view with associated ViewModel
        //Called for initial loasding of the Advanced view
        [HttpGet]

        public ActionResult SaleIndex()
        {
            return View();
        }
        public ActionResult genReport(DateTime start, DateTime end)
            {
                List<dynamic> d = new List<dynamic>();
                List<SALE> sales = db.SALES.ToList();

                foreach (SALE s in sales)
                {
                    dynamic obj = new ExpandoObject();
                    if (s.SALE_DATE_ >= start && s.SALE_DATE_ <= end)
                    {

                        obj.saleid = s.SALES_ID;
                        obj.date = s.SALE_DATE_;
                        obj.AcceptedOffer = s.ACCEPTED_OFFER;
                        obj.make = s.OFFER.CAR.MODEL.MAKE.MAKE_NAME;
                        obj.makeid = s.OFFER.CAR.MODEL.MAKE_ID;
                        obj.model = s.OFFER.CAR.MODEL.MODEL_NAME;
                        obj.paymenttype = s.PAYMENT.PAYMENTTYPE;
                        obj.CarReg = s.OFFER.CAR.CAR_REG;
                        obj.client = s.OFFER.USER.FIRSTNAME + " " + s.OFFER.USER.LASTNAME;
                        d.Add(obj);
                    }
                }
                var b = d.GroupBy(o => o.make).ToList();

                List<int> counts = new List<int>();
                List<string> makes = new List<string>();
                List<DataPoint> points = new List<DataPoint>();
                foreach (var i in b)
                {

                    points.Add(new DataPoint(i.Key, i.Count()));
                    counts.Add(i.Count());
                    makes.Add(i.Key);
                }

                ViewBag.counts = counts;
                ViewBag.Makes = makes;
                ViewBag.start = start;
                ViewBag.end = end;

                ViewBag.DataPoints = JsonConvert.SerializeObject(points);

                return View(b);

            }
            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        
    

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
        public ActionResult SalesReport(SalesVM vm)
        {
            using (VehlutionEntities db = new VehlutionEntities())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;

                    vm.Employees = GetEmployees(vm.SelectedEmployeeID);

                    vm.employee = db.EMPLOYEEs.Where(x => x.EMPLYEE_ID == vm.SelectedEmployeeID).FirstOrDefault();
               
                    var list = db.EMPLYEE_SALES.Where(pp => pp.EMPLOYEE.EMPLYEE_ID == vm.employee.EMPLYEE_ID
                    && pp.SALE.SALE_DATE_ >= vm.DateFrom && pp.SALE.SALE_DATE_ <= vm.DateTo)
                        .ToList()
                        .Select(rr => new ReportRecord()
                        {
                            orderDate = rr.SALE.SALE_DATE_.Value.ToString("dd-mmm-yyy"),
                            Amount = Convert.ToDouble(rr.SALE.ACCEPTED_OFFER),
                            PaymentMethod = rr.SALE.PAYMENT_ID.ToString(),
                            Employee = rr.EMPLOYEE.FULL_NAME,
                            EmployeeID = rr.EMPLYEE_ID
                        });

                    vm.results = list.GroupBy(g => g.PaymentMethod).ToList();

                    vm.chartData = list.GroupBy(g => g.Employee).ToDictionary(g => g.Key, g => g.Sum(v => v.Amount));

                    TempData["chartData"] = vm.chartData;
                    TempData["records"] = list.ToList();
                    TempData["employee"] = vm.employee;
                    return View(vm);
                }
                catch (Exception ex)
                {
                    return Redirect("SalesReport");
                }

                
            }
        }
        public ActionResult EmployeeSalesChart()
        {
            var data = TempData["chartData"];
            return View("chartData");
        }

        public ActionResult ExportSalesReportPDF()
        {
            ReportDocument report = new ReportDocument();
            report.Load(Path.Combine(Server.MapPath("~/Reports/SalesReport.rpt")));
            //Dataset
            report.SetDataSource(GetAdvancedDataSet());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream strem = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            strem.Seek(0, SeekOrigin.Begin);
            return File(strem, "application/pdf", "SalesReport.pdf");
        }

        public ActionResult ExportSalesReportWord()
        {
            ReportDocument report = new ReportDocument();
            report.Load(Path.Combine(Server.MapPath("~/Reports/SalesReport.rpt")));
            //Dataset
            report.SetDataSource(GetAdvancedDataSet());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream strem = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            strem.Seek(0, SeekOrigin.Begin);
            return File(strem, "application/pdf", "SalesReport.doc");
        }

        public SalesModel GetAdvancedDataSet()
        {
            SalesModel data = new SalesModel();

            //Once data has been added
            data.Employee.Clear();
            data.Employee_Sales.Clear();

            DataRow vrow = data.Employee.NewRow();
            EMPLOYEE employee = (EMPLOYEE)TempData["employee"];
            vrow["EmployeeID"] = employee.EMPLYEE_ID;
            vrow["FullName"] = employee.FULL_NAME;
            data.Employee.Rows.Add(vrow);

            foreach (var item in (IEnumerable<ReportRecord>)TempData["records"])
            {
                DataRow row = data.Employee_Sales.NewRow();
                row["OrderDate"] = item.orderDate;
                row["Amount"] = item.Amount;
                row["PaymentType"] = item.PaymentMethod;
                row["Employee"] = item.Employee;
                row["EmployeeID"] = item.EmployeeID;
                data.Employee_Sales.Rows.Add(row);
            }

            TempData["chartData"] = TempData["chartData"];
            TempData["records"] = TempData["records"];
            TempData["employee"] = TempData["employee"];

            return data;
        }


        //Code for Car Parts Report
        [HttpGet]
        public ActionResult CarPartsReport()
        {
            ViewBag.Message = "Car Parts Report";
            return View(getCarPartData());
        }

        public CarPartsModel getCarPartData()
        {
            CarPartsModel reportData = new CarPartsModel();
            using (VehlutionEntities db = new VehlutionEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var parts = db.CAR_PARTS.Where(pp => pp.RESTOCKORDER >= pp.STOCKONHAND)
                .Select(rr => new CarPartsClass
                {
                    ID = rr.CARPARTS_ID,
                    PartName = rr.PARTNAME,
                    ReorderPoint = rr.RESTOCKORDER.Value,
                    StockOnHand = rr.STOCKONHAND.Value
                }).ToList();

                reportData.CarParts.Rows.Clear();
                foreach (var item in parts)
                {
                    DataRow row = reportData.CarParts.NewRow();
                    row["ID"] = item.ID;
                    row["PartName"] = item.PartName;
                    row["ReorderStock"] = item.ReorderPoint;
                    row["StockOnHand"] = item.StockOnHand;
                    reportData.CarParts.Rows.Add(row);
                }
                return reportData;

            }

        }

        public ActionResult ExportCarPartsPDF()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Reporting/CarParts.rpt")));
            report.SetDataSource(getCarPartData());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CarPartsReport.pdf");
        }

        public ActionResult ExportCarPartsWord()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Reporting/CarParts.rpt")));
            report.SetDataSource(getCarPartData());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/msword", "CarPartsReport.doc");
        }

        //Code for Mechanic Jobs Report 
        [HttpGet]
        public ActionResult MechanicJobReport()
        {
            ViewBag.Message = "Mechanic Job Report";
            return View(getMechanicData());
        }

        public MechanicJobModel getMechanicData()
        {
            MechanicJobModel reportData = new MechanicJobModel();

            using (VehlutionEntities db = new VehlutionEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var jobs = db.MECHANIC_JOB.Select(rr => new MechanicJobClass
                {
                    ID = rr.MECHANICJOB_ID,
                    JobDate = rr.JOB_DATE.Value,
                    JobTime = rr.JOB_TIME.Value,
                    Car = rr.CAR.CAR_REG,
                    MechanicName = rr.MECHANIC.FULL_NAME_
                }).ToList();

                reportData.MechanicJob.Rows.Clear();
                foreach (var item in jobs)
                {
                    DataRow row = reportData.MechanicJob.NewRow();
                    row["ID"] = item.ID;
                    row["JobDate"] = item.JobDate;
                    row["JobTime"] = item.JobTime;
                    row["Car"] = item.Car;
                    row["Mechanic"] = item.MechanicName;
                    reportData.MechanicJob.Rows.Add(row);
                }
                return reportData;

            }

        }
        public ActionResult ExportMechanicPDF()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Report/MechanicJob.rpt")));
            report.SetDataSource(getMechanicData());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "MechanicJobReport.pdf");
        }

        public ActionResult ExportMechanicWord()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Report/MechanicJob.rpt")));
            report.SetDataSource(getMechanicData());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/msword", "MechanicJobReport.doc");
        }


        //Code for Purchases report 
        [HttpGet]
        public ActionResult PurchasesReport()
        {
            ViewBag.Message = "Purchases Report";
            return View(getPurchaseData());
        }

        public PurchasesModel getPurchaseData()
        {
            PurchasesModel reportData = new PurchasesModel();
            Advanced purchasesClass = new Advanced();

            using (VehlutionEntities db = new VehlutionEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var purch = db.PURCHASES.Select(rr => new PurchasesClass
                {
                    ID = rr.PURCHASE_ID,
                    ClientName = rr.USER.FIRSTNAME + " " + rr.USER.LASTNAME,
                    PurchaseDate = rr.PURCHASEDATE_.Value,
                    AcceptedOffer = rr.COST_.Value,
                    Car = rr.CAR.CAR_REG,
                    Colour = rr.CAR.CAR_REG
                }).ToList();

                reportData.Purchases.Rows.Clear();
                foreach (var item in purch)
                {
                    DataRow row = reportData.Purchases.NewRow();
                    row["ID"] = item.ID;
                    row["ClientName"] = item.ClientName;
                    row["PurchaseDate"] = item.PurchaseDate;
                    row["AcceptedOffer"] = item.AcceptedOffer;
                    row["Car"] = item.Car;
                    row["CarColour"] = item.Colour;
                    reportData.Purchases.Rows.Add(row);
                }

                purchasesClass.results = purch.GroupBy(g => g.Colour).ToList();

                return reportData;

            }

        }
        public ActionResult ExportPurchasesPDF()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Report/Purchases.rpt")));
            report.SetDataSource(getPurchaseData());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "PurchasesReport.pdf");
        }

        public ActionResult ExportPurchasesWord()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Report/Purchases.rpt")));
            report.SetDataSource(getPurchaseData());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/msword", "PurchaseReport.doc");
        }


        //Code for Client Bookings report
        [HttpGet]
        public ActionResult ClientBookingsReport()
        {
            ViewBag.Message = "Client Bookings Report";
            return View(getClientData());
        }

        public ClientBookingsModel getClientData()
        {
            ClientBookingsModel reportData = new ClientBookingsModel();

            using (VehlutionEntities db = new VehlutionEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var book = db.CAR_BOOKING.Where(cc => cc.CAR_BOOKING_SLOTS.BOOKING_DATES.DATE >= DateTime.Today).Select(rr => new ClientBookingsClass
                {
                    ID = rr.CARBOOKING_ID,
                    ClientName = rr.USER.FIRSTNAME + " " + rr.USER.LASTNAME,
                    Date = rr.CAR_BOOKING_SLOTS.BOOKING_DATES.DATE.Value,
                    Time = rr.CAR_BOOKING_SLOTS.BOOKING_TIMES.START_TIME_.Value,
                    Car = rr.CAR_ID.ToString(), 
                    Employee = rr.CAR_BOOKING_SLOTS.EMPLOYEE.FULL_NAME.ToString()
                    
                }).ToList();

                reportData.Client_Booking.Rows.Clear();
                foreach (var item in book)
                {
                    DataRow row = reportData.Client_Booking.NewRow();
                    row["ID"] = item.ID;
                    row["ClientName"] = item.ClientName;
                    row["Date"] = item.Date;
                    row["Time"] = item.Time;
                    row["Car"] = item.Car;
                    row["Employee"] = item.Employee;
                    reportData.Client_Booking.Rows.Add(row);
                }
                return reportData;

            }

        }
        public ActionResult ExportClientBookingsPDF()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Report/ClientBookings.rpt")));
            report.SetDataSource(getClientData());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ClientBookingsReport.pdf");
        }

        public ActionResult ExportClientBookingsWord()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Report/ClientBookings.rpt")));
            report.SetDataSource(getPurchaseData());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/msword", "ClientBookingsReport.doc");
        }

        //Code for Admin Bookings report
        [HttpGet]
        public ActionResult AdminBookingsReport()
        {
            ViewBag.Message = "Client Bookings Report";
            return View(getAdminData());
        }

        public AdminBookingsModel getAdminData()
        {
            AdminBookingsModel reportData = new AdminBookingsModel();

            using (VehlutionEntities db = new VehlutionEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var book = db.BOOKING_FOR_POSSIBLE_PURCHASE.Where(cc => cc.BOOKING_DATE >= DateTime.Today).Select(rr => new AdminBookingsClass
                {
                    ID = rr.BOOKING_ID,
                    ClientName = rr.USER.FIRSTNAME + " " + rr.USER.LASTNAME,
                    Date = rr.BOOKING_DATE.Value,
                    Time = rr.BOOKING_TIME.Value,
                    Car = rr.CAR.CAR_REG
                }).ToList();

                reportData.AdminBookings.Rows.Clear();
                foreach (var item in book)
                {
                    DataRow row = reportData.AdminBookings.NewRow();
                    row["ID"] = item.ID;
                    row["ClientName"] = item.ClientName;
                    row["Date"] = item.Date;
                    row["Time"] = item.Time;
                    row["Car"] = item.Car;
                    reportData.AdminBookings.Rows.Add(row);
                }
                return reportData;

            }

        }
        public ActionResult ExportAdminBookingsPDF()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Report/AdminBookings.rpt")));
            report.SetDataSource(getClientData());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "AdminBookingsReport.pdf");
        }

        public ActionResult ExportAdminBookingsWord()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Report/AdminBookings.rpt")));
            report.SetDataSource(getPurchaseData());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/msword", "AdminBookingsReport.doc");
        }
        public ActionResult PurchIndex()
        {
            return View();
        }
        [HttpPost]


        public ActionResult PurchgenReport(DateTime start, DateTime end)
        {
            List<dynamic> d = new List<dynamic>();
            List<Purchase> sales = db.PURCHASES.OrderByDescending(x => x.PURCHASEDATE_).ToList();
            foreach (Purchase s in sales)
            {
                dynamic obj = new ExpandoObject();
                if (s.PURCHASEDATE_ >= start && s.PURCHASEDATE_ <= end)
                {

                    obj.saleid = s.PURCHASE_ID;
                    obj.date = s.PURCHASEDATE_;
                    obj.AcceptedOffer = s.COST_;
                    obj.make = s.CAR.MODEL.MAKE.MAKE_NAME;
                    obj.makeid = s.CAR.MODEL.MAKE_ID;
                    obj.model = s.CAR.MODEL.MODEL_NAME;
                    obj.CarReg = s.CAR.CAR_REG;
                    obj.client = s.USER.FIRSTNAME + " " +s.USER.LASTNAME + " " + s.USER.EMAIL;
                    d.Add(obj);
                }
            }
            var b = d.GroupBy(o => o.make).ToList();

            List<int> counts = new List<int>();
            List<string> makes = new List<string>();

            foreach (var i in b)
            {


                counts.Add(i.Count());
                makes.Add(i.Key);
            }

            ViewBag.counts = counts;
            ViewBag.Makes = makes;
            ViewBag.start = start;
            ViewBag.end = end;



            return View(b);

        }


    }
}
