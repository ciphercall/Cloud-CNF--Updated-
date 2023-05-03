using Mvc_CNFApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_CNFApp.Controllers.GL
{
    public class ProcessMissMatchController : AppController
    {
        //
        // GET: /ProcessMissMatch/

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("POST")]
        public ActionResult Index(PageModel model, string command)
        {
            if (command == "Miss Match Report")
            {
                TempData["ProcessMissMatch"] = model;
                return RedirectToAction("ProcessMissMatchReport");
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }



        public ActionResult ProcessMissMatchReport()
        {
            PageModel model = (PageModel)TempData["ProcessMissMatch"];
            return View(model);
        }

    }
}
