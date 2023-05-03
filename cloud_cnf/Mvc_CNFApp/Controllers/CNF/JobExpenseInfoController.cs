using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;
using Mvc_CNFApp.Models;
using RazorPDF;

namespace Mvc_CNFApp.Controllers
{
    public class JobExpenseInfoController : AppController
    {

        private CnfDbContext db = new CnfDbContext();

        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;
        public JobExpenseInfoController()
        {
            ViewData["HighLight_Menu_CnfForm"] = "heighlight";
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }



        public ASL_LOG aslLog = new ASL_LOG();

        public void Insert_Asl_LogData(CNF_JOBEXPMST aCnfJobexpmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == aCnfJobexpmst.COMPID && n.USERID == aCnfJobexpmst.INSUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(aCnfJobexpmst.COMPID);
            aslLog.USERID = aCnfJobexpmst.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCnfJobexpmst.INSIPNO;
            aslLog.LOGLTUDE = aCnfJobexpmst.INSLTUDE;
            aslLog.TABLEID = "CNF_JOBEXPMST";

            string expensename = "";
            var expnse_cdFind =
                (from n in db.GlAcchartDbSet
                 where n.COMPID == aCnfJobexpmst.COMPID && n.ACCOUNTCD == aCnfJobexpmst.EXPCD
                 select n).ToList();
            foreach (var name in expnse_cdFind)
            {
                expensename = name.ACCOUNTNM;
            }

            aslLog.LOGDATA =
                Convert.ToString("TransDate: " + aCnfJobexpmst.TRANSDT + ",\nTransYY: " + aCnfJobexpmst.TRANSYY + ",\nTransNo: " + aCnfJobexpmst.TRANSNO + ",\nJOBYY: " + aCnfJobexpmst.JOBYY + ",\nJOBTP: " + aCnfJobexpmst.JOBTP + ",\nJOBNO: " + aCnfJobexpmst.JOBNO + ",\nExpense By: " + expensename + ",\nRemarks: " + aCnfJobexpmst.REMARKS + ".");
            aslLog.USERPC = aCnfJobexpmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Insert_Asl_LogData_CNF_JOBEXP(CNF_JOBEXP aCnfJobexp)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == aCnfJobexp.COMPID && n.USERID == aCnfJobexp.INSUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(aCnfJobexp.COMPID);
            aslLog.USERID = aCnfJobexp.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCnfJobexp.INSIPNO;
            aslLog.LOGLTUDE = aCnfJobexp.INSLTUDE;
            aslLog.TABLEID = "CNF_JOBEXP";
            string expensename = "";
            var expnse_cdFind =
                (from n in db.CnfExpenseDbSet
                 where n.COMPID == aCnfJobexp.COMPID && n.EXPID == aCnfJobexp.EXPID
                 select n).ToList();
            foreach (var name in expnse_cdFind)
            {
                expensename = name.EXPNM;
            }

            aslLog.LOGDATA =
                Convert.ToString("SL: " + aCnfJobexp.EXPSL + ",\nExpense name: " + expensename + ",\nAmount: " + aCnfJobexp.EXPAMT + ",\nRemarks: " + aCnfJobexp.REMARKS + ".");
            aslLog.USERPC = aCnfJobexp.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        public void Update_Asl_LogData_CNF_JOBEXP(CNF_JOBEXP aCnfJobexp)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == aCnfJobexp.COMPID && n.USERID == aCnfJobexp.INSUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(aCnfJobexp.COMPID);
            aslLog.USERID = aCnfJobexp.INSUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCnfJobexp.INSIPNO;
            aslLog.LOGLTUDE = aCnfJobexp.INSLTUDE;
            aslLog.TABLEID = "CNF_JOBEXP";

            string expensename = "";
            var expnse_cdFind =
                (from n in db.CnfExpenseDbSet
                 where n.COMPID == aCnfJobexp.COMPID && n.EXPID == aCnfJobexp.EXPID
                 select n).ToList();
            foreach (var name in expnse_cdFind)
            {
                expensename = name.EXPNM;
            }
            aslLog.LOGDATA =
                Convert.ToString("SL: " + aCnfJobexp.EXPSL + ",\nExpense name: " + expensename + ",\nAmount: " + aCnfJobexp.EXPAMT + ",\nRemarks: " + aCnfJobexp.REMARKS + ".");
            aslLog.USERPC = aCnfJobexp.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }



        public void Delete_Asl_LogData_CNF_JOBEXP(CNF_JOBEXP modelDelete)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == modelDelete.COMPID && n.USERID == modelDelete.INSUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(modelDelete.COMPID);
            aslLog.USERID = modelDelete.INSUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = modelDelete.UPDIPNO;
            aslLog.LOGLTUDE = modelDelete.UPDLTUDE;
            aslLog.TABLEID = "CNF_JOBEXP";
            string expensename = "";
            var expnse_cdFind =
                (from n in db.CnfExpenseDbSet
                 where n.COMPID == modelDelete.COMPID && n.EXPID == modelDelete.EXPID
                 select n).ToList();
            foreach (var name in expnse_cdFind)
            {
                expensename = name.EXPNM;
            }
            aslLog.LOGDATA =
                Convert.ToString("SL: " + modelDelete.EXPSL + ",\nExpense name: " + expensename + ",\nAmount: " + modelDelete.EXPAMT + ",\nRemarks: " + modelDelete.REMARKS + ".");
            aslLog.USERPC = modelDelete.USERPC;
            db.AslLogDbSet.Add(aslLog);
           
        }



        public ASL_DELETE AslDelete = new ASL_DELETE();

        public void Delete_ASL_DELETE_CNF_JOBEXP(CNF_JOBEXP modelDelete)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == modelDelete.COMPID && n.USERID == modelDelete.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(modelDelete.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = modelDelete.UPDIPNO;
            AslDelete.DELLTUDE = modelDelete.UPDLTUDE;
            AslDelete.TABLEID = "CNF_JOBEXP";

            string expensename = "";
            var expnse_cdFind =
                (from n in db.CnfExpenseDbSet
                 where n.COMPID == modelDelete.COMPID && n.EXPID == modelDelete.EXPID
                 select n).ToList();
            foreach (var name in expnse_cdFind)
            {
                expensename = name.EXPNM;
            }

            AslDelete.DELDATA = Convert.ToString("SL: " + modelDelete.EXPSL + ",\nExpense name: " + expensename + ",\nAmount: " + modelDelete.EXPAMT + ",\nRemarks: " + modelDelete.REMARKS + ".");
            AslDelete.USERPC = modelDelete.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
           
        }






        // GET: /JobExpenseInfo/
        public ActionResult Index()
        {
            var dt = (PageModel)TempData["Model"];
            return View(dt);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PageModel model, String command)
        {
            if ((command == "Submit") && model.ACnfJobexpmst.TRANSDT != null && model.ACnfJob.Cnf_JobID != 0 && model.ACnfJobexpmst.EXPCD != null && model.ACnfJobexpmst.TRANSNO !=null)
            {
                model.ACnfJobexpmst.USERPC = strHostName;
                model.ACnfJobexpmst.INSIPNO = ipAddress.ToString();
                model.ACnfJobexpmst.INSTIME = Convert.ToDateTime(td);
                //Insert User ID save AslUSerco table attribute INSUSERID
                model.ACnfJobexpmst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                var result =
                    db.CnfJobexpmstDbSet.Count(
                        d => d.TRANSNO == model.ACnfJobexpmst.TRANSNO && d.TRANSYY == model.ACnfJobexpmst.TRANSYY
                             && d.COMPID == model.ACnfJobexpmst.COMPID);
                if (result == 0)
                {
                    //.........................................................Create Permission Check
                    var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
                    var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

                    var createStatus = "";

                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
                    string query = string.Format(
                            "SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='JobExpenseInfo' AND COMPID='{0}' AND USERID = '{1}'",
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
                        TempData["Model"] = model;
                        //TempData["ExpCID"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;

                        TempData["ShowAddButton"] = "Show Add Button";
                        TempData["message"] = "Permission not granted!";
                        return RedirectToAction("Index");
                    }
                    //...............................................................................................
                    var res = (from n in db.CnfJobDbSet where model.ACnfJobexpmst.COMPID == n.COMPID && model.ACnfJob.Cnf_JobID == n.Cnf_JobID select n);
                    foreach (var cnfJob in res)
                    {
                        model.ACnfJobexpmst.JOBNO = cnfJob.JOBNO;
                    }

                   

                    db.CnfJobexpmstDbSet.Add(model.ACnfJobexpmst);
                    db.SaveChanges();
                    Insert_Asl_LogData(model.ACnfJobexpmst);
                    db.SaveChanges();

                    string date = model.ACnfJobexpmst.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["message"] = "Invoice No: '" + model.ACnfJobexpmst.TRANSNO + "' successfully created,now create job expenses for this invoice.";
                    TempData["ShowAddButton"] = "Show Add Button";
                    TempData["Model"] = model;
                    TempData["TRANSNO"] = model.ACnfJobexpmst.TRANSNO;
                    TempData["TRANSYY"] = model.ACnfJobexpmst.TRANSYY;
                    TempData["yy"] = model.ACnfJobexpmst.JOBYY;

                    TempData["Hide_SubmitButton"] = "Hide Submit Button";
                    return RedirectToAction("Index");
                }

                else
                {
                    //TempData["message"] = "TRANSNO: '" + model.ACnfJobexpmst.TRANSNO +
                    //                      "' successfully Updated.\n Please Create the Job Expense List.";
                    TempData["ShowAddButton"] = "Show Add Button";
                    model = null;
                    TempData["Model"] = model;
                    return RedirectToAction("Index");
                }
            }
            else if ((command == "Add") && model.ACnfJobexpmst.TRANSDT != null && model.ACnfJob.Cnf_JobID != null && model.ACnfJobexpmst.EXPCD != null && model.ACnfJobexpmst.TRANSNO != null)
            {
                model.ACnfJobexp.USERPC = strHostName;
                model.ACnfJobexp.INSIPNO = ipAddress.ToString();
                model.ACnfJobexp.INSTIME = Convert.ToDateTime(td);
                //Insert User ID save AslUSerco table attribute INSUSERID
                model.ACnfJobexp.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                //.........................................................Create Permission Check
                var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
                var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

                var createStatus = "";

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
                string query = string.Format(
                        "SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='JobExpenseInfo' AND COMPID='{0}' AND USERID = '{1}'",
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
                    TempData["Model"] = model;
                    //TempData["ExpCID"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;

                    TempData["ShowAddButton"] = "Show Add Button";
                    TempData["message"] = "Permission not granted!";
                    return RedirectToAction("Index");
                }
                //...............................................................................................

                var res = db.CnfJobexpDbSet.Where(a => a.COMPID == model.ACnfJobexpmst.COMPID && a.TRANSNO == model.ACnfJobexpmst.TRANSNO && a.TRANSYY == model.ACnfJobexpmst.TRANSYY).Count(a => a.EXPID == model.ACnfJobexp.EXPID) != 0;
                if (res == true)//found ID
                {
                    var result = db.CnfJobexpDbSet.Where(a => a.COMPID == model.ACnfJobexpmst.COMPID && a.TRANSNO == model.ACnfJobexpmst.TRANSNO && a.TRANSYY == model.ACnfJobexpmst.TRANSYY && a.EXPID == model.ACnfJobexp.EXPID);
                    foreach (CNF_JOBEXP m in result)
                    {
                        m.EXPID = model.ACnfJobexp.EXPID;
                        m.EXPAMT = model.ACnfJobexp.EXPAMT;
                        m.REMARKS = model.ACnfJobexp.REMARKS;
                        m.INSLTUDE = model.ACnfJobexpmst.INSLTUDE;
                    }
                    Insert_Asl_LogData_CNF_JOBEXP(model.ACnfJobexp);
                    db.SaveChanges();

                }
                else
                {
                    var expSerial = db.CnfJobexpDbSet.Where(d => d.TRANSNO == model.ACnfJobexpmst.TRANSNO && d.TRANSYY == model.ACnfJobexpmst.TRANSYY
                          && d.COMPID == model.ACnfJobexpmst.COMPID).Max(d => d.EXPSL);

                    model.ACnfJobexp.EXPSL = Convert.ToInt64(expSerial) + 1;
                    model.ACnfJobexp.COMPID = model.ACnfJobexpmst.COMPID;
                    model.ACnfJobexp.TRANSDT = model.ACnfJobexpmst.TRANSDT;
                    model.ACnfJobexp.TRANSYY = model.ACnfJobexpmst.TRANSYY;
                    model.ACnfJobexp.TRANSNO = model.ACnfJobexpmst.TRANSNO;
                    model.ACnfJobexp.JOBYY = model.ACnfJobexpmst.JOBYY;
                    model.ACnfJobexp.JOBTP = model.ACnfJobexpmst.JOBTP;
                    var result = (from n in db.CnfJobDbSet where model.ACnfJobexpmst.COMPID == n.COMPID && model.ACnfJob.Cnf_JobID == n.Cnf_JobID select n);
                    foreach (var cnfJob in result)
                    {
                        model.ACnfJobexp.JOBNO = cnfJob.JOBNO;
                    }
                    model.ACnfJobexp.EXPCD = model.ACnfJobexpmst.EXPCD;
                    model.ACnfJobexp.INSLTUDE = model.ACnfJobexpmst.INSLTUDE;

                    Insert_Asl_LogData_CNF_JOBEXP(model.ACnfJobexp);
                    db.CnfJobexpDbSet.Add(model.ACnfJobexp);
                    db.SaveChanges();

                }

                string date = model.ACnfJobexpmst.TRANSDT.ToString();
                DateTime MyDateTime = DateTime.Parse(date);
                string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                TempData["transdate"] = currentDate;
                TempData["ShowAddButton"] = "Show Add Button";
                TempData["Model"] = model;
                TempData["TRANSNO"] = model.ACnfJobexpmst.TRANSNO;
                TempData["TRANSYY"] = model.ACnfJobexpmst.TRANSYY;
                TempData["yy"] = model.ACnfJobexpmst.JOBYY;
                TempData["Current_UPDLTUDE"] = model.ACnfJobexpmst.INSLTUDE;
                model.ACnfJobexp.EXPID = null;
                model.ACnfJobexp.EXPAMT = null;
                model.ACnfJobexp.REMARKS = "";

                TempData["Hide_SubmitButton"] = "Hide Submit Button";
                return RedirectToAction("Index");
            }
            else if (command == "Edit")
            {
                //Permission Check
                Int64 LoggedCompId = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
                Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                var checkPermission = from role in db.AslRoleDbSet
                                      where role.USERID == loggedUserID && role.COMPID == LoggedCompId && role.CONTROLLERNAME == "JobExpenseInfo"
                                      select new { role.UPDATER };
                string Update = "";
                foreach (var VARIABLE in checkPermission)
                {
                    Update = VARIABLE.UPDATER;
                }

                if (Update == "I")
                {
                    ViewBag.UpdatePermission = "Permission Denied !";
                    return View("Index");
                }

                return RedirectToAction("Edit");
            }
            else if ((command == "Update") && model.ACnfJobexpmst.TRANSDT != null && model.ACnfJob.Cnf_JobID != null && model.ACnfJobexpmst.EXPCD != null)
            {
                var result = db.CnfJobexpDbSet.Where(a => a.COMPID == model.ACnfJobexpmst.COMPID && a.TRANSNO == model.ACnfJobexpmst.TRANSNO && a.TRANSYY == model.ACnfJobexpmst.TRANSYY && a.EXPID == model.ACnfJobexp.EXPID);
                CNF_JOBEXP jobExp_logData = new CNF_JOBEXP();
                foreach (CNF_JOBEXP m in result)
                {
                    m.EXPID = model.ACnfJobexp.EXPID;
                    m.EXPAMT = model.ACnfJobexp.EXPAMT;
                    m.REMARKS = model.ACnfJobexp.REMARKS;

                    m.USERPC = strHostName;
                    m.UPDIPNO = ipAddress.ToString();
                    m.UPDTIME = Convert.ToDateTime(td);
                    m.UPDLTUDE = model.ACnfJobexpmst.INSLTUDE;
                    //Insert User ID save AslUSerco table attribute INSUSERID
                    m.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    jobExp_logData = m;
                }

                Update_Asl_LogData_CNF_JOBEXP(jobExp_logData);
                db.SaveChanges();

                string date = model.ACnfJobexpmst.TRANSDT.ToString();
                DateTime MyDateTime = DateTime.Parse(date);
                string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                TempData["transdate"] = currentDate;
                TempData["ShowAddButton"] = "Show Add Button";
                TempData["Model"] = model;
                TempData["TRANSNO"] = model.ACnfJobexpmst.TRANSNO;
                TempData["TRANSYY"] = model.ACnfJobexpmst.TRANSYY;
                TempData["yy"] = model.ACnfJobexpmst.JOBYY;
                TempData["Current_UPDLTUDE"] = model.ACnfJobexpmst.INSLTUDE;
                model.ACnfJobexp.EXPID = null;
                model.ACnfJobexp.EXPAMT = null;
                model.ACnfJobexp.REMARKS = "";

                TempData["Hide_SubmitButton"] = "Hide Submit Button";
                return RedirectToAction("Index");
            }
            else if (command == "Complete")
            {
                model = null;
                TempData["Model"] = model;
                TempData["message"] = "Complete Successfully!";
                return RedirectToAction("Index");
            }
            else if (command == "Print")
            {
                var JOBNO = (from n in db.CnfJobDbSet where model.ACnfJobexpmst.COMPID == n.COMPID && model.ACnfJob.Cnf_JobID == n.Cnf_JobID select n);
                foreach (var cnfJob in JOBNO)
                {
                    model.ACnfJobexpmst.JOBNO = cnfJob.JOBNO;
                }

                //CNF_JOBEXPMST aCnfJobexpmst=new CNF_JOBEXPMST();
                List<CNF_JOBEXPMST> getJobexpmsts = new List<CNF_JOBEXPMST>();
                var result = (db.CnfJobexpmstDbSet.Where(n => n.COMPID == model.ACnfJobexpmst.COMPID &&
                                                   n.TRANSDT == model.ACnfJobexpmst.TRANSDT &&
                                                   n.JOBNO == model.ACnfJobexpmst.JOBNO && n.TRANSNO == model.ACnfJobexpmst.TRANSNO && n.JOBYY == model.ACnfJobexpmst.JOBYY)
               .Select(n => new { compid = n.COMPID, expHeadCD = n.EXPCD, transdate = n.TRANSDT, JobNo = n.JOBNO, transNo = n.TRANSNO, jobyear = n.JOBYY })).ToList();
                foreach (var VARIABLE in result)
                {
                    getJobexpmsts.Add(new CNF_JOBEXPMST() { COMPID = VARIABLE.compid, TRANSDT = VARIABLE.transdate, EXPCD = VARIABLE.expHeadCD, JOBNO = VARIABLE.JobNo, TRANSNO = VARIABLE.transNo, JOBYY = VARIABLE.jobyear });
                }
                //var pdf = new PdfResult(getJobexpmsts, "GetExpenseVoucharReport");
                //return pdf;
                TempData["GetExpenseVoucharReport_List"] = getJobexpmsts;
                return RedirectToAction("GetExpenseVoucharReport");

            }
            else
            {
                TempData["jobexpense"] = "You are missing some important fields";
            }

            return RedirectToAction("Index");
        }






        public ActionResult gridUpdate(Int64 id, Int64 cid, Int64 TransYear, Int64 TransNO, Int64 Serial, PageModel model)
        {
            //Permission Check
            Int64 LoggedCompId = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            var checkPermission = from role in db.AslRoleDbSet
                                  where role.USERID == loggedUserID && role.COMPID == LoggedCompId && role.CONTROLLERNAME == "JobExpenseInfo"
                                  select new { role.UPDATER };
            string Update = "";
            foreach (var VARIABLE in checkPermission)
            {
                Update = VARIABLE.UPDATER;
            }

            if (Update == "I")
            {
                TempData["message"] = "Permission Denied !";
                return View("Index");
            }
            //...................................................

            model.ACnfJobexp = db.CnfJobexpDbSet.Find(id);

            Int64 jobExpmstid = 0;
            var findID = db.CnfJobexpmstDbSet.Where(e => e.COMPID == cid && e.TRANSNO == TransNO);
            foreach (var m in findID)
            {
                jobExpmstid = m.cnf_JobExpmstID;
            }
            model.ACnfJobexpmst = db.CnfJobexpmstDbSet.Find(jobExpmstid);

            var findName = (from n in db.CnfExpenseDbSet where n.COMPID == cid && n.EXPID == model.ACnfJobexp.EXPID select n).ToList();
            foreach (var n in findName)
            {
                TempData["expenseName"] = n.EXPNM;
            }

            var cnf_Job = db.CnfJobDbSet.Where(e => e.COMPID == cid && e.JOBNO == model.ACnfJobexp.JOBNO && e.JOBTP == model.ACnfJobexp.JOBTP &&
                     e.JOBYY == model.ACnfJobexp.JOBYY);
            foreach (var m in cnf_Job)
            {
                model.ACnfJob = m;
            }

            string date = model.ACnfJobexpmst.TRANSDT.ToString();
            DateTime MyDateTime = DateTime.Parse(date);
            string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
            TempData["transdate"] = currentDate;
            TempData["ShowAddButton"] = null;
            TempData["Model"] = model;
            TempData["TRANSNO"] = model.ACnfJobexpmst.TRANSNO;
            TempData["TRANSYY"] = model.ACnfJobexpmst.TRANSYY;
            TempData["yy"] = model.ACnfJobexpmst.JOBYY;
            TempData["Hide_SubmitButton"] = "Hide Submit Button";
            return RedirectToAction("Index");
        }



        public ActionResult gridDelete(Int64 id, Int64 cid, Int64 TransYear, Int64 TransNO, Int64 Serial, PageModel model)
        {
            //Permission Check
            Int64 LoggedCompId = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            var checkPermission = from role in db.AslRoleDbSet
                                  where role.USERID == loggedUserID && role.COMPID == LoggedCompId && role.CONTROLLERNAME == "JobExpenseInfo"
                                  select new { role.DELETER };
            string Delete = "";
            foreach (var VARIABLE in checkPermission)
            {
                Delete = VARIABLE.DELETER;
            }

            if (Delete == "I")
            {
                TempData["message"] = "Permission Denied !";
                return View("Index");
            }
            //...................................................

            CNF_JOBEXP aCnfJobexp = db.CnfJobexpDbSet.Find(id);

            //aCnfJobexp.USERPC = strHostName;
            //aCnfJobexp.UPDIPNO = ipAddress.ToString();
            //aCnfJobexp.UPDTIME = Convert.ToDateTime(td);
            //aCnfJobexp.UPDLTUDE = TempData["Current_UPDLTUDE"].ToString();
            ////Insert User ID save AslUSerco table attribute INSUSERID
            //aCnfJobexp.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            //Delete_ASL_DELETE_CNF_JOBEXP(aCnfJobexp);
            //Delete_Asl_LogData_CNF_JOBEXP(aCnfJobexp);

            db.CnfJobexpDbSet.Remove(aCnfJobexp);
            db.SaveChanges();

            Int64 jobExpmstid = 0;
            var findID = db.CnfJobexpmstDbSet.Where(e => e.COMPID == cid && e.TRANSNO == TransNO);
            foreach (var m in findID)
            {
                jobExpmstid = m.cnf_JobExpmstID;
            }
            model.ACnfJobexpmst = db.CnfJobexpmstDbSet.Find(jobExpmstid);

            var cnf_Job = db.CnfJobDbSet.Where(e => e.COMPID == cid && e.JOBNO == aCnfJobexp.JOBNO && e.JOBTP == aCnfJobexp.JOBTP &&
                    e.JOBYY == aCnfJobexp.JOBYY);
            foreach (var m in cnf_Job)
            {
                model.ACnfJob = m;
            }

            string date = model.ACnfJobexpmst.TRANSDT.ToString();
            DateTime MyDateTime = DateTime.Parse(date);
            string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
            TempData["transdate"] = currentDate;
            TempData["ShowAddButton"] = "Show Add Button";
            TempData["Model"] = model;
            TempData["TRANSNO"] = model.ACnfJobexpmst.TRANSNO;
            TempData["TRANSYY"] = model.ACnfJobexpmst.TRANSYY;
            TempData["yy"] = model.ACnfJobexpmst.JOBYY;
            TempData["Hide_SubmitButton"] = "Hide Submit Button";
            return RedirectToAction("Index");

        }



        public ActionResult Edit()
        {
            var dt = (PageModel)TempData["Model"];
            return View(dt);
        }





        [AcceptVerbs("POST")]
        [ActionName("Edit")]
        public ActionResult Edit(PageModel model, string command)
        {
            if (command == "New")
            {
                return RedirectToAction("Index");
            }
            else if (command == "Search")
            {
                TempData["Current_UPDLTUDE"] = model.ACnfJobexpmst.INSLTUDE;
                string date = model.ACnfJobexpmst.TRANSDT.ToString();
                DateTime MyDateTime = DateTime.Parse(date);
                string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                TempData["transdate"] = currentDate;
                TempData["ShowAddButton"] = "Show Add Button";
                TempData["Model"] = model;
                TempData["TRANSNO"] = model.ACnfJobexpmst.TRANSNO;
                TempData["TRANSYY"] = model.ACnfJobexpmst.TRANSYY;
                TempData["JOBNO"] = model.ACnfJobexpmst.JOBNO;
              
                model.ACnfJobexp.EXPID = null;
                model.ACnfJobexp.EXPAMT = null;
                model.ACnfJobexp.REMARKS = "";

                return RedirectToAction("Edit");
            }
            else if (command == "Add")
            {
                model.ACnfJobexp.USERPC = strHostName;
                model.ACnfJobexp.INSIPNO = ipAddress.ToString();
                model.ACnfJobexp.INSTIME = Convert.ToDateTime(td);
                //Insert User ID save AslUSerco table attribute INSUSERID
                model.ACnfJobexp.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                //.........................................................Create Permission Check
                var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
                var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

                var createStatus = "";

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
                string query = string.Format(
                        "SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='JobExpenseInfo' AND COMPID='{0}' AND USERID = '{1}'",
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
                    TempData["Model"] = model;
                    //TempData["ExpCID"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;

                    TempData["ShowAddButton"] = "Show Add Button";
                    TempData["message"] = "Permission not granted!";
                    return RedirectToAction("Edit");
                }
                //...............................................................................................

                var res = db.CnfJobexpDbSet.Where(a => a.COMPID == model.ACnfJobexpmst.COMPID && a.TRANSNO == model.ACnfJobexpmst.TRANSNO && a.TRANSYY == model.ACnfJobexpmst.TRANSYY).Count(a => a.EXPID == model.ACnfJobexp.EXPID) != 0;
                if (res == true)//found ID
                {
                    var result = db.CnfJobexpDbSet.Where(a => a.COMPID == model.ACnfJobexpmst.COMPID && a.TRANSNO == model.ACnfJobexpmst.TRANSNO && a.TRANSYY == model.ACnfJobexpmst.TRANSYY && a.EXPID == model.ACnfJobexp.EXPID);
                    foreach (CNF_JOBEXP m in result)
                    {
                        m.EXPID = model.ACnfJobexp.EXPID;
                        m.EXPAMT = model.ACnfJobexp.EXPAMT;
                        m.REMARKS = model.ACnfJobexp.REMARKS;
                        m.INSLTUDE = model.ACnfJobexpmst.INSLTUDE;
                    }
                    Insert_Asl_LogData_CNF_JOBEXP(model.ACnfJobexp);
                    db.SaveChanges();

                }
                else
                {
                    var expSerial = db.CnfJobexpDbSet.Where(d => d.TRANSNO == model.ACnfJobexpmst.TRANSNO && d.TRANSYY == model.ACnfJobexpmst.TRANSYY
                          && d.COMPID == model.ACnfJobexpmst.COMPID).Max(d => d.EXPSL);

                    model.ACnfJobexp.EXPSL = Convert.ToInt64(expSerial) + 1;
                    model.ACnfJobexp.COMPID = model.ACnfJobexpmst.COMPID;
                    model.ACnfJobexp.TRANSDT = model.ACnfJobexpmst.TRANSDT;
                    model.ACnfJobexp.TRANSYY = model.ACnfJobexpmst.TRANSYY;
                    model.ACnfJobexp.TRANSNO = model.ACnfJobexpmst.TRANSNO;
                    model.ACnfJobexp.JOBYY = model.ACnfJobexpmst.JOBYY;
                    model.ACnfJobexp.JOBTP = model.ACnfJobexpmst.JOBTP;
                    model.ACnfJobexp.JOBNO = model.ACnfJobexpmst.JOBNO;
                    model.ACnfJobexp.EXPCD = model.ACnfJobexpmst.EXPCD;
                    model.ACnfJobexp.INSLTUDE = model.ACnfJobexpmst.INSLTUDE;

                    Insert_Asl_LogData_CNF_JOBEXP(model.ACnfJobexp);
                    db.CnfJobexpDbSet.Add(model.ACnfJobexp);
                    db.SaveChanges();

                }

                string date = model.ACnfJobexpmst.TRANSDT.ToString();
                DateTime MyDateTime = DateTime.Parse(date);
                string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                TempData["transdate"] = currentDate;
                TempData["ShowAddButton"] = "Show Add Button";
                TempData["Model"] = model;
                TempData["TRANSNO"] = model.ACnfJobexpmst.TRANSNO;
                TempData["TRANSYY"] = model.ACnfJobexpmst.TRANSYY;
                TempData["JOBNO"] = model.ACnfJobexpmst.JOBNO; 
                TempData["Current_UPDLTUDE"] = model.ACnfJobexpmst.INSLTUDE;
                model.ACnfJobexp.EXPID = null;
                model.ACnfJobexp.EXPAMT = null;
                model.ACnfJobexp.REMARKS = "";

                return RedirectToAction("Edit");
            }
            else if (command == "Update")
            {
                var result = db.CnfJobexpDbSet.Where(a => a.COMPID == model.ACnfJobexpmst.COMPID && a.TRANSNO == model.ACnfJobexpmst.TRANSNO && a.TRANSYY == model.ACnfJobexpmst.TRANSYY && a.EXPID == model.ACnfJobexp.EXPID);
                CNF_JOBEXP jobExp_logData = new CNF_JOBEXP();
                foreach (CNF_JOBEXP m in result)
                {
                    m.EXPID = model.ACnfJobexp.EXPID;
                    m.EXPAMT = model.ACnfJobexp.EXPAMT;
                    m.REMARKS = model.ACnfJobexp.REMARKS;

                    m.USERPC = strHostName;
                    m.UPDIPNO = ipAddress.ToString();
                    m.UPDTIME = Convert.ToDateTime(td);
                    m.UPDLTUDE = model.ACnfJobexpmst.INSLTUDE;
                    //Insert User ID save AslUSerco table attribute INSUSERID
                    m.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    jobExp_logData = m;
                }

                Update_Asl_LogData_CNF_JOBEXP(jobExp_logData);
                db.SaveChanges();

                string date = model.ACnfJobexpmst.TRANSDT.ToString();
                DateTime MyDateTime = DateTime.Parse(date);
                string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                TempData["transdate"] = currentDate;
                TempData["ShowAddButton"] = "Show Add Button";
                TempData["Model"] = model;
                TempData["TRANSNO"] = model.ACnfJobexpmst.TRANSNO;
                TempData["TRANSYY"] = model.ACnfJobexpmst.TRANSYY;
                TempData["JOBNO"] = model.ACnfJobexpmst.JOBNO;
                TempData["Current_UPDLTUDE"] = model.ACnfJobexpmst.INSLTUDE;
                model.ACnfJobexp.EXPID = null;
                model.ACnfJobexp.EXPAMT = null;
                model.ACnfJobexp.REMARKS = "";

                return RedirectToAction("Edit");
            }
            else if (command == "Complete")
            {
                model = null;
                TempData["Model"] = model;
                TempData["message"] = "Complete Successfully!";
                return RedirectToAction("Edit");
            }
            else if (command == "Print")
            {
                //CNF_JOBEXPMST aCnfJobexpmst=new CNF_JOBEXPMST();
                List<CNF_JOBEXPMST> getJobexpmsts = new List<CNF_JOBEXPMST>();
                var result = (db.CnfJobexpmstDbSet.Where(n => n.COMPID == model.ACnfJobexpmst.COMPID &&
                                                   n.TRANSDT == model.ACnfJobexpmst.TRANSDT &&
                                                   n.JOBNO == model.ACnfJobexpmst.JOBNO && n.TRANSNO == model.ACnfJobexpmst.TRANSNO && n.JOBYY == model.ACnfJobexpmst.JOBYY)
               .Select(n => new { compid = n.COMPID, expHeadCD = n.EXPCD, transdate = n.TRANSDT, JobNo = n.JOBNO, transNo = n.TRANSNO, jobyear = n.JOBYY })).ToList();
                foreach (var VARIABLE in result)
                {
                    getJobexpmsts.Add(new CNF_JOBEXPMST() { COMPID = VARIABLE.compid, TRANSDT = VARIABLE.transdate, EXPCD = VARIABLE.expHeadCD, JOBNO = VARIABLE.JobNo, TRANSNO = VARIABLE.transNo, JOBYY = VARIABLE.jobyear });
                }
                //var pdf = new PdfResult(getJobexpmsts, "GetExpenseVoucharReport");
                //return pdf;
                TempData["GetExpenseVoucharReport_List"] = getJobexpmsts;
                return RedirectToAction("GetExpenseVoucharReport");

            }
            return RedirectToAction("Edit");

        }



        public ActionResult GetExpenseVoucharReport()
        {
            var list = TempData["GetExpenseVoucharReport_List"];
            return View(list);
        }


        public ActionResult gridUpdate_Edit(Int64 id, Int64 cid, Int64 TransYear, Int64 TransNO, Int64 Serial, Int64 ExpCD, PageModel model)
        {
            model.ACnfJobexp = db.CnfJobexpDbSet.Find(id);

            Int64 jobExpmstid = 0;
            var findID = db.CnfJobexpmstDbSet.Where(e => e.COMPID == cid && e.TRANSNO == TransNO);
            foreach (var m in findID)
            {
                jobExpmstid = m.cnf_JobExpmstID;
            }
            model.ACnfJobexpmst = db.CnfJobexpmstDbSet.Find(jobExpmstid);

            var res1 = from m in db.GlAcchartDbSet where m.COMPID == cid && m.ACCOUNTCD == ExpCD select new { m.ACCOUNTNM };
            foreach (var a in res1)
            {
                TempData["AccountName"] = a.ACCOUNTNM;
            }


            var findName = (from n in db.CnfExpenseDbSet where n.COMPID == cid && n.EXPID == model.ACnfJobexp.EXPID select n).ToList();
            foreach (var n in findName)
            {
                TempData["expenseName"] = n.EXPNM;
            }

            //var cnf_Job = db.CnfJobDbSet.Where(e => e.COMPID == cid && e.JOBNO == model.ACnfJobexp.JOBNO && e.JOBTP == model.ACnfJobexp.JOBTP &&
            //            e.JOBYY == model.ACnfJobexp.JOBYY);
            //foreach (var m in cnf_Job)
            //{
            //    model.ACnfJob = m;
            //}


            string date = model.ACnfJobexpmst.TRANSDT.ToString();
            DateTime MyDateTime = DateTime.Parse(date);
            string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
            TempData["transdate"] = currentDate;
            TempData["ShowAddButton"] = null;
            TempData["Model"] = model;
            TempData["TRANSNO"] = model.ACnfJobexpmst.TRANSNO;
            TempData["TRANSYY"] = model.ACnfJobexpmst.TRANSYY;
            TempData["JOBNO"] = model.ACnfJobexp.JOBNO;
            return RedirectToAction("Edit");
        }



        public ActionResult gridDelete_Edit(Int64 id, Int64 cid, Int64 TransYear, Int64 TransNO, Int64 Serial, Int64 ExpCD, PageModel model)
        {
            //Permission Check

            Int64 LoggedCompId = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            var checkPermission = from role in db.AslRoleDbSet
                                  where role.USERID == loggedUserID && role.COMPID == LoggedCompId && role.CONTROLLERNAME == "JobExpenseInfo"
                                  select new { role.DELETER };
            string Delete = "";
            foreach (var VARIABLE in checkPermission)
            {
                Delete = VARIABLE.DELETER;
            }

            if (Delete == "I")
            {
                TempData["message"] = "Permission Denied !";
                return View("Edit");
            }
            //...................................................
            model.ACnfJobexp.cnf_JobEXPID = id;
            CNF_JOBEXP aCnfJobexp = db.CnfJobexpDbSet.Find(model.ACnfJobexp.cnf_JobEXPID);

            //aCnfJobexp.USERPC = strHostName;
            //aCnfJobexp.UPDIPNO = ipAddress.ToString();
            //aCnfJobexp.UPDTIME = Convert.ToDateTime(td);
            //aCnfJobexp.UPDLTUDE = TempData["Current_UPDLTUDE"].ToString();

          
            //aCnfJobexp.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            //CNF_JOBEXP modelDelete = db.CnfJobexpDbSet.Find(model.ACnfJobexp.cnf_JobEXPID);
            //modelDelete.USERPC = strHostName;
            //modelDelete.UPDIPNO = ipAddress.ToString();
            //modelDelete.UPDTIME = Convert.ToDateTime(td);
            //modelDelete.UPDLTUDE = TempData["Current_UPDLTUDE"].ToString();

            //Delete_ASL_DELETE_CNF_JOBEXP(modelDelete);
            //Delete_Asl_LogData_CNF_JOBEXP(modelDelete);
            //db.SaveChanges();

            db.CnfJobexpDbSet.Remove(aCnfJobexp);
            db.SaveChanges();



            Int64 jobExpmstid = 0,jobno=0;
            var findID = db.CnfJobexpmstDbSet.Where(e => e.COMPID == cid && e.TRANSNO == TransNO);
            foreach (var m in findID)
            {
                jobExpmstid = m.cnf_JobExpmstID;
                jobno=Convert.ToInt64(m.JOBNO);
            }
            model.ACnfJobexpmst = db.CnfJobexpmstDbSet.Find(jobExpmstid);
          

            var res1 = from m in db.GlAcchartDbSet where m.COMPID == cid && m.ACCOUNTCD == ExpCD select new { m.ACCOUNTNM };
            foreach (var a in res1)
            {
                TempData["AccountName"] = a.ACCOUNTNM;
            }

            string date = model.ACnfJobexpmst.TRANSDT.ToString();
            DateTime MyDateTime = DateTime.Parse(date);
            string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
            TempData["transdate"] = currentDate;
            TempData["ShowAddButton"] = "delete";
            TempData["Model"] = model;
            TempData["TRANSNO"] = model.ACnfJobexpmst.TRANSNO;
            TempData["TRANSYY"] = model.ACnfJobexpmst.TRANSYY;
            TempData["JOBNO"] = jobno;
            return RedirectToAction("Edit");

        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DateChanged(DateTime changedtxt)
        {
            Int64 transNo = 0;

            string converttoString = Convert.ToString(changedtxt.ToString("dd-MMM-yyyy"));
            string getYear = converttoString.Substring(7, 4);
            Int64 year = Convert.ToInt64(getYear);
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            Int64 maxData = Convert.ToInt64((from n in db.CnfJobexpmstDbSet
                                             where compid == n.COMPID
                                                 && year == n.TRANSYY
                                             select n.TRANSNO).Max());

            if (maxData == 0)
            {
                transNo = Convert.ToInt64(year + "00000" + 1);
            }
            else
            {
                transNo = maxData + 1;
            }

            var result = new { TRANSYY = year, TRANSNO = transNo };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult JobNOChanged(Int64 changedtxt)
        {

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var res = from n in db.CnfJobDbSet where compid == n.COMPID && changedtxt == n.Cnf_JobID select new { n.JOBYY, n.JOBTP };
            string jobType = "";
            Int64 year = 0;
            foreach (var VARIABLE in res)
            {
                year = Convert.ToInt64(VARIABLE.JOBYY);
                jobType = VARIABLE.JOBTP;
            }
            var result = new { JOBYY = year, JOBTP = jobType };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetJOBNO(string theDate)
        {
            DateTime dt = Convert.ToDateTime(theDate);
            //Datetime formet
            IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
            DateTime dd = DateTime.Parse(theDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime datetm = Convert.ToDateTime(dd);

            List<SelectListItem> trans = new List<SelectListItem>();
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var transres = (from n in db.CnfJobexpmstDbSet
                            where n.TRANSDT == dd && n.COMPID == compid
                            select new
                            {
                                n.TRANSNO
                            }
                            )
                            .ToList();

            string transNO = null;
            foreach (var f in transres)
            {
                trans.Add(new SelectListItem { Text = f.TRANSNO.ToString(), Value = f.TRANSNO.ToString() });
            }
            return Json(new SelectList(trans, "Value", "Text"));
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult TRANSNOChanged(Int64 changedDropDown)
        {
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var res = from n in db.CnfJobexpmstDbSet where compid == n.COMPID && changedDropDown == n.TRANSNO select new { n.JOBYY, n.JOBTP, n.TRANSYY, n.JOBNO, n.REMARKS, n.EXPCD, n.TRANSDT };
            string jobType = "", Remarks = "", AccountName = "", transDate = "";
            Int64 JobYear = 0, transYear = 0, JobNo = 0, Expcd = 0;
            foreach (var m in res)
            {
                JobYear = Convert.ToInt64(m.JOBYY);
                jobType = m.JOBTP;
                transYear = Convert.ToInt64(m.TRANSYY);
                JobNo = Convert.ToInt64(m.JOBNO);
                Remarks = m.REMARKS;
                Expcd = Convert.ToInt64(m.EXPCD);

                string date = m.TRANSDT.ToString();
                DateTime MyDateTime = DateTime.Parse(date);
                transDate = MyDateTime.ToString("dd-MMM-yyyy");
            }

            var res1 = from m in db.GlAcchartDbSet where m.COMPID == compid && m.ACCOUNTCD == Expcd select new { m.ACCOUNTNM };
            foreach (var a in res1)
            {
                AccountName = a.ACCOUNTNM;
            }
            var result = new { JOBYY = JobYear, JOBTP = jobType, TRANSYY = transYear, JOBNO = JobNo, REMARKS = Remarks, ACCOUNTNM = AccountName, EXPCD = Expcd, TRANSDT = transDate };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JobNofordropdown(Int64 changedtxt, string changedtxt2)
        {
            Int64 compid = Convert.ToInt64(changedtxt2);



            List<SelectListItem> listJobNo = new List<SelectListItem>();


            var resultJobNo = (db.CnfJobDbSet.Join(db.GlAcchartDbSet, cnfjob => cnfjob.COMPID, glacchart => glacchart.COMPID,
                (cnfjob, glacchart) => new { cnfjob, glacchart })
                .Where(a => a.cnfjob.PARTYID == a.glacchart.ACCOUNTCD && a.cnfjob.COMPID == compid && a.cnfjob.JOBYY == changedtxt)
                .Select(a => new { a.cnfjob.JOBNO, a.cnfjob.JOBYY, a.cnfjob.JOBTP, a.glacchart.ACCOUNTNM, a.cnfjob.Cnf_JobID }).OrderBy(a => a.JOBNO));
            foreach (var n in resultJobNo)
            {
                if (n != null)
                {
                    listJobNo.Add(new SelectListItem { Text = (n.JOBNO.ToString() + " | " + n.JOBYY.ToString() + " | " + n.JOBTP + " | " + n.ACCOUNTNM.ToString()), Value = n.Cnf_JobID.ToString() });
                }
            }






            return Json(new SelectList(listJobNo, "Value", "Text"));





        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
