using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_CNFApp.Models
{
    public class CNF_PARTY
    {
        [Key]
        public Int64 CNF_PARTYId { get; set; }

        public Int64? COMPID { get; set; }
        public Int64? PARTYID { get; set; }

        [Required(ErrorMessage = "Party Name can not be empty!")]
        [Display(Name = "Party Name")]
        public string PARTYNM { get; set; }

        //[Required(ErrorMessage = "User ADDRESS can not be empty!")]
        [Display(Name = "Party Address")]
        public string ADDRESS { get; set; }

        [Required(ErrorMessage = "Contact No can not be empty!")]
        [Remote("Check_ContactNo", "AslCompany", ErrorMessage = "Party contact no already exists")]
        [Display(Name = "Contact No")]
        [RegularExpression(@"^(8{2})([0-9]{11})", ErrorMessage = "Insert a valid phone number like 8801711001100")]
        public string CONTACTNO { get; set; }

        //[Required(ErrorMessage = "Email address can not be empty!")]
        [Remote("Check_EmailId", "AslCompany", ErrorMessage = "Party email id already exists")]
        [Display(Name = "Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email ADDRESS")]
        public string EMAILID { get; set; }
        [Remote("Check_EmailId", "AslCompany", ErrorMessage = "Party email id already exists")]
        [Display(Name = "Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email ADDRESS")]
        public string EMAILID2 { get; set; }

        public string CONTACTNO2 { get; set; }

        [Display(Name = "Webpage ID")]
        public string WEBID { get; set; }

        [Display(Name = "Authorized Person")]
        public string APNM { get; set; }

        [Display(Name = "Authorized Contact No")]
        public string APNO { get; set; }

        [Required(ErrorMessage = "STATUS can not be empty!")]
        [Display(Name = "Status")]
        public string STATUS { get; set; }
        public string REMARKS { get; set; }





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