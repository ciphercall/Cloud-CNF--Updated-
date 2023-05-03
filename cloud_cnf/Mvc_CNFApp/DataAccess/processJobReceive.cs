using System.Net;
using Mvc_CNFApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_CNFApp.DataAccess
{
    public class processJobReceive
    {
        public static string jobreceiveProcess(DateTime TransDate)
        {
                  IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
         DateTime td;


        CnfDbContext db = new CnfDbContext();
        //Get Ip ADDRESS,Time & user PC Name
         string strHostName;
         IPHostEntry ipHostInfo;
         IPAddress ipAddress;

           strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            var checkdata = (from n in db.CnfJobrcvs where n.TRANSDT == TransDate && n.COMPID == compid select n).OrderBy(x => x.TRANSNO).ToList();
            
            string converttoString = Convert.ToString(TransDate.ToString("dd-MMM-yyyy"));
            string getYear = converttoString.Substring(9, 2);
            string getMonth = converttoString.Substring(3, 3);
            string Month = getMonth + "-" + getYear;

            if (checkdata.Count != 0)
            {
                var forremovedata = (from l in db.GlMasterDbSet
                                     where l.COMPID == compid && l.TRANSDT == TransDate && l.TABLEID=="CNF_JOBRCV"
                                     select l).ToList();

                foreach (var VARIABLE in forremovedata)
                {
                  
                        db.GlMasterDbSet.Remove(VARIABLE);
                    
                }



                db.SaveChanges();
                foreach (var x in checkdata)
                {
                    if ((x.TRANSFOR == "ADVANCE") || (x.TRANSFOR == "NORMAL"))
                    {
                        Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                            where a.COMPID == compid && a.TRANSTP == "MREC"
                            select a.TRANSSL).Max());
                        GL_MASTER model = new GL_MASTER();
                        if (maxSlCheck == 0)
                        {
                            model.TRANSSL = 10001;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "MREC";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "OFFICIAL";
                            model.TRANSMODE = x.TRANSFOR;
                            model.COSTPID = null;
                            model.DEBITCD = x.DEBITCD;
                            model.CREDITCD = x.PARTYID;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = Convert.ToString(x.JOBYY + "-" + x.JOBTP + "-" + x.JOBNO + "-" + x.REMARKS);
                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "CNF_JOBRCV";


                            model.DEBITAMT = x.AMOUNT;
                            model.CREDITAMT = 0;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;


                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = 10002;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "MREC";
                            model.TRANSMY =Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "OFFICIAL";
                            model.TRANSMODE = x.TRANSFOR;
                            model.COSTPID = null;
                            model.DEBITCD = x.PARTYID;
                            model.CREDITCD = x.DEBITCD;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = Convert.ToString(x.JOBYY + "-" + x.JOBTP + "-" + x.JOBNO + "-" + x.REMARKS);

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "CNF_JOBRCV";

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.AMOUNT;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);

                            //Insert_Process_LogData(model);
                            db.SaveChanges();

                        }
                        else
                        {
                            model.TRANSSL = maxSlCheck + 1;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "MREC";
                            model.TRANSMY =Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "OFFICIAL";
                            model.TRANSMODE = x.TRANSFOR;
                            model.COSTPID = null;
                            model.DEBITCD = x.DEBITCD;
                            model.CREDITCD = x.PARTYID;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = Convert.ToString(x.JOBYY + "-" + x.JOBTP + "-" + x.JOBNO + "-" + x.REMARKS);

                            model.DEBITAMT = x.AMOUNT;
                            model.CREDITAMT = 0;

                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "CNF_JOBRCV";

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = maxSlCheck + 2;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "MREC";
                            model.TRANSMY =Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "OFFICIAL";
                            model.TRANSMODE = x.TRANSFOR;
                            model.COSTPID = null;
                            model.DEBITCD = x.PARTYID;
                            model.CREDITCD = x.DEBITCD;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = Convert.ToString(x.JOBYY + "-" + x.JOBTP + "-" + x.JOBNO + "-" + x.REMARKS);
                            ;

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.AMOUNT;

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "CNF_JOBRCV";

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);
                            //Insert_Process_LogData(model);
                            db.SaveChanges();
                        }
                    }
                    else if(x.TRANSFOR == "DISCOUNT")
                    {
                          Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                            where a.COMPID == compid && a.TRANSTP == "JOUR"
                            select a.TRANSSL).Max());
                        GL_MASTER model = new GL_MASTER();
                        if (maxSlCheck == 0)
                        {
                            model.TRANSSL = 10001;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "OFFICIAL";
                            model.TRANSMODE = x.TRANSFOR;
                            model.COSTPID = null;
                            model.DEBITCD = x.DEBITCD;
                            model.CREDITCD = x.PARTYID;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = Convert.ToString(x.JOBYY + "-" + x.JOBTP + "-" + x.JOBNO + "-" + x.REMARKS);
                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "CNF_JOBRCV";


                            model.DEBITAMT = x.AMOUNT;
                            model.CREDITAMT = 0;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;


                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = 10002;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY =Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "OFFICIAL";
                            model.TRANSMODE = x.TRANSFOR;
                            model.COSTPID = null;
                            model.DEBITCD = x.PARTYID;
                            model.CREDITCD = x.DEBITCD;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = Convert.ToString(x.JOBYY + "-" + x.JOBTP + "-" + x.JOBNO + "-" + x.REMARKS);

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "CNF_JOBRCV";

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.AMOUNT;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);

                            //Insert_Process_LogData(model);
                            db.SaveChanges();

                        }
                        else 
                        {
                            model.TRANSSL = maxSlCheck + 1;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY =Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "OFFICIAL";
                            model.TRANSMODE = x.TRANSFOR;
                            model.COSTPID = null;
                            model.DEBITCD = x.DEBITCD;
                            model.CREDITCD = x.PARTYID;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = Convert.ToString(x.JOBYY +"-"+ x.JOBTP+"-" + x.JOBNO+"-" + x.REMARKS);

                            model.DEBITAMT = x.AMOUNT;
                            model.CREDITAMT = 0;

                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "CNF_JOBRCV";

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = maxSlCheck + 2;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "OFFICIAL";
                            model.TRANSMODE = x.TRANSFOR;
                            model.COSTPID = null;
                            model.DEBITCD = x.PARTYID;
                            model.CREDITCD = x.DEBITCD;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = Convert.ToString(x.JOBYY + "-" + x.JOBTP + "-" + x.JOBNO + "-" + x.REMARKS);
                            

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.AMOUNT;

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "CNF_JOBRCV";

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);
                            //Insert_Process_LogData(model);
                            db.SaveChanges();
                        }
                    }
                }
              
            }
            else
            {
                var forremovedata = (from l in db.GlMasterDbSet
                                     where l.COMPID == compid && l.TRANSDT == TransDate && l.TABLEID=="CNF_JOBRCV"
                                     select l).ToList();

                foreach (var VARIABLE in forremovedata)
                {
                  
                        db.GlMasterDbSet.Remove(VARIABLE);
                    
                }



                db.SaveChanges();
               
            }
            return null;
           
        }
    }
}