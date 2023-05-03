using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Mvc_CNFApp.Models;
using System.Net;

namespace Mvc_CNFApp.Controllers
{
    public class ReceiveJobInfoController : AppController
    {
        private CnfDbContext db = new CnfDbContext();

        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public ReceiveJobInfoController()
        {
            ViewData["HighLight_Menu_CnfForm"] = "heighlight";
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }



        public ASL_LOG aslLog = new ASL_LOG();

        public void Insert_Asl_LogData(CNF_JOBRCV aCnfJobrcv)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == aCnfJobrcv.COMPID && n.USERID == aCnfJobrcv.INSUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(aCnfJobrcv.COMPID);
            aslLog.USERID = aCnfJobrcv.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCnfJobrcv.INSIPNO;
            aslLog.LOGLTUDE = aCnfJobrcv.INSLTUDE;
            aslLog.TABLEID = "CNF_JOBRCV";

            string partyname = "",receivedTo="";
            var partynamefind =
                (from n in db.CnfPartyDbSet
                 where n.COMPID == aCnfJobrcv.COMPID && n.PARTYID == aCnfJobrcv.PARTYID
                 select n).ToList();
            foreach (var name in partynamefind)
            {
                partyname = name.PARTYNM;
            }

            var receivedTofind =
               (from n in db.GlAcchartDbSet
                where n.COMPID == aCnfJobrcv.COMPID && n.ACCOUNTCD == aCnfJobrcv.DEBITCD
                select n).ToList();
            foreach (var name in receivedTofind)
            {
                receivedTo = name.ACCOUNTNM;
            }
            aslLog.LOGDATA =
                Convert.ToString("TransDate: " + aCnfJobrcv.TRANSDT + ",\nTransYY: " + aCnfJobrcv.TRANSYY + ",\nTransNo: " + aCnfJobrcv.TRANSNO + ",\nTransFor: " + aCnfJobrcv.TRANSFOR + ",\nJOBNO: " + aCnfJobrcv.JOBNO + ",\nParty Name: " + partyname + ",\nJob Year: " + aCnfJobrcv.JOBYY + ",\nJobTP: " + aCnfJobrcv.JOBTP + ",\nReceived To: " + receivedTo + ",\nRemarks: " + aCnfJobrcv.REMARKS + ",\nAmount: " + aCnfJobrcv.AMOUNT + ".");
            aslLog.USERPC = aCnfJobrcv.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Update_Asl_LogData(CNF_JOBRCV aCnfJobrcv)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == aCnfJobrcv.COMPID && n.USERID == aCnfJobrcv.UPDUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aCnfJobrcv.COMPID);
            aslLog.USERID = aCnfJobrcv.UPDUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCnfJobrcv.UPDIPNO;
            aslLog.LOGLTUDE = aCnfJobrcv.UPDLTUDE;
            aslLog.TABLEID = "CNF_JOBRCV";
            string partyname = "", receivedTo="";
            var partynamefind =
                (from n in db.CnfPartyDbSet
                 where n.COMPID == aCnfJobrcv.COMPID && n.PARTYID == aCnfJobrcv.PARTYID
                 select n).ToList();
            foreach (var name in partynamefind)
            {
                partyname = name.PARTYNM;
            }
            var receivedTofind =
              (from n in db.GlAcchartDbSet
               where n.COMPID == aCnfJobrcv.COMPID && n.ACCOUNTCD == aCnfJobrcv.DEBITCD
               select n).ToList();
            foreach (var name in receivedTofind)
            {
                receivedTo = name.ACCOUNTNM;
            }
            aslLog.LOGDATA =
               Convert.ToString("TransDate: " + aCnfJobrcv.TRANSDT + ",\nTransYY: " + aCnfJobrcv.TRANSYY + ",\nTransNo: " + aCnfJobrcv.TRANSNO
                                 + ",\nTransFor: " + aCnfJobrcv.TRANSFOR + ",\nJOBNO: " + aCnfJobrcv.JOBNO + ",\nPARTY NAME: " + partyname
                                 + ",\nJob Year: " + aCnfJobrcv.JOBYY + ",\nJobTP: " + aCnfJobrcv.JOBTP + ",\nReceived To: " + receivedTo 
                                    + ",\nRemarks: " + aCnfJobrcv.REMARKS + ",\nAmount: " + aCnfJobrcv.AMOUNT + ".");
            aslLog.USERPC = aCnfJobrcv.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        public void Delete_Asl_LogData(CNF_JOBRCV aCnfJobrcv)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == aCnfJobrcv.COMPID && n.USERID == aCnfJobrcv.UPDUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aCnfJobrcv.COMPID);
            aslLog.USERID = aCnfJobrcv.UPDUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCnfJobrcv.UPDIPNO;
            aslLog.LOGLTUDE = aCnfJobrcv.UPDLTUDE;
            aslLog.TABLEID = "CNF_JOBRCV";
            string partyname = "", receivedTo="";
            var partynamefind =
                (from n in db.CnfPartyDbSet
                 where n.COMPID == aCnfJobrcv.COMPID && n.PARTYID == aCnfJobrcv.PARTYID
                 select n).ToList();
            foreach (var name in partynamefind)
            {
                partyname = name.PARTYNM;
            }

            var receivedTofind =
              (from n in db.GlAcchartDbSet
               where n.COMPID == aCnfJobrcv.COMPID && n.ACCOUNTCD == aCnfJobrcv.DEBITCD
               select n).ToList();
            foreach (var name in receivedTofind)
            {
                receivedTo = name.ACCOUNTNM;
            }
            aslLog.LOGDATA =
            Convert.ToString("TransDate: " + aCnfJobrcv.TRANSDT + ",\nTransYY: " + aCnfJobrcv.TRANSYY + ",\nTransNo: " + aCnfJobrcv.TRANSNO
                                + ",\nTransFor: " + aCnfJobrcv.TRANSFOR + ",\nJOBNO: " + aCnfJobrcv.JOBNO + ",\nParty Name: " + partyname
                                + ",\nJob Year: " + aCnfJobrcv.JOBYY + ",\nJobTP: " + aCnfJobrcv.JOBTP + ",\nReceived To: " + receivedTo
                                   + ",\nRemarks: " + aCnfJobrcv.REMARKS + ",\nAmount: " + aCnfJobrcv.AMOUNT + ".");
            aslLog.USERPC = aCnfJobrcv.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public ASL_DELETE AslDelete = new ASL_DELETE();
        public void Delete_ASL_DELETE(CNF_JOBRCV aCnfJobrcv)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("HH:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslDeleteDbSet
                     where n.COMPID == aCnfJobrcv.COMPID && n.USERID == aCnfJobrcv.UPDUSERID
                     select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(aCnfJobrcv.COMPID);
            AslDelete.USERID = aCnfJobrcv.UPDUSERID;
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = aCnfJobrcv.UPDIPNO;
            AslDelete.DELLTUDE = aCnfJobrcv.UPDLTUDE;
            AslDelete.TABLEID = "CNF_JOBRCV";
            string partyname = "", receivedTo="";
            var partynamefind =
                (from n in db.CnfPartyDbSet
                 where n.COMPID == aCnfJobrcv.COMPID && n.PARTYID == aCnfJobrcv.PARTYID
                 select n).ToList();
            foreach (var name in partynamefind)
            {
                partyname = name.PARTYNM;
            }
            var receivedTofind =
              (from n in db.GlAcchartDbSet
               where n.COMPID == aCnfJobrcv.COMPID && n.ACCOUNTCD == aCnfJobrcv.DEBITCD
               select n).ToList();
            foreach (var name in receivedTofind)
            {
                receivedTo = name.ACCOUNTNM;
            }
            AslDelete.DELDATA =
                 Convert.ToString("TransDate: " + aCnfJobrcv.TRANSDT + ",\nTransYY: " + aCnfJobrcv.TRANSYY + ",\nTransNo: " + aCnfJobrcv.TRANSNO
                                + ",\nTransFor: " + aCnfJobrcv.TRANSFOR + ",\nJOBNO: " + aCnfJobrcv.JOBNO + ",\nPARTY NAME: " + partyname
                                + ",\nJob Year: " + aCnfJobrcv.JOBYY + ",\nJobTP: " + aCnfJobrcv.JOBTP + ",\nReceived To: " + receivedTo
                                   + ",\nRemarks: " + aCnfJobrcv.REMARKS + ",\nAmount: " + aCnfJobrcv.AMOUNT + ".");
            AslDelete.USERPC = aCnfJobrcv.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }

        public ActionResult Index()
        {
            return View();
        }


        // GET: /ReceiveJobInfoCreate/
        public ActionResult Create()
        { 
            //<br/> brack removed;
            if (System.Web.HttpContext.Current.Session["LoggedUserType"] != null)
            {
                var LoggedUserTp = System.Web.HttpContext.Current.Session["LoggedUserType"].ToString();
                if (LoggedUserTp == "CompanyAdmin" || LoggedUserTp == "UserAdmin" || LoggedUserTp == "User")
                {
                    TempData["BrackFieldDropFromLayout_AslUserCOController"] = " <br/> brack removed";
                }
            }
            return View();
        }

        // POST: /Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CNF_JOBRCV aCnfJobrcv)
        {
            if (ModelState.IsValid)
            {
           
              
                var res = (from n in db.CnfJobDbSet where aCnfJobrcv.COMPID == n.COMPID && aCnfJobrcv.CNF_JOBRCVID == n.Cnf_JobID select n);
                foreach (var cnfJob in res)
                {
                    aCnfJobrcv.JOBNO = cnfJob.JOBNO;
                    aCnfJobrcv.PARTYID = cnfJob.PARTYID;
                }


                //Int64 findPartyID = Convert.ToInt64(from n in db.CnfJobDbSet where aCnfJobrcv.COMPID == n.COMPID && aCnfJobrcv.CNF_JOBRCVID == n.Cnf_JobID select n.PARTYID);
                
                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];


                aCnfJobrcv.USERPC = strHostName;
                aCnfJobrcv.INSIPNO = ipAddress.ToString();
                aCnfJobrcv.INSTIME = Convert.ToDateTime(td);
             
                aCnfJobrcv.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                Int64 maxData = Convert.ToInt64((from n in db.CnfJobrcvs where n.COMPID == aCnfJobrcv.COMPID select n.TRANSNO).Max());

                if (maxData == 0)
                {
                    string transyear = Convert.ToString(aCnfJobrcv.TRANSYY);
                    aCnfJobrcv.TRANSNO = Convert.ToInt64(transyear + "0001");
                }
                else
                {
                    aCnfJobrcv.TRANSNO = maxData + 1;
                }

                Insert_Asl_LogData(aCnfJobrcv);
                db.CnfJobrcvs.Add(aCnfJobrcv);
                db.SaveChanges();

                TempData["JOBRCVCreationMessage"] = "Job Receive created! ";
                db.SaveChanges();
                return RedirectToAction("Create");

            }
            return View();
        }

        // GET: /ReceiveJobInfoUpdate/
        public ActionResult Update()
        { 
            //<br/> brack removed;
            if (System.Web.HttpContext.Current.Session["LoggedUserType"] != null)
            {
                var LoggedUserTp = System.Web.HttpContext.Current.Session["LoggedUserType"].ToString();
                if (LoggedUserTp == "CompanyAdmin" || LoggedUserTp == "UserAdmin" || LoggedUserTp == "User")
                {
                    TempData["BrackFieldDropFromLayout_AslUserCOController"] = " <br/> brack removed";
                }
            }
            return View();
        }

        // POST: /ReceiveJobInfoUpdate/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CNF_JOBRCV aCnfJobrcv)
        {
            //if (aCnfJobrcv.TRANSNO != null && aCnfJobrcv.TRANSFOR != null && aCnfJobrcv.DEBITCD != null &&
            //    aCnfJobrcv.AMOUNT != null )
            //{
                db.Entry(aCnfJobrcv).State = EntityState.Modified;

                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                aCnfJobrcv.USERPC = strHostName;
                aCnfJobrcv.UPDIPNO = ipAddress.ToString();
                aCnfJobrcv.UPDTIME = Convert.ToDateTime(td);
                //asluserco.UPDTIME = DateTime.Parse(td.ToString(), dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                //Update User ID save AslUSerco table attribute INSUSERID
                aCnfJobrcv.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                aCnfJobrcv.INSIPNO = aCnfJobrcv.INSIPNO;
                aCnfJobrcv.INSTIME = aCnfJobrcv.INSTIME; 

                aCnfJobrcv.INSUSERID = aCnfJobrcv.INSUSERID;
                aCnfJobrcv.INSLTUDE = aCnfJobrcv.INSLTUDE;
         
                Update_Asl_LogData(aCnfJobrcv);

                db.SaveChanges();
                TempData["JobReceiveUpdateMessage"] = "'" + aCnfJobrcv.TRANSNO + "' successfully updated!";
                return RedirectToAction("Update");
            //}
            //else
            //{
            //    return View("Update");
            //}
        }
        // GET: /ReceiveJobInfoDelete/
        public ActionResult Delete()
        {
            //<br/> brack removed;
            if (System.Web.HttpContext.Current.Session["LoggedUserType"] != null)
            {
                var LoggedUserTp = System.Web.HttpContext.Current.Session["LoggedUserType"].ToString();
                if (LoggedUserTp == "CompanyAdmin" || LoggedUserTp == "UserAdmin" || LoggedUserTp == "User")
                {
                    TempData["BrackFieldDropFromLayout_AslUserCOController"] = " <br/> brack removed";
                }
            }
            return View();
        }
       
    
        //POST:/ReceiveJobInfoDelete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CNF_JOBRCV aCnfJobrcv)
        {
            if (aCnfJobrcv.TRANSNO != null )
            {
                CNF_JOBRCV acCnfJovrcvDelete = db.CnfJobrcvs.Find(aCnfJobrcv.CNF_JOBRCVID);
             

                //Get Ip ADDRESS,Time & user PC Name 
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                acCnfJovrcvDelete.USERPC = strHostName;
                acCnfJovrcvDelete.UPDIPNO = ipAddress.ToString();
                acCnfJovrcvDelete.UPDTIME = Convert.ToDateTime(td);

                //Delete User ID save AslUSerco table attribute INSUSERID
                acCnfJovrcvDelete.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                //Get current LOGLTUDE data 
                acCnfJovrcvDelete.UPDLTUDE = aCnfJobrcv.INSLTUDE;

                Delete_Asl_LogData(acCnfJovrcvDelete);
                Delete_ASL_DELETE(acCnfJovrcvDelete);

                db.CnfJobrcvs.Remove(acCnfJovrcvDelete);
                db.SaveChanges();

                TempData["JobRCVDeleteMessage"] = "'" + acCnfJovrcvDelete.TRANSNO + "' successfully Deleted!";
                return RedirectToAction("Delete");
            }
            else
            {
                return View("Delete");
            }
        }

        //JseonResult for DateChanged and get year.............................Start
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DateChanged_getYear(DateTime changedtxt)
        {
           
           
                string converttoString = Convert.ToString(changedtxt.ToString("dd-MMM-yyyy"));
                string getYear = converttoString.Substring(7, 4);
                Int64 year = Convert.ToInt64(getYear);
                var result = new { YEAR = year };
                return Json(result, JsonRequestBehavior.AllowGet);
          

           



        }
        //JseonResult for DateChanged and get year.............................End

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult JobNoChanged_getJobinfo(Int64 changedtxt)
        {
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var selectdata = from n in db.CnfJobDbSet where compid == n.COMPID && changedtxt==n.Cnf_JobID select new {n.JOBYY,n.JOBTP,n.PARTYID};
            string jobType = "", partyName="";
            Int64 jobYear = 0, partyid=0;
            foreach (var l in selectdata)
            {
                jobType = l.JOBTP;
                jobYear = Convert.ToInt64(l.JOBYY);
                partyid = Convert.ToInt64(l.PARTYID);
            }

            var findpartyName = from n in db.GlAcchartDbSet where n.ACCOUNTCD == partyid select new {n.ACCOUNTNM};
            foreach (var VARIABLE in findpartyName)
            {
                partyName = VARIABLE.ACCOUNTNM;
            }
            //Int64 year = selectdata.JOBYY;

            var result = new { Year = jobYear, Type = jobType, Party = partyName };
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult TransYearChanged(Int64 txtTransYear)
        {
            List<SelectListItem>getTransNo = new List<SelectListItem>();
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var result = (from n in db.CnfJobrcvs where n.TRANSYY==txtTransYear && n.COMPID==compid select new{n.TRANSNO} ).ToList() ;
            foreach (var f in result)
            {
                getTransNo.Add(new SelectListItem {Text= f.TRANSNO.ToString(),Value=f.TRANSNO.ToString()});
                
            }

            return Json(getTransNo, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Update_SelectTransNo(Int64 changedtxt)
        {
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var selectdata = from n in db.CnfJobrcvs where compid == n.COMPID && changedtxt == n.TRANSNO select new {n.CNF_JOBRCVID,n.TRANSDT,n.TRANSYY,
                n.TRANSFOR,n.JOBNO, n.ADVSHOW,
                n.JOBYY,n.JOBTP,n.PARTYID,n.DEBITCD,n.REMARKS,n.AMOUNT,n.INSUSERID,n.INSLTUDE,n.INSIPNO,n.INSTIME,n.UPDLTUDE };
            string JobType = "", partyName = "", Transfor = "", Remarks = "", TransDate = "", instime = "", insipno = "", insltude = "",advshow="";
            //DateTime TransDate;
            Int64 TablePID = 0, JobYear = 0, PartyId = 0, TransYear = 0, JobNo = 0, DebitaccountId = 0, Amount = 0, inuserid=0;
           
            foreach (var l in selectdata)
            {
                TablePID = Convert.ToInt64(l.CNF_JOBRCVID);
                string xx = Convert.ToString(l.TRANSDT);
                DateTime date = DateTime.Parse(xx);
                TransDate = date.ToString("dd-MMM-yyyy");

                TransYear = Convert.ToInt64(l.TRANSYY);
                Transfor = Convert.ToString(l.TRANSFOR);
                JobNo = Convert.ToInt64(l.JOBNO);
                JobYear = Convert.ToInt64(l.JOBYY);
                JobType = l.JOBTP;
                
                PartyId = Convert.ToInt64(l.PARTYID);

                DebitaccountId = Convert.ToInt64(l.DEBITCD);
                Remarks = Convert.ToString(l.REMARKS);
                Amount = Convert.ToInt64(l.AMOUNT);

                advshow = l.ADVSHOW;




                string x = Convert.ToString(l.INSTIME);
                DateTime date2 = DateTime.Parse(x);
                instime = date2.ToString("dd-MMM-yyyy");

                insipno = Convert.ToString(l.INSIPNO);
                insltude = Convert.ToString(l.INSLTUDE);
                inuserid = Convert.ToInt64(l.INSUSERID);
              

            }

            var findpartyName = from n in db.GlAcchartDbSet where n.ACCOUNTCD == PartyId select new { n.ACCOUNTNM };
            foreach (var VARIABLE in findpartyName)
            {
                partyName = VARIABLE.ACCOUNTNM;
            }
            //List<SelectListItem> Debitlist = new List<SelectListItem>();
            //var ans = from n in db.GlAcchartDbSet where n.COMPID == compid && n.ACCOUNTCD == DebitaccountId select new { n.ACCOUNTCD, n.ACCOUNTNM };
            //foreach (var x in ans)
            //{
            //    Debitlist.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
            //}
            var result = new { keyid = TablePID, Date = TransDate, Year = TransYear, For = Transfor, No = JobNo, JYear = JobYear, type = JobType, Advance = advshow, partyid = PartyId, Party = partyName, AccountNo = DebitaccountId, remarks = Remarks, amount = Amount, InsTime = instime, InsIPNo = insipno, InslTude = insltude, InUserId = inuserid };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModeChange_getDebitcd(string changedtxt)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            //DateTime dt = Convert.ToDateTime(changedtxt);

        




            //DateTime datetm = Convert.ToDateTime(dd);

            List<SelectListItem> debitcd = new List<SelectListItem>();


            if (changedtxt == "ADVANCE" || changedtxt == "NORMAL")
            {
                Int64 accounthead = Convert.ToInt64(Convert.ToString(comid) + "101");
                var fordebitcd = (from n in db.GlAcchartDbSet
                                  where n.COMPID == comid && (n.HEADCD == accounthead || n.HEADCD==accounthead+1)
                                  select new
                                  {
                                      n.ACCOUNTCD,
                                      n.ACCOUNTNM
                                  }
                            )
                            .ToList();

                foreach (var f in fordebitcd)
                {

                    debitcd.Add(new SelectListItem { Text = f.ACCOUNTNM, Value = f.ACCOUNTCD.ToString() });
                }

              

                return Json(new SelectList(debitcd, "Value", "Text"));
                
            }
            else if (changedtxt == "DISCOUNT")
            {
                Int64 accountcd = Convert.ToInt64(Convert.ToString(comid) + "4010003");
                var fordebitcd = (from n in db.GlAcchartDbSet
                                  where n.COMPID == comid && n.ACCOUNTCD == accountcd 
                                  select new
                                  {
                                      n.ACCOUNTCD,
                                      n.ACCOUNTNM
                                  }
                           )
                           .ToList();
                foreach (var f in fordebitcd)
                {

                    debitcd.Add(new SelectListItem { Text = f.ACCOUNTNM, Value = f.ACCOUNTCD.ToString() });
                }
                return Json(new SelectList(debitcd, "Value", "Text"));
            }
            else
            {
                return null;
            }



        }

        public JsonResult getJobno(Int64 changedtxt, string changedtxt2)
        {
            Int64 compid = Convert.ToInt64(changedtxt2);


            List<SelectListItem> listJobNo = new List<SelectListItem>();
            var resultJobNo = (db.CnfJobDbSet.Join(db.GlAcchartDbSet, cnfjob => cnfjob.COMPID, glacchart => glacchart.COMPID,
          (cnfjob, glacchart) => new { cnfjob, glacchart })
          .Where(@t => @t.cnfjob.PARTYID == @t.glacchart.ACCOUNTCD && @t.cnfjob.COMPID == compid && t.cnfjob.JOBYY == changedtxt)
          .Select(@t => new { @t.cnfjob.JOBNO, @t.cnfjob.JOBYY, @t.cnfjob.JOBTP, @t.glacchart.ACCOUNTNM, @t.cnfjob.Cnf_JobID }).OrderBy(@t => @t.JOBNO));
            foreach (var n in resultJobNo)
            {
                //CNF_JOB acCnfJob = db.CnfJobDbSet.FirstOrDefault(e => e.Cnf_JobID == n.Cnf_JobID);
                if (n != null)
                {
                    listJobNo.Add(new SelectListItem { Text = (n.JOBNO.ToString() + "|" + n.JOBYY.ToString() + "|" + n.JOBTP + "|" + n.ACCOUNTNM.ToString()), Value = n.Cnf_JobID.ToString() });
                }
            }
            foreach (var n in resultJobNo)
            {

                if (n != null)
                {
                    listJobNo.Add(new SelectListItem { Text = (n.JOBNO.ToString() + "|" + n.JOBYY.ToString() + "|" + n.JOBTP + "|" + n.ACCOUNTNM.ToString()), Value = n.Cnf_JobID.ToString() });
                }
            }






            return Json(new SelectList(listJobNo, "Value", "Text"));





        }

    }
}
