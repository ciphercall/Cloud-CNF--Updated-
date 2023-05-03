using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_CNFApp.Models
{
    public class CNF_JOB
    {
        [Key]
        public Int64 Cnf_JobID { get; set; }
        public Int64? COMPID { get; set; }

        //[Required(ErrorMessage = "JOBCDT required!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime? JOBCDT { get; set; }

        [Required(ErrorMessage = "REGID required!")]
        [Display(Name = "Reg.ID")]
        public string REGID { get; set; }//CHITTAGONG, COMILLA, BENAPOLE, DEPZ, ICD, AEPZ, AIRPORT

        [Required(ErrorMessage = "Job Year required!")]
        [Display(Name = "Job Year")]
        public Int64? JOBYY { get; set; }

        [Required(ErrorMessage = "JOBTP required!")]
        [Display(Name = "Job Type")]
        public string JOBTP { get; set; }

        [Required(ErrorMessage = "JOBNO required!")]
        [Display(Name = "Job No")]
        public Int64? JOBNO { get; set; }

        [Required(ErrorMessage = "Party Name required!")]
        public Int64? PARTYID { get; set; }
        public string PartyName { get; set; }

        public string FLIGHTNO { get; set; }

         [Display(Name = "Consignee Name")]
        public string CONSIGNEENM { get; set; }

        [Display(Name = "Consignee Address")]
        public string CONSIGNEEADD { get; set; }

        [Display(Name = "Supplier Name")]
        public string SUPPLIERNM { get; set; }

        [Display(Name = "Packages")]
        public string PKGS { get; set; }

        [Display(Name = "Goods")]
        public string GOODS { get; set; }

        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "Gross Weight")]
        public decimal? WTGROSS { get; set; }

        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "Net Weight")]
        public decimal? WTNET { get; set; }

        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "C&F Value (USD)")]
        public decimal? CNFV_USD { get; set; }

        [Display(Name = "Exchange Type")]
        public string CNFV_ETP { get; set; }

        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "Exchange Rate")]
        public decimal? CNFV_ERT { get; set; }

        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "C&F Value (BDT)")]
        public decimal? CNFV_BDT { get; set; }

        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "CRF Value (USD)")]
        public decimal? CRFV_USD { get; set; }

        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "Assessable Value")]
        public decimal? ASSV_BDT { get; set; }

        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "Commission")]
        public decimal? COMMAMT { get; set; }


        [Range(typeof(Decimal), "0", "999999999999999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Display(Name = "CBM")]
        public decimal? CBM { get; set; }

        [Display(Name = "Container No")]
        public string CONTNO { get; set; }


        [Display(Name = "Invoice No")]
        public string DOCINVNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Invoice Date")]
        public DateTime? DOCINVDT { get; set; }

        [Display(Name = "Permit No")]
        public string PERMITNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Permit Date")]
        public DateTime? PERMITDT { get; set; }

        [Display(Name = "Bill No(Manual)")]
        public string BILLNOM { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Bill Date(Manual)")]
        public DateTime? BILLDTM { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Bill Forwarding Date")]
        public DateTime? BILLFDT { get; set; }

        [Display(Name = "B/E No")]
        public string BENO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "B/E Date")]
        public DateTime? BEDT { get; set; }

        [Display(Name = "L/C No")]
        public string LCNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "L/C Date")]
        public DateTime? LCDT { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "STATUS can not be empty!")]
        public string STATUS { get; set; }



        //............................................

        public string EXPNO { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EXPDT { get; set; }

        public string PARTSHIPMENT { get; set; }
        public string CRFNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CRFDT { get; set; }

        [Display(Name = "B/L No")]
        public string BLNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BLDT { get; set; }
        public string BTNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BTDT { get; set; }
        public string LCANO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LCADT { get; set; }
        public string AWBNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AWBDT { get; set; }
        public string HAWBNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? HAWBDT { get; set; }
        public string UNDTNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Unstuffing Date")]
        public DateTime? UNDTDT { get; set; }

        [Display(Name = "Mother Vessel")]
        public string MVESSEL { get; set; }

         [Display(Name = "Feder Vessel")]
        public string FVESSEL { get; set; }
        public string MARKSNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ETB { get; set; }

         [Display(Name = "Beath Shed No")]
        public string BERTHSNO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Arrival Date")]
        public DateTime? ARRIVEDT { get; set; }

         [Display(Name = "Rotation No")]
        public string ROTNO { get; set; }

         [Display(Name = "Line No")]
        public string LINENO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Delivery Date")]
        public DateTime? DELIVERYDT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Common Landing Date")]
        public DateTime? CLDT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Wharfent Date")]
        public DateTime? WFDT { get; set; }


        public string PCNO { get; set; }
        public DateTime? PCDT { get; set; }
        public string SPAGNM { get; set; }
        public string FWAGNM { get; set; }
        public string DESTN { get; set; }
        public string PTEMP { get; set; }
        public string VATRNO { get; set; }

        public string CSEALNO { get; set; }
        public string REMARKS { get; set; }
        //.................................................

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