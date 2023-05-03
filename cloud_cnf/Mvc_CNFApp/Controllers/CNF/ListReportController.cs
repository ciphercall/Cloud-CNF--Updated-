using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_CNFApp.Models;
using RazorPDF;

namespace Mvc_CNFApp.Controllers
{
    public class ListReportController : AppController
    {
        private CnfDbContext db = new CnfDbContext();

       

        public ActionResult GetListOfParty()
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            //var pdf = new PdfResult(null, "GetListOfParty");
            //return pdf;
            return View();
        }


        public ActionResult GetExpensesList()
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            //var pdf = new PdfResult(null, "GetExpensesList");
            //return pdf;
            return View();
        }

        public ActionResult Get_HeadOfAccounts_List()
        {
            ViewData["HighLight_Menu_CnfReport"] = "heighlight";
            //var pdf = new PdfResult(null, "Get_HeadOfAccounts_List");
            //return pdf;
            return View();
        }

    }
}
