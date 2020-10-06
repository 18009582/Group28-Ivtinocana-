using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace Vehlution_Everything_.Controllers
{
    public class DownloadContractController : Controller
    {
        // GET: DownloadContract
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Download()
        {
            string path = Server.MapPath("~/Contract/");
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] files = dirInfo.GetFiles("*.*");
            List<string> lst = new List<string>(files.Length);
            foreach (var item in files)
            {
                lst.Add(item.Name);
            }
            return View(lst);
        }

        public ActionResult DownloadFile(string filename)
        {
            if (Path.GetExtension(filename) == ".docx")
            {
                string fullPath = Path.Combine(Server.MapPath("~/Contract/"), filename);
                return File(fullPath, "Contract/docx", "AGREEMENT FOR THE SALE OF A MOTOR VEHICLE.docx");


            }

            else
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }
    }
}