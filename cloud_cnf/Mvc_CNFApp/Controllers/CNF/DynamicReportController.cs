using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Mvc_CNFApp.Models;

namespace Mvc_CNFApp.Controllers
{
    public class DynamicReportController : AppController
    {
        private CnfDbContext db = new CnfDbContext();

        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;

        public DynamicReportController()
        {
            ViewData["HighLight_Menu_CnfForm"] = "heighlight";
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            //td = DateTime.Now;
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }




        public ASL_LOG aslLog = new ASL_LOG();

        // SAVE ALL INFORMATION from AccountHeadModel TO Asl_lOG Database Table.
        public void Insert_LogData(ASL_DREPORT aslDreport)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aslDreport.COMPID && n.USERID == aslDreport.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aslDreport.COMPID);
            aslLog.USERID = aslDreport.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aslDreport.INSIPNO;
            aslLog.LOGLTUDE = aslDreport.INSLTUDE;
            aslLog.TABLEID = "ASL_DREPORT";
            aslLog.LOGDATA = Convert.ToString("SL: " + aslDreport.RPTSL + ",\nFiled ID: " + aslDreport.FIELDID + ".");
            aslLog.USERPC = aslDreport.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }



        public void Update_ASL_LOG_LogData(ASL_DREPORT aslDreport)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aslDreport.COMPID && n.USERID == aslDreport.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aslDreport.COMPID);
            aslLog.USERID = aslDreport.INSUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aslDreport.INSIPNO;
            aslLog.LOGLTUDE = aslDreport.INSLTUDE;
            aslLog.TABLEID = "ASL_DREPORT";
            aslLog.LOGDATA = Convert.ToString("SL: " + aslDreport.RPTSL + ",\nFiled ID: " + aslDreport.FIELDID + ".");
            aslLog.USERPC = aslDreport.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }



        // SAVE ALL INFORMATION from AccountHeadModel TO Asl_lOG Database Table.
        public void Delete_ASL_LOG_LogData(ASL_DREPORT aslDreport)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aslDreport.COMPID && n.USERID == aslDreport.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aslDreport.COMPID);
            aslLog.USERID = aslDreport.INSUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aslDreport.UPDIPNO;
            aslLog.LOGLTUDE = aslDreport.UPDLTUDE;
            aslLog.TABLEID = "ASL_DREPORT";
            aslLog.LOGDATA = Convert.ToString("SL: " + aslDreport.RPTSL + ",\nFiled ID: " + aslDreport.FIELDID + ".");
            aslLog.USERPC = aslDreport.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }








        public ASL_DELETE AslDelete = new ASL_DELETE();

        // Delete ALL INFORMATION from GL_ACCHARMST TO ASL_DELETE Database Table.
        public void Delete_ASL_DELETE(ASL_DREPORT aslDreport)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == aslDreport.COMPID && n.USERID == aslDreport.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(aslDreport.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = aslDreport.UPDIPNO;
            AslDelete.DELLTUDE = aslDreport.UPDLTUDE;
            AslDelete.TABLEID = "ASL_DREPORT";
            AslDelete.DELDATA = Convert.ToString("SL: " + aslDreport.RPTSL + ",\nFiled ID: " + aslDreport.FIELDID + ".");
            AslDelete.USERPC = aslDreport.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }








        // GET: /DynamicReport/
        public ActionResult DynamicReportForm()
        {
            var dt = (ASL_DREPORT)TempData["model"];
            return View(dt);
        }



        [HttpPost]
        public ActionResult DynamicReportForm(ASL_DREPORT aslDreport, string command)
        {
            if (command == "Search")
            {
                TempData["model"] = aslDreport;
                TempData["ShowAddButton"] = "Show add button";
                //TempData["TABLEID"] = aslDreport.TABLEID;
                TempData["RPTNM"] = aslDreport.RPTNM;
                TempData["RPTTP"] = aslDreport.RPTTP;
                TempData["latitute_deleteAccount"] = aslDreport.INSLTUDE;
                return View("DynamicReportForm");
            }
            else if (command == "Add")
            {
                //.........................................................Create Permission Check
                var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
                var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

                var createStatus = "";

                System.Data.SqlClient.SqlConnection conn =
                    new System.Data.SqlClient.SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
                string query =
                    string.Format(
                        "SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='DynamicReport' AND COMPID='{0}' AND USERID = '{1}'",
                        LoggedCompId, loggedUserID);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);

                foreach (DataRow row in ds.Rows)
                {
                    createStatus = row["INSERTR"].ToString();
                }

                conn.Close();

                if (createStatus == 'I'.ToString())
                {
                    TempData["model"] = aslDreport;
                    TempData["RPTNM"] = aslDreport.RPTNM;
                    TempData["RPTTP"] = aslDreport.RPTTP;
                    TempData["message"] = "Permission not granted!";
                    return RedirectToAction("DynamicReportForm");
                }
                //...............................................................................................

                aslDreport.USERPC = strHostName;
                aslDreport.INSIPNO = ipAddress.ToString();
                aslDreport.INSTIME = Convert.ToDateTime(td);
                //Insert User ID save POS_ITEMMST table attribute (INSUSERID) field
                aslDreport.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


                Int64 maxData = Convert.ToInt64((from n in db.AslDreportDbSet
                                                 where n.COMPID == aslDreport.COMPID && n.RPTNM == aslDreport.RPTNM
                                                     && n.RPTTP == aslDreport.RPTTP
                                                 select n.RPTSL).Max());

                var result = db.AslDreportDbSet.Count(d => d.RPTNM == aslDreport.RPTNM
                                            && d.COMPID == aslDreport.COMPID &&
                                            d.RPTTP == aslDreport.RPTTP);

                if (result == 0)
                {
                    aslDreport.RPTSL = maxData + 1;
                    Insert_LogData(aslDreport);
                    db.AslDreportDbSet.Add(aslDreport);
                    db.SaveChanges();
                }
                else if (result > 0)
                {
                    if (aslDreport.FIELDID != null)
                    {
                        var getPreviousResult = from m in db.AslDreportDbSet
                                                where m.RPTNM == aslDreport.RPTNM && m.COMPID == aslDreport.COMPID &&
                                                      m.RPTTP == aslDreport.RPTTP
                                                select m;


                        foreach (ASL_DREPORT report in getPreviousResult)
                        {
                            if (aslDreport.FIELDID != null)
                            {
                                report.FIELDID = Convert.ToString(report.FIELDID + "," + " " + aslDreport.FIELDID);
                                aslDreport.FIELDID = report.FIELDID;
                            }
                            else
                            {
                                report.FIELDID = report.FIELDID.ToString();
                                aslDreport.FIELDID = report.FIELDID;
                            }
                            report.UPDIPNO = ipAddress.ToString();
                            report.USERPC = strHostName;
                            report.UPDTIME = Convert.ToDateTime(td);
                            report.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            report.UPDLTUDE = aslDreport.INSLTUDE;
                            aslDreport.RPTSL = report.RPTSL;
                        }

                        //...............................................................................................
                        //count Field Name
                        char[] delimiterChars = { ',', ' ' };
                        var fieldid = aslDreport.FIELDID;
                        string text = Convert.ToString(fieldid);
                        string[] words = text.Split(delimiterChars);
                        int count = 0;
                        foreach (string s in words)
                        {
                            if (s != "")
                            {
                                count++;
                            }
                        }

                        if (count < 3 || 7 < count)
                        {
                            TempData["message"] = "Minimum select 3 column name & Maximum select 7 column name";
                        }
                        //...............................................................................................



                        Update_ASL_LOG_LogData(aslDreport);
                        db.SaveChanges();
                    }

                }

                aslDreport.TABLEID = null;
                TempData["model"] = aslDreport;
                TempData["ShowAddButton"] = "Show add button";
                //TempData["TABLEID"] = aslDreport.TABLEID;
                TempData["RPTNM"] = aslDreport.RPTNM;
                TempData["RPTTP"] = aslDreport.RPTTP;
                return RedirectToAction("DynamicReportForm");
            }

            return View();
        }





        //public ActionResult Edit(Int64 id, Int64 cid, string reportName, string reportType, string tableID, Int64 sl)
        //{
        //    //.........................................................Create Permission Check
        //    var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
        //    var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

        //    var updateStatus = "";

        //    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
        //    string query1 = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='DynamicReport' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
        //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query1, conn);
        //    conn.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable ds = new DataTable();
        //    da.Fill(ds);

        //    foreach (DataRow row in ds.Rows)
        //    {
        //        updateStatus = row["UPDATER"].ToString();
        //    }
        //    conn.Close();
        //    if (updateStatus == 'I'.ToString())
        //    {
        //        TempData["message"] = "Permission not granted!";
        //        return RedirectToAction("DynamicReportForm");
        //    }

        //    //...............................................................................................

        //    ASL_DREPORT aslDreport = db.AslDreportDbSet.Find(id);

        //    TempData["model"] = aslDreport;
        //    TempData["ShowAddButton"] = null;
        //    TempData["TABLEID"] = aslDreport.TABLEID;
        //    TempData["RPTNM"] = aslDreport.RPTNM;
        //    TempData["RPTTP"] = aslDreport.RPTTP;

        //    return RedirectToAction("DynamicReportForm");

        //}


        public ActionResult Delete(Int64 id, Int64 cid, string reportName, string reportType, string tableID, Int64 sl)
        {
            //.........................................................Create Permission Check
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

            var deleteStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
            string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='DynamicReport' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                deleteStatus = row["DELETER"].ToString();
            }
            conn.Close();
            if (deleteStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("DynamicReportForm");

            }
            //...............................................................................................

            ASL_DREPORT aslDreport = db.AslDreportDbSet.Find(id);
            aslDreport.TABLEID = null;
            TempData["model"] = aslDreport;
            TempData["ShowAddButton"] = "Show add button";
            //TempData["TABLEID"] = aslDreport.TABLEID;
            TempData["RPTNM"] = aslDreport.RPTNM;
            TempData["RPTTP"] = aslDreport.RPTTP;

            aslDreport.USERPC = strHostName;
            aslDreport.UPDIPNO = ipAddress.ToString();
            aslDreport.UPDTIME = Convert.ToDateTime(td);
            //Delete User ID save POS_ITEMMST table attribute (UPDUSERID) field
            aslDreport.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            if (TempData["latitute_deleteAccount"] != null)
            {
                //Get current LOGLTUDE data 
                aslDreport.UPDLTUDE = TempData["latitute_deleteAccount"].ToString();
            }
            Delete_ASL_LOG_LogData(aslDreport);
            Delete_ASL_DELETE(aslDreport);

            db.AslDreportDbSet.Remove(aslDreport);
            db.SaveChanges();

            return RedirectToAction("DynamicReportForm");
        }















        //public JsonResult TagSearch(string term)
        //{
        //    var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
        //    var tags = (from p in db.AslDreportDbSet
        //               where compid == p.COMPID
        //               select p.RPTNM).Distinct();

        //    return this.Json(tags.Where(t => t.StartsWith(term)),
        //                    JsonRequestBehavior.AllowGet);
        //}


        ////AutoComplete 
        //[AcceptVerbs(HttpVerbs.Get)]
        //public JsonResult ReportNameChanged(string changedTextRPTNM, Int64 changedTextCompId)
        //{
        //    // var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
        //    string reportType = "";
        //    var rt = db.AslDreportDbSet.Where(n => n.COMPID == changedTextCompId && n.RPTNM == changedTextRPTNM).Select(n => new
        //    {
        //        reportType = n.RPTTP
        //    }).Distinct();

        //    foreach (var n in rt)
        //    {
        //        reportType = n.reportType;
        //    }
        //    var result = new { RPTTP = reportType };
        //    return Json(result, JsonRequestBehavior.AllowGet);

        //}




        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult TABLEIDFieldChanged(string dropDownJobType)
        {
            List<SelectListItem> getFieldID = new List<SelectListItem>();
            var result = (from n in db.AslTableFieldDbSet
                          where n.TABLEID == dropDownJobType
                          select new { n.FIELDID, n.FIELDNAME }).ToList();


            foreach (var f in result)
            {

                getFieldID.Add(new SelectListItem { Text = f.FIELDNAME.ToString(), Value = f.FIELDID.ToString() });
            }

            return Json(getFieldID, JsonRequestBehavior.AllowGet);
        }





        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
