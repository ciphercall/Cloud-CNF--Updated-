using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc_CNFApp.Models;

namespace Mvc_CNFApp.Controllers
{
    public class ExpenseInformationController : AppController
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

        public ExpenseInformationController()
        {
            ViewData["HighLight_Menu_CnfForm"] = "heighlight";
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }




      //create asl_log object
        public ASL_LOG aslLog = new ASL_LOG();

        // SAVE ALL INFORMATION from CNF_ExpenseHeadModel TO Asl_lOG Database Table.
        public void Insert_CNF_ExpMst_LogData(CNF_ExpenseHeadModel aExpenseHeadModel)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aExpenseHeadModel.CNF_EXPMSTModel.COMPID && n.USERID == aExpenseHeadModel.CNF_EXPMSTModel.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(aExpenseHeadModel.CNF_EXPMSTModel.COMPID);
            aslLog.USERID = aExpenseHeadModel.CNF_EXPMSTModel.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aExpenseHeadModel.CNF_EXPMSTModel.INSIPNO;
            aslLog.LOGLTUDE = aExpenseHeadModel.CNF_EXPMSTModel.INSLTUDE;
            aslLog.TABLEID = "CNF_EXPMST";
            aslLog.LOGDATA = Convert.ToString("\nEXP CName: " + aExpenseHeadModel.CNF_EXPMSTModel.EXPCNM +  ".");
            aslLog.USERPC = aExpenseHeadModel.CNF_EXPMSTModel.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        // Edit ALL INFORMATION from CNF_EXPMST TO Asl_lOG Database Table.
        public void Update_CNF_EXPMST_LogData(CNF_EXPMST posExpmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == posExpmst.COMPID && n.USERID == posExpmst.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(posExpmst.COMPID);
            aslLog.USERID = posExpmst.UPDUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = posExpmst.UPDIPNO;
            aslLog.LOGLTUDE = posExpmst.UPDLTUDE;
            aslLog.TABLEID = "CNF_EXPMST";
            aslLog.LOGDATA = Convert.ToString( "\nEXP CName: " + posExpmst.EXPCNM + ".");
            aslLog.USERPC = posExpmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        // Delete ALL INFORMATION from CNF_EXPMST TO Asl_lOG Database Table.
        public void Delete_CNF_EXPMST_LogData(CNF_EXPMST posExpmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == posExpmst.COMPID && n.USERID == posExpmst.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(posExpmst.COMPID);
            aslLog.USERID = posExpmst.UPDUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = posExpmst.UPDIPNO;
            aslLog.LOGLTUDE = posExpmst.UPDLTUDE;
            aslLog.TABLEID = "CNF_EXPMST";
            aslLog.LOGDATA = Convert.ToString("\nEXP CName: " + posExpmst.EXPCNM + ".");
            aslLog.USERPC = posExpmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }



        //  SAVE ALL INFORMATION from CNF_EXPENSE TO Asl_lOG Database Table.
        public void Insert_CNF_Expense_LogData(CNF_EXPENSE cnfExpense)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == cnfExpense.COMPID && n.USERID == cnfExpense.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(cnfExpense.COMPID);
            aslLog.USERID = cnfExpense.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfExpense.INSIPNO;
            aslLog.LOGLTUDE = cnfExpense.INSLTUDE;
            aslLog.TABLEID = "CNF_EXPENSE";
            string expenseHead = "";
            var headnamefind =
                (from n in db.CnfExpmstDbSet
                 where n.COMPID == cnfExpense.COMPID && n.EXPCID == cnfExpense.EXPCID
                 select n).ToList();
            foreach (var name in headnamefind)
            {
                expenseHead = name.EXPCNM;
            }
            aslLog.LOGDATA = Convert.ToString("EXP HeadName: " + expenseHead + ",\nEXP Name: " + cnfExpense.EXPNM + ".");
            aslLog.USERPC = cnfExpense.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        // Edit ALL INFORMATION from CNF_EXPENSE TO Asl_lOG Database Table.
        public void Update_CNF_EXPENSE_LogData(ASL_LOG aslLOGRef)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aslLOGRef.COMPID && n.USERID == aslLOGRef.USERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(aslLOGRef.COMPID);
            aslLog.USERID = aslLOGRef.USERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aslLOGRef.LOGIPNO;
            aslLog.LOGLTUDE = aslLOGRef.LOGLTUDE;
            aslLog.TABLEID = "CNF_EXPENSE";
            aslLog.LOGDATA = aslLOGRef.LOGDATA;
            aslLog.USERPC = strHostName;
            db.AslLogDbSet.Add(aslLog);
        }


        // Delete ALL INFORMATION from CNF_EXPENSE TO Asl_lOG Database Table.
        public void Delete_CNF_EXPENSE_LogData(CNF_EXPENSE cnfExpense)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == cnfExpense.COMPID && n.USERID == cnfExpense.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(cnfExpense.COMPID);
            aslLog.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = cnfExpense.UPDIPNO;
            aslLog.LOGLTUDE = cnfExpense.UPDLTUDE;
            aslLog.TABLEID = "CNF_EXPENSE";
            string expenseHead = "";
            var headnamefind =
                (from n in db.CnfExpmstDbSet
                 where n.COMPID == cnfExpense.COMPID && n.EXPCID == cnfExpense.EXPCID
                 select n).ToList();
            foreach (var name in headnamefind)
            {
                expenseHead = name.EXPCNM;
            }
            aslLog.LOGDATA = Convert.ToString("EXP HeadName: " + expenseHead + ",\nEXP Name: " + cnfExpense.EXPNM + ".");
            aslLog.USERPC = cnfExpense.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }




        // Create ASL_DELETE object and it used to this Delete_ASL_DELETE (AslUserco aslUsercos).
        public ASL_DELETE AslDelete = new ASL_DELETE();

        // Delete ALL INFORMATION from CNF_EXPMST TO ASL_DELETE Database Table.
        public void Delete_ASL_DELETE_POS_HEADMST(CNF_EXPMST posExpmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == posExpmst.COMPID && n.USERID == posExpmst.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(posExpmst.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = posExpmst.UPDIPNO;
            AslDelete.DELLTUDE = posExpmst.UPDLTUDE;
            AslDelete.TABLEID = "CNF_EXPMST";
            AslDelete.DELDATA = Convert.ToString("\nEXP CName: " + posExpmst.EXPCNM + ".");
            AslDelete.USERPC = posExpmst.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }


        // Delete ALL INFORMATION from CNF_EXPENSE TO ASL_DELETE Database Table.
        public void Delete_ASL_DELETE_CNF_EXPENSE(CNF_EXPENSE cnfExpense)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == cnfExpense.COMPID && n.USERID == cnfExpense.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(cnfExpense.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = cnfExpense.UPDIPNO;
            AslDelete.DELLTUDE = cnfExpense.UPDLTUDE;
            AslDelete.TABLEID = "CNF_EXPENSE";
            string expenseHead = "";
            var headnamefind =
                (from n in db.CnfExpmstDbSet
                 where n.COMPID == cnfExpense.COMPID && n.EXPCID == cnfExpense.EXPCID
                 select n).ToList();
            foreach (var name in headnamefind)
            {
                expenseHead = name.EXPCNM;
            }
            AslDelete.DELDATA = Convert.ToString("EXP Head: " + expenseHead +  ",\nEXP Name: " + cnfExpense.EXPNM + ".");
            AslDelete.USERPC = cnfExpense.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }





        // GET: /Expense Information Head/
        [AcceptVerbs("GET")]
        [ActionName("Index")]
        public ActionResult Index()
        {
            var dt = (CNF_ExpenseHeadModel)TempData["cnfExpenseHead"];
            return View(dt);
        }



        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(CNF_ExpenseHeadModel aCnfExpenseHeadModel, string command)
        {
            if (command == "Add")
            {
                //.........................................................Create Permission Check
                var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
                var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

                var createStatus = "";

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
                string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='ExpenseInformation' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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
                    TempData["cnfExpenseHead"] = aCnfExpenseHeadModel;
                    TempData["ExpCID"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;
                  
                    TempData["message"] = "Permission not granted!";
                    return RedirectToAction("Index");
                }
                //...............................................................................................
               
                aCnfExpenseHeadModel.CNF_EXPENSEModel.COMPID = aCnfExpenseHeadModel.CNF_EXPMSTModel.COMPID;
                aCnfExpenseHeadModel.CNF_EXPENSEModel.EXPCID = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;

                if (aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID == null)
                {
                    TempData["message"] = "Enter CNF_EXPCID First";
                    return View("Index");
                }
                aCnfExpenseHeadModel.CNF_EXPENSEModel.USERPC = strHostName;
                aCnfExpenseHeadModel.CNF_EXPENSEModel.INSIPNO = ipAddress.ToString();
                aCnfExpenseHeadModel.CNF_EXPENSEModel.INSTIME = td;
                aCnfExpenseHeadModel.CNF_EXPENSEModel.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                aCnfExpenseHeadModel.CNF_EXPENSEModel.INSLTUDE = aCnfExpenseHeadModel.CNF_EXPMSTModel.INSLTUDE;


                try
                {
                    

                        CNF_EXPMST mstCnfExpmst_CompID = db.CnfExpmstDbSet.FirstOrDefault(r => (r.COMPID == aCnfExpenseHeadModel.CNF_EXPMSTModel.COMPID));
                        Int64 ExpCID = Convert.ToInt64(aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID);
                        CNF_EXPMST mstCnfExpmst_ExpCID = db.CnfExpmstDbSet.FirstOrDefault(r => (r.EXPCID == ExpCID));

                        if (mstCnfExpmst_CompID == null && mstCnfExpmst_ExpCID == null)
                        {
                            TempData["ShowAddButton"] = "Show Add Button";
                            TempData["message"] = "EXPCID not found ";
                            return View("Index");
                        }
                        else
                        {
                            Int64 maxData = Convert.ToInt64((from n in db.CnfExpenseDbSet where n.COMPID == aCnfExpenseHeadModel.CNF_EXPMSTModel.COMPID && n.EXPCID == aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID select n.EXPID).Max());

                            Int64 R = Convert.ToInt64(ExpCID + "99");

                            if (maxData == 0)
                            {
                                aCnfExpenseHeadModel.CNF_EXPENSEModel.EXPID = Convert.ToInt64(ExpCID + "01");
                               
                                Insert_CNF_Expense_LogData(aCnfExpenseHeadModel.CNF_EXPENSEModel);

                                db.CnfExpenseDbSet.Add(aCnfExpenseHeadModel.CNF_EXPENSEModel);
                                if (db.SaveChanges() > 0)
                                {
                                    TempData["message"] = "Expense List Successfully Saved";
                                    aCnfExpenseHeadModel.CNF_EXPENSEModel.EXPNM = "";
                                    
                                  



                                }

                            }
                            else if (maxData <= R)
                            {

                                aCnfExpenseHeadModel.CNF_EXPENSEModel.EXPID = maxData + 1;
                             
                                Insert_CNF_Expense_LogData(aCnfExpenseHeadModel.CNF_EXPENSEModel);

                                db.CnfExpenseDbSet.Add(aCnfExpenseHeadModel.CNF_EXPENSEModel);
                                db.SaveChanges();
                                TempData["message"] = "Account Successfully Saved";
                                aCnfExpenseHeadModel.CNF_EXPENSEModel.EXPNM = "";
                        
                                


                            }
                            else
                            {
                                TempData["message"] = "Account entry not possible";
                                TempData["ShowAddButton"] = "Show Add Button";

                            }
                        }

                    
                }
                catch (Exception ex)
                {

                }
                TempData["ShowAddButton"] = "Show Add Button";
                TempData["cnfExpenseHead"] = aCnfExpenseHeadModel;
                TempData["ExpCID"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;
              
                return RedirectToAction("Index");
            }


            if (command == "Submit")
            {
                    if (aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCNM != null)
                    {
                       
                        //Get Ip ADDRESS,Time & user PC Name
                        string strHostName = System.Net.Dns.GetHostName();
                        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                        IPAddress ipAddress = ipHostInfo.AddressList[0];


                        aCnfExpenseHeadModel.CNF_EXPMSTModel.USERPC = strHostName;
                        aCnfExpenseHeadModel.CNF_EXPMSTModel.INSIPNO = ipAddress.ToString();
                        aCnfExpenseHeadModel.CNF_EXPMSTModel.INSTIME = Convert.ToDateTime(td);
                        //Insert User ID save POS_ITEMMST table attribute (INSUSERID) field
                        aCnfExpenseHeadModel.CNF_EXPMSTModel.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                        Int64 companyID = Convert.ToInt64(aCnfExpenseHeadModel.CNF_EXPMSTModel.COMPID);


                        Int64 minCategoryId = Convert.ToInt64((from n in db.CnfExpmstDbSet where n.COMPID == companyID select n.EXPCID).Min());
                       
                        //if (aAccountHeadModel.PosItemmstModel.CATID == null)
                        //{
                        //    aAccountHeadModel.PosItemmstModel.CATID = minCategoryId;
                        //}


                        var result = db.CnfExpmstDbSet.Count(d => d.EXPCNM == aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCNM
                                                                  && d.COMPID == companyID);
                        
                       
                        if (result == 0)
                        {
                          
                            
                                    
                            
                            //.........................................................Create Permission Check
                            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
                            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

                            var createStatus = "";

                            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
                            string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='ExpenseInformation' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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
                                TempData["cnfExpenseHead"] = aCnfExpenseHeadModel;
                                TempData["ExpCID"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;
                              
                                TempData["ShowAddButton"] = "Show Add Button";
                                TempData["message"] = "Permission not granted!";
                                return RedirectToAction("Index");
                            }
                            //...............................................................................................


                            AslUserco aslUserco = db.AslUsercoDbSet.FirstOrDefault(r => (r.COMPID == companyID));
                            if (aslUserco == null)
                            {
                                TempData["message"] = " User ID not found ";
                                TempData["ShowAddButton"] = "Show Add Button";
                            }
                            else
                            {
                                Int64 maxData = Convert.ToInt64((from n in db.CnfExpmstDbSet where n.COMPID == aCnfExpenseHeadModel.CNF_EXPMSTModel.COMPID select n.EXPCID).Max());
                                


                               
                                Int64 R = Convert.ToInt64(aCnfExpenseHeadModel.CNF_EXPMSTModel.COMPID + "99");
                                Int64 comId = Convert.ToInt64(aCnfExpenseHeadModel.CNF_EXPMSTModel.COMPID);
                              
                                if (maxData == 0)
                                {


                                    aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID = Convert.ToInt64(comId + "01");
                                

                                    Insert_CNF_ExpMst_LogData(aCnfExpenseHeadModel);

                                    db.CnfExpmstDbSet.Add(aCnfExpenseHeadModel.CNF_EXPMSTModel);
                                    db.SaveChanges();

                                    TempData["message"] = "\""+ aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCNM + "\" successfully saved.\n Please Create the Expense List.";
                                    TempData["ShowAddButton"] = "Show Add Button";
                                    TempData["cnfExpenseHead"] = aCnfExpenseHeadModel;
                                    TempData["ExpCID"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;
                                   
                                    
                                    return RedirectToAction("Index");
                                }
                                     
                                
                                else if (maxData <= R)
                                {

                                    aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID = maxData + 1;
                                 


                                    Insert_CNF_ExpMst_LogData(aCnfExpenseHeadModel);

                                    db.CnfExpmstDbSet.Add(aCnfExpenseHeadModel.CNF_EXPMSTModel);
                                    db.SaveChanges();

                                    TempData["message"] = "\""+aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCNM + "\" successfully saved.\n Please Create the Expense List.";
                                    TempData["ShowAddButton"] = "Show Add Button";
                                    TempData["cnfExpenseHead"] = aCnfExpenseHeadModel;
                                    TempData["ExpCID"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;
                                   
                                    return RedirectToAction("Index");
                                }
                                
                               

                                else
                                {
                                    TempData["ShowAddButton"] = "Show Add Button";
                                    TempData["message"] = "Not possible entry ";
                                    return RedirectToAction("Index");
                                }
                            }
                        }
                        else if (result > 0)
                        {
                            var ans = from n in db.CnfExpmstDbSet
                                      where n.COMPID == aCnfExpenseHeadModel.CNF_EXPMSTModel.COMPID
                                          && n.EXPCNM == aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCNM
                                      select new { n.EXPCID };
                            foreach(var a in ans)
                            {
                                aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID = a.EXPCID;
                            }
                                    

                            TempData["ShowAddButton"] = "Show Add Button";
                            TempData["cnfExpenseHead"] = aCnfExpenseHeadModel;
                            TempData["ExpCID"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;

                            TempData["latitute_deleteAccount"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.INSLTUDE;
                            return RedirectToAction("Index");
                        }
                    }

                    //else if (aCnfExpenseHeadModel.HEADNM == null && aCnfExpenseHeadModel.HEADTP==0)
                    //{
                    //    ViewBag.CategoryMsg = "Please Select Head Type and Enter Head Name.";
                    //    return View("Index");
                    //}
                    

                
            }

            if (command == "Update")
            {
                var query =
                    from a in db.CnfExpenseDbSet
                    where (a.EXPID == aCnfExpenseHeadModel.CNF_EXPENSEModel.EXPID && a.COMPID == aCnfExpenseHeadModel.CNF_EXPMSTModel.COMPID && a.EXPCID == aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID)
                    select a;
                aCnfExpenseHeadModel.CNF_EXPENSEModel.COMPID = aCnfExpenseHeadModel.CNF_EXPMSTModel.COMPID;
                aCnfExpenseHeadModel.CNF_EXPENSEModel.EXPCID = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;
               

                foreach (CNF_EXPENSE a in query)
                {
                    // Insert any additional changes to column values.
                    a.EXPNM = aCnfExpenseHeadModel.CNF_EXPENSEModel.EXPNM;


                    
                    a.UPDIPNO = ipAddress.ToString();
                    a.UPDTIME = td;
                    a.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    a.UPDLTUDE = aCnfExpenseHeadModel.CNF_EXPMSTModel.INSLTUDE;
                    string expenseHead = "";
                    var headnamefind =
                        (from n in db.CnfExpmstDbSet
                         where n.COMPID == aCnfExpenseHeadModel.CNF_EXPENSEModel.COMPID && n.EXPCID == aCnfExpenseHeadModel.CNF_EXPENSEModel.EXPCID
                         select n).ToList();
                    foreach (var name in headnamefind)
                    {
                        expenseHead = name.EXPCNM;
                    }
                    TempData["AccountLogData"] = Convert.ToString("Head Name: " + expenseHead + ",\nAccount Name: " + a.EXPNM + ".");

                }

                ASL_LOG aslLogref = new ASL_LOG();

                aslLogref.LOGIPNO = ipAddress.ToString();
                aslLogref.COMPID = aCnfExpenseHeadModel.CNF_EXPENSEModel.COMPID;
                aCnfExpenseHeadModel.CNF_EXPENSEModel.INSLTUDE = aCnfExpenseHeadModel.CNF_EXPMSTModel.INSLTUDE;
                aslLogref.LOGLTUDE = aCnfExpenseHeadModel.CNF_EXPENSEModel.INSLTUDE;

                //Update User ID save ASL_ROLE table attribute UPDUSERID
                aslLogref.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                aslLogref.LOGDATA = TempData["AccountLogData"].ToString();
                Update_CNF_EXPENSE_LogData(aslLogref);

                db.SaveChanges();

                TempData["cnfExpenseHead"] = aCnfExpenseHeadModel;
                TempData["ExpCID"] = aCnfExpenseHeadModel.CNF_EXPMSTModel.EXPCID;
               
                TempData["ShowAddButton"] = "Show Add Button";
                aCnfExpenseHeadModel.CNF_EXPENSEModel.EXPNM = "";

               
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }


        public ActionResult EditAccountUpdate(Int64 id, Int64 cid, Int64 Expcid, Int64 itemId, string itemname, CNF_ExpenseHeadModel model)
        {
            //.........................................................Create Permission Check
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

            var updateStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
            string query1 = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='ExpenseInformation' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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

            //add the data from database to model
            var expcid = from m in db.CnfExpmstDbSet where m.EXPCID == Expcid && m.COMPID == cid select m;
            foreach (var categoryResult in expcid)
            {
                model.CNF_EXPMSTModel.COMPID = cid;
                model.CNF_EXPMSTModel.EXPCID = Expcid;
                model.CNF_EXPMSTModel.EXPCNM = categoryResult.EXPCNM;
               
              
            }

     
            TempData["cnfExpenseHead"] = model;
            TempData["ExpCID"] = Expcid;
          
            TempData["ShowAddButton"] = null;
            if (updateStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("Index");
            }
            //...............................................................................................

            model.CNF_EXPENSEModel = db.CnfExpenseDbSet.Find(id);

            var item = from r in db.CnfExpenseDbSet where r.CNF_EXPID == id select r.EXPNM;
            foreach (var it in item)
            {
                model.CNF_EXPENSEModel.EXPNM = it.ToString();
            }

            return RedirectToAction("Index");

        }


        public ActionResult AccountDelete(Int64 id, Int64 cid,Int64 Expcid, Int64 itemId, string itemname, CNF_ExpenseHeadModel model)
        {
            //.........................................................Create Permission Check
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

            var deleteStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CnfDbContext"].ToString());
            string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='ExpenseInformation' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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
            //add the data from database to model
            var expcid = from m in db.CnfExpmstDbSet where m.EXPCID == Expcid && m.COMPID == cid select m;
            foreach (var categoryResult in expcid)
            {
                model.CNF_EXPMSTModel.COMPID = cid;
                model.CNF_EXPMSTModel.EXPCID = Expcid;
                model.CNF_EXPMSTModel.EXPCNM = categoryResult.EXPCNM;
               
            }

           
            TempData["cnfExpenseHead"] = model;
            TempData["ExpCID"] = Expcid;
           
            TempData["ShowAddButton"] = "Show Add Button";
            if (deleteStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("Index");

            }
            //...............................................................................................

            CNF_EXPENSE Accountitem = db.CnfExpenseDbSet.Find(id);
            //Get Ip ADDRESS,Time & user PC Name
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            Accountitem.USERPC = strHostName;
            Accountitem.UPDIPNO = ipAddress.ToString();
            Accountitem.UPDTIME = Convert.ToDateTime(td);
            //Delete User ID save POS_ITEMMST table attribute (UPDUSERID) field
            Accountitem.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            if (TempData["latitute_deleteAccount"] != null)
            {
                //Get current LOGLTUDE data 
                Accountitem.UPDLTUDE = TempData["latitute_deleteAccount"].ToString();
            }

            Delete_CNF_EXPENSE_LogData(Accountitem);
            Delete_ASL_DELETE_CNF_EXPENSE(Accountitem);
            db.CnfExpenseDbSet.Remove(Accountitem);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        




        //AutoComplete 
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ItemNameChanged(string changedText)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
           

            String itemName = "";

            var rt = db.CnfExpmstDbSet.Where(n => n.EXPCNM.StartsWith(changedText) &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             headname = n.EXPCNM

                                                         }).ToList();
            int lenChangedtxt = changedText.Length;

            Int64 cont = rt.Count();
            Int64 k = 0;
            string status = "";
            if (changedText != "" && cont != 0)
            {
                while (status != "no")
                {
                    k = 0;
                    foreach (var n in rt)
                    {
                        string ss = Convert.ToString(n.headname);
                        string subss = "";
                        if (ss.Length >= lenChangedtxt)
                        {
                            subss = ss.Substring(0, lenChangedtxt);
                            subss = subss.ToUpper();
                        }
                        else
                        {
                            subss = "";
                        }


                        if (subss == changedText.ToUpper())
                        {
                            k++;
                        }
                        if (k == cont)
                        {
                            status = "yes";
                            lenChangedtxt++;
                            if (ss.Length > lenChangedtxt - 1)
                            {
                                changedText = changedText + ss[lenChangedtxt - 1];
                            }

                        }
                        else
                        {
                            status = "no";
                            //lenChangedtxt--;
                        }

                    }

                }
                if (lenChangedtxt == 1)
                {
                    itemName = changedText.Substring(0, lenChangedtxt);
                }

                else
                {
                    itemName = changedText.Substring(0, lenChangedtxt - 1);
                }



            }
            else
            {
                itemName = changedText;
            }







            String itemId2 = "";
            string remarks = "";
            var rt2 = db.CnfExpmstDbSet.Where(n => n.EXPCNM == changedText &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             Headid = n.EXPCID
                                                            
                                                         });
            foreach (var n in rt2)
            {
                itemId2 = Convert.ToString(n.Headid);
               
            }

            var result = new { HeadName = itemName, headid = itemId2};
            return Json(result, JsonRequestBehavior.AllowGet);

           

        }



        //AutoComplete
        public JsonResult TagSearch(string term)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());

          
           
               

                var tags = from p in db.CnfExpmstDbSet
                           where p.COMPID == compid
                           select p.EXPCNM;

                return this.Json(tags.Where(t => t.StartsWith(term)),
                           JsonRequestBehavior.AllowGet);
           



        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
