using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_CNFApp.Models.CNF
{
    public class CNF_JOBDTL
    {
        [Key]
        public Int64 ID { get; set; }

        public Int64 COMPID { get; set; }
        public string JOBTP { get; set; }


        public Int64 JOBYY { get; set; }


        public Int64 JOBNO { get; set; }

        public string BILLREF { get; set; }
        public string BILLRANGE { get; set; }

   
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? SBANKPAY { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? LABORCHG { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? SSHIPCHG { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? TRANSCHG { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? HMISCCHG { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? RECPTCHG { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? OTHERCHG { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? DEMURCHG { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? DOCPCOST { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? EPVALUE { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? ITCPCUST { get; set; }
        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? ITCFINVV { get; set; }

        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        //[Display(Name = "Amount")]
        public decimal? ITCPEMP { get; set; }
        public decimal? MISCCOSTRIDT { get; set; }
        public decimal? IMPORTDUTY { get; set; }
        public decimal? PCDEMURRAGE { get; set; }
        public decimal? REWORKCOST { get; set; }
        public decimal?SPCOST { get; set; }




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