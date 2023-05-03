using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_CNFApp.Models
{
    public class CNF_JOBBILL
    {
        [Key]
        public Int64 Cnf_JobBillID { get; set; }

        [Display(Name = "Company Id")]
        public Int64? COMPID { get; set; }


        [Display(Name = "Job No")]
        public Int64? JOBNO { get; set; }

        [Display(Name = "Job Year")]
        public Int64? JOBYY { get; set; }
        [Display(Name = "Job Type")]
        public string JOBTP { get; set; }
        [Display(Name = "Expense ID")]
        public Int64? EXPID { get; set; }


        //[Required(ErrorMessage = "Amount required!")]
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "Amount")]
        public decimal? BILLAMT { get; set; }

        [Display(Name = "Bill Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
    
        public DateTime? BILLPDT { get; set; }

        [Display(Name = "Expense Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EXPPDT { get; set; }

        [Display(Name="Expense Name")]
        public string EXPNM { get; set; }

        [Display(Name = "Party Name")]
        public string PARTYNM { get; set; }

        [Display(Name = "Remarks")]
        public string REMARKS { get; set; }

        [Display(Name = "Bill Serial")]
        public Int64? BILLSL { get; set; }






        [Display(Name = "User PC")]
        public string USERPC { get; set; }
        public Int64? INSUSERID { get; set; }

        [Display(Name = "Insert Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? INSTIME { get; set; }

        [Display(Name = "Inesrt IP ADDRESS")]
        public string INSIPNO { get; set; }
        public string INSLTUDE { get; set; }
        public Int64? UPDUSERID { get; set; }

        [Display(Name = "Update Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UPDTIME { get; set; }

        [Display(Name = "Update IP ADDRESS")]
        public string UPDIPNO { get; set; }
        public string UPDLTUDE { get; set; }
    }
}