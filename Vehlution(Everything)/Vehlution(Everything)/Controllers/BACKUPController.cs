using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Vehlution_Everything_.Controllers
{
    public class BACKUPController : Controller
    {
        // GET: BACKUP
        public ActionResult Backup()
        {
            return View();
        }

        public ActionResult Backup1(string serve, string database)
        {
            try
            {
                DateTime d = DateTime.Now;
                string dd = d.Day + "-" + d.Month;
                string servname = serve;
                string dbname = database;

                string aaa = @"Data Source=" + servname + ";Integrated Security=True;Initial Catalog=" + dbname + "";
                SqlConnection con = new SqlConnection(aaa);
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["BackupCatalogDBSoft.Properties.Settings."+dbname+"ConnectionString"].ToString();

                con.Open();
                string str = "USE " + dbname + ";";
                string str1 = "BACKUP DATABASE " + dbname +
                    " TO DISK = 'C:\\Database\\" + dbname + "_" + dd +
                    ".Bak' WITH FORMAT,MEDIANAME = 'Z_SQLServerBackups',NAME = 'Full Backup of " + dbname + "';";
                SqlCommand cmd1 = new SqlCommand(str, con);
                SqlCommand cmd2 = new SqlCommand(str1, con);
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                con.Close();
                TempData["AlertMessage"] = "Successfully Completed Backup. You can find this file (DB Name.Bak) in your Disk D:\\.... never edit this file name.";
                return RedirectToAction("Backup");
            }
            catch(Exception err)
            {
                TempData["AlertMessage"] = "There was an error backing up this database: " + err;
                return RedirectToAction("Backup");
            }
            
        }
    }
}