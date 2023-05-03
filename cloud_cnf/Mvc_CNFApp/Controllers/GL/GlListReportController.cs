using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_CNFApp.Models;

namespace Mvc_CNFApp.Controllers.GL
{
    public class GlListReportController : AppController
    {
        private CnfDbContext db = new CnfDbContext();
        public ActionResult Get_HeadOfAccounts_List()
        {
            ViewData["HighLight_Menu_GL_Report"] = "heighlight";
            //var pdf = new PdfResult(null, "Get_HeadOfAccounts_List");
            //return pdf;
            return View();
        }

        public ActionResult Get_ListofCostPool()
        {
            ViewData["HighLight_Menu_GL_Report"] = "heighlight";
            return View();
        }
    }
}
