using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Mvc_CNFApp.Models;

namespace Mvc_CNFApp.Controllers
{
    public class PartyController : AppController
    {
        private CnfDbContext db = new CnfDbContext();

        //Datetime formet
        private IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public PartyController()
        {
            ViewData["HighLight_Menu_CnfForm"] = "heighlight";
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }


        // Create ASL_LOG object and it used to this Insert_Asl_LogData, Update_Asl_LogData, Delete_Asl_LogData (AslUserco aslUsercos).
        public ASL_LOG aslLog = new ASL_LOG();

        // SAVE ALL INFORMATION from CNF_PARTY TO Asl_lOG Database Table.
        public void Insert_Asl_LogData(CNF_PARTY cnfParty)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == cnfParty.COMPID && n.USERID == cnfParty.INSUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(cnfParty.COMPID);
            aslLog.USERID = cnfParty.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfParty.INSIPNO;
            aslLog.LOGLTUDE = cnfParty.INSLTUDE;
            aslLog.TABLEID = "CNF_PARTY";
            aslLog.LOGDATA =
                Convert.ToString("User Name: " + cnfParty.PARTYNM + ",\nAddress: " + cnfParty.ADDRESS +
                                 ",\nContact No: " + cnfParty.CONTACTNO + ",\nEmail ID: " + cnfParty.EMAILID +
                                 ",\nWebpage Id: " + cnfParty.WEBID + ",\nAPNM: " + cnfParty.APNM + ",\nAPNO: " +
                                 cnfParty.APNO + ",\nStatus: " + cnfParty.STATUS + ".");
            aslLog.USERPC = cnfParty.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }



        // SAVE ALL INFORMATION from CNF_COMMISSION TO Asl_lOG Database Table.
        public void Insert_Asl_LogData_ForComission(CNF_COMMISSION cnfCommission)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == cnfCommission.COMPID && n.USERID == cnfCommission.INSUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(cnfCommission.COMPID);
            aslLog.USERID = cnfCommission.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfCommission.INSIPNO;
            aslLog.LOGLTUDE = cnfCommission.INSLTUDE;
            aslLog.TABLEID = "CNF_COMMISSION";
            aslLog.LOGDATA =
                Convert.ToString("sl: " + cnfCommission.COMMSL + ",\nExctp: " + cnfCommission.EXCTP + ",\nValue For: " +
                                 cnfCommission.VALUEFR + ",\nValue To: " + cnfCommission.VALUETO + ",\nValue Type: " +
                                 cnfCommission.VALUETP + ",\nCommant: " + cnfCommission.COMMAMT + ".");
            aslLog.USERPC = cnfCommission.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }




        public void Insert_Asl_LogData_GLACCHART(GL_ACCHART aGlAcchart)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == aGlAcchart.COMPID && n.USERID == aGlAcchart.INSUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(aGlAcchart.COMPID);
            aslLog.USERID = aGlAcchart.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aGlAcchart.INSIPNO;
            aslLog.LOGLTUDE = aGlAcchart.INSLTUDE;
            aslLog.TABLEID = "GL_ACCHART";
            string headName = "";
            var headnamefind =
                (from n in db.GlAccharmstDbSet
                 where n.COMPID == aGlAcchart.COMPID && n.HEADCD == aGlAcchart.HEADCD
                 select n).ToList();
            foreach (var name in headnamefind)
            {
                headName = name.HEADNM;
            }
            aslLog.LOGDATA =
                Convert.ToString("CNF_PARTY to  GL_ACCHART" + ",\nHead Type: " + aGlAcchart.HEADTP + ",\nHead Name: " +
                                 headName + ",\nAccount Name: " +
                                 aGlAcchart.ACCOUNTNM + ".");
            aslLog.USERPC = aGlAcchart.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }




        // Edit ALL INFORMATION from CNF_PARTY TO Asl_lOG Database Table.
        public void Update_Asl_LogData(CNF_PARTY cnfParty)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == cnfParty.COMPID && n.USERID == cnfParty.UPDUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(cnfParty.COMPID);
            aslLog.USERID = cnfParty.UPDUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfParty.UPDIPNO;
            aslLog.LOGLTUDE = cnfParty.UPDLTUDE;
            aslLog.TABLEID = "CNF_PARTY";
            aslLog.LOGDATA =
                Convert.ToString("User Name: " + cnfParty.PARTYNM + ",\nAddress: " + cnfParty.ADDRESS +
                                 ",\nContact No: " + cnfParty.CONTACTNO + ",\nEmail ID: " + cnfParty.EMAILID +
                                 ",\nWebpage Id: " + cnfParty.WEBID + ",\nAPNM: " + cnfParty.APNM + ",\nAPNO: " +
                                 cnfParty.APNO + ",\nStatus: " + cnfParty.STATUS + ".");
            aslLog.USERPC = cnfParty.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }




        // Edit ALL INFORMATION from CNF_COMMISSION TO Asl_lOG Database Table.
        public void Update_ASL_LogData_ForComission(ASL_LOG aslLog)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == aslLog.COMPID && n.USERID == aslLog.USERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aslLog.COMPID);
            aslLog.USERID = aslLog.USERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aslLog.LOGIPNO;
            aslLog.LOGLTUDE = aslLog.LOGLTUDE;
            aslLog.TABLEID = "CNF_COMMISSION";
            aslLog.LOGDATA = aslLog.LOGDATA;
            aslLog.USERPC = aslLog.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        // Delete ALL INFORMATION from CNF_PARTY TO Asl_lOG Database Table.
        public void Delete_Asl_LogData(CNF_PARTY cnfParty)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == cnfParty.COMPID && n.USERID == cnfParty.UPDUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(cnfParty.COMPID);
            aslLog.USERID = cnfParty.UPDUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfParty.UPDIPNO;
            aslLog.LOGLTUDE = cnfParty.UPDLTUDE;
            aslLog.TABLEID = "CNF_PARTY";
            aslLog.LOGDATA =
                Convert.ToString("User Name: " + cnfParty.PARTYNM + ",\nAddress: " + cnfParty.ADDRESS +
                                 ",\nContact No: " + cnfParty.CONTACTNO + ",\nEmail ID: " + cnfParty.EMAILID +
                                 ",\nWebpage Id: " + cnfParty.WEBID + ",\nAPNM: " + cnfParty.APNM + ",\nAPNO: " +
                                 cnfParty.APNO + ",\nStatus: " + cnfParty.STATUS + ".");
            aslLog.USERPC = cnfParty.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        // Delete ALL INFORMATION from CNF_COMMISSION TO Asl_lOG Database Table.
        public void Delete_ASL_LOG_CnfCommission(CNF_COMMISSION cnfCommission)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == cnfCommission.COMPID && n.USERID == cnfCommission.UPDUSERID
                     select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(cnfCommission.COMPID);
            aslLog.USERID = cnfCommission.UPDUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfCommission.UPDIPNO;
            aslLog.LOGLTUDE = cnfCommission.UPDLTUDE;
            aslLog.TABLEID = "CNF_COMMISSION";
            aslLog.LOGDATA =
               Convert.ToString("sl: " + cnfCommission.COMMSL + ",\nExctp: " + cnfCommission.EXCTP + ",\nValue For: " +
                                 cnfCommission.VALUEFR + ",\nValue To: " + cnfCommission.VALUETO + ",\nValue Type: " +
                                 cnfCommission.VALUETP + ",\nCommant: " + cnfCommission.COMMAMT + ".");
            aslLog.USERPC = cnfCommission.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        // Create ASL_DELETE object and it used to this Delete_ASL_DELETE (AslUserco aslUsercos).
        public ASL_DELETE AslDelete = new ASL_DELETE();

        // Delete ALL INFORMATION from AslUserCo TO ASL_DELETE Database Table.
        public void Delete_ASL_DELETE(CNF_PARTY cnfParty)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("HH:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslDeleteDbSet
                     where n.COMPID == cnfParty.COMPID && n.USERID == cnfParty.UPDUSERID
                     select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(cnfParty.COMPID);
            AslDelete.USERID = cnfParty.UPDUSERID;
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = cnfParty.UPDIPNO;
            AslDelete.DELLTUDE = cnfParty.UPDLTUDE;
            AslDelete.TABLEID = "CNF_PARTY";
            AslDelete.DELDATA =
                Convert.ToString("User Name: " + cnfParty.PARTYNM + ",\nAddress: " + cnfParty.ADDRESS +
                                 ",\nContact No: " + cnfParty.CONTACTNO + ",\nEmail ID: " + cnfParty.EMAILID +
                                 ",\nWebpage Id: " + cnfParty.WEBID + ",\nAPNM: " + cnfParty.APNM + ",\nAPNO: " +
                                 cnfParty.APNO + ",\nStatus: " + cnfParty.STATUS + ".");
            AslDelete.USERPC = cnfParty.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }


        // Delete ALL INFORMATION from CNF_COMMISSION TO AslDelete Database Table.
        public void Delete_ASL_DELETE_CnfCommission(CNF_COMMISSION cnfCommission)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslDeleteDbSet
                     where n.COMPID == cnfCommission.COMPID && n.USERID == cnfCommission.UPDUSERID
                     select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(cnfCommission.COMPID);
            AslDelete.USERID = cnfCommission.UPDUSERID;
            AslDelete.DELSLNO = aslLog.LOGSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = cnfCommission.UPDIPNO;
            AslDelete.DELLTUDE = cnfCommission.UPDLTUDE;
            AslDelete.TABLEID = "CNF_COMMISSION";
            AslDelete.DELDATA =
               Convert.ToString("sl: " + cnfCommission.COMMSL + ",\nExctp: " + cnfCommission.EXCTP + ",\nValue For: " +
                                 cnfCommission.VALUEFR + ",\nValue To: " + cnfCommission.VALUETO + ",\nValue Type: " +
                                 cnfCommission.VALUETP + ",\nCommant: " + cnfCommission.COMMAMT + ".");
            AslDelete.USERPC = cnfCommission.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }
        // GET: /PartyIndex/
        public ActionResult PartyIndex()
        {
            return View();
        }


        // GET: /PartyCreate/
        public ActionResult PartyCreate()
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
        public ActionResult PartyCreate(CNF_PARTY aCnfParty)
        {
            if (ModelState.IsValid)
            {
                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];


                aCnfParty.USERPC = strHostName;
                aCnfParty.INSIPNO = ipAddress.ToString();
                aCnfParty.INSTIME = Convert.ToDateTime(td);

                Int64 maxData =
                    Convert.ToInt64(
                        (from n in db.CnfPartyDbSet where aCnfParty.COMPID == n.COMPID select n.PARTYID).Max());

                Int64 R = Convert.ToInt64(aCnfParty.COMPID + "103" + "9999");
                if (maxData == 0)
                {
                    aCnfParty.PARTYID = Convert.ToInt64(aCnfParty.COMPID + "103" + "0001");

                    //Check GL_ACCHART AccountCD
                    int getAccountCD_Count =
                        (from m in db.GlAcchartDbSet
                         where m.COMPID == aCnfParty.COMPID && m.ACCOUNTCD == aCnfParty.PARTYID
                         select m).Count();

                    if (getAccountCD_Count != 0)
                    {
                        TempData["PartyCreationMessage"] = "You can not create this party. It already exists! ";
                        return RedirectToAction("PartyCreate");
                    }

                    //Insert User ID save AslUSerco table attribute INSUSERID
                    aCnfParty.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    Insert_Asl_LogData(aCnfParty);
                    db.CnfPartyDbSet.Add(aCnfParty);
                    db.SaveChanges();

                    TempData["PartyCreationMessage"] = "User Name: '" + aCnfParty.PARTYNM +
                                                       "' successfully registered! ";
                    //GL_ACCHART database list add.
                    GL_ACCHART gl_Acchart = new GL_ACCHART();
                    gl_Acchart.COMPID = Convert.ToInt64(aCnfParty.COMPID);
                    gl_Acchart.HEADTP = 1;
                    gl_Acchart.HEADCD = Convert.ToInt64(aCnfParty.COMPID + "103");
                    gl_Acchart.ACCOUNTCD = aCnfParty.PARTYID;
                    gl_Acchart.ACCOUNTNM = aCnfParty.PARTYNM;
                    gl_Acchart.REMARKS = null;
                    gl_Acchart.USERPC = strHostName;
                    gl_Acchart.INSUSERID = Convert.ToInt64(aCnfParty.INSUSERID);
                    gl_Acchart.INSIPNO = ipAddress.ToString();
                    gl_Acchart.INSTIME = Convert.ToDateTime(td);
                    gl_Acchart.INSLTUDE = aCnfParty.INSLTUDE;
                    Insert_Asl_LogData_GLACCHART(gl_Acchart);
                    db.GlAcchartDbSet.Add(gl_Acchart);
                    db.SaveChanges();

                    return RedirectToAction("PartyCreate");


                }
                else if (maxData <= R)
                {
                    aCnfParty.PARTYID = maxData + 1;

                    //Check GL_ACCHART AccountCD
                    int getAccountCD_Count =
                        (from m in db.GlAcchartDbSet
                         where m.COMPID == aCnfParty.COMPID && m.ACCOUNTCD == aCnfParty.PARTYID
                         select m).Count();

                    if (getAccountCD_Count != 0)
                    {
                        TempData["PartyCreationMessage"] = "You can not create this party. It already exists in Account Head Creation form! ";
                        return RedirectToAction("PartyCreate");
                    }


                    //Insert User ID save AslUSerco table attribute INSUSERID
                    aCnfParty.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    Insert_Asl_LogData(aCnfParty);
                    db.CnfPartyDbSet.Add(aCnfParty);
                    db.SaveChanges();

                    TempData["PartyCreationMessage"] = "User Name: '" + aCnfParty.PARTYNM +
                                                       "' successfully registered! ";

                    //GL_ACCHART database list add.
                    GL_ACCHART gl_Acchart = new GL_ACCHART();
                    gl_Acchart.COMPID = Convert.ToInt64(aCnfParty.COMPID);
                    gl_Acchart.HEADTP = 1;
                    gl_Acchart.HEADCD = Convert.ToInt64(aCnfParty.COMPID + "103");
                    gl_Acchart.ACCOUNTCD = aCnfParty.PARTYID;
                    gl_Acchart.ACCOUNTNM = aCnfParty.PARTYNM;
                    gl_Acchart.REMARKS = null;
                    gl_Acchart.USERPC = strHostName;
                    gl_Acchart.INSUSERID = Convert.ToInt64(aCnfParty.INSUSERID);
                    gl_Acchart.INSIPNO = ipAddress.ToString();
                    gl_Acchart.INSTIME = Convert.ToDateTime(td);
                    gl_Acchart.INSLTUDE = aCnfParty.INSLTUDE;
                    Insert_Asl_LogData_GLACCHART(gl_Acchart);
                    db.GlAcchartDbSet.Add(gl_Acchart);
                    db.SaveChanges();

                    return RedirectToAction("PartyCreate");
                }
                else
                {
                    TempData["PartyCreationMessage"] = "Not possible entry! ";
                    return RedirectToAction("PartyCreate");
                }

            }
            return View();
        }


        // GET: /PartyUpdate/
        public ActionResult PartyUpdate()
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
        public ActionResult PartyUpdate(CNF_PARTY aCnfParty)
        {
            if (aCnfParty.PARTYNM != null && aCnfParty.CONTACTNO != null &&
                 aCnfParty.STATUS != null)
            {
                db.Entry(aCnfParty).State = EntityState.Modified;

                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                aCnfParty.USERPC = strHostName;
                aCnfParty.UPDIPNO = ipAddress.ToString();
                aCnfParty.UPDTIME = Convert.ToDateTime(td);
                //asluserco.UPDTIME = DateTime.Parse(td.ToString(), dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                //Update User ID save AslUSerco table attribute INSUSERID
                aCnfParty.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                //GL_ACCHART AccountCD find
                var findAccountCD = from m in db.GlAcchartDbSet
                                    where m.COMPID == aCnfParty.COMPID && m.ACCOUNTCD == aCnfParty.PARTYID
                                    select m;
                foreach (var glAcchart in findAccountCD)
                {
                    db.Entry(glAcchart).State = EntityState.Modified;
                    glAcchart.ACCOUNTCD = aCnfParty.PARTYID;
                    glAcchart.ACCOUNTNM = aCnfParty.PARTYNM;
                    glAcchart.USERPC = strHostName;
                    glAcchart.UPDUSERID = aCnfParty.UPDUSERID;
                    glAcchart.UPDLTUDE = aCnfParty.UPDLTUDE;
                    glAcchart.UPDIPNO = ipAddress.ToString();
                    glAcchart.UPDTIME = Convert.ToDateTime(td);
                }
                db.SaveChanges();

                Update_Asl_LogData(aCnfParty);
                db.SaveChanges();
                TempData["PartyUpdateMessage"] = "'" + aCnfParty.PARTYNM + "' successfully updated!";
                return RedirectToAction("PartyUpdate");
            }
            else
            {
                return View("PartyUpdate");
            }

        }

        // GET: /PartyDelete/
        public ActionResult PartyDelete()
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
        public ActionResult PartyDelete(CNF_PARTY aCnfParty)
        {
            if (aCnfParty.PARTYNM != null  &&
                aCnfParty.CONTACTNO != null && aCnfParty.STATUS != null)
            {
                CNF_PARTY acCnfPartyDelete = db.CnfPartyDbSet.Find(aCnfParty.CNF_PARTYId);

                //Get Ip ADDRESS,Time & user PC Name 
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                acCnfPartyDelete.USERPC = strHostName;
                acCnfPartyDelete.UPDIPNO = ipAddress.ToString();
                acCnfPartyDelete.UPDTIME = Convert.ToDateTime(td);

                //Delete User ID save AslUSerco table attribute INSUSERID
                acCnfPartyDelete.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                //Get current LOGLTUDE data 
                acCnfPartyDelete.UPDLTUDE = aCnfParty.UPDLTUDE;

                Int64 cnfParty_findPartyID =
                    (from m in db.CnfJobDbSet
                     where m.COMPID == acCnfPartyDelete.COMPID && m.PARTYID == acCnfPartyDelete.PARTYID
                     select m).Count();

                if (cnfParty_findPartyID != 0)
                {
                    TempData["PartyDeleteMessage"] = "You can't delete this party name, because it already used in job information!";
                }
                else
                {
                    Delete_Asl_LogData(acCnfPartyDelete);
                    Delete_ASL_DELETE(acCnfPartyDelete);

                    db.CnfPartyDbSet.Remove(acCnfPartyDelete);
                    db.SaveChanges();

                    TempData["PartyDeleteMessage"] = "'" + acCnfPartyDelete.PARTYNM + "' successfully Deleted!";
                }

                return RedirectToAction("PartyDelete");
            }
            else
            {
                return View("PartyDelete");
            }
        }


        //AutoComplete 
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetPartyInformation(Int64 changedDropDown)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            String partyName = "",
                address = "",
                contactNo = "",
                emailID = "",
                  contactNo2 = "",
                emailID2 = "",
                webID = "",
                ApNm = "",
                ApNo = "",
                remarks="",
                status = "",
                inserttime = "",
                insertIpno = "",
                insltude = "",
                userpc = "";
            Int64 CNFpartyId = 0, companyID = 0, insertUserId = 0;
            var rt = db.CnfPartyDbSet.Where(n => n.PARTYID == changedDropDown &&
                                                 n.COMPID == compid).Select(n => new
                                                 {
                                                     CNFpartyId = n.CNF_PARTYId,
                                                     companyID = n.COMPID,
                                                     partyName = n.PARTYNM,
                                                     address = n.ADDRESS,
                                                     contactNo = n.CONTACTNO,
                                                     emailID = n.EMAILID,
                                                     contactNo2 = n.CONTACTNO2,
                                                     emailID2 = n.EMAILID2,
                                                     webID = n.WEBID,
                                                     ApNm = n.APNM,
                                                     ApNo = n.APNO,
                                                     remarks=n.REMARKS,
                                                     status = n.STATUS,
                                                     insuserid = n.INSUSERID,
                                                     instime = n.INSTIME,
                                                     insipno = n.INSIPNO,
                                                     insltude = n.INSLTUDE

                                                 });
            foreach (var n in rt)
            {
                CNFpartyId = n.CNFpartyId;
                companyID = Convert.ToInt64(n.companyID);
                partyName = Convert.ToString(n.partyName);
                address = Convert.ToString(n.address);
                contactNo = Convert.ToString(n.contactNo);
                emailID = Convert.ToString(n.emailID);
                contactNo2 = Convert.ToString(n.contactNo2);
                emailID2 = Convert.ToString(n.emailID2);
                webID = Convert.ToString(n.webID);
                ApNm = Convert.ToString(n.ApNm);
                ApNo = Convert.ToString(n.ApNo);
                remarks = n.remarks;
                status = Convert.ToString(n.status);
                insertUserId = Convert.ToInt64(n.insuserid);
                inserttime = Convert.ToString(n.instime);
                insertIpno = Convert.ToString(n.insipno);
                insltude = Convert.ToString(n.insltude);
            }

            var result = new
            {
                CNF_PARTYId = CNFpartyId,
                COMPID = companyID,
                PARTYNM = partyName,
                ADDRESS = address,
                CONTACTNO = contactNo,
                EMAILID = emailID,
                CONTACTNO2 = contactNo2,
                EMAILID2 = emailID2,
                WEBID = webID,
                APNM = ApNm,
                APNO = ApNo,
                REMARKS=remarks,
                STATUS = status,
                INSUSERID = insertUserId,
                INSTIME = inserttime,
                INSIPNO = insertIpno,
                INSLTUDE = insltude
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public ActionResult PartyCommission()
        {
            var dt = (PageModel)TempData["data"];
            return View(dt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartyCommission(PageModel model, String command)
        {
            model.ACnfCommission.PARTYID = model.AGL_acchart.ACCOUNTCD;
            if (command == "Search")
            {
                TempData["partyId"] = model.AGL_acchart.ACCOUNTCD;
                TempData["data"] = model;
                TempData["Latitude_PassingDeleteCommission"] = model.ACnfCommission.INSLTUDE;
                model.ACnfCommission.EXCTP = "";
                model.ACnfCommission.VALUEFR = 0;
                model.ACnfCommission.VALUETO = 0;
                model.ACnfCommission.VALUETP = "";
                model.ACnfCommission.COMMAMT = 0;
                return RedirectToAction("PartyCommission");
            }
            else if (command == "Add")
            {
                if (model.ACnfCommission.EXCTP != null && model.ACnfCommission.VALUETP != null && (model.ACnfCommission.COMMAMT == null || model.ACnfCommission.VALUEFR == null || model.ACnfCommission.VALUETO == null))
                {
                    ViewBag.NullFieldMessage = "Value From,To & Commamt field must be Valid?";
                    TempData["partyId"] = model.AGL_acchart.ACCOUNTCD;
                    TempData["data"] = model;
                    return View("PartyCommission");
                }
                else if (model.ACnfCommission.EXCTP == null || model.ACnfCommission.VALUETP == null || model.ACnfCommission.COMMAMT == null || model.ACnfCommission.VALUEFR == null || model.ACnfCommission.VALUETO == null)
                {
                    ViewBag.NullFieldMessage = "All field must be required?";
                    TempData["partyId"] = model.AGL_acchart.ACCOUNTCD;
                    TempData["data"] = model;
                    return View("PartyCommission");
                }
                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                model.ACnfCommission.USERPC = strHostName;
                model.ACnfCommission.INSIPNO = ipAddress.ToString();
                model.ACnfCommission.INSTIME = Convert.ToDateTime(td);
                model.ACnfCommission.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                Int64 maxData = Convert.ToInt64((from n in db.CnfCommissionDbSet
                                                 where n.COMPID == model.ACnfCommission.COMPID && n.PARTYID == model.AGL_acchart.ACCOUNTCD
                                                 select n.COMMSL).Max());
                Int64 R = 99999999;

                if (maxData == 0)
                {
                    model.ACnfCommission.COMMSL = 1;
                    Insert_Asl_LogData_ForComission(model.ACnfCommission);

                    db.CnfCommissionDbSet.Add(model.ACnfCommission);
                    db.SaveChanges();
                    TempData["Partycommmessage"] = "Party Commission Successfully Saved";
                    model.ACnfCommission.EXCTP = "";
                    model.ACnfCommission.VALUEFR = 0;
                    model.ACnfCommission.VALUETO = 0;
                    model.ACnfCommission.VALUETP = "";
                    model.ACnfCommission.COMMAMT = 0;


                }
                else if (maxData <= R)
                {
                    model.ACnfCommission.COMMSL = maxData + 1;
                    Insert_Asl_LogData_ForComission(model.ACnfCommission);

                    db.CnfCommissionDbSet.Add(model.ACnfCommission);
                    db.SaveChanges();
                    TempData["message"] = "Party Commission Successfully Saved";
                    model.ACnfCommission.EXCTP = "";
                    model.ACnfCommission.VALUEFR = 0;
                    model.ACnfCommission.VALUETO = 0;
                    model.ACnfCommission.VALUETP = "";
                    model.ACnfCommission.COMMAMT = 0;
                }
                else
                {
                    TempData["Partycommmessage"] = "Party Commission entry not possible";
                    TempData["ShowUpdateButton"] = "Show Update Button";
                }

                TempData["partyId"] = model.AGL_acchart.ACCOUNTCD;
                TempData["data"] = model;
                return RedirectToAction("PartyCommission");
            }
            else if (command == "Update")
            {
                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                var query =
                    from a in db.CnfCommissionDbSet
                    where
                        (a.PARTYID == model.AGL_acchart.ACCOUNTCD && a.COMPID == model.ACnfCommission.COMPID &&
                         a.COMMSL == model.ACnfCommission.COMMSL)
                    select a;
                // aCategoryItemModel.RmsItemModel.COMPID = aCategoryItemModel.PosItemmstModel.COMPID;
                //  aCategoryItemModel.RmsItemModel.CATID = aCategoryItemModel.PosItemmstModel.CATID;


                foreach (CNF_COMMISSION a in query)
                {
                    // Insert any additional changes to column values.
                    a.EXCTP = model.ACnfCommission.EXCTP;
                    a.VALUEFR = model.ACnfCommission.VALUEFR;
                    a.VALUETO = model.ACnfCommission.VALUETO;
                    a.VALUETP = model.ACnfCommission.VALUETP;
                    a.COMMAMT = model.ACnfCommission.COMMAMT;
                    a.USERPC = strHostName;
                    a.UPDIPNO = ipAddress.ToString();
                    a.UPDTIME = td;
                    a.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    a.UPDLTUDE = model.ACnfCommission.INSLTUDE;
                    TempData["UpdateCommisionLogData"] =
                        Convert.ToString("sl:" + model.ACnfCommission.COMMSL + ",\nExctp: " + model.ACnfCommission.EXCTP +
                                         ",\nValue For: " + model.ACnfCommission.VALUEFR + ",\nValue To: " +
                                         model.ACnfCommission.VALUETO + ",\nValue Type: " + model.ACnfCommission.VALUETP +
                                         ",\nCommant: " + model.ACnfCommission.COMMAMT + ".");

                }

                ASL_LOG aslLogUpdate = new ASL_LOG();

                aslLogUpdate.COMPID = model.ACnfCommission.COMPID;
                aslLogUpdate.LOGIPNO = ipAddress.ToString();
                aslLogUpdate.LOGLTUDE = model.ACnfCommission.INSLTUDE;
                aslLogUpdate.USERPC = strHostName;
                //Update User ID save ASL_ROLE table attribute UPDUSERID
                aslLogUpdate.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                aslLogUpdate.LOGDATA = TempData["UpdateCommisionLogData"].ToString();
                Update_ASL_LogData_ForComission(aslLogUpdate);

                db.SaveChanges();

                TempData["data"] = model;
                TempData["partyId"] = model.AGL_acchart.ACCOUNTCD;
                TempData["ShowUpdateButton"] = null;
                model.ACnfCommission.EXCTP = "";
                model.ACnfCommission.VALUEFR = 0;
                model.ACnfCommission.VALUETO = 0;
                model.ACnfCommission.VALUETP = "";
                model.ACnfCommission.COMMAMT = 0;
                return RedirectToAction("PartyCommission");

            }
            else
            {
                return RedirectToAction("PartyCommission");
            }
        }




        public ActionResult CommissionUpdate(Int64 id, Int64 cid, Int64 partyID, Int64 Serial)
        {
            //.........................................................Create Permission Check
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

            var updateStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
            string query1 = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='Party' and ACTIONNAME='PartyCommission' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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

            PageModel model = new PageModel();
            model.ACnfCommission.COMPID = cid;
            model.AGL_acchart.ACCOUNTCD = partyID;

            var findHeadType = (from n in db.GlAcchartDbSet where n.COMPID == cid && n.ACCOUNTCD == partyID select n.HEADTP).Single();
            if (findHeadType == 1)
            {
                model.HeadType = "ASSET"; //1:ASSET, 2:LIABILITY, 3:INCOME, 4:EXPENDITURE
            }
            else if (findHeadType == 2)
            {
                model.HeadType = "LIABILITY";
            }
            else if (findHeadType == 3)
            {
                model.HeadType = "INCOME";
            }
            else if (findHeadType == 4)
            {
                model.HeadType = "EXPENDITURE";
            }


            if (updateStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("PartyCommission");
            }
            //...............................................................................................

            model.ACnfCommission = db.CnfCommissionDbSet.Find(id);

            var item = from r in db.CnfCommissionDbSet where r.Cnf_ComissionID == id select r;
            foreach (var it in item)
            {
                model.ACnfCommission = it;
            }
            TempData["data"] = model;
            TempData["partyId"] = partyID;
            TempData["ShowUpdateButton"] = "Show update Button";
            return RedirectToAction("PartyCommission");

        }


        public ActionResult CommissionDelete(Int64 id, Int64 cid, Int64 partyID, Int64 Serial)
        {
            //.........................................................Create Permission Check
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

            var deleteStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
            string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='Party' and ACTIONNAME='PartyCommission' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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

            PageModel model = new PageModel();
            model.ACnfCommission.COMPID = cid;
            model.AGL_acchart.ACCOUNTCD = partyID;
            ////add the data from database to model
            //var categoryName_Remarks = from m in db.PosItemMstDbSet where m.CATID == catid && m.COMPID == cid select m;
            //foreach (var categoryResult in categoryName_Remarks)
            //{
            //    model.PosItemmstModel.COMPID = cid;
            //    model.PosItemmstModel.CATID = catid;
            //    model.PosItemmstModel.CATNM = categoryResult.CATNM;
            //    model.PosItemmstModel.REMARKS = categoryResult.REMARKS;
            //}
            //TempData["category"] = model;
            //TempData["categoryId"] = catid;
            //TempData["ShowAddButton"] = "Show Add Button";
            if (deleteStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("PartyCommission");

            }
            //...............................................................................................

            //RMS_ITEM rmsitem = db.RmsItemDbSet.Find(id);
            CNF_COMMISSION aCnfCommission = db.CnfCommissionDbSet.Find(id);
            //Get Ip ADDRESS,Time & user PC Name
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            aCnfCommission.USERPC = strHostName;
            aCnfCommission.UPDIPNO = ipAddress.ToString();
            aCnfCommission.UPDTIME = Convert.ToDateTime(td);
            //Delete User ID save CNF_COMMISSION table attribute (UPDUSERID) field
            aCnfCommission.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            if (TempData["Latitude_PassingDeleteCommission"] != null)
            {
                //Get current LOGLTUDE data 
                aCnfCommission.UPDLTUDE = TempData["Latitude_PassingDeleteCommission"].ToString();
            }

            Delete_ASL_LOG_CnfCommission(aCnfCommission);
            Delete_ASL_DELETE_CnfCommission(aCnfCommission);
            db.CnfCommissionDbSet.Remove(aCnfCommission);
            db.SaveChanges();
            TempData["data"] = model;
            TempData["partyId"] = partyID;
            TempData["ShowUpdateButton"] = null;
            return RedirectToAction("PartyCommission");
        }




        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetHeadType(Int64 changedDropDown)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());

            int HeadType = 0;
            string Head = "";
            var rt = db.GlAcchartDbSet.Where(n => n.COMPID == compid && n.ACCOUNTCD == changedDropDown).Select(n => new
            {
                headType = n.HEADTP,
            });

            foreach (var n in rt)
            {
                HeadType = Convert.ToInt16(n.headType);
            }

            if (HeadType == 1)
            {
                Head = "ASSET";
            }
            else if (HeadType == 2)
            {
                Head = "LIABILITY";
            }
            else if (HeadType == 3)
            {
                Head = "INCOME";
            }
            else if (HeadType == 4)
            {
                Head = "EXPENDITURE";
            }

            var result = new { HEADTP = Head };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public JsonResult Check_ContactNo(string contactno)
        {
            var result = db.CnfPartyDbSet.Count(d => d.CONTACTNO == contactno) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Check_EmailId(string emailId)
        {
            var result1 = db.CnfPartyDbSet.Count(a => a.EMAILID == emailId) == 0;
            return Json(result1, JsonRequestBehavior.AllowGet);
        }
    }
}
