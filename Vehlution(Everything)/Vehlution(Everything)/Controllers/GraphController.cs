using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Vehlution_Everything_.Models;

namespace Vehlution_Everything_.Controllers
{
    public class GraphController : Controller
    {
        // GET: Graph
        public ActionResult Index()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint("January", 90));
            dataPoints.Add(new DataPoint("February", 90));
            dataPoints.Add(new DataPoint("March", 90));
            dataPoints.Add(new DataPoint("April", 95));
            dataPoints.Add(new DataPoint("May", 100));
            dataPoints.Add(new DataPoint("June", 100));
            dataPoints.Add(new DataPoint("July", 90));
            dataPoints.Add(new DataPoint("August", 100));
            dataPoints.Add(new DataPoint("September", 90));
            dataPoints.Add(new DataPoint("October", 95));
            dataPoints.Add(new DataPoint("November", 95));
            dataPoints.Add(new DataPoint("December", 100));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}