using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vehlution_Everything_.Controllers
{
    public class RESTOREController : Controller
    {
        // GET: RESTORE
        public ActionResult Restore()
        {
            return View();
        }

        public ActionResult Restore1(HttpPostedFileBase file, string serve, string database)
        {
            try
            {
                string pic = null;
                if (file != null)
                {
                    pic = System.IO.Path.GetFileName(file.FileName);

                    string servername = serve;
                    string databasename = database;

                    SqlConnection con = new SqlConnection(@"Data Source=" + servername + ";Integrated Security=True;Initial Catalog=" + databasename + "");

                    con.Open();
                    string str = "USE master;";
                    string str1 = "ALTER DATABASE " + databasename + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE ; ";
                    string str3 = "RESTORE DATABASE " + databasename + " FROM DISK = 'C:\\Database\\" + pic + "' WITH REPLACE";

                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    SqlCommand cmd3 = new SqlCommand(str3, con);

                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();

                    con.Close();
                    TempData["AlertMessage"] = "Successfully Restored you Database. ";
                    return RedirectToAction("Backup", "BACKUP");
                }
                return RedirectToAction("Backup", "BACKUP");
            }
            catch
            {
                TempData["AlertMessage"] = "There was an error backing up this database: ";
                return RedirectToAction("Backup", "BACKUP");
            }
           
        }
    }
}