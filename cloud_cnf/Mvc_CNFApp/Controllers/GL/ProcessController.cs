using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Mvc_CNFApp.DataAccess;
using Mvc_CNFApp.Models;

namespace Mvc_CNFApp.Controllers
{
    public class ProcessController : AppController
    {
        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;


        CnfDbContext db = new CnfDbContext();
        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;



        public ProcessController()
        {
            ViewData["HighLight_Menu_GL_Form"] = "heighlight";
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }

        public ASL_LOG aslLog = new ASL_LOG();


        public void Insert_Process_LogData(PageModel model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == model.AGlMaster.COMPID && n.USERID == model.AGlMaster.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(model.AGlMaster.COMPID);
            aslLog.USERID = model.AGlMaster.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.AGlMaster.INSIPNO;
            aslLog.LOGLTUDE = model.AGlMaster.INSLTUDE;
            aslLog.TABLEID = "GL_MASTER PROCESS";

            string username = "";
            var UserNameFind = (from n in db.AslUsercoDbSet where n.USERID == aslLog.USERID select n).ToList();
            foreach (var name in UserNameFind)
            {
                username = name.USERNM;
            }

            aslLog.LOGDATA = Convert.ToString("Process: " + "Process to GlMaster" + "Time: " + aslLog.LOGTIME + ",\nDate: " + model.AGlMaster.TRANSDT + ",\nUserName: " + username + ".");
            aslLog.USERPC = model.AGlMaster.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }
        // GET: /Process/

        public ActionResult Index()
        {
            return View();
        }


        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(PageModel model, string command)
        {
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            if (command == "Process")
            {
                if (model.AGlMaster.TRANSDT != null)
                {

                   


                    var checkDate = (from n in db.GlStransDbSet where n.TRANSDT == model.AGlMaster.TRANSDT && n.COMPID == compid select n).OrderBy(x => x.TRANSNO).ToList();
                   
                    var checkDate_Mtrans = (from n in db.MtransdbSet where n.TRANSDT == model.AGlMaster.TRANSDT && n.COMPID == compid select n).OrderBy(x => x.TRANSNO).ToList();

                    //var checkDataFromJobBill =
                    //    (from n in db.CnfJobbillsDbSet
                    //     join m in db.CnfJobDbSet on n.COMPID equals m.COMPID
                    //     join o in db.GlAcchartDbSet on m.COMPID equals o.COMPID
                    //        where n.COMPID == compid && n.BILLPDT == model.AGlMaster.TRANSDT
                    //        && m.COMPID==compid && m.JOBYY==n.JOBYY && m.JOBTP==n.JOBTP && n.JOBNO==m.JOBNO && m.PARTYID==o.ACCOUNTCD
                    //        && o.COMPID==compid && o.ACCOUNTNM==n.PARTYNM
                           
                    //        group new{n,m} by new {n.COMPID,n.JOBNO,n.JOBTP,n.JOBYY,n.PARTYNM,n.BILLPDT,m.COMMAMT}
                    //        into grp
                    //        select new
                    //        {
                    //            Count = grp.Count(),
                    //           jobno=grp.Key.JOBNO,
                    //           jobtp=grp.Key.JOBTP,
                    //           jobyy=grp.Key.JOBYY,
                    //          partyname=grp.Key.PARTYNM,
                    //            billpdt=grp.Key.BILLPDT,
                    //            compid = grp.Key.COMPID,
                    //            sumbillamount = grp.Sum(x => x.n.BILLAMT)+grp.Key.COMMAMT
                             
                            
                    //        }).ToList();
                    var checkDataFromJobBill = (from n in db.CnfJobbillsDbSet where n.COMPID == compid && n.BILLPDT == model.AGlMaster.TRANSDT select new { n.JOBNO,n.JOBTP,n.JOBYY,n.PARTYNM}).Distinct().ToList();
                    DateTime transdt = Convert.ToDateTime(model.AGlMaster.TRANSDT);

                    var result = processJobReceive.jobreceiveProcess(transdt);

                    if (checkDate.Count != 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {
                            if (VARIABLE.TABLEID == "GL_STRANS")
                            {
                                db.GlMasterDbSet.Remove(VARIABLE);
                            }
                        }



                        db.SaveChanges();

                        foreach (var x in checkDate)
                        {


                            if (x.TRANSTP == "MREC")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 10001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";


                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;


                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 10002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);

                                    Insert_Process_LogData(model);
                                    db.SaveChanges();

                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }

                            }

                            else if (x.TRANSTP == "MPAY")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 20001;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = 20002;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            else if (x.TRANSTP == "JOUR")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 30001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 30002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";
                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;
                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            else if (x.TRANSTP == "CONT")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 40001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";


                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 40002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;
                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();


                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            TempData["message"] = "Processing Done";




                        }



                    }
                    else if (checkDate.Count == 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {
                            if (VARIABLE.TABLEID == "GL_STRANS")
                            {
                                db.GlMasterDbSet.Remove(VARIABLE);
                            }
                        }



                        db.SaveChanges();
                    }

                   

                    if (checkDataFromJobBill.Count() != 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT && l.TABLEID == "CNF_JOBBILL"
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {

                            db.GlMasterDbSet.Remove(VARIABLE);

                        }



                        db.SaveChanges();
                        int i = 0;
                        //for (i = 0; i < checkDataFromJobBill.Count; i++)
                        foreach (var x in checkDataFromJobBill)
                        {

                            Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                where a.COMPID == compid && a.TRANSTP == "JOUR" && a.TABLEID == "CNF_JOBBILL"
                                                                select a.TRANSSL).Max());
                            Int64 maxTransNo = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                where a.COMPID == compid && a.TRANSTP == "JOUR" && a.TABLEID == "CNF_JOBBILL"
                                                                select a.TRANSNO).Max());


                            decimal sum_billamount = Convert.ToDecimal((from n in db.CnfJobbillsDbSet
                                                      where n.COMPID == compid && n.BILLPDT == model.AGlMaster.TRANSDT
                                                          && n.JOBNO == x.JOBNO && n.JOBTP == x.JOBTP
                                                      select n.BILLAMT).Sum());
                            decimal commamount = Convert.ToDecimal((from n in db.CnfJobDbSet where n.COMPID == compid && n.JOBNO == x.JOBNO && n.JOBTP == x.JOBTP && n.JOBYY == x.JOBYY select n.COMMAMT).Sum());
                            sum_billamount = sum_billamount + commamount;

                            string Transmonthyear = "";
                            DateTime ab = Convert.ToDateTime(model.AGlMaster.TRANSDT);
                            string convertDate = ab.ToString("dd-MMM-yyyy");
                            string getYear = convertDate.Substring(9, 2);
                            string getMonth = convertDate.Substring(3, 3);
                            Transmonthyear = getMonth + "-" + getYear;



                            DateTime ab1 = Convert.ToDateTime(model.AGlMaster.TRANSDT);
                            string convertDate1 = ab.ToString("dd-MM-yyyy");
                            string getyear1 = convertDate1.Substring(6, 4);
                            string getmonth1 = convertDate1.Substring(3, 2);
                            string halftransno = getyear1 + getmonth1;
                            string partyname = x.PARTYNM;
                            var findPartyid =
                                (from n in db.CnfPartyDbSet where n.COMPID == compid && n.PARTYNM == partyname select n)
                                    .ToList();
                            Int64 partyid = 0;
                            foreach (var cnfParty in findPartyid)
                            {
                                partyid = Convert.ToInt64(cnfParty.PARTYID);
                            }

                            if (maxSlCheck == 0)
                            {
                                model.AGlMaster.TRANSSL = 30001;

                                model.AGlMaster.TRANSDT = model.AGlMaster.TRANSDT;
                                model.AGlMaster.COMPID = compid;
                                model.AGlMaster.TRANSTP = "JOUR";
                                model.AGlMaster.TRANSMY = Transmonthyear;
                                model.AGlMaster.TRANSNO = Convert.ToInt64(halftransno + "0001");
                                model.AGlMaster.TRANSFOR = "OFFICIAL";
                                model.AGlMaster.TRANSMODE = "CASH";
                                model.AGlMaster.COSTPID = null;
                                model.AGlMaster.DEBITCD = partyid;
                                model.AGlMaster.CREDITCD = Convert.ToInt64(Convert.ToString(compid) + "3010001");
                                model.AGlMaster.CHEQUENO = null;
                                model.AGlMaster.CHEQUEDT = null;
                                model.AGlMaster.REMARKS = Convert.ToString("Bill-" + x.JOBTP + "-" + x.JOBYY+ "-" + x.JOBNO);

                                model.AGlMaster.DEBITAMT = sum_billamount;
                                model.AGlMaster.CREDITAMT = 0;

                                model.AGlMaster.TRANSDRCR = "DEBIT";
                                model.AGlMaster.TABLEID = "CNF_JOBBILL";

                                model.AGlMaster.USERPC = strHostName;
                                model.AGlMaster.INSIPNO = ipAddress.ToString();
                                model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                model.AGlMaster.INSUSERID =
                                    Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                db.GlMasterDbSet.Add(model.AGlMaster);
                                db.SaveChanges();


                                model.AGlMaster.TRANSSL = 30002;

                                model.AGlMaster.TRANSDT = model.AGlMaster.TRANSDT;
                                model.AGlMaster.COMPID = Convert.ToInt64(compid);
                                model.AGlMaster.TRANSTP = "JOUR";
                                model.AGlMaster.TRANSMY = Transmonthyear;
                                model.AGlMaster.TRANSNO = Convert.ToInt64(halftransno + "0001");
                                model.AGlMaster.TRANSFOR = "OFFICIAL";
                                model.AGlMaster.TRANSMODE = "CASH";
                                model.AGlMaster.COSTPID = null;
                                model.AGlMaster.DEBITCD = Convert.ToInt64(Convert.ToString(compid) + "3010001");
                                model.AGlMaster.CREDITCD = partyid;
                                model.AGlMaster.CHEQUENO = null;
                                model.AGlMaster.CHEQUEDT = null;
                                model.AGlMaster.REMARKS = Convert.ToString("Bill-" + x.JOBTP + "-" + x.JOBYY + "-" + x.JOBNO);

                                model.AGlMaster.DEBITAMT = 0;
                                model.AGlMaster.CREDITAMT = sum_billamount;

                                model.AGlMaster.TRANSDRCR = "CREDIT";
                                model.AGlMaster.TABLEID = "CNF_JOBBILL";

                                model.AGlMaster.USERPC = strHostName;
                                model.AGlMaster.INSIPNO = ipAddress.ToString();
                                model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                model.AGlMaster.INSUSERID =
                                    Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                db.GlMasterDbSet.Add(model.AGlMaster);
                                Insert_Process_LogData(model);
                                db.SaveChanges();
                            }
                            else
                            {
                                model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                model.AGlMaster.TRANSDT = model.AGlMaster.TRANSDT;
                                model.AGlMaster.COMPID = Convert.ToInt64(compid);
                                model.AGlMaster.TRANSTP = "JOUR";
                                model.AGlMaster.TRANSMY = Transmonthyear;
                                if (maxTransNo == 0)
                                {
                                    model.AGlMaster.TRANSNO = Convert.ToInt64(halftransno + "0001");
                                }
                                else
                                {
                                    if (maxTransNo.ToString().StartsWith(getyear1) == true)
                                    {
                                        model.AGlMaster.TRANSNO = maxTransNo + 1;
                                    }
                                    else
                                    {
                                        model.AGlMaster.TRANSNO = Convert.ToInt64(halftransno + "0001");
                                    }
                                }
                                model.AGlMaster.TRANSFOR = "OFFICIAL";
                                model.AGlMaster.TRANSMODE = "CASH";
                                model.AGlMaster.COSTPID = null;
                                model.AGlMaster.DEBITCD = partyid;
                                model.AGlMaster.CREDITCD = Convert.ToInt64(Convert.ToString(compid) + "3010001");
                                model.AGlMaster.CHEQUENO = null;
                                model.AGlMaster.CHEQUEDT = null;
                                model.AGlMaster.REMARKS = Convert.ToString("Bill-" + x.JOBTP + "-" + x.JOBYY + "-" + x.JOBNO);
                                model.AGlMaster.DEBITAMT = sum_billamount;
                                model.AGlMaster.CREDITAMT = 0;

                                model.AGlMaster.TRANSDRCR = "DEBIT";
                                model.AGlMaster.TABLEID = "CNF_JOBBILL";

                                model.AGlMaster.USERPC = strHostName;
                                model.AGlMaster.INSIPNO = ipAddress.ToString();
                                model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                model.AGlMaster.INSUSERID =
                                    Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                db.GlMasterDbSet.Add(model.AGlMaster);
                                db.SaveChanges();


                                model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                model.AGlMaster.TRANSDT = model.AGlMaster.TRANSDT;
                                model.AGlMaster.COMPID = Convert.ToInt64(compid);
                                model.AGlMaster.TRANSTP = "JOUR";
                                model.AGlMaster.TRANSMY = Transmonthyear;
                                if (maxTransNo == 0)
                                {
                                    model.AGlMaster.TRANSNO = Convert.ToInt64(halftransno + "0002");
                                }
                                else
                                {
                                    if (maxTransNo.ToString().StartsWith(getyear1) == true)
                                    {
                                        model.AGlMaster.TRANSNO = maxTransNo + 1;
                                    }
                                    else
                                    {
                                        model.AGlMaster.TRANSNO = Convert.ToInt64(halftransno + "0002");
                                    }
                                }
                                model.AGlMaster.TRANSFOR = "OFFICIAL";
                                model.AGlMaster.TRANSMODE = "CASH";
                                model.AGlMaster.COSTPID = null;
                                model.AGlMaster.DEBITCD = Convert.ToInt64(Convert.ToString(compid) + "3010001");
                                model.AGlMaster.CREDITCD = partyid;
                                model.AGlMaster.CHEQUENO = null;
                                model.AGlMaster.CHEQUEDT = null;
                                model.AGlMaster.REMARKS = Convert.ToString("Bill-" + x.JOBTP + "-" + x.JOBYY + "-" + x.JOBNO);

                                model.AGlMaster.DEBITAMT = 0;
                                model.AGlMaster.CREDITAMT = sum_billamount;

                                model.AGlMaster.TRANSDRCR = "CREDIT";
                                model.AGlMaster.TABLEID = "CNF_JOBBILL";

                                model.AGlMaster.USERPC = strHostName;
                                model.AGlMaster.INSIPNO = ipAddress.ToString();
                                model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                model.AGlMaster.INSUSERID =
                                    Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                db.GlMasterDbSet.Add(model.AGlMaster);
                                Insert_Process_LogData(model);
                                db.SaveChanges();
                            }


                            TempData["message"] = "Processing Done";

                        }
                       

    

                    }
                    else if (checkDataFromJobBill.Count == 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                            where
                                l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT && l.TABLEID == "CNF_JOBBILL"
                            select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {

                            db.GlMasterDbSet.Remove(VARIABLE);

                        }



                        db.SaveChanges();
                    }
                    if (checkDate_Mtrans.Count != 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {
                            if (VARIABLE.TABLEID == "GL_MTRANS")
                            {
                                db.GlMasterDbSet.Remove(VARIABLE);
                            }
                        }



                        db.SaveChanges();

                        foreach (var x in checkDate_Mtrans)
                        {


                            if (x.TRANSTP == "MREC")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_MTRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 50001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                 
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";


                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;


                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 50002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                   
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);

                                    Insert_Process_LogData(model);
                                    db.SaveChanges();

                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                   
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                  
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }

                            }

                            else if (x.TRANSTP == "MPAY")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_MTRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 60001;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                  
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = 60002;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                               
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                              
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                 
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            else if (x.TRANSTP == "JOUR")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_MTRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 70001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                               
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 70002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                  
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                 
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";
                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;
                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            else if (x.TRANSTP == "CONT")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_MTRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 80001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";


                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 80002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                              
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;
                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();


                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                  
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                            
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;
                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            TempData["message"] = "Processing Done";




                        }



                    }
                    else if (checkDate_Mtrans.Count == 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {
                            if (VARIABLE.TABLEID == "GL_MTRANS")
                            {
                                db.GlMasterDbSet.Remove(VARIABLE);
                            }
                        }



                        db.SaveChanges();
                    }
                    else 
                    {
                        TempData["message"] = "No no...process would not done";

                    }

                }

            }

            else if (command == "Expense Process")
            {
                if (model.AGlMaster.TRANSDT != null)
                {
                    var checkDataFromJobExp =
                      (from n in db.CnfJobexpDbSet
                       where n.COMPID == compid && n.TRANSDT == model.AGlMaster.TRANSDT
                       group new { n } by new { n.COMPID, n.TRANSDT, n.TRANSNO, n.JOBNO, n.JOBTP, n.JOBYY, n.EXPCD }
                           into grp
                           select new
                           {
                               jobno = grp.Key.JOBNO,
                               jobtp = grp.Key.JOBTP,
                               jobyy = grp.Key.JOBYY,
                               compid = grp.Key.COMPID,
                               transdt = grp.Key.TRANSDT,
                               transno = grp.Key.TRANSNO,
                               expcd = grp.Key.EXPCD,
                               sumexpamount = grp.Sum(x => x.n.EXPAMT),

                           }).ToList();

                    if (checkDataFromJobExp.Count != 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT && l.TABLEID == "CNF_JOBEXP"
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {

                            db.GlMasterDbSet.Remove(VARIABLE);

                        }



                        db.SaveChanges();
                        int i = 0;
                        for (i = 0; i < checkDataFromJobExp.Count; i++)
                        {
                            if ((checkDataFromJobExp[i].expcd.ToString().Substring(3, 3) == "101") ||
                                (checkDataFromJobExp[i].expcd.ToString().Substring(3, 3) == "102")) //Cash or Bank
                            {

                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == "MPAY"
                                                                    select a.TRANSSL).Max());

                                string Transmonthyear = "";
                                DateTime ab = Convert.ToDateTime(checkDataFromJobExp[i].transdt);
                                string convertDate = ab.ToString("dd-MMM-yyyy");
                                string getYear = convertDate.Substring(9, 2);
                                string getMonth = convertDate.Substring(3, 3);
                                Transmonthyear = getMonth + "-" + getYear;





                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 20001;


                                    model.AGlMaster.TRANSDT = checkDataFromJobExp[i].transdt;
                                    model.AGlMaster.COMPID = checkDataFromJobExp[i].compid;
                                    model.AGlMaster.TRANSTP = "MPAY";
                                    model.AGlMaster.TRANSMY = Transmonthyear;
                                    model.AGlMaster.TRANSNO = checkDataFromJobExp[i].transno;
                                    model.AGlMaster.TRANSFOR = "OFFICIAL";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = null;
                                    model.AGlMaster.DEBITCD = Convert.ToInt64(Convert.ToString(checkDataFromJobExp[i].compid) + "4010001");
                                    model.AGlMaster.CREDITCD = Convert.ToInt64(checkDataFromJobExp[i].expcd);
                                    model.AGlMaster.CHEQUENO = null;
                                    model.AGlMaster.CHEQUEDT = null;
                                    model.AGlMaster.REMARKS = Convert.ToString("Expense-" + checkDataFromJobExp[i].jobtp + "-" + checkDataFromJobExp[i].jobyy + "-" + checkDataFromJobExp[i].jobno + "-");
                                    //model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = checkDataFromJobExp[i].sumexpamount;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "CNF_JOBEXP";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = 20002;


                                    model.AGlMaster.TRANSDT = checkDataFromJobExp[i].transdt;
                                    model.AGlMaster.COMPID = checkDataFromJobExp[i].compid;
                                    model.AGlMaster.TRANSTP = "MPAY";
                                    model.AGlMaster.TRANSMY = Transmonthyear;
                                    model.AGlMaster.TRANSNO = checkDataFromJobExp[i].transno;
                                    model.AGlMaster.TRANSFOR = "OFFICIAL";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = null;
                                    model.AGlMaster.DEBITCD = Convert.ToInt64(checkDataFromJobExp[i].expcd);
                                    model.AGlMaster.CREDITCD = Convert.ToInt64(Convert.ToString(checkDataFromJobExp[i].compid) + "4010001"); ;
                                    model.AGlMaster.CHEQUENO = null;
                                    model.AGlMaster.CHEQUEDT = null;
                                    model.AGlMaster.REMARKS = Convert.ToString("Expense-" + checkDataFromJobExp[i].jobtp + "-" + checkDataFromJobExp[i].jobyy + "-" + checkDataFromJobExp[i].jobno + "-");

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = checkDataFromJobExp[i].sumexpamount;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "CNF_JOBEXP";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;


                                    model.AGlMaster.TRANSDT = checkDataFromJobExp[i].transdt;
                                    model.AGlMaster.COMPID = checkDataFromJobExp[i].compid;
                                    model.AGlMaster.TRANSTP = "MPAY";
                                    model.AGlMaster.TRANSMY = Transmonthyear;
                                    model.AGlMaster.TRANSNO = checkDataFromJobExp[i].transno;
                                    model.AGlMaster.TRANSFOR = "OFFICIAL";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = null;
                                    model.AGlMaster.DEBITCD = Convert.ToInt64(Convert.ToString(checkDataFromJobExp[i].compid) + "4010001");
                                    model.AGlMaster.CREDITCD = Convert.ToInt64(checkDataFromJobExp[i].expcd);
                                    model.AGlMaster.CHEQUENO = null;
                                    model.AGlMaster.CHEQUEDT = null;
                                    model.AGlMaster.REMARKS = Convert.ToString("Expense-" + checkDataFromJobExp[i].jobtp + "-" + checkDataFromJobExp[i].jobyy + "-" + checkDataFromJobExp[i].jobno + "-");

                                    model.AGlMaster.DEBITAMT = checkDataFromJobExp[i].sumexpamount;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "CNF_JOBEXP";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;


                                    model.AGlMaster.TRANSDT = checkDataFromJobExp[i].transdt;
                                    model.AGlMaster.COMPID = checkDataFromJobExp[i].compid;
                                    model.AGlMaster.TRANSTP = "MPAY";
                                    model.AGlMaster.TRANSMY = Transmonthyear;
                                    model.AGlMaster.TRANSNO = checkDataFromJobExp[i].transno;
                                    model.AGlMaster.TRANSFOR = "OFFICIAL";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = null;
                                    model.AGlMaster.DEBITCD = Convert.ToInt64(checkDataFromJobExp[i].expcd);
                                    model.AGlMaster.CREDITCD = Convert.ToInt64(Convert.ToString(checkDataFromJobExp[i].compid) + "4010001"); ;
                                    model.AGlMaster.CHEQUENO = null;
                                    model.AGlMaster.CHEQUEDT = null;
                                    model.AGlMaster.REMARKS = Convert.ToString("Expense-" + checkDataFromJobExp[i].jobtp + "-" + checkDataFromJobExp[i].jobyy + "-" + checkDataFromJobExp[i].jobno + "-");

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = checkDataFromJobExp[i].sumexpamount;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "CNF_JOBEXP";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == "JOUR"
                                                                    select a.TRANSSL).Max());

                                string Transmonthyear = "";
                                DateTime ab = Convert.ToDateTime(checkDataFromJobExp[i].transdt);
                                string convertDate = ab.ToString("dd-MMM-yyyy");
                                string getYear = convertDate.Substring(9, 2);
                                string getMonth = convertDate.Substring(3, 3);
                                Transmonthyear = getMonth + "-" + getYear;




                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 30001;

                                    model.AGlMaster.TRANSDT = checkDataFromJobExp[i].transdt;
                                    model.AGlMaster.COMPID = checkDataFromJobExp[i].compid;
                                    model.AGlMaster.TRANSTP = "JOUR";
                                    model.AGlMaster.TRANSMY = Transmonthyear;
                                    model.AGlMaster.TRANSNO = checkDataFromJobExp[i].transno;
                                    model.AGlMaster.TRANSFOR = "OFFICIAL";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = null;
                                    model.AGlMaster.DEBITCD = Convert.ToInt64(Convert.ToString(checkDataFromJobExp[i].compid) + "4010001");
                                    model.AGlMaster.CREDITCD = Convert.ToInt64(checkDataFromJobExp[i].expcd);
                                    model.AGlMaster.CHEQUENO = null;
                                    model.AGlMaster.CHEQUEDT = null;
                                    model.AGlMaster.REMARKS = Convert.ToString("Expense-" + checkDataFromJobExp[i].jobtp + "-" + checkDataFromJobExp[i].jobyy + "-" + checkDataFromJobExp[i].jobno + "-");

                                    model.AGlMaster.DEBITAMT = checkDataFromJobExp[i].sumexpamount;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "CNF_JOBEXP";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = 30002;

                                    model.AGlMaster.TRANSDT = checkDataFromJobExp[i].transdt;
                                    model.AGlMaster.COMPID = checkDataFromJobExp[i].compid;
                                    model.AGlMaster.TRANSTP = "JOUR";
                                    model.AGlMaster.TRANSMY = Transmonthyear;
                                    model.AGlMaster.TRANSNO = checkDataFromJobExp[i].transno;
                                    model.AGlMaster.TRANSFOR = "OFFICIAL";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = null;
                                    model.AGlMaster.DEBITCD = Convert.ToInt64(checkDataFromJobExp[i].expcd);
                                    model.AGlMaster.CREDITCD = Convert.ToInt64(Convert.ToString(checkDataFromJobExp[i].compid) + "4010001");
                                    model.AGlMaster.CHEQUENO = null;
                                    model.AGlMaster.CHEQUEDT = null;
                                    model.AGlMaster.REMARKS = Convert.ToString("Expense-" + checkDataFromJobExp[i].jobtp + "-" + checkDataFromJobExp[i].jobyy + "-" + checkDataFromJobExp[i].jobno + "-");

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = checkDataFromJobExp[i].sumexpamount;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "CNF_JOBEXP";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = checkDataFromJobExp[i].transdt;
                                    model.AGlMaster.COMPID = checkDataFromJobExp[i].compid;
                                    model.AGlMaster.TRANSTP = "JOUR";
                                    model.AGlMaster.TRANSMY = Transmonthyear;
                                    model.AGlMaster.TRANSNO = checkDataFromJobExp[i].transno;
                                    model.AGlMaster.TRANSFOR = "OFFICIAL";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = null;
                                    model.AGlMaster.DEBITCD = Convert.ToInt64(Convert.ToString(checkDataFromJobExp[i].compid) + "4010001");
                                    model.AGlMaster.CREDITCD = Convert.ToInt64(checkDataFromJobExp[i].expcd);
                                    model.AGlMaster.CHEQUENO = null;
                                    model.AGlMaster.CHEQUEDT = null;
                                    model.AGlMaster.REMARKS = Convert.ToString("Expense-" + checkDataFromJobExp[i].jobtp + "-" + checkDataFromJobExp[i].jobyy + "-" + checkDataFromJobExp[i].jobno + "-");

                                    model.AGlMaster.DEBITAMT = checkDataFromJobExp[i].sumexpamount;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "CNF_JOBEXP";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = checkDataFromJobExp[i].transdt;
                                    model.AGlMaster.COMPID = checkDataFromJobExp[i].compid;
                                    model.AGlMaster.TRANSTP = "JOUR";
                                    model.AGlMaster.TRANSMY = Transmonthyear;
                                    model.AGlMaster.TRANSNO = checkDataFromJobExp[i].transno;
                                    model.AGlMaster.TRANSFOR = "OFFICIAL";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = null;
                                    model.AGlMaster.DEBITCD = Convert.ToInt64(checkDataFromJobExp[i].expcd);
                                    model.AGlMaster.CREDITCD = Convert.ToInt64(Convert.ToString(checkDataFromJobExp[i].compid) + "4010001");
                                    model.AGlMaster.CHEQUENO = null;
                                    model.AGlMaster.CHEQUEDT = null;
                                    model.AGlMaster.REMARKS = Convert.ToString("Expense-" + checkDataFromJobExp[i].jobtp + "-" + checkDataFromJobExp[i].jobyy + "-" + checkDataFromJobExp[i].jobno + "-");

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = checkDataFromJobExp[i].sumexpamount;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "CNF_JOBEXP";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }

                            TempData["message"] = "Processing Done For Expense";

                        }









                    }
                    else if (checkDataFromJobExp.Count == 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT && l.TABLEID == "CNF_JOBEXP"
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {

                            db.GlMasterDbSet.Remove(VARIABLE);

                        }



                        db.SaveChanges();
                    }
                }
               

            }

            else
            {
                TempData["message"] = null;
            }




            return RedirectToAction("Index");
        }


    }
}
