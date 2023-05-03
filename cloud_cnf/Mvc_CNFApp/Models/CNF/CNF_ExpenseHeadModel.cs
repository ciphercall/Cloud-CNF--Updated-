using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_CNFApp.Models
{
    public class CNF_ExpenseHeadModel
    {
         public CNF_ExpenseHeadModel()
        {

            this.CNF_EXPMSTModel = new CNF_EXPMST();
            this.CNF_EXPENSEModel = new CNF_EXPENSE();
        }

        public CNF_EXPMST CNF_EXPMSTModel { get; set; }
        public CNF_EXPENSE CNF_EXPENSEModel { get; set; }
        
        public decimal? Total { get; set; }
        public string Empty { get; set; }//It used for readonly value(HtmlTextBoxfor) hold.

       

        //[Required(ErrorMessage = "CNF_Expense name can not be empty!")]
        //[Display(Name = "CNF_Expense Name")]
        //public string EXPCNM { get; set; }

      
    }
}