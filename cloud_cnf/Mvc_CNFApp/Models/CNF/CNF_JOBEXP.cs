using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_CNFApp.Models
{
    public class CNF_JOBEXP
    {
        [Key]
        public Int64 cnf_JobEXPID { get; set; }

        public Int64? COMPID { get; set; }

        [Display(Name = "Insert Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TRANSDT { get; set; }
        public Int64? TRANSYY { get; set; }
        public Int64? TRANSNO { get; set; }
        public Int64? JOBYY { get; set; }
        public string JOBTP { get; set; }
        public Int64? JOBNO { get; set; }
        public Int64? EXPCD { get; set; }
        public Int64? EXPSL { get; set; }
        public Int64? EXPID { get; set; }

        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        public decimal? EXPAMT { get; set; }
        public string REMARKS { get; set; }




        [Display(Name = "User PC")]
        public string USERPC { get; set; }
        public Int64 INSUSERID { get; set; }

        [Display(Name = "Insert Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? INSTIME { get; set; }

        [Display(Name = "Inesrt IP ADDRESS")]
        public string INSIPNO { get; set; }
        public string INSLTUDE { get; set; }
        public Int64 UPDUSERID { get; set; }

        [Display(Name = "Update Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UPDTIME { get; set; }

        [Display(Name = "Update IP ADDRESS")]
        public string UPDIPNO { get; set; }
        public string UPDLTUDE { get; set; }

    }
}