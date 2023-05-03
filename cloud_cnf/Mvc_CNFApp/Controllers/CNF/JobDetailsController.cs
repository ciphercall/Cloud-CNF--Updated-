using Mvc_CNFApp.Models;
using Mvc_CNFApp.Models.CNF;
using Mvc_CNFApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_CNFApp.Controllers.CNF
{
    public class JobDetailsController : AppController
    {
        private CnfDbContext db = new CnfDbContext();
        // GET: /JobDetails/

        public ActionResult Index()
        {
            return View();
        }
        
        [AcceptVerbs("POST")]
        public ActionResult Index(CnfJobDetailsDTO model,string command)
        {
            if(command=="Submit")
            {
                Int64 id = Convert.ToInt64(model.JOBNO);//technically this is unique row id

                var result =
                    (from n in db.CnfJobDbSet
                     where n.COMPID == model.COMPID && n.Cnf_JobID == id
                     select new { n.JOBNO });
                foreach (var VARIABLE in result)
                {
                    model.JOBNO = Convert.ToInt64(VARIABLE.JOBNO);
                }

                CNF_JOBDTL obj = new CNF_JOBDTL();

                obj.COMPID = model.COMPID;
                obj.JOBTP = model.JOBTP;
                obj.JOBNO = model.JOBNO;
                obj.JOBYY = model.JOBYY;
                obj.BILLRANGE = model.BILLRANGE;
                obj.BILLREF = model.BILLREF;
                obj.SBANKPAY = model.SBANKPAY;
                obj.LABORCHG = model.LABORCHG;
                obj.OTHERCHG = model.OTHERCHG;
                obj.RECPTCHG = model.RECPTCHG;
                obj.SSHIPCHG = model.SSHIPCHG;
                obj.TRANSCHG = model.TRANSCHG;
                obj.ITCPEMP = model.ITCPEMP;
                obj.ITCPCUST = model.ITCPCUST;
                obj.ITCFINVV = model.ITCFINVV;
                obj.HMISCCHG = model.HMISCCHG;
                obj.EPVALUE = model.EPVALUE;
                obj.DOCPCOST = model.DOCPCOST;
                obj.DEMURCHG = model.DEMURCHG;

                obj.INSLTUDE = model.INSLTUDE;

                obj.SPCOST = model.SPCOST;
                obj.REWORKCOST = model.REWORKCOST;
                obj.MISCCOSTRIDT = model.MISCCOSTRIDT;
                obj.IMPORTDUTY = model.IMPORTDUTY;
                obj.PCDEMURRAGE = model.PCDEMURRAGE;

                db.CnfJobDetailDbSet.Add(obj);
                db.SaveChanges();


                return View();
            }
            else if(command=="Update")
            {

                Int64 id = Convert.ToInt64(model.JOBNO);//technically this is unique row id

                var result =
                    (from n in db.CnfJobDbSet
                     where n.COMPID == model.COMPID && n.Cnf_JobID == id
                     select new { n.JOBNO });
                foreach (var VARIABLE in result)
                {
                    model.JOBNO = Convert.ToInt64(VARIABLE.JOBNO);
                }

                var search_datafrom_jobdetails = (from n in db.CnfJobDetailDbSet where n.COMPID == model.COMPID && n.JOBNO == model.JOBNO && n.JOBTP == model.JOBTP && n.JOBYY == model.JOBYY select n).ToList();
                foreach(var ss in search_datafrom_jobdetails)
                {
                    ss.ID = ss.ID;
                    ss.COMPID = ss.COMPID;
                    ss.JOBNO = model.JOBNO;
                    ss.JOBTP = model.JOBTP;
                    ss.JOBYY = model.JOBYY;
                    ss.BILLRANGE = model.BILLRANGE;
                    ss.BILLREF = model.BILLREF;
                    ss.SBANKPAY = model.SBANKPAY;
                    ss.SSHIPCHG = model.SSHIPCHG;
                    ss.LABORCHG = model.LABORCHG;
                    ss.OTHERCHG = model.OTHERCHG;
                    ss.RECPTCHG = model.RECPTCHG;
                    ss.TRANSCHG = model.TRANSCHG;
                    ss.ITCPEMP = model.ITCPEMP;
                    ss.ITCPCUST = model.ITCPCUST;
                    ss.ITCFINVV = model.ITCFINVV;
                    ss.HMISCCHG = model.HMISCCHG;
                    ss.EPVALUE = model.EPVALUE;
                    ss.DOCPCOST = model.DOCPCOST;
                    ss.DEMURCHG = model.DEMURCHG;

                    ss.SPCOST = model.SPCOST;
                    ss.REWORKCOST = model.REWORKCOST;
                    ss.MISCCOSTRIDT = model.MISCCOSTRIDT;
                    ss.IMPORTDUTY = model.IMPORTDUTY;
                    ss.PCDEMURRAGE = model.PCDEMURRAGE;

                    db.SaveChanges();
                }

                return View();
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult JobNoChanged_getJobinfo(Int64 changedtxt)
        {
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var selectdata = from n in db.CnfJobDbSet where compid == n.COMPID && changedtxt == n.Cnf_JobID select new { n.JOBNO, n.JOBYY, n.JOBTP};
            string jobType = "", partyName = "";
            Int64 jobYear = 0, partyid = 0, jobno = 0;
            foreach (var l in selectdata)
            {
                jobType = l.JOBTP;
                jobYear = Convert.ToInt64(l.JOBYY);
            
                jobno = Convert.ToInt64(l.JOBNO);
            }

            var search_data_from_jobdetals = (from n in db.CnfJobDetailDbSet where n.COMPID == compid && n.JOBNO == jobno && n.JOBTP == jobType && n.JOBYY == jobYear select n).ToList();
            var search_party = (from n in db.CnfJobDbSet where n.COMPID == compid && n.JOBNO == jobno && n.JOBTP == jobType && n.JOBYY == jobYear select n).ToList();
            string partyname = "", forwardingDate = "";
            foreach(var x in search_party)
            {
                var search_partyname = (from n in db.CnfPartyDbSet where n.COMPID == x.COMPID && n.PARTYID == x.PARTYID select n).ToList();
                foreach(var ss in search_partyname)
                {
                    partyname = ss.PARTYNM;
                }
                if(x.BILLFDT!=null)
                {
                    string dd = Convert.ToString(x.BILLFDT);
                    DateTime aa = DateTime.Parse(dd);
                    forwardingDate = aa.ToString("dd-MMM-yyyy");
                }
               
            }

            if(search_data_from_jobdetals.Count==0)
            {

                var result = new { Year = jobYear, Type = jobType, Message = "Submit", Party = partyname , BillFDT=forwardingDate};
                 return Json(result, JsonRequestBehavior.AllowGet);
            }
            //var findpartyName = from n in db.GlAcchartDbSet where n.ACCOUNTCD == partyid select new { n.ACCOUNTNM };
            //foreach (var VARIABLE in findpartyName)
            //{
            //    partyName = VARIABLE.ACCOUNTNM;
            //}

            else
            {
                string billrange = "", billref = "";
                decimal sbankpay = 0, laborcharge = 0,sshipcharge=0,transcharge=0,hmisccharge=0,recptcharge=0,othercharge=0,
                    demurcharge=0,docpcost=0,epvalue=0,itcpcust=0,itcfinvv=0,itcpemp=0,
                    mISCCOSTRIDT=0, iMPORTDUTY=0, pCDEMURRAGE=0, rEWORKCOST=0, sPCOST=0;
                foreach(var ss in search_data_from_jobdetals)
                {
                    sbankpay = Convert.ToDecimal(ss.SBANKPAY);
                    laborcharge = Convert.ToDecimal(ss.LABORCHG);
                    sshipcharge = Convert.ToDecimal(ss.SSHIPCHG);
                    transcharge = Convert.ToDecimal(ss.TRANSCHG);
                    hmisccharge = Convert.ToDecimal(ss.HMISCCHG);
                    recptcharge = Convert.ToDecimal(ss.RECPTCHG);
                    othercharge = Convert.ToDecimal(ss.OTHERCHG);
                    demurcharge = Convert.ToDecimal(ss.DEMURCHG);
                    docpcost = Convert.ToDecimal(ss.DOCPCOST);
                    epvalue = Convert.ToDecimal(ss.EPVALUE);
                    itcpcust = Convert.ToDecimal(ss.ITCPCUST);
                    itcfinvv = Convert.ToDecimal(ss.ITCFINVV);
                    itcpemp = Convert.ToDecimal(ss.ITCPEMP);
                    billrange = ss.BILLRANGE;
                    billref = ss.BILLREF;
                    mISCCOSTRIDT = Convert.ToDecimal(ss.MISCCOSTRIDT);
                    iMPORTDUTY = Convert.ToDecimal(ss.IMPORTDUTY);
                    pCDEMURRAGE = Convert.ToDecimal(ss.PCDEMURRAGE);
                    rEWORKCOST = Convert.ToDecimal(ss.REWORKCOST);
                    sPCOST = Convert.ToDecimal(ss.SPCOST);


                }
                var result = new { Year = jobYear, Type = jobType, Message = "Update",Sb=sbankpay,lab=laborcharge,ssh=sshipcharge,tra=transcharge,
                hmi=hmisccharge,rec=recptcharge,oth=othercharge, dem=demurcharge,doc=docpcost,epv=epvalue,itc=itcpcust,itfin=itcfinvv,itemp=itcpemp,
                Billrange=billrange,
                                   Billref = billref,
                                   Party = partyname,
                                   BillFDT = forwardingDate,
                    MISCCOSTRIDT= mISCCOSTRIDT,
                    IMPORTDUTY= iMPORTDUTY,
                    PCDEMURRAGE= pCDEMURRAGE,
                    REWORKCOST= rEWORKCOST,
                    SPCOST= sPCOST



                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
          
           
        }


        public ActionResult JobDetailsRF()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JobDetailsRF(PageModel model)
        {
            if (model.ACnfJob.JOBTP == "IMPORT")
            {
                TempData["GetJobDetailsReport"] = model;
                return RedirectToAction("GetJobDetailsReport");
            }
            else if(model.ACnfJob.JOBTP == "EXPORT")
            {

                TempData["GetJobDetailsReportExport"] = model;
                return RedirectToAction("GetJobDetailsReportExport");
            }
            else
            {
                return null;
            }
         
        }

        public ActionResult GetJobDetailsReport()
        {
            PageModel model = (PageModel)TempData["GetJobDetailsReport"];
            return View(model);
        }

        public ActionResult GetJobDetailsReportExport()
        {
            PageModel model = (PageModel)TempData["GetJobDetailsReportExport"];
            return View(model);
        }

        public JsonResult TagSearch(string term,string Compid,string Jobno,string Jobyear,string Jobtype)
        {
            //var compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            Int64 compid = Convert.ToInt64(Compid);
            Int64 jobno = Convert.ToInt64(Jobno);
            Int64 jobyear = Convert.ToInt64(Jobyear);

            var selectdata = from n in db.CnfJobDbSet where compid == n.COMPID && jobno == n.Cnf_JobID select new { n.JOBNO, n.JOBYY, n.JOBTP };
            string jobType = "", partyName = "";
            Int64 jobYear = 0, partyid = 0, job = 0;
            foreach (var l in selectdata)
            {
                jobType = l.JOBTP;
                jobYear = Convert.ToInt64(l.JOBYY);

                job = Convert.ToInt64(l.JOBNO);
            }




            var tags = from p in db.CnfJobDetailDbSet
                       where p.COMPID == compid && p.JOBNO == job && p.JOBTP == jobType && p.JOBYY == jobYear
                       select new { p.BILLREF };

            return this.Json(tags.Where(t => t.BILLREF.StartsWith(term)),
                       JsonRequestBehavior.AllowGet);




        }

        public JsonResult TagSearch2(string term, string Compid, string Jobno, string Jobyear, string Jobtype)
        {
            //var compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            Int64 compid = Convert.ToInt64(Compid);
            Int64 jobno = Convert.ToInt64(Jobno);
            Int64 jobyear = Convert.ToInt64(Jobyear);

            var selectdata = from n in db.CnfJobDbSet where compid == n.COMPID && jobno == n.Cnf_JobID select new { n.JOBNO, n.JOBYY, n.JOBTP };
            string jobType = "", partyName = "";
            Int64 jobYear = 0, partyid = 0, job = 0;
            foreach (var l in selectdata)
            {
                jobType = l.JOBTP;
                jobYear = Convert.ToInt64(l.JOBYY);

                job = Convert.ToInt64(l.JOBNO);
            }




            var tags = from p in db.CnfJobDetailDbSet
                       where p.COMPID == compid && p.JOBNO == job && p.JOBTP == jobType && p.JOBYY == jobYear
                       select new { p.BILLRANGE };

            return this.Json(tags.Where(t => t.BILLRANGE.StartsWith(term)),
                       JsonRequestBehavior.AllowGet);




        }

    }
}
