using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_CNFApp.Models
{
    public class CNF_JOBRCV
    {
        [Key]
        public Int64 CNF_JOBRCVID { get; set; }

        [Display(Name = "Company Id")]
        public Int64? COMPID { get; set; }
        
        [Required(ErrorMessage = "TransDate required!")]
        [Display(Name = "Trans Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TRANSDT { get; set; }
        
        [Required(ErrorMessage = "TransYear required!")]
        [Display(Name = "Trans Year")]
        public Int64 TRANSYY { get; set; }
        [Display(Name = "Trans No")]
        public Int64? TRANSNO { get; set; }
        
        [Required(ErrorMessage = "TransFor required!")]
        [Display(Name = "Trans For")]
        public string TRANSFOR { get; set; }

        [Display(Name = "Job Year")]
        public int JOBYY { get; set; }

        [Display(Name = "Job Type")]
        public string JOBTP { get; set; }
        
        // [Required(ErrorMessage = "Job No required!")]
        [Display(Name = "Job No")]
        public Int64? JOBNO { get; set; }

        [Display(Name = "Party Id")]
        public Int64? PARTYID { get; set; }
        
        [Display(Name = "Debit Code")]
        public Int64? DEBITCD { get; set; }
        
        [Display(Name = "Remarks")]
        public string REMARKS { get; set; }
        
        [Required(ErrorMessage = "Amount required!")]
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "Amount")]
        public decimal? AMOUNT { get; set; }

        public string ADVSHOW { get; set; }










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