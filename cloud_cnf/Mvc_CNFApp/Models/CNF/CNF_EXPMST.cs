﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_CNFApp.Models
{
    public class CNF_EXPMST
    {

        [Key]
        public Int64 CNF_EXPMSTID { get; set; }

        [Display(Name = "Company ID")]
        public Int64? COMPID { get; set; }

       

        [Display(Name = "Expense CID")]
        public Int64? EXPCID { get; set; }

        [Required(ErrorMessage = "CNF_Expense name can not be empty!")]
        [Display(Name = "CNF_Expense Name")]
        public string EXPCNM { get; set; }

     




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