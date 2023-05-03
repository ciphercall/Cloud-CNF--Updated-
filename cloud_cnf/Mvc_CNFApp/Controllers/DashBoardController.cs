using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_CNFApp.Models;

namespace Mvc_CNFApp.Controllers
{
    public class DashBoardController : AppController
    {
        //
        // GET: /ShowJob/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPost(Int64 tid, Int64 compid, Int64 jobno,string type,string partyname,Int64 year, PageModel model)
        {
            model.ACnfJobbill.JOBNO = jobno;
            model.ACnfJobbill.COMPID = compid;
            model.ACnfJobbill.JOBTP = type;
            model.ACnfJobbill.PARTYNM = partyname;
            model.ACnfJobbill.JOBYY = year;
            TempData["JobAtGlance_Model"] = model;
            return RedirectToAction("GetJobAtGlanceReport");
        }
        public ActionResult GetJobAtGlanceReport()
        {
            PageModel model = (PageModel)TempData["JobAtGlance_Model"];
            return View(model);
        }
    }
}
