using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using Mvc_CNFApp.Models;

namespace Mvc_CNFApp.Controllers
{
    public class JobInformationController : AppController
    {
        private CnfDbContext db = new CnfDbContext();

        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;
        public JobInformationController()
        {
            ViewData["HighLight_Menu_CnfForm"] = "heighlight";
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }



        // Create ASL_LOG object and it used to this Insert_Asl_LogData, Update_Asl_LogData, Delete_Asl_LogData (CNF_JOB cnfJob).
        public ASL_LOG aslLog = new ASL_LOG();

        // SAVE ALL INFORMATION from CNF_PARTY TO Asl_lOG Database Table.
        public void Insert_Asl_LogData(CNF_JOB cnfJob)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == cnfJob.COMPID && n.USERID == cnfJob.INSUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(cnfJob.COMPID);
            aslLog.USERID = cnfJob.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfJob.INSIPNO;
            aslLog.LOGLTUDE = cnfJob.INSLTUDE;
            aslLog.TABLEID = "CNF_JOB";

            string partyname = "";
            var party_name =
                (from n in db.CnfPartyDbSet where n.COMPID == cnfJob.COMPID && n.PARTYID == cnfJob.PARTYID select n)
                    .ToList();
            foreach (var name in party_name)
            {
                partyname = name.PARTYNM;
            }

            aslLog.LOGDATA =
                Convert.ToString("JOBCDT: " + cnfJob.JOBCDT + ",\nREGID: " + cnfJob.REGID + ",\nJOBYY: " + cnfJob.JOBYY + ",\nJOBTP: " + cnfJob.JOBTP + ",\nJOBNO: " + cnfJob.JOBNO + ",\nPARTYNAME: " + partyname + ",\nCONSIGNEENM: " + cnfJob.CONSIGNEENM + ",\nCONSIGNEEADD: " + cnfJob.CONSIGNEEADD + ",\nCONSIGNEEADD: " + cnfJob.CONSIGNEEADD + ",\nPKGS: " + cnfJob.PKGS + ",\nGOODS: " + cnfJob.GOODS + ",\nWTGROSS: " + cnfJob.WTGROSS + ",\nWTNET: " + cnfJob.WTNET + ",\nCNFV_USD: " + cnfJob.CNFV_USD + ",\nCNFV_ETP: " + cnfJob.CNFV_ETP + ",\nCNFV_ERT: " + cnfJob.CNFV_ERT + ",\nCNFV_BDT: " + cnfJob.CNFV_BDT +
                ",\nCNFV_USD: " + cnfJob.CNFV_USD + ",\nASSV_BDT: " + cnfJob.ASSV_BDT + ",\nCOMMAMT: " + cnfJob.COMMAMT + ",\nCONTNO: " + cnfJob.CONTNO + ",\nDOCINVNO: " + cnfJob.DOCINVNO + ",\nDOCINVDT: " + cnfJob.DOCINVDT + ",\nPERMITNO: " + cnfJob.PERMITNO + ",\nPERMITDT: " + cnfJob.PERMITDT + ",\nBILLNOM: " + cnfJob.BILLNOM + ",\nBILLDTM: " + cnfJob.BILLDTM + ",\nBILLFDT: " + cnfJob.BILLFDT + ",\nBENO: " + cnfJob.BENO + ",\nBEDT: " + cnfJob.BEDT + ",\nLCNO: " + cnfJob.LCNO + ",\nLCDT: " + cnfJob.LCDT + ",\nStatus: " + cnfJob.STATUS + ".");
            aslLog.USERPC = cnfJob.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        // SAVE ALL INFORMATION from CNF_PARTY TO Asl_lOG Database Table.
        public void Update_Asl_LogData(CNF_JOB cnfJob)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == cnfJob.COMPID && n.USERID == cnfJob.UPDUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(cnfJob.COMPID);
            aslLog.USERID = cnfJob.UPDUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfJob.UPDIPNO;
            aslLog.LOGLTUDE = cnfJob.UPDLTUDE;
            aslLog.TABLEID = "CNF_JOB";
            string partyname = "";
            var party_name =
                (from n in db.CnfPartyDbSet where n.COMPID == cnfJob.COMPID && n.PARTYID == cnfJob.PARTYID select n)
                    .ToList();
            foreach (var name in party_name)
            {
                partyname = name.PARTYNM;
            }

            aslLog.LOGDATA =
                Convert.ToString("JOBCDT: " + cnfJob.JOBCDT + ",\nREGID: " + cnfJob.REGID + ",\nJOBYY: " + cnfJob.JOBYY + ",\nJOBTP: " + cnfJob.JOBTP + ",\nJOBNO: " + cnfJob.JOBNO + ",\nPARTYNAME: " + partyname + ",\nCONSIGNEENM: " + cnfJob.CONSIGNEENM + ",\nCONSIGNEEADD: " + cnfJob.CONSIGNEEADD + ",\nCONSIGNEEADD: " + cnfJob.CONSIGNEEADD + ",\nPKGS: " + cnfJob.PKGS + ",\nGOODS: " + cnfJob.GOODS + ",\nWTGROSS: " + cnfJob.WTGROSS + ",\nWTNET: " + cnfJob.WTNET + ",\nCNFV_USD: " + cnfJob.CNFV_USD + ",\nCNFV_ETP: " + cnfJob.CNFV_ETP + ",\nCNFV_ERT: " + cnfJob.CNFV_ERT + ",\nCNFV_BDT: " + cnfJob.CNFV_BDT +
                ",\nCNFV_USD: " + cnfJob.CNFV_USD + ",\nASSV_BDT: " + cnfJob.ASSV_BDT + ",\nCOMMAMT: " + cnfJob.COMMAMT + ",\nCONTNO: " + cnfJob.CONTNO + ",\nDOCINVNO: " + cnfJob.DOCINVNO + ",\nDOCINVDT: " + cnfJob.DOCINVDT + ",\nPERMITNO: " + cnfJob.PERMITNO + ",\nPERMITDT: " + cnfJob.PERMITDT + ",\nBILLNOM: " + cnfJob.BILLNOM + ",\nBILLDTM: " + cnfJob.BILLDTM + ",\nBILLFDT: " + cnfJob.BILLFDT + ",\nBENO: " + cnfJob.BENO + ",\nBEDT: " + cnfJob.BEDT + ",\nLCNO: " + cnfJob.LCNO + ",\nLCDT: " + cnfJob.LCDT + ",\nStatus: " + cnfJob.STATUS + ".");
            aslLog.USERPC = cnfJob.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        public void Update_Asl_LogData_2ndPart(CNF_JOB cnfJob)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == cnfJob.COMPID && n.USERID == cnfJob.UPDUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(cnfJob.COMPID);
            aslLog.USERID = cnfJob.UPDUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfJob.UPDIPNO;
            aslLog.LOGLTUDE = cnfJob.UPDLTUDE;
            aslLog.TABLEID = "CNF_JOB";
            aslLog.LOGDATA =
                Convert.ToString("CRFNO: " + cnfJob.CRFNO + ",\nCRFDT: " + cnfJob.CRFDT + ",\nBLNO: " + cnfJob.BLNO + ",\nBLDT: " + cnfJob.BLDT + ",\nBTNO: " + cnfJob.BTNO + ",\nBTDT: " + cnfJob.BTDT + ",\nLCANO: " + cnfJob.LCANO + ",\nLCADT: " + cnfJob.LCADT + ",\nAWBNO: " + cnfJob.AWBNO + ",\nAWBDT: " + cnfJob.AWBDT + ",\nHAWBNO: " + cnfJob.HAWBNO + ",\nHAWBDT: " + cnfJob.HAWBDT + ",\nUNDTNO: " + cnfJob.UNDTNO + ",\nUNDTDT: " + cnfJob.UNDTDT + ",\nMVESSEL: " + cnfJob.MVESSEL + ",\nFVESSEL: " + cnfJob.FVESSEL + ",\nMARKSNO: " + cnfJob.MARKSNO + ",\nETB: " + cnfJob.ETB + ",\nBERTHSNO: " + cnfJob.BERTHSNO + ",\nARRIVEDT: " + cnfJob.ARRIVEDT + ",\nROTNO: " + cnfJob.ROTNO + ",\nLINENO: " + cnfJob.LINENO + ",\nDELIVERYDT: " + cnfJob.DELIVERYDT + ",\nCLDT: " + cnfJob.CLDT + ",\nWFDT: " + cnfJob.WFDT + ".");
            aslLog.USERPC = cnfJob.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        // SAVE ALL INFORMATION from CNF_PARTY TO Asl_lOG Database Table.
        public void Delete_Asl_LogData(CNF_JOB cnfJob)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == cnfJob.COMPID && n.USERID == cnfJob.UPDUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(cnfJob.COMPID);
            aslLog.USERID = cnfJob.UPDUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfJob.UPDIPNO;
            aslLog.LOGLTUDE = cnfJob.UPDLTUDE;
            aslLog.TABLEID = "CNF_JOB";
            string partyname = "";
            var party_name =
                (from n in db.CnfPartyDbSet where n.COMPID == cnfJob.COMPID && n.PARTYID == cnfJob.PARTYID select n)
                    .ToList();
            foreach (var name in party_name)
            {
                partyname = name.PARTYNM;
            }
            aslLog.LOGDATA =
                Convert.ToString("JOBCDT: " + cnfJob.JOBCDT + ",\nREGID: " + cnfJob.REGID + ",\nJOBYY: " + cnfJob.JOBYY + ",\nJOBTP: " + cnfJob.JOBTP + ",\nJOBNO: " + cnfJob.JOBNO + ",\nPARTYNAME: " + partyname + ",\nCONSIGNEENM: " + cnfJob.CONSIGNEENM + ",\nCONSIGNEEADD: " + cnfJob.CONSIGNEEADD + ",\nCONSIGNEEADD: " + cnfJob.CONSIGNEEADD + ",\nPKGS: " + cnfJob.PKGS + ",\nGOODS: " + cnfJob.GOODS + ",\nWTGROSS: " + cnfJob.WTGROSS + ",\nWTNET: " + cnfJob.WTNET + ",\nCNFV_USD: " + cnfJob.CNFV_USD + ",\nCNFV_ETP: " + cnfJob.CNFV_ETP + ",\nCNFV_ERT: " + cnfJob.CNFV_ERT + ",\nCNFV_BDT: " + cnfJob.CNFV_BDT +
                ",\nCNFV_USD: " + cnfJob.CNFV_USD + ",\nASSV_BDT: " + cnfJob.ASSV_BDT + ",\nCOMMAMT: " + cnfJob.COMMAMT + ",\nCONTNO: " + cnfJob.CONTNO + ",\nDOCINVNO: " + cnfJob.DOCINVNO + ",\nDOCINVDT: " + cnfJob.DOCINVDT + ",\nPERMITNO: " + cnfJob.PERMITNO + ",\nPERMITDT: " + cnfJob.PERMITDT + ",\nBILLNOM: " + cnfJob.BILLNOM + ",\nBILLDTM: " + cnfJob.BILLDTM + ",\nBILLFDT: " + cnfJob.BILLFDT + ",\nBENO: " + cnfJob.BENO + ",\nBEDT: " + cnfJob.BEDT + ",\nLCNO: " + cnfJob.LCNO + ",\nLCDT: " + cnfJob.LCDT + ",\nStatus: " + cnfJob.STATUS + ".");
            aslLog.USERPC = cnfJob.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }



        // Create ASL_DELETE object and it used to this Delete_ASL_DELETE (Cnf aCnf).
        public ASL_DELETE AslDelete = new ASL_DELETE();
        // SAVE ALL INFORMATION from CNF_PARTY TO Asl_lOG Database Table.
        public void Delete_ASL_DELETE(CNF_JOB cnfJob)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslDeleteDbSet
                     where n.COMPID == cnfJob.COMPID && n.USERID == cnfJob.UPDUSERID
                     select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }


            AslDelete.COMPID = Convert.ToInt64(cnfJob.COMPID);
            AslDelete.USERID = cnfJob.UPDUSERID;
            AslDelete.DELSLNO = aslLog.LOGSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = cnfJob.UPDIPNO;
            AslDelete.DELLTUDE = cnfJob.UPDLTUDE;
            AslDelete.TABLEID = "CNF_JOB";
            string partyname = "";
            var party_name =
                (from n in db.CnfPartyDbSet where n.COMPID == cnfJob.COMPID && n.PARTYID == cnfJob.PARTYID select n)
                    .ToList();
            foreach (var name in party_name)
            {
                partyname = name.PARTYNM;
            }
            AslDelete.DELDATA =
                Convert.ToString("JOBCDT: " + cnfJob.JOBCDT + ",\nREGID: " + cnfJob.REGID + ",\nJOBYY: " + cnfJob.JOBYY + ",\nJOBTP: " + cnfJob.JOBTP + ",\nJOBNO: " + cnfJob.JOBNO + ",\nPARTYNAME: " + partyname + ",\nCONSIGNEENM: " + cnfJob.CONSIGNEENM + ",\nCONSIGNEEADD: " + cnfJob.CONSIGNEEADD + ",\nCONSIGNEEADD: " + cnfJob.CONSIGNEEADD + ",\nPKGS: " + cnfJob.PKGS + ",\nGOODS: " + cnfJob.GOODS + ",\nWTGROSS: " + cnfJob.WTGROSS + ",\nWTNET: " + cnfJob.WTNET + ",\nCNFV_USD: " + cnfJob.CNFV_USD + ",\nCNFV_ETP: " + cnfJob.CNFV_ETP + ",\nCNFV_ERT: " + cnfJob.CNFV_ERT + ",\nCNFV_BDT: " + cnfJob.CNFV_BDT +
                ",\nCNFV_USD: " + cnfJob.CNFV_USD + ",\nASSV_BDT: " + cnfJob.ASSV_BDT + ",\nCOMMAMT: " + cnfJob.COMMAMT + ",\nCONTNO: " + cnfJob.CONTNO + ",\nDOCINVNO: " + cnfJob.DOCINVNO + ",\nDOCINVDT: " + cnfJob.DOCINVDT + ",\nPERMITNO: " + cnfJob.PERMITNO + ",\nPERMITDT: " + cnfJob.PERMITDT + ",\nBILLNOM: " + cnfJob.BILLNOM + ",\nBILLDTM: " + cnfJob.BILLDTM + ",\nBILLFDT: " + cnfJob.BILLFDT + ",\nBENO: " + cnfJob.BENO + ",\nBEDT: " + cnfJob.BEDT + ",\nLCNO: " + cnfJob.LCNO + ",\nLCDT: " + cnfJob.LCDT + ",\nStatus: " + cnfJob.STATUS + ".");
            AslDelete.USERPC = cnfJob.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }







        // GET: /JobInformation/
        public ActionResult Index()
        {
            return View();
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CNF_JOB aCnfJob)
        {
            if (ModelState.IsValid)
            {
                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                
                var check_duplicate = (from n in db.CnfJobDbSet where n.COMPID == aCnfJob.COMPID && n.JOBYY == aCnfJob.JOBYY && n.JOBTP == aCnfJob.JOBTP && n.JOBNO == aCnfJob.JOBNO select n).ToList();
                if(check_duplicate.Count==0)
                {
                    aCnfJob.USERPC = strHostName;
                    aCnfJob.INSIPNO = ipAddress.ToString();
                    aCnfJob.INSTIME = Convert.ToDateTime(td);
                    //Insert User ID save AslUSerco table attribute INSUSERID
                    aCnfJob.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    if (aCnfJob.COMMAMT == null)
                    {
                        aCnfJob.COMMAMT = 0;
                    }
                    if(aCnfJob.JOBCDT!=null)
                    {
                        

                        var search_partyname = (from n in db.GlAcchartDbSet where n.COMPID == aCnfJob.COMPID && n.ACCOUNTCD == aCnfJob.PARTYID select n).ToList();
                        foreach(var ss in search_partyname)
                        {
                            aCnfJob.PartyName = ss.ACCOUNTNM;
                        }
                        db.CnfJobDbSet.Add(aCnfJob);
                        db.SaveChanges();
                        Insert_Asl_LogData(aCnfJob);
                        db.SaveChanges();

                        TempData["JOBCreationMessage"] = "Job created! ";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        TempData["JOBCreationMessage"] = "Job Creation Date Error! ";
                        return RedirectToAction("Create");
                    }
                    
                }
                else
                {
                    aCnfJob.JOBNO = aCnfJob.JOBNO + 1;
                    aCnfJob.USERPC = strHostName;
                    aCnfJob.INSIPNO = ipAddress.ToString();
                    aCnfJob.INSTIME = Convert.ToDateTime(td);
                    //Insert User ID save AslUSerco table attribute INSUSERID
                    aCnfJob.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    if (aCnfJob.COMMAMT == null)
                    {
                        aCnfJob.COMMAMT = 0;
                    }
                    if (aCnfJob.JOBCDT != null)
                    {
                        
                        var search_partyname = (from n in db.GlAcchartDbSet where n.COMPID == aCnfJob.COMPID && n.ACCOUNTCD == aCnfJob.PARTYID select n).ToList();
                        foreach (var ss in search_partyname)
                        {
                            aCnfJob.PartyName = ss.ACCOUNTNM;
                        }
                        db.CnfJobDbSet.Add(aCnfJob);
                        db.SaveChanges();
                        Insert_Asl_LogData(aCnfJob);
                        db.SaveChanges();

                        TempData["JOBCreationMessage"] = "Job created! ";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        TempData["JOBCreationMessage"] = "Job Creation Date Error! ";
                        return RedirectToAction("Create");
                    }

                }

              

            }
            return View();
        }



       

       



        public ActionResult FirstPartUpdate()
        {
            ////<br/> brack removed;
            //if (System.Web.HttpContext.Current.Session["LoggedUserType"] != null)
            //{
            //    var LoggedUserTp = System.Web.HttpContext.Current.Session["LoggedUserType"].ToString();
            //    if (LoggedUserTp == "CompanyAdmin" || LoggedUserTp == "UserAdmin" || LoggedUserTp == "User")
            //    {
            //        TempData["BrackFieldDropFromLayout_AslUserCOController"] = " <br/> brack removed";
            //    }
            //}
            return View("FirstPartUpdate");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstPartUpdate(CNF_JOB aCnfJob,string command)
        {
            if (command == "Update")
            {
                db.Entry(aCnfJob).State = EntityState.Modified;

                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                aCnfJob.USERPC = strHostName;
                aCnfJob.UPDIPNO = ipAddress.ToString();
                aCnfJob.UPDTIME = Convert.ToDateTime(td);
                //Update User ID save AslUSerco table attribute INSUSERID
                aCnfJob.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                if (aCnfJob.COMMAMT == null)
                {
                    aCnfJob.COMMAMT = 0;
                }
                Update_Asl_LogData(aCnfJob);
                var search_partyname = (from n in db.GlAcchartDbSet where n.COMPID == aCnfJob.COMPID && n.ACCOUNTCD == aCnfJob.PARTYID select n).ToList();
                foreach (var ss in search_partyname)
                {
                    aCnfJob.PartyName = ss.ACCOUNTNM;
                }

                db.SaveChanges();
                TempData["JOBUpdateMessage"] = "Job updated! ";
                return RedirectToAction("FirstPartUpdate");
            }
            else
            {
                CNF_JOB cnfmodel = db.CnfJobDbSet.Find(aCnfJob.Cnf_JobID);
                PageModel model = new PageModel();
                model.ACnfJob = cnfmodel;
                //var pdf = new PdfResult(model, "GetJobWiseExpensesReport");
                //return pdf;
                TempData["GetJobInfo"] = model;
                return RedirectToAction("GetJobInfoReport");
            }
           

        }
        public ActionResult GetJobInfoReport()
        {
            PageModel model = (PageModel)TempData["GetJobInfo"];
            return View(model);
        }
        public ActionResult SecondPartUpdate()
        {
            ////<br/> brack removed;
            //if (System.Web.HttpContext.Current.Session["LoggedUserType"] != null)
            //{
            //    var LoggedUserTp = System.Web.HttpContext.Current.Session["LoggedUserType"].ToString();
            //    if (LoggedUserTp == "CompanyAdmin" || LoggedUserTp == "UserAdmin" || LoggedUserTp == "User")
            //    {
            //        TempData["BrackFieldDropFromLayout_AslUserCOController"] = " <br/> brack removed";
            //    }
            //}
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SecondPartUpdate(CNF_JOB aCnfJob,string command)
        {
            if(command=="Update")
            {
                db.Entry(aCnfJob).State = EntityState.Modified;

                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                aCnfJob.USERPC = strHostName;
                aCnfJob.UPDIPNO = ipAddress.ToString();
                aCnfJob.UPDTIME = Convert.ToDateTime(td);
                //Update User ID save AslUSerco table attribute INSUSERID
                aCnfJob.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                Update_Asl_LogData_2ndPart(aCnfJob);

                db.SaveChanges();
                TempData["JOBUpdateMessage"] = "Job updated! ";
                return RedirectToAction("SecondPartUpdate");
            }
            else
            {
                CNF_JOB cnfmodel = db.CnfJobDbSet.Find(aCnfJob.Cnf_JobID);
                PageModel model = new PageModel();
                model.ACnfJob = cnfmodel;
                //var pdf = new PdfResult(model, "GetJobWiseExpensesReport");
                //return pdf;
                TempData["GetJobInfo"] = model;
                return RedirectToAction("GetJobInfoReport");
            }

        }

        // GET: /PartyDelete/
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CNF_JOB aCnfJob)
        {
            CNF_JOB acCnfJobDelete = db.CnfJobDbSet.Find(aCnfJob.Cnf_JobID);

            //Get Ip ADDRESS,Time & user PC Name 
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            acCnfJobDelete.USERPC = strHostName;
            acCnfJobDelete.UPDIPNO = ipAddress.ToString();
            acCnfJobDelete.UPDTIME = Convert.ToDateTime(td);

            //Delete User ID save AslUSerco table attribute INSUSERID
            acCnfJobDelete.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            //Get current LOGLTUDE data 
            acCnfJobDelete.UPDLTUDE = aCnfJob.UPDLTUDE;

            Delete_Asl_LogData(acCnfJobDelete);
            Delete_ASL_DELETE(acCnfJobDelete);

            db.CnfJobDbSet.Remove(acCnfJobDelete);
            db.SaveChanges();

            TempData["JOBDeleteMessage"] = "Job Deleted! ";
            return RedirectToAction("Delete");

        }






        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DateChanged_getYear(DateTime changedtxt, string ddltxtJobType)
        {
            Int64 jobNo = 0;
            if (ddltxtJobType == "")
            {
                string converttoString = Convert.ToString(changedtxt.ToString("dd-MMM-yyyy"));
                string getYear = converttoString.Substring(7, 4);
                Int64 year = Convert.ToInt64(getYear);
                var result = new { YEAR = year, JOBNO = jobNo };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string converttoString = Convert.ToString(changedtxt.ToString("dd-MMM-yyyy"));
                string getYear = converttoString.Substring(7, 4);
                Int64 year = Convert.ToInt64(getYear);
                Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
                Int64 maxData = Convert.ToInt64((from n in db.CnfJobDbSet
                                                 where compid == n.COMPID
                                                     && year == n.JOBYY && n.JOBTP == ddltxtJobType
                                                 select n.JOBNO).Max());

                if (maxData == 0)
                {
                    jobNo = 1;
                }
                else
                {
                    jobNo = maxData + 1;
                }

                var result = new { YEAR = year, JOBNO = jobNo };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetJobNO(DateTime idJOBCDT, string ddltxtJobType)
        {
            Int64 jobNo = 0;
            if (ddltxtJobType == "")
            {
                var nullResult = new { JOBNO = jobNo };
                return Json(nullResult, JsonRequestBehavior.AllowGet);
            }
            string converttoString = Convert.ToString(idJOBCDT.ToString("dd-MMM-yyyy"));
            string getYear = converttoString.Substring(7, 4);
            Int64 year = Convert.ToInt64(getYear);

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            Int64 maxData = Convert.ToInt64((from n in db.CnfJobDbSet
                                             where compid == n.COMPID
                                                 && year == n.JOBYY && n.JOBTP == ddltxtJobType
                                             select n.JOBNO).Max());

            if (maxData == 0)
            {
                jobNo = 1;
            }
            else
            {
                jobNo = maxData + 1;
            }

            var result = new { JOBNO = jobNo };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult JobTypeChanged(string dropDownJobType, Int64 txtjobYear)
        {
            List<SelectListItem> getJobNO = new List<SelectListItem>();
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var result = (from n in db.CnfJobDbSet
                          where n.JOBYY == txtjobYear && n.COMPID == compid && n.JOBTP == dropDownJobType
                          select new { n.JOBNO }).ToList();


            foreach (var f in result)
            {

                getJobNO.Add(new SelectListItem { Text = f.JOBNO.ToString(), Value = f.JOBNO.ToString() });
            }

            return Json(getJobNO, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetAllData(Int64 changedDropDown, string ddltxtJobType, Int64 txtjobYear)
        {
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            String jobcdt = "", regid = "", consigneenm = "", consigneeadd = "", suppliernm = "", pkgs = "", goods = "", contno = "", docinvno = "", docinvdt = "", permitno = "",
                permitdt = "", billnom = "", billdtm = "", billfdt = "", beno = "", bedt = "", lcno = "", lcdt = "", status = "", inserttime = "", insertIpno = "", insltude = "", PartyName = "",
                crfno = "", crfdt = "", blno = "", bldt = "", btno = "", btdt = "", lcano = "", lcadt = "", awbno = "", awbdt = "", hawbno = "", hawbdt = "", undtno = "", undtdt = "", mvessel = "",
                fvessel = "",flightno="", marksno = "", etb = "", berthsno = "", arrivedt = "", rotno = "", lineno = "", deliverydt = "", cldt = "", wfdt = "", cnfvEtp = "",expno="",
                expdt="",part="",pcno="",pcdt="",spagnm="",fwagnm="",dstn="",ptemp="",vatrno="",csealno="",remarks="",partyname="";
            Int64 Id = 0, companyId = 0, partyId = 0, insertUserId = 0;
            Decimal wtgross = 0,cbm=0, wtnet = 0, cnfvUsd = 0, cnfvErt = 0, cnfvBdt = 0, crfvUsd = 0, assvBdt = 0, commamt = 0;

            var rt = db.CnfJobDbSet.Where(n => n.JOBNO == changedDropDown && n.JOBTP == ddltxtJobType && n.JOBYY == txtjobYear && n.COMPID == compid)
                .Select(n => new { n.Cnf_JobID, n.COMPID, n.JOBCDT, n.REGID, n.PARTYID, n.CONSIGNEENM, n.CONSIGNEEADD, n.SUPPLIERNM, n.PKGS, n.GOODS,
                    n.WTGROSS, n.WTNET, n.CNFV_USD, n.CNFV_ETP, n.CNFV_ERT, n.CNFV_BDT, n.CRFV_USD, n.ASSV_BDT, n.COMMAMT, n.CONTNO, n.DOCINVNO, n.DOCINVDT,
                    n.PERMITNO, n.PERMITDT, n.BILLNOM,n.FLIGHTNO, n.BILLDTM, n.BILLFDT, n.BENO, n.BEDT, n.LCNO, n.LCDT, n.STATUS,n.CBM, n.INSUSERID, n.INSTIME,
                    n.INSLTUDE, n.INSIPNO, n.CRFNO, n.CRFDT, n.BLNO, n.BLDT, n.BTNO, n.BTDT, n.LCANO, n.LCADT, n.AWBNO, n.AWBDT, n.HAWBNO, n.HAWBDT,
                    n.UNDTNO, n.UNDTDT, n.MVESSEL, n.FVESSEL, n.MARKSNO, n.ETB, n.BERTHSNO, n.ARRIVEDT, n.ROTNO, n.LINENO, n.DELIVERYDT, n.CLDT, n.WFDT,n.EXPDT,n.EXPNO,n.PARTSHIPMENT,
                n.PCNO,n.PCDT,n.SPAGNM,n.FWAGNM,n.DESTN,n.PTEMP,n.VATRNO,n.CSEALNO,n.REMARKS,n.PartyName});

            foreach (var myData in rt)
            {
                Id = myData.Cnf_JobID;
                //companyId = Convert.ToInt64(myData.COMPID);
                string JOBCDT = myData.JOBCDT.ToString();
                DateTime JOBCDT_DateTime = DateTime.Parse(JOBCDT);
                jobcdt = JOBCDT_DateTime.ToString("dd-MMM-yyyy");
                regid = myData.REGID;
                partyId = Convert.ToInt64(myData.PARTYID);
                consigneenm = myData.CONSIGNEENM;
                consigneeadd = myData.CONSIGNEEADD;
                suppliernm = myData.SUPPLIERNM;
                pkgs = myData.PKGS;
                goods = myData.GOODS;
                wtgross = Convert.ToDecimal(myData.WTGROSS);
                wtnet = Convert.ToDecimal(myData.WTNET);
                cnfvUsd = Convert.ToDecimal(myData.CNFV_USD);
                cnfvEtp = myData.CNFV_ETP;
                cnfvErt = Convert.ToDecimal(myData.CNFV_ERT);
                cnfvBdt = Convert.ToDecimal(myData.CNFV_BDT);
                crfvUsd = Convert.ToDecimal(myData.CRFV_USD);
                assvBdt = Convert.ToDecimal(myData.ASSV_BDT);
                commamt = Convert.ToDecimal(myData.COMMAMT);
                contno = myData.CONTNO;
                docinvno = myData.DOCINVNO;
                flightno = myData.FLIGHTNO;
                string DOCINVDT = myData.DOCINVDT.ToString();
                if (DOCINVDT == "") { docinvdt = ""; }
                else
                {
                    DateTime DOCINVDT_DateTime = DateTime.Parse(DOCINVDT);
                    docinvdt = DOCINVDT_DateTime.ToString("dd-MMM-yyyy");
                }
                permitno = myData.PERMITNO;
                string PERMITDT = myData.PERMITDT.ToString();
                if (PERMITDT == "") { permitdt = ""; }
                else
                {
                    DateTime PERMITDT_DateTime = DateTime.Parse(PERMITDT);
                    permitdt = PERMITDT_DateTime.ToString("dd-MMM-yyyy");
                }
                billnom = myData.BILLNOM;
                string BILLDTM = myData.BILLDTM.ToString();
                if (BILLDTM == "") { billdtm = ""; }
                else
                {
                    DateTime BILLDTM_DateTime = DateTime.Parse(BILLDTM);
                    billdtm = BILLDTM_DateTime.ToString("dd-MMM-yyyy");
                }
                string BILLFDT = myData.BILLFDT.ToString();
                if (BILLFDT == "") { billfdt = ""; }
                else
                {
                    DateTime BILLFDT_DateTime = DateTime.Parse(BILLFDT);
                    billfdt = BILLFDT_DateTime.ToString("dd-MMM-yyyy");
                }
                beno = myData.BENO;
                string BEDT = myData.BEDT.ToString();
                if (BEDT == "") { bedt = ""; }
                else
                {
                    DateTime BEDT_DateTime = DateTime.Parse(BEDT);
                    bedt = BEDT_DateTime.ToString("dd-MMM-yyyy");
                }
                lcno = myData.LCNO;
                string LCDT = myData.LCDT.ToString();
                if (LCDT == "") { lcdt = ""; }
                else
                {
                    DateTime LCDT_DateTime = DateTime.Parse(LCDT);
                    lcdt = LCDT_DateTime.ToString("dd-MMM-yyyy");
                }
                status = myData.STATUS;
                cbm = Convert.ToDecimal(myData.CBM);

                insertUserId = myData.INSUSERID;
                inserttime = Convert.ToString(myData.INSTIME);
                insertIpno = myData.INSLTUDE;
                insltude = myData.INSIPNO;


                crfno = myData.CRFNO;
                string CRFDT = myData.CRFDT.ToString();
                if (CRFDT == "") { crfdt = ""; }
                else
                {
                    DateTime CRFDT_DateTime = DateTime.Parse(CRFDT);
                    crfdt = CRFDT_DateTime.ToString("dd-MMM-yyyy");
                }
                blno = myData.BLNO;
                string BLDT = myData.BLDT.ToString();
                if (BLDT == "") { bldt = ""; }
                else
                {
                    DateTime BLDT_DateTime = DateTime.Parse(BLDT);
                    bldt = BLDT_DateTime.ToString("dd-MMM-yyyy");
                }
                btno = myData.BTNO;
                string BTDT = myData.BTDT.ToString();
                if (BTDT == "") { btdt = ""; }
                else
                {
                    DateTime BTDT_DateTime = DateTime.Parse(BTDT);
                    btdt = BTDT_DateTime.ToString("dd-MMM-yyyy");
                }
                lcano = myData.LCANO;
                string LCADT = myData.LCADT.ToString();
                if (LCADT == "") { lcadt = ""; }
                else
                {
                    DateTime LCADT_DateTime = DateTime.Parse(LCADT);
                    lcadt = LCADT_DateTime.ToString("dd-MMM-yyyy");
                }
                awbno = myData.AWBNO;
                string AWBDT = myData.AWBDT.ToString();
                if (AWBDT == "") { awbdt = ""; }
                else
                {
                    DateTime AWBDT_DateTime = DateTime.Parse(AWBDT);
                    awbdt = AWBDT_DateTime.ToString("dd-MMM-yyyy");
                }
                hawbno = myData.HAWBNO;
                string HAWBDT = myData.HAWBDT.ToString();
                if (HAWBDT == "") { hawbdt = ""; }
                else
                {
                    DateTime HAWBDT_DateTime = DateTime.Parse(HAWBDT);
                    hawbdt = HAWBDT_DateTime.ToString("dd-MMM-yyyy");
                }
                undtno = myData.UNDTNO;
                string UNDTDT = myData.UNDTDT.ToString();
                if (UNDTDT == "") { undtdt = ""; }
                else
                {
                    DateTime UNDTDT_DateTime = DateTime.Parse(UNDTDT);
                    undtdt = UNDTDT_DateTime.ToString("dd-MMM-yyyy");
                }
                mvessel = myData.MVESSEL;
                fvessel = myData.FVESSEL;
                marksno = myData.MARKSNO;
                string ETB = myData.ETB.ToString();
                if (ETB == "") { etb = ""; }
                else
                {
                    DateTime ETB_DateTime = DateTime.Parse(ETB);
                    etb = ETB_DateTime.ToString("dd-MMM-yyyy");
                }
                berthsno = myData.BERTHSNO;
                string ARRIVEDT = myData.ARRIVEDT.ToString();
                if (ARRIVEDT == "") { arrivedt = ""; }
                else
                {
                    DateTime ARRIVEDT_DateTime = DateTime.Parse(ARRIVEDT);
                    arrivedt = ARRIVEDT_DateTime.ToString("dd-MMM-yyyy");
                }
                rotno = myData.ROTNO;
                lineno = myData.LINENO;
                string DELIVERYDT = myData.DELIVERYDT.ToString();
                if (DELIVERYDT == "") { deliverydt = ""; }
                else
                {
                    DateTime DELIVERYDT_DateTime = DateTime.Parse(DELIVERYDT);
                    deliverydt = DELIVERYDT_DateTime.ToString("dd-MMM-yyyy");
                }
                string CLDT = myData.CLDT.ToString();
                if (CLDT == "") { cldt = ""; }
                else
                {
                    DateTime CLDT_DateTime = DateTime.Parse(CLDT);
                    cldt = CLDT_DateTime.ToString("dd-MMM-yyyy");
                }
                string WFDT = myData.WFDT.ToString();
                if (WFDT == "") { wfdt = ""; }
                else
                {
                    DateTime WFDT_DateTime = DateTime.Parse(WFDT);
                    wfdt = WFDT_DateTime.ToString("dd-MMM-yyyy");
                }
                string expdate = myData.EXPDT.ToString();
                if (expdate == "")
                {
                    expdt = "";
                }
                else
                {
                    DateTime ExpDate = DateTime.Parse(expdate);
                    expdt = ExpDate.ToString("dd-MMM-yyyy");
                }
                expno = myData.EXPNO;
                part = myData.PARTSHIPMENT;
                pcno=myData.PCNO;
                pcdt=myData.PCDT.ToString();
                if(pcdt==""){pcdt="";}
                else{
                     DateTime Pcdt = DateTime.Parse(pcdt);
                    pcdt = Pcdt.ToString("dd-MMM-yyyy");
                }
                spagnm=myData.SPAGNM;
                fwagnm=myData.FWAGNM;
                dstn=myData.DESTN;
                ptemp=myData.PTEMP;
                vatrno=myData.VATRNO;
                csealno=myData.CSEALNO;
                remarks=myData.REMARKS;
                partyname = myData.PartyName;

            }

            var check_bill = (from n in db.CnfJobbillsDbSet where n.COMPID == compid && n.JOBNO == changedDropDown && n.JOBTP == ddltxtJobType && n.JOBYY == txtjobYear select n).ToList().Count();
            string partyfield = "";
            if(check_bill!=0)
            {
                partyfield = "readonly";
            }
            else
            {
                partyfield = "";
            }
            var result = new
            {
                Cnf_JobID = Id,
                //companyID = companyId,
                JOBCDT = jobcdt,
                REGID = regid,
                party = partyId,
                partynm=partyname,
                CONSIGNEENM = consigneenm,
                CONSIGNEEADD = consigneeadd,
                SUPPLIERNM = suppliernm,
                PKGS = pkgs,
                GOODS = goods,
                WTGROSS = wtgross,
                WTNET = wtnet,
                CNFV_USD = cnfvUsd,
                CNFV_ETP = cnfvEtp,
                CNFV_ERT = cnfvErt,
                CNFV_BDT = cnfvBdt,
                CRFV_USD = crfvUsd,
                ASSV_BDT = assvBdt,
                COMMAMT = commamt,
                CONTNO = contno,
                DOCINVNO = docinvno,
                DOCINVDT = docinvdt,
                PERMITNO = permitno,
                PERMITDT = permitdt,
                FLIGHTNO=flightno,
                BILLNOM = billnom,
                BILLDTM = billdtm,
                BILLFDT = billfdt,
                BENO = beno,
                BEDT = bedt,
                LCNO = lcno,
                LCDT = lcdt,
                STATUS = status,
                CBM=cbm,

                INSUSERID = insertUserId,
                INSTIME = inserttime,
                INSIPNO = insertIpno,
                INSLTUDE = insltude,

                CRFNO = crfno,
                CRFDT = crfdt,
                BLNO = blno,
                BLDT = bldt,
                BTNO = btno,
                BTDT = btdt,
                LCANO = lcano,
                LCADT = lcadt,
                AWBNO = awbno,
                AWBDT = awbdt,
                HAWBNO = hawbno,
                HAWBDT = hawbdt,
                UNDTNO = undtno,
                UNDTDT = undtdt,
                MVESSEL = mvessel,
                FVESSEL = fvessel,
                MARKSNO = marksno,
                ETB = etb,
                BERTHSNO = berthsno,
                ARRIVEDT = arrivedt,
                ROTNO = rotno,
                LINENO = lineno,
                DELIVERYDT = deliverydt,
                CLDT = cldt,
                WFDT = wfdt,
               EXPNO=expno,
               EXPDT=expdt,
                PARTSHIPMENT=part,
                PARTYFIELD = partyfield,

                PCNO=pcno,
                PCDT=pcdt,
                SPAGNM=spagnm,
                FWAGNM=fwagnm,
                DESTN=dstn,
                PTEMP=ptemp,
                VATRNO=vatrno,
                CSEALNO=csealno,
                REMARKS=remarks





            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetAllData_ForDeleteOperation(Int64 changedDropDown, string ddltxtJobType, Int64 txtjobYear)
        {
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            String jobcdt = "", regid = "", consigneenm = "", consigneeadd = "", suppliernm = "", pkgs = "", goods = "", contno = "", docinvno = "", docinvdt = "", permitno = "",
                permitdt = "", billnom = "", billdtm = "", billfdt = "", beno = "", bedt = "", lcno = "", lcdt = "", status = "", inserttime = "", insertIpno = "", insltude = "", PartyName = "", cnfvEtp = "";
            Int64 Id = 0, companyId = 0, partyId = 0, insertUserId = 0;
            Decimal wtgross = 0, wtnet = 0, cnfvUsd = 0, cnfvErt = 0, cnfvBdt = 0, crfvUsd = 0, assvBdt = 0, commamt = 0;

            var rt = db.CnfJobDbSet.Where(n => n.JOBNO == changedDropDown && n.JOBTP == ddltxtJobType && n.JOBYY == txtjobYear && n.COMPID == compid).Select(n => new { n.Cnf_JobID, n.COMPID, n.JOBCDT, n.REGID, n.PARTYID, n.CONSIGNEENM, n.CONSIGNEEADD, n.SUPPLIERNM, n.PKGS, n.GOODS, n.WTGROSS, n.WTNET, n.CNFV_USD, n.CNFV_ETP, n.CNFV_ERT, n.CNFV_BDT, n.CRFV_USD, n.ASSV_BDT, n.COMMAMT, n.CONTNO, n.DOCINVNO, n.DOCINVDT, n.PERMITNO, n.PERMITDT, n.BILLNOM, n.BILLDTM, n.BILLFDT, n.BENO, n.BEDT, n.LCNO, n.LCDT, n.STATUS, n.INSUSERID, n.INSTIME, n.INSLTUDE, n.INSIPNO });

            foreach (var myData in rt)
            {
                Id = myData.Cnf_JobID;
                //companyId = Convert.ToInt64(myData.COMPID);
                string JOBCDT = myData.JOBCDT.ToString();
                DateTime JOBCDT_DateTime = DateTime.Parse(JOBCDT);
                jobcdt = JOBCDT_DateTime.ToString("dd-MMM-yyyy");
                regid = myData.REGID;
                partyId = Convert.ToInt64(myData.PARTYID);
                consigneenm = myData.CONSIGNEENM;
                consigneeadd = myData.CONSIGNEEADD;
                suppliernm = myData.SUPPLIERNM;
                pkgs = myData.PKGS;
                goods = myData.GOODS;
                wtgross = Convert.ToDecimal(myData.WTGROSS);
                wtnet = Convert.ToDecimal(myData.WTNET);
                cnfvUsd = Convert.ToDecimal(myData.CNFV_USD);
                cnfvEtp = myData.CNFV_ETP;
                cnfvErt = Convert.ToDecimal(myData.CNFV_ERT);
                cnfvBdt = Convert.ToDecimal(myData.CNFV_BDT);
                crfvUsd = Convert.ToDecimal(myData.CRFV_USD);
                assvBdt = Convert.ToDecimal(myData.ASSV_BDT);
                commamt = Convert.ToDecimal(myData.COMMAMT);
                contno = myData.CONTNO;
                docinvno = myData.DOCINVNO;

                string DOCINVDT = myData.DOCINVDT.ToString();
                if (DOCINVDT == "")
                {
                    docinvdt = "";
                }
                else
                {
                    DateTime DOCINVDT_DateTime = DateTime.Parse(DOCINVDT);
                    docinvdt = DOCINVDT_DateTime.ToString("dd-MMM-yyyy");
                }

                permitno = myData.PERMITNO;

                string PERMITDT = myData.PERMITDT.ToString();
                if (PERMITDT == "")
                {
                    permitdt = "";
                }
                else
                {
                    DateTime PERMITDT_DateTime = DateTime.Parse(PERMITDT);
                    permitdt = PERMITDT_DateTime.ToString("dd-MMM-yyyy");
                }


                billnom = myData.BILLNOM;

                string BILLDTM = myData.BILLDTM.ToString();
                if (BILLDTM == "")
                {
                    billdtm = "";
                }
                else
                {
                    DateTime BILLDTM_DateTime = DateTime.Parse(BILLDTM);
                    billdtm = BILLDTM_DateTime.ToString("dd-MMM-yyyy");
                }


                string BILLFDT = myData.BILLFDT.ToString();
                if (BILLFDT == "")
                {
                    billfdt = "";
                }
                else
                {
                    DateTime BILLFDT_DateTime = DateTime.Parse(BILLFDT);
                    billfdt = BILLFDT_DateTime.ToString("dd-MMM-yyyy");
                }

                beno = myData.BENO;

                string BEDT = myData.BEDT.ToString();
                if (BEDT == "")
                {
                    bedt = "";
                }
                else
                {
                    DateTime BEDT_DateTime = DateTime.Parse(BEDT);
                    bedt = BEDT_DateTime.ToString("dd-MMM-yyyy");
                }


                lcno = myData.LCNO;

                string LCDT = myData.LCDT.ToString();
                if (LCDT == "")
                {
                    lcdt = "";
                }
                else
                {
                    DateTime LCDT_DateTime = DateTime.Parse(LCDT);
                    lcdt = LCDT_DateTime.ToString("dd-MMM-yyyy");
                }

                status = myData.STATUS;

                insertUserId = myData.INSUSERID;
                inserttime = Convert.ToString(myData.INSTIME);
                insertIpno = myData.INSLTUDE;
                insltude = myData.INSIPNO;
            }


            var findPartyName = from n in db.GlAcchartDbSet where n.ACCOUNTCD == partyId select new { n.ACCOUNTNM };
            foreach (var name in findPartyName)
            {
                PartyName = name.ACCOUNTNM;
            }

            var result = new
            {
                Cnf_JobID = Id,
                //companyID = companyId,
                JOBCDT = jobcdt,
                REGID = regid,
                party = PartyName,
                CONSIGNEENM = consigneenm,
                CONSIGNEEADD = consigneeadd,
                SUPPLIERNM = suppliernm,
                PKGS = pkgs,
                GOODS = goods,
                WTGROSS = wtgross,
                WTNET = wtnet,
                CNFV_USD = cnfvUsd,
                CNFV_ETP = cnfvEtp,
                CNFV_ERT = cnfvErt,
                CNFV_BDT = cnfvBdt,
                CRFV_USD = crfvUsd,
                ASSV_BDT = assvBdt,
                COMMAMT = commamt,
                CONTNO = contno,
                DOCINVNO = docinvno,
                DOCINVDT = docinvdt,
                PERMITNO = permitno,
                PERMITDT = permitdt,
                BILLNOM = billnom,
                BILLDTM = billdtm,
                BILLFDT = billfdt,
                BENO = beno,
                BEDT = bedt,
                LCNO = lcno,
                LCDT = lcdt,
                STATUS = status,

                INSUSERID = insertUserId,
                INSTIME = inserttime,
                INSIPNO = insertIpno,
                INSLTUDE = insltude
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CNFBDT(Decimal ExchangeRate, Decimal ValueUSD)
        {
            Decimal valueUSD = 0;
            valueUSD = ExchangeRate * ValueUSD;

            var result = new { Valueusd = valueUSD };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ForCommissionCalculation(Decimal CnfAssvBDT, Int64 CnfPartyId, string CnfExchangeTp)
        {
            Decimal comm = 0;

            //CNF_JOB aCnfjob=new CNF_JOB();
            //Int64 compid =Convert.ToInt64(aCnfjob.COMPID);

            var comdata = (from n in db.CnfCommissionDbSet
                           where n.PARTYID == CnfPartyId && n.EXCTP == CnfExchangeTp && n.VALUEFR <= CnfAssvBDT && n.VALUETO >= CnfAssvBDT
                           select n).ToList();
            var party_remarks = (from n in db.CnfPartyDbSet where n.PARTYID == CnfPartyId select n).ToList();

            string type = "";
            Decimal comamount=0;
            foreach(var a in comdata)
            {
                type = a.VALUETP;
                comamount = Convert.ToDecimal(a.COMMAMT);
            }
           
            if(type=="Percentage")
            {
                
                comm = CnfAssvBDT * (comamount /(Decimal) 100);
            }
            else 
            {
                comm = comamount;
            }

            string remarks = "";
            foreach(var x in party_remarks)
            {
                remarks = x.REMARKS;
            }
            var result = new { commit = comm, Type = type, Percentage = comamount, Remarks = remarks };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

  
        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
