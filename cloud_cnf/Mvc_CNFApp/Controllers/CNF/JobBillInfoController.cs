using System.Data;
using System.Data.SqlClient;
using System.Net;
using Mvc_CNFApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorPDF;

namespace Mvc_CNFApp.Controllers
{
    public class JobBillInfoController : AppController
    {
        private CnfDbContext db = new CnfDbContext();

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public JobBillInfoController()
        {
            ViewData["HighLight_Menu_CnfForm"] = "heighlight";
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }



        //create asl_log object
        public ASL_LOG aslLog = new ASL_LOG();

        //  SAVE ALL INFORMATION from CNF_JOBBILL TO Asl_lOG Database Table.
        public void Insert_CNF_JOBBILL_LogData(CNF_JOBBILL aCnfJobbill)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aCnfJobbill.COMPID && n.USERID == aCnfJobbill.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aCnfJobbill.COMPID);
            aslLog.USERID = aCnfJobbill.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCnfJobbill.INSIPNO;
            aslLog.LOGLTUDE = aCnfJobbill.INSLTUDE;
            aslLog.TABLEID = "CNF_JOBBILL";
            aslLog.LOGDATA = Convert.ToString("JOBNO: " + aCnfJobbill.JOBNO + ",\nJOB Year: " + aCnfJobbill.JOBYY +",\nBill Amount: " + aCnfJobbill.BILLAMT + ",\nEXP Date: " + aCnfJobbill.EXPPDT + ",\nEXP Name: " + aCnfJobbill.EXPNM + ",\nParty Name: " + aCnfJobbill.PARTYNM + ",\nRemarks: " + aCnfJobbill.REMARKS + ",\nBill SL: " + aCnfJobbill.BILLSL + ".");
            aslLog.USERPC = aCnfJobbill.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Update_CNF_JOBBILL_LogData(CNF_JOBBILL aCnfJobbill)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aCnfJobbill.COMPID && n.USERID == aCnfJobbill.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aCnfJobbill.COMPID);
            aslLog.USERID = aCnfJobbill.UPDUSERID;
            aslLog.LOGTYPE = "Update";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCnfJobbill.UPDIPNO;
            aslLog.LOGLTUDE = aCnfJobbill.UPDLTUDE;
            aslLog.TABLEID = "CNF_JOBBILL";
            aslLog.LOGDATA = Convert.ToString("JOBNO: " + aCnfJobbill.JOBNO + ",\nJOB Year: " + aCnfJobbill.JOBYY  + ",\nBill Amount: " + aCnfJobbill.BILLAMT + ",\nEXP Date: " + aCnfJobbill.EXPPDT + ",\nEXP Name: " + aCnfJobbill.EXPNM + ",\nParty Name: " + aCnfJobbill.PARTYNM + ",\nRemarks: " + aCnfJobbill.REMARKS + ",\nBill SL: " + aCnfJobbill.BILLSL + ".");
            aslLog.USERPC = aCnfJobbill.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        public void Delete_CNF_JOBBILL_LogData(CNF_JOBBILL aCnfJobbill)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aCnfJobbill.COMPID && n.USERID == aCnfJobbill.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aCnfJobbill.COMPID);
            aslLog.USERID = aCnfJobbill.UPDUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCnfJobbill.UPDIPNO;
            aslLog.LOGLTUDE = aCnfJobbill.UPDLTUDE;
            aslLog.TABLEID = "CNF_JOBBILL";
            aslLog.LOGDATA = Convert.ToString("JOBNO: " + aCnfJobbill.JOBNO + ",\nJOB Year: " + aCnfJobbill.JOBYY  + ",\nBill Amount: " + aCnfJobbill.BILLAMT + ",\nEXP Date: " + aCnfJobbill.EXPPDT + ",\nEXP Name: " + aCnfJobbill.EXPNM + ",\nParty Name: " + aCnfJobbill.PARTYNM + ",\nRemarks: " + aCnfJobbill.REMARKS + ",\nBill SL: " + aCnfJobbill.BILLSL + ".");
            aslLog.USERPC = aCnfJobbill.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        public ASL_DELETE AslDelete = new ASL_DELETE();
        public void Delete_Asl_Delete(CNF_JOBBILL aCnfJobbill)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == aCnfJobbill.COMPID && n.USERID == aCnfJobbill.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(aCnfJobbill.COMPID);
            AslDelete.USERID = aCnfJobbill.UPDUSERID;
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = aCnfJobbill.UPDIPNO;
            AslDelete.DELLTUDE = aCnfJobbill.UPDLTUDE;
            AslDelete.TABLEID = "CNF_JOBBILL";
            AslDelete.DELDATA = Convert.ToString("JOBNO: " + aCnfJobbill.JOBNO + ",\nJOB Year: " + aCnfJobbill.JOBYY + ",\nBill Amount: " + aCnfJobbill.BILLAMT + ",\nEXP Date: " + aCnfJobbill.EXPPDT + ",\nEXP Name: " + aCnfJobbill.EXPNM + ",\nParty Name: " + aCnfJobbill.PARTYNM + ",\nRemarks: " + aCnfJobbill.REMARKS + ",\nBill SL: " + aCnfJobbill.BILLSL + ".");
            AslDelete.USERPC = aCnfJobbill.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }


        // GET: /JobBillInfo/
        public ActionResult Index()
        {
            var dt = (CNF_JOBBILL)TempData["cnfJobBill"];
            return View(dt);

        }

        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(CNF_JOBBILL aCnfJobbill, string command)
        {
            if (command == "Add")
            {
                //.........................................................Create Permission Check
                var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
                var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();
                var createStatus = "";

                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
                string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='JobBillInfo' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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
                    TempData["ShowAddButton"] = "Show Add Button";
                    TempData["cnfJobBill"] = aCnfJobbill;
                    TempData["JobNo"] = aCnfJobbill.JOBNO;
                    TempData["JobYear"] = aCnfJobbill.JOBYY;
                    TempData["BillDate"] = aCnfJobbill.BILLPDT;
                    TempData["message"] = "Permission not granted!";
                    return RedirectToAction("Index");
                }

                aCnfJobbill.USERPC = strHostName;
                aCnfJobbill.INSIPNO = ipAddress.ToString();
                aCnfJobbill.INSTIME = Convert.ToDateTime(td);
                aCnfJobbill.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                aCnfJobbill.INSLTUDE = aCnfJobbill.INSLTUDE;

                aCnfJobbill.COMPID = Convert.ToInt64(LoggedCompId);


                var res = (from n in db.CnfExpenseDbSet where n.EXPID == aCnfJobbill.EXPID select new { n.EXPNM });
                foreach (var re in res)
                {
                    aCnfJobbill.EXPNM = re.EXPNM;
                }

                Int64 jobnO = Convert.ToInt64(aCnfJobbill.JOBNO);

                var result =
                    (from n in db.CnfJobDbSet
                     where n.COMPID == aCnfJobbill.COMPID && n.Cnf_JobID == aCnfJobbill.JOBNO
                     select new { n.JOBNO });
                foreach (var VARIABLE in result)
                {
                    aCnfJobbill.JOBNO = VARIABLE.JOBNO;
                }

                TempData["JobNo"] = aCnfJobbill.JOBNO;
                TempData["JobYear"] = aCnfJobbill.JOBYY;
                TempData["JobTp"] = aCnfJobbill.JOBTP;
                DateTime ab = Convert.ToDateTime(aCnfJobbill.BILLPDT);
                string billdate = ab.ToString("dd-MMM-yyyy");
                TempData["BillDate"] = billdate;
                TempData["PartyName"] = aCnfJobbill.PARTYNM;
                Insert_CNF_JOBBILL_LogData(aCnfJobbill);


                db.CnfJobbillsDbSet.Add(aCnfJobbill);
                db.SaveChanges();

                aCnfJobbill.JOBNO = jobnO;
                TempData["cnfJobBill"] = aCnfJobbill;
                //TempData["JobYear"] = aCnfJobbill.JOBYY;
                TempData["ShowAddButton"] = "Show Add Button";

                aCnfJobbill.EXPID = 0;
                aCnfJobbill.BILLAMT = 0;
                aCnfJobbill.BILLSL = 0;
                aCnfJobbill.REMARKS = "";

                return RedirectToAction("Index");

            }
            if (command == "Submit")
            {
                if (aCnfJobbill.JOBNO != 0)
                {

                    //Get Ip ADDRESS,Time & user PC Name
                    string strHostName = System.Net.Dns.GetHostName();
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];


                    aCnfJobbill.USERPC = strHostName;
                    aCnfJobbill.INSIPNO = ipAddress.ToString();
                    aCnfJobbill.INSTIME = Convert.ToDateTime(td);
                    //Insert User ID save POS_ITEMMST table attribute (INSUSERID) field
                    aCnfJobbill.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                    Int64 companyID = Convert.ToInt64(aCnfJobbill.COMPID);

                    aCnfJobbill.EXPNM = "";
                    aCnfJobbill.BILLAMT = 0;
                    aCnfJobbill.BILLSL = 0;
                    Int64 jobnO = Convert.ToInt64(aCnfJobbill.JOBNO);
                    TempData["ShowAddButton"] = "Show Add Button";
                    var result = (from n in db.CnfJobDbSet
                                  where n.COMPID == aCnfJobbill.COMPID && n.Cnf_JobID == aCnfJobbill.JOBNO
                                  select new { n.JOBNO });
                    foreach (var VARIABLE in result)
                    {
                        aCnfJobbill.JOBNO = VARIABLE.JOBNO;
                    }

                    var findbillpdt =
             (from n in db.CnfJobbillsDbSet where n.COMPID == aCnfJobbill.COMPID && n.JOBNO == aCnfJobbill.JOBNO && n.JOBYY == aCnfJobbill.JOBYY
                  && n.JOBTP == aCnfJobbill.JOBTP select n)
                 .ToList();
                    string billdate = "";
                    foreach (var cnfJobbill in findbillpdt)
                    {
                        if (cnfJobbill.PARTYNM == aCnfJobbill.PARTYNM)
                        {
                            DateTime ab = Convert.ToDateTime(cnfJobbill.BILLPDT);
                            billdate = ab.ToString("dd-MMM-yyyy");
                            break;
                        }

                    }
                    if (findbillpdt.Count == 0)
                    {
                        billdate = "";
                    }
                    TempData["JobNo"] = aCnfJobbill.JOBNO;
                    TempData["JobYear"] = aCnfJobbill.JOBYY;
                    TempData["JobTp"] = aCnfJobbill.JOBTP;
                    TempData["BillDate"] = billdate;
                    TempData["PartyName"] = aCnfJobbill.PARTYNM;
                    //  TempData["HeadTP"] = aCnfJobbill.GLACCHARMSTModel.HEADTP;
                    TempData["latitute_deleteAccount"] = aCnfJobbill.INSLTUDE;
                    //aCnfJobbill.JOBNO = jobnO;
                    TempData["cnfJobBill"] = aCnfJobbill;

                    return RedirectToAction("Index");
                }
            }
            if (command == "Update")
            {
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                Int64 jobnO = Convert.ToInt64(aCnfJobbill.JOBNO);
                var result = (from n in db.CnfJobDbSet
                              where n.COMPID == aCnfJobbill.COMPID && n.Cnf_JobID == aCnfJobbill.JOBNO
                              select new { n.JOBNO });
                foreach (var VARIABLE in result)
                {
                    aCnfJobbill.JOBNO = VARIABLE.JOBNO;
                }
                TempData["JobNo"] = aCnfJobbill.JOBNO;

                var query =
                    from a in db.CnfJobbillsDbSet
                    where (a.Cnf_JobBillID == aCnfJobbill.Cnf_JobBillID && a.COMPID == aCnfJobbill.COMPID)
                    select a;

                var res = (from n in db.CnfExpenseDbSet where n.EXPID == aCnfJobbill.EXPID select new { n.EXPNM });
                foreach (var re in res)
                {
                    aCnfJobbill.EXPNM = re.EXPNM;
                }
                foreach (CNF_JOBBILL a in query)
                {

                    a.JOBNO = aCnfJobbill.JOBNO;
                    a.JOBTP = aCnfJobbill.JOBTP;
                    a.JOBYY = aCnfJobbill.JOBYY;
                    a.PARTYNM = aCnfJobbill.PARTYNM;

                    a.EXPID = aCnfJobbill.EXPID;
                    a.EXPNM = aCnfJobbill.EXPNM;


                    //a.EXPNM = a.EXPNM;
                    a.BILLAMT = aCnfJobbill.BILLAMT;
                    a.EXPPDT = aCnfJobbill.EXPPDT;
                    a.REMARKS = aCnfJobbill.REMARKS;
                    a.BILLSL = aCnfJobbill.BILLSL;


                    a.UPDIPNO = ipAddress.ToString();
                    a.UPDTIME = Convert.ToDateTime(td);
                    a.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    a.UPDLTUDE = aCnfJobbill.INSLTUDE;
                    a.USERPC = strHostName;

                    aCnfJobbill.UPDIPNO = ipAddress.ToString();
                    aCnfJobbill.UPDTIME = Convert.ToDateTime(td);
                    aCnfJobbill.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    aCnfJobbill.UPDLTUDE = aCnfJobbill.INSLTUDE;
                    aCnfJobbill.USERPC = strHostName;
                }

                Update_CNF_JOBBILL_LogData(aCnfJobbill);

                db.SaveChanges();
                TempData["ShowAddButton"] = "Show Add Button";
                TempData["JobYear"] = aCnfJobbill.JOBYY;
              
                TempData["JobTp"] = aCnfJobbill.JOBTP;
                DateTime ab = Convert.ToDateTime(aCnfJobbill.BILLPDT);
                string billdate = ab.ToString("dd-MMM-yyyy");
                TempData["BillDate"] = billdate;
              
                TempData["PartyName"] = aCnfJobbill.PARTYNM;


                aCnfJobbill.EXPID = 0;
                aCnfJobbill.BILLAMT = 0;
                aCnfJobbill.BILLSL = 0;
                aCnfJobbill.REMARKS = "";
                aCnfJobbill.JOBNO = jobnO;
                TempData["cnfJobBill"] = aCnfJobbill;

                return RedirectToAction("Index");
            }
            if (command == "Print")
            {
                var result = (from n in db.CnfJobDbSet
                              where n.COMPID == aCnfJobbill.COMPID && n.Cnf_JobID == aCnfJobbill.JOBNO
                              select new { n.JOBNO });
                foreach (var VARIABLE in result)
                {
                    aCnfJobbill.JOBNO = VARIABLE.JOBNO;
                }
                //var pdf = new PdfResult(aCnfJobbill, "GetBillReport");
                //return pdf;
                if (aCnfJobbill.COMPID == 101)
                {
                    TempData["GetBillReport_Model"] = aCnfJobbill;
                    return RedirectToAction("GetBillReport");
                }
                else if (aCnfJobbill.COMPID == 102)
                {
                    TempData["GetBillReport_Model"] = aCnfJobbill;
                    return RedirectToAction("GetBillReport2");
                }
                else if(aCnfJobbill.COMPID==106)
                {
                    TempData["GetBillReport_Model"] = aCnfJobbill;
                    return RedirectToAction("GetBillReport3");
                }
                else if (aCnfJobbill.COMPID == 105)
                {
                    TempData["GetBillReport_Model"] = aCnfJobbill;
                    return RedirectToAction("GetBillReport4");
                }
                else if (aCnfJobbill.COMPID == 108)
                {
                    TempData["GetBillReport_Model"] = aCnfJobbill;
                    return RedirectToAction("GetBillReportCtc");
                }
                else if(aCnfJobbill.COMPID==110)
                {
                    TempData["GetBillReport_Model"] = aCnfJobbill;
                    return RedirectToAction("GetBillReport10");
                }
                else if (aCnfJobbill.COMPID == 111)
                {
                    TempData["GetBillReport_Model"] = aCnfJobbill;
                    return RedirectToAction("GetBillReport11");
                }
                else if (aCnfJobbill.COMPID == 112)
                {
                    TempData["GetBillReport_Model"] = aCnfJobbill;
                    return RedirectToAction("GetBillReport12");
                }
                else if (aCnfJobbill.COMPID == 113)
                {
                    TempData["GetBillReport_Model"] = aCnfJobbill;
                    return RedirectToAction("GetBillReport13");
                }
                else
                {
                    TempData["GetBillReport_Model"] = aCnfJobbill;
                    return RedirectToAction("GetBillReportDemo");
                }

               

            }
            return RedirectToAction("Index");
        }




        public ActionResult GetBillReport()
        {
            CNF_JOBBILL model = (CNF_JOBBILL)TempData["GetBillReport_Model"];
            return View(model);
        }

        public ActionResult GetBillReportDemo()
        {
            CNF_JOBBILL model = (CNF_JOBBILL)TempData["GetBillReport_Model"];
            return View(model);
        }

        public ActionResult GetBillReport2()
        {
            CNF_JOBBILL model = (CNF_JOBBILL)TempData["GetBillReport_Model"];
            return View(model);
        }

        public ActionResult GetBillReport3()
        {
            CNF_JOBBILL model = (CNF_JOBBILL)TempData["GetBillReport_Model"];
            return View(model);
        }
        public ActionResult GetBillReport4()
        {
            CNF_JOBBILL model = (CNF_JOBBILL)TempData["GetBillReport_Model"];
            return View(model);
        }

        public ActionResult GetBillReportCtc()
        {
            CNF_JOBBILL model = (CNF_JOBBILL)TempData["GetBillReport_Model"];
            return View(model);
        }
        public ActionResult GetBillReport10()
        {
            CNF_JOBBILL model = (CNF_JOBBILL)TempData["GetBillReport_Model"];
            return View(model);
        }

        public ActionResult GetBillReport11()
        {
            CNF_JOBBILL model = (CNF_JOBBILL)TempData["GetBillReport_Model"];
            return View(model);
        }

        public ActionResult GetBillReport12()
        {
            CNF_JOBBILL model = (CNF_JOBBILL)TempData["GetBillReport_Model"];
            return View(model);
        }


        public ActionResult GetBillReport13()
        {
            CNF_JOBBILL model = (CNF_JOBBILL)TempData["GetBillReport_Model"];
            return View(model);
        }

        //.....Job No Select and filled the other field ....Start.....
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult JobNoChanged_getJobinfo(Int64 changedtxt)
        {
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var selectdata = from n in db.CnfJobDbSet where compid == n.COMPID && changedtxt == n.Cnf_JobID select new {n.JOBNO, n.JOBYY, n.JOBTP, n.PARTYID };
            string jobType = "", partyName = "";
            Int64 jobYear = 0, partyid = 0,jobno=0;
            foreach (var l in selectdata)
            {
                jobType = l.JOBTP;
                jobYear = Convert.ToInt64(l.JOBYY);
                partyid = Convert.ToInt64(l.PARTYID);
                jobno = Convert.ToInt64(l.JOBNO);
            }

            var findpartyName = from n in db.GlAcchartDbSet where n.ACCOUNTCD == partyid select new { n.ACCOUNTNM };
            foreach (var VARIABLE in findpartyName)
            {
                partyName = VARIABLE.ACCOUNTNM;
            }
         

            var result = new { Year = jobYear, Type = jobType, Party = partyName };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //.....Job No Select and filled the other field ....End.....




        public ActionResult EditJobBillUpdate(Int64 id, Int64 cid, Int64 jobNo, Int64 jobyear, string jobtype, DateTime BillDate, Int64 expid, string expnm, CNF_JOBBILL model)
        {
            //.........................................................Create Permission Check
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

            var updateStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
            string query1 = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='JobBillInfo' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query1, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                updateStatus = row["UPDATER"].ToString();
            }
            conn.Close();
            if (updateStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("Index");
            }
            //...............................................................................................

            //add the data from database to model
            var pid = from m in db.CnfJobbillsDbSet where m.Cnf_JobBillID == id && m.COMPID == cid select m;
            foreach (var Result in pid)
            {
                model.COMPID = cid;
                model.Cnf_JobBillID = id;
                model.JOBNO = jobNo;
                model.JOBYY = jobyear;
                model.JOBTP = jobtype;
                model.PARTYNM = Result.PARTYNM;
                model.BILLPDT = Result.BILLPDT;
                model.EXPID = expid;
                model.EXPNM = expnm;
                model.BILLAMT = Result.BILLAMT;
                model.EXPPDT = Result.EXPPDT;
                model.REMARKS = Result.REMARKS;
                model.BILLSL = Result.BILLSL;

            }

            string date = model.EXPPDT.ToString();
            DateTime y = DateTime.Parse(date);
            string xxx = y.ToString("dd-MMM-yyyy");

            TempData["expdate"] = xxx;
            TempData["JobNo"] = model.JOBNO;
            TempData["JobYear"] = model.JOBYY;

            TempData["JobTp"] = model.JOBTP;
            string billdate = BillDate.ToString("dd-MMM-yyyy");
            TempData["BillDate"] = billdate;
         
            TempData["PartyName"] = model.PARTYNM;
            TempData["ShowAddButton"] = null;


            var result = (from n in db.CnfJobDbSet
                          where n.COMPID == cid && n.JOBNO == jobNo && n.JOBYY == jobyear && n.JOBTP == jobtype
                          select new { n.Cnf_JobID });
            foreach (var VARIABLE in result)
            {
                model.JOBNO = VARIABLE.Cnf_JobID;
            }
            TempData["cnfJobBill"] = model;




            return RedirectToAction("Index");

        }




        public ActionResult BillingJobDelete(Int64 id, Int64 cid, Int64 jobNo, Int64 jobyear, string jobtype, string partyname, DateTime BillDate, CNF_JOBBILL model)
        {
            //.........................................................Create Permission Check
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

            var deleteStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
            string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='JobBillInfo' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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
                return RedirectToAction("Index");

            }
            //...............................................................................................

            CNF_JOBBILL pid = db.CnfJobbillsDbSet.Find(id);
            //Get Ip ADDRESS,Time & user PC Name
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            pid.USERPC = strHostName;
            pid.UPDIPNO = ipAddress.ToString();
            pid.UPDTIME = Convert.ToDateTime(td);
            //Delete User ID save POS_ITEMMST table attribute (UPDUSERID) field
            pid.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            var de = from m in db.CnfJobbillsDbSet where m.Cnf_JobBillID == id && m.COMPID == cid select m;
            foreach (var Result in de)
            {
                model.COMPID = cid;
                model.Cnf_JobBillID = id;
                model.JOBNO = jobNo;
                model.JOBYY = jobyear;
                model.JOBTP = jobtype;
                model.PARTYNM = Result.PARTYNM;
                model.BILLPDT = Result.BILLPDT;
                model.INSLTUDE = Result.INSLTUDE;
                model.EXPID = Result.EXPID;
                model.BILLAMT = Result.BILLAMT;
                model.EXPPDT = Result.EXPPDT;
                model.EXPNM = Result.EXPNM;
                model.REMARKS = Result.REMARKS;
                model.BILLSL = Result.BILLSL;
            }

            model.USERPC = pid.USERPC;
            model.UPDIPNO = pid.UPDIPNO;
            model.UPDTIME = Convert.ToDateTime(pid.UPDTIME);
            model.UPDUSERID = Convert.ToInt64(pid.UPDUSERID);
            model.UPDLTUDE = model.INSLTUDE;

            Delete_Asl_Delete(model);
            Delete_CNF_JOBBILL_LogData(model);

            TempData["ShowAddButton"] = "Show Add Button";
            TempData["JobNo"] = model.JOBNO;
            TempData["JobTp"] = model.JOBTP;
            TempData["JobYear"] = model.JOBYY;
            TempData["PartyName"] = model.PARTYNM;

            string billdate = BillDate.ToString("dd-MMM-yyyy");
            TempData["BillDate"] = billdate;

            var result = (from n in db.CnfJobDbSet
                          where n.COMPID == cid && n.JOBNO == jobNo && n.JOBYY == jobyear && n.JOBTP == jobtype
                          select new { n.Cnf_JobID });
            foreach (var VARIABLE in result)
            {
                model.JOBNO = VARIABLE.Cnf_JobID;
            }
            TempData["cnfJobBill"] = model;

            db.CnfJobbillsDbSet.Remove(pid);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //[AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getJobno(Int64 changedtxt, string changedtxt2, string typechange)
        {
            Int64 compid = Convert.ToInt64(changedtxt2);
            List<SelectListItem> listJobNo = new List<SelectListItem>();
            //var resultJobNo = (db.CnfJobDbSet.Join(db.GlAcchartDbSet, cnfjob => cnfjob.COMPID, glacchart => glacchart.COMPID,
            //(cnfjob, glacchart) => new { cnfjob, glacchart })
            //.Where(@t => @t.cnfjob.PARTYID == @t.glacchart.ACCOUNTCD && @t.cnfjob.COMPID == compid && t.cnfjob.JOBYY == changedtxt && t.cnfjob.JOBTP == typechange)
            //.Select(@t => new { @t.cnfjob.JOBNO, @t.cnfjob.JOBYY, @t.cnfjob.JOBTP, @t.glacchart.ACCOUNTNM, @t.cnfjob.Cnf_JobID }).OrderBy(@t => @t.JOBNO));
            var resultJobNo = (from n in db.CnfJobDbSet where n.COMPID == compid && n.JOBYY == changedtxt && n.JOBTP == typechange select n).ToList();
            foreach (var n in resultJobNo)
            {
                
                if (n != null)
                {
                    listJobNo.Add(new SelectListItem { Text = (n.JOBNO.ToString() + "|" + n.JOBYY.ToString() + "|" + n.JOBTP + "|" + n.PartyName.ToString()), Value = n.Cnf_JobID.ToString() });
                }
            }
            return Json(new SelectList(listJobNo, "Value", "Text"));
         }


    }
}
