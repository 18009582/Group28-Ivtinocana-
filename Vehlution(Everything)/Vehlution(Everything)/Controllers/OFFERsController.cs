using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehlution_Everything_.Models;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;

namespace Vehlution_Everything_.Controllers
{
    public class OFFERsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: OFFERs
        public ActionResult AdminIndex()
        {
            //where pending //where type = 1 client 3=admin
            var oFFERS = db.OFFERS.Where(zz => zz.STATUS_ID == 3 && zz.OFFERTYPE_ID == 1).Include(o => o.CAR).Include(o => o.USER).Include(o => o.OFFER_STATUS).Include(o => o.OFFER_TYPE);
            return View(oFFERS.ToList());
        }
        //need to take in client id as a parameter
        public ActionResult ClientIndex()
        {
            //where pending //where type = 1 client 2=admin
            int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);
            var oFFERS = db.OFFERS.Include(o => o.CAR)
                .Include(o => o.USER)
                .Include(o => o.OFFER_STATUS)
                .Include(o => o.OFFER_TYPE)
                .Where(zz => zz.STATUS_ID == 3 && zz.CAR.USER_ID == clientid && zz.OFFER_TYPE.OFFERTYPE_ID == 2);

            return View(oFFERS.ToList());
        }

        public ActionResult AcceptA(int id)
        {
            try
            {
                OFFER oFFER = db.OFFERS.Find(id);
                oFFER.STATUS_ID = 1;
                db.SaveChanges();
                if (oFFER.USER_ID != null)
                {
                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    string email = db.USERs.Where(zz => zz.USER_ID == oFFER.USER_ID).First().EMAIL;
                    var receiverEmail = new MailAddress(email, "Receiver");
                    var password = "Ivtinocana";
                    var sub = "Accepted Offer";
                    var body = "Good day " + oFFER.CAR.USER.FIRSTNAME +
                        "\n We hope you are doing well, you have make the following offer on one of our cars." +
                        "\n Offer ID: " + oFFER.OFFER_ID + " " +
                        "\n Car Registration : " + oFFER.CAR.CAR_REG + " " +
                        "\n Car Details: " + oFFER.CAR.MODEL.MAKE.MAKE_NAME + " " + oFFER.CAR.MODEL.MODEL_NAME +
                        "\n Colour: " + oFFER.CAR.COLOUR.COLOUR_NAME + 
                        "\n Price: R " + oFFER.AMOUNT + "  " +
                        "\n This offer has been accepted! " +
                        "\n Please click on this link to download the car contract" +
                        "\n https://localhost:44348/DownloadContract/Download" +
                        "\n Please contact your salesperson if you have any questions" +
                        "\n Yours Faithfully, " +
                        "\n Vehlution ";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };

                    using (var mess = new System.Net.Mail.MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })

                    {
                        smtp.Send(mess);
                    }
                    try
                    {
                        //Create an instance for word app  
                        Application winword = new Application();

                        //Set animation status for word application  
                        //winword.ShowAnimation = false;

                        //Set status for word application is to be visible or not.  
                        winword.Visible = false;

                        //Create a missing variable for missing value  
                        object missing = System.Reflection.Missing.Value;

                        //Create a new document  
                        Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                        //Add header into the document  
                        foreach (Section section in document.Sections)
                        {
                            //Get the header range and add the header details.  
                            Range headerRange = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                            headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                            headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            headerRange.Font.ColorIndex = WdColorIndex.wdBlue;
                            headerRange.Font.Size = 10;
                            headerRange.Text = "AGREEMENT FOR THE SALE OF A MOTOR VEHICLE";
                        }

                        //Add the footers into the document  
                        foreach (Section wordSection in document.Sections)
                        {
                            //Get the footer range and add the footer details.  
                            Range footerRange = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                            footerRange.Font.ColorIndex = WdColorIndex.wdDarkRed;
                            footerRange.Font.Size = 10;
                            footerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            footerRange.Text = "Footer text goes here";
                        }

                        //adding text to document  
                        document.Content.SetRange(0, 0);
                        document.Content.Text = "Entered into by and between " + Environment.NewLine +
                                                "JDE Traders" + Environment.NewLine +
                                                "________________________" + Environment.NewLine +
                                                "(hereinafter referred to as “the Seller”)" + Environment.NewLine + Environment.NewLine +
                                                "_____________________________" + Environment.NewLine +
                                                "(hereinafter referred to as “the Purchaser”)" + Environment.NewLine + Environment.NewLine +
                                                "1. Sale" + Environment.NewLine +
                                                "The Seller hereby sells to the Purchaser the following motor vehicle:" + Environment.NewLine +
                                                "Make: " + oFFER.CAR.MODEL.MAKE.MAKE_NAME + Environment.NewLine +
                                                "Model: " + oFFER.CAR.MODEL.MODEL_NAME + Environment.NewLine +
                                                "Year: " + oFFER.CAR.YEAR + Environment.NewLine +
                                                "Registration Number: " + oFFER.CAR.CAR_REG + Environment.NewLine + Environment.NewLine +
                                                "2. Purchase Price " + Environment.NewLine +
                                                "2.1 The purchase price payable by the Purchaser to the Seller the sum of: " + Environment.NewLine +
                                                "R: " + oFFER.AMOUNT + Environment.NewLine +
                                                "2.2 The full purchase price is payable prior to the vehicle's papers being released." + Environment.NewLine + Environment.NewLine +
                                                "3. WARRANTIES AND VOETSTOOTS SALE" + Environment.NewLine +
                                                "3.1 The seller sells the vehicle voetstoots, and, subject to what is set out below, as is, with no warranties whatsoever," + Environment.NewLine +
                                                "3.2. The Seller warrants that the Seller has the full right and authority to sell the vehicle" + Environment.NewLine +
                                                "3.3 The Seller warrants that the vehicle is fully paid for and the vehicle is sold free of any liens and encumbrances." + Environment.NewLine +
                                                "3.4 The Seller undertakes to sign such forms and change of ownership documentation to enable the Purchaser to transfer the Vehicle into his/her name against payment of the purchase price." + Environment.NewLine +
                                                "3.5 The Seller warrants that the Vehicle is roadworthy, the Purchaser however shall be responsible for any costs associated with the roadworthy test. " + Environment.NewLine + Environment.NewLine +
                                                "4. WHOLE AGREEMENT" + Environment.NewLine +
                                                "This is the whole agreement between the parties and no amendment, alteration or addition thereto shall be any force or effect unless reduced in writing and signed by both parties." + Environment.NewLine +
                                                "Signed at_____________________________________(place) on this ____________________" + Environment.NewLine + Environment.NewLine +
                                                "___________________________                                      __________________________________" + Environment.NewLine +
                                                " Seller                                                                            Witness" + Environment.NewLine + Environment.NewLine +
                                                "__________________________                                       __________________________________" + Environment.NewLine +
                                                " Purchaser                                                                         Witness ";

                        //Save the document  
                        object filename = @"C:\Users\hoole\Documents\GitHub\Group28-Ivtinocana-\Vehlution(Everything)\Vehlution(Everything)\Contract\AGREEMENT FOR THE SALE OF A MOTOR VEHICLE.docx";
                        document.SaveAs2(ref filename);
                        document.Close(ref missing, ref missing, ref missing);
                        document = null;
                        winword.Quit(ref missing, ref missing, ref missing);
                        winword = null;
                    }
                    catch(Exception err)
                    {
                        TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                        return RedirectToAction("AdminIndex");
                    }    
                }
                TempData["AlertMessage"] = "You have just accepted this offer";
                return RedirectToAction("AdminIndex");
            }
            catch(Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("AdminIndex");
            }
            
        }
        public ActionResult RejectA(int id)
        {
            try
            {
                OFFER oFFER = db.OFFERS.Find(id);
                oFFER.STATUS_ID = 2;
                db.SaveChanges();
                if (oFFER.USER_ID != null)
                {
                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    if (oFFER.USER_ID != null)
                    {
                        string email = db.USERs.Where(zz => zz.USER_ID == oFFER.USER_ID).First().EMAIL;
                        var receiverEmail = new MailAddress(email, "Receiver");
                        var password = "Ivtinocana";
                        var sub = "Rejected Offer";
                        var body = "Good day " + oFFER.CAR.USER.FIRSTNAME +
                       "\n We hope you are doing well, you have make the following offer on one of our cars." +
                       "\n Offer ID: " + oFFER.OFFER_ID + " " +
                       "\n Car Registration : " + oFFER.CAR.CAR_REG + " " +
                       "\n Car Details: " + oFFER.CAR.MODEL.MAKE.MAKE_NAME + " " + oFFER.CAR.MODEL.MODEL_NAME +
                       "\n Colour: " + oFFER.CAR.COLOUR.COLOUR_NAME +
                       "\n Price: R " + oFFER.AMOUNT + "  " +
                       "\n Unfortunately we have rejected this offer. " +
                       "\n Please feel free to contact us for more information." +
                       "\n Yours Faithfully, " +
                       "\n Vehlution ";
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new System.Net.Mail.MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                    }
                }
                TempData["AlertMessage"] = "You have just rejected this offer";
                return RedirectToAction("AdminIndex");
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("AdminIndex");
            }
        }

        public ActionResult AcceptC(int id)
        {
            try
            {
                OFFER oFFER = db.OFFERS.Find(id);
                oFFER.STATUS_ID = 1;
                db.SaveChanges();
                if (oFFER.USER_ID != 1)
                {

                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    var receiverEmail = new MailAddress("vehlutionReceive@gmail.com", "Receiver");
                    var password = "Ivtinocana";
                    var sub = "Accepted Offer";
                    var body = "Good day " + oFFER.CAR.USER.FIRSTNAME +
                         "\n We hope you are doing well, you have make the following offer on my car." +
                         "\n Offer ID: " + oFFER.OFFER_ID + " " +
                         "\n Car Registration : " + oFFER.CAR.CAR_REG + " " +
                         "\n Car Details: " + oFFER.CAR.MODEL.MAKE.MAKE_NAME + " " + oFFER.CAR.MODEL.MODEL_NAME +
                         "\n Colour: " + oFFER.CAR.COLOUR.COLOUR_NAME +
                         "\n Price: R " + oFFER.AMOUNT + "  " +
                         "\n I would like to accept this offer" +
                         "\n Please contact me via email to finalize this sale: " + oFFER.CAR.USER.EMAIL +
                         "\n Yours Faithfully, " +
                         "\n" + oFFER.USER.FIRSTNAME + oFFER.USER.LASTNAME;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new System.Net.Mail.MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                }
                TempData["AlertMessage"] = "You have just accepted this offer";
                return RedirectToAction("ClientIndex");
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("ClientIndex");
            }
        

    }
        public ActionResult RejectC(int id)
        {
            try
            {
                OFFER oFFER = db.OFFERS.Find(id);
                oFFER.STATUS_ID = 2;
                db.SaveChanges();
                if (oFFER.USER_ID != null)
                {

                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    var receiverEmail = new MailAddress("vehlutionReceive@gmail.com", "Receiver");
                    var password = "Ivtinocana";
                    var sub = "Rejected Offer";
                    var body = "Good day " + oFFER.CAR.USER.FIRSTNAME +
                      "\n We hope you are doing well, you have make the following offer on one of our cars." +
                      "\n Offer ID: " + oFFER.OFFER_ID + " " +
                      "\n Car Registration : " + oFFER.CAR.CAR_REG + " " +
                      "\n Car Details: " + oFFER.CAR.MODEL.MAKE.MAKE_NAME + " " + oFFER.CAR.MODEL.MODEL_NAME +
                      "\n Colour: " + oFFER.CAR.COLOUR.COLOUR_NAME +
                      "\n Price: R " + oFFER.AMOUNT + "  " +
                      "\n Unfortunately I have rejected this offer. " +
                      "\n Please feel free to contact me for more information." + oFFER.CAR.USER.EMAIL +
                      "\n Yours Faithfully, " +
                      "\n" + oFFER.USER.FIRSTNAME + oFFER.USER.LASTNAME;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new System.Net.Mail.MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                }


                TempData["AlertMessage"] = "You have just rejected this offer";
                return RedirectToAction("AdminIndex");
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("AdminIndex");
            }
}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
