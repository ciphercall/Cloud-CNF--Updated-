using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Mvc_CNFApp.Models;
using RazorPDF;

namespace Mvc_CNFApp.Controllers
{
    public class JobReportController : AppController
    {
        
        private CnfDbContext db = new CnfDbContext();

        public ActionResult GetJobWiseExpenses()
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            return View();
        }


        [HttpPost]
        public ActionResult GetJobWiseExpenses(PageModel model,string command)
        {
            if (command == "Expense")
            {
                CNF_JOB aCnfJob = db.CnfJobDbSet.Find(model.ACnfJob.Cnf_JobID);
                model.ACnfJob = aCnfJob;
                //var pdf = new PdfResult(model, "GetJobWiseExpensesReport");
                //return pdf;
                TempData["GetJobWiseExpenses_Model"] = model;
                return RedirectToAction("GetJobWiseExpensesReport");
            }
            else
            {
                CNF_JOB aCnfJob = db.CnfJobDbSet.Find(model.ACnfJob.Cnf_JobID);
                model.ACnfJob = aCnfJob;
                //var pdf = new PdfResult(model, "GetJobWiseExpensesReport");
                //return pdf;
                TempData["GetJobWiseReceive"] = model;
                return RedirectToAction("JobWiseReceive");
            }
          
        }

        public ActionResult GetJobWiseExpensesReport()
        {
            PageModel model = (PageModel)TempData["GetJobWiseExpenses_Model"];
            return View(model);
        }

        public ActionResult JobWiseReceive()
        {
            PageModel model = (PageModel)TempData["GetJobWiseReceive"];
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult JobNOChanged(Int64 changedtxt)
        {

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var res = from n in db.CnfJobDbSet where compid == n.COMPID && changedtxt == n.Cnf_JobID select new { n.JOBYY, n.JOBTP, n.PARTYID, n.JOBNO };
            string jobType = "", PartyName = "";
            Int64 year = 0, partyId = 0, jobNo = 0;
            foreach (var VARIABLE in res)
            {
                year = Convert.ToInt64(VARIABLE.JOBYY);
                jobType = VARIABLE.JOBTP;
                jobNo = Convert.ToInt64(VARIABLE.JOBNO);
                partyId = Convert.ToInt64(VARIABLE.PARTYID);
            }

            var res2 = from n in db.GlAcchartDbSet where n.COMPID == compid && n.ACCOUNTCD == partyId select new { n.ACCOUNTNM };
            foreach (var m in res2)
            {
                PartyName = m.ACCOUNTNM;
            }

            var result = new { JOBYY = year, JOBTP = jobType, PARTYNM = PartyName, JOBNO = jobNo };
            return Json(result, JsonRequestBehavior.AllowGet);
        }







        public ActionResult PartyWiseJOBRegister()
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult PartyWiseJOBRegister(CNF_JOB aCnfJobModel)
        {
            //var pdf = new PdfResult(aCnfJobModel, "GetPartyWiseJOBRegisterReport");
            //return pdf;
            TempData["PartyWiseJOBRegister_Model"] = aCnfJobModel;
            return RedirectToAction("GetPartyWiseJOBRegisterReport");
        }

        public ActionResult GetPartyWiseJOBRegisterReport()
        {
            CNF_JOB model = (CNF_JOB)TempData["PartyWiseJOBRegister_Model"];
            return View(model);
        }







        public ActionResult JOBRegister_JobType()
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult JOBRegister_JobType(CNF_JOB aCnfJobModel)
        {
            //var pdf = new PdfResult(aCnfJobModel, "GetJOBRegister_JobTypeReport");
            //return pdf;
            TempData["JOBRegister_JobType_Model"] = aCnfJobModel;
            return RedirectToAction("GetJOBRegister_JobTypeReport");
        }

        public ActionResult GetJOBRegister_JobTypeReport()
        {
            CNF_JOB model = (CNF_JOB)TempData["JOBRegister_JobType_Model"];
            return View(model);
        }








        public ActionResult ExpenseRegisterDetails()
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult ExpenseRegisterDetails(PageModel model)
        {
            //var pdf = new PdfResult(model, "GetExpenseRegisterDetailsReport");
            //return pdf;
            TempData["ExpenseRegisterDetails_Model"] = model;
            return RedirectToAction("GetExpenseRegisterDetailsReport");
        }

        public ActionResult GetExpenseRegisterDetailsReport()
        {
            PageModel model = (PageModel)TempData["ExpenseRegisterDetails_Model"];
            return View(model);
        }








        public ActionResult ExpenseHeadRegisterIndex()//Form
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult ExpenseHeadRegisterIndex(PageModel model,string command)
        {
            //var pdf = new PdfResult(model, "GetExpenseHeadRegisterReport");
            //return pdf;
            if(command=="Report")
            {
                TempData["ExpenseHeadRegisterIndex_Model"] = model;
                return RedirectToAction("GetExpenseHeadRegisterReport");
            }
            else
            {
                TempData["BillHeadRegisterIndex_Model"] = model;
                return RedirectToAction("GetBillHeadRegisterReport");
            }
           
        }

        public ActionResult GetExpenseHeadRegisterReport()
        {
            PageModel model = (PageModel)TempData["ExpenseHeadRegisterIndex_Model"];
            return View(model);
        }

        public ActionResult GetBillHeadRegisterReport()
        {
            PageModel model = (PageModel)TempData["BillHeadRegisterIndex_Model"];
            return View(model);
        }





        public ActionResult JobWiseExAndBillIndex()//Form
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult JobWiseExAndBillIndex(PageModel model, string command)
        {
            //var pdf = new PdfResult(model, "GetJobWiseExAndBillReport");
            //return pdf;
            if (command == "Report")
            {
                TempData["JobWiseExAndBillIndex_Model"] = model;
                return RedirectToAction("GetJobWiseExAndBillReport");
            }
            else if (command=="Submit")
            {
                TempData["JobWiseExAndBillIndex_Model"] = model;
                return RedirectToAction("GetPeriodicReport");
            }
            else if (command == "Submit Report")
            {
                TempData["JobWiseExAndBillIndex_Model2"] = model;
                return RedirectToAction("GetPeriodicReport2");
            }
            else
            {
                return null;
            }
           
        }
        public ActionResult GetJobWiseExAndBillReport()
        {
            PageModel model = (PageModel)TempData["JobWiseExAndBillIndex_Model"];
            return View(model);
        }

        public ActionResult GetPeriodicReport()
        {
            PageModel model = (PageModel)TempData["JobWiseExAndBillIndex_Model"];
            return View(model);
        }


        public ActionResult GetPeriodicReport2()
        {
            PageModel model = (PageModel)TempData["JobWiseExAndBillIndex_Model2"];
            return View(model);
        }



        public ActionResult JobAtGlance()//Form
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult JobAtGlance(PageModel model)
        {
            var result =
                    (from n in db.CnfJobDbSet
                     where n.COMPID == model.ACnfJobbill.COMPID && n.Cnf_JobID == model.ACnfJob.Cnf_JobID
                     select new { n.JOBNO });
            foreach (var VARIABLE in result)
            {
                model.ACnfJobbill.JOBNO = VARIABLE.JOBNO;
            }
            //var pdf = new PdfResult(model, "GetJobAtGlanceReport");
            //return pdf;
            TempData["JobAtGlance_Model"] = model;
            return RedirectToAction("GetJobAtGlanceReport");
        }

        public ActionResult GetJobAtGlanceReport()
        {
            PageModel model = (PageModel)TempData["JobAtGlance_Model"];
            return View(model);
        }



        public ActionResult JobExpenseReceive()//Form Only for Chamak company
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult JobExpenseReceive(PageModel model)
        {
            var result =
                    (from n in db.CnfJobDbSet
                     where n.COMPID == model.ACnfJobbill.COMPID && n.Cnf_JobID == model.ACnfJob.Cnf_JobID
                     select new { n.JOBNO });
            foreach (var VARIABLE in result)
            {
                model.ACnfJobbill.JOBNO = VARIABLE.JOBNO;
            }
            //var pdf = new PdfResult(model, "GetJobAtGlanceReport");
            //return pdf;
            TempData["JobAtGlance_Model"] = model;
            return RedirectToAction("GetJobExpenseReceiveReport");
        }

        public ActionResult GetJobExpenseReceiveReport()
        {
            PageModel model = (PageModel)TempData["JobAtGlance_Model"];
            return View(model);
        }




        public ActionResult PartyLedger()
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult PartyLedger(PageModel model, string command)
        {
            //var pdf = new PdfResult(model, "GetPartyLedgerReport");
            //return pdf;
            if (command == "Only Bill")
            {
                TempData["PartyLedger_Model"] = model;
                return RedirectToAction("GetPartyLedgerReport");
            }
            else if (command == "Ledger")
            {

                TempData["POS_Ledger"] = model;
                return RedirectToAction("LedgerReport");
            }
            else
            {
                return null;
            }
            
        }

        public ActionResult GetPartyLedgerReport()
        {
            PageModel model = (PageModel)TempData["PartyLedger_Model"];
            return View(model);
        }

        public ActionResult LedgerReport()
        {
            PageModel model = (PageModel)TempData["POS_Ledger"];
            return View(model);
        }








        public ActionResult BillForwardingIndex()//Form 
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult BillForwardingIndex(PageModel model)
        {
            //var pdf = new PdfResult(model, "GetBillForwardingReport");
            //return pdf;
            TempData["BillForwardingIndexr_Model"] = model;
            return RedirectToAction("GetBillForwardingReport");
        }

        public ActionResult GetBillForwardingReport()
        {
            PageModel model = (PageModel)TempData["BillForwardingIndexr_Model"];
            return View(model);
        }




        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
