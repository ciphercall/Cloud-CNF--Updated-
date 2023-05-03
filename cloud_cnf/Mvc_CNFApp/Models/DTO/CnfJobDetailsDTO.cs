using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_CNFApp.Models.DTO
{
    public class CnfJobDetailsDTO
    {
        public Int64 ID { get; set; }

        public Int64 COMPID { get; set; }
        public string JOBTP { get; set; }


        public Int64 JOBYY { get; set; }


        public Int64 JOBNO { get; set; }

        public string BILLREF { get; set; }
        public string BILLRANGE { get; set; }


     
        public decimal? SBANKPAY { get; set; }
     
        public decimal? LABORCHG { get; set; }
      
        public decimal? SSHIPCHG { get; set; }
     
        public decimal? TRANSCHG { get; set; }
    
    
        public decimal? HMISCCHG { get; set; }
       
      
        public decimal? RECPTCHG { get; set; }
     
        public decimal? OTHERCHG { get; set; }
      
        public decimal? DEMURCHG { get; set; }
     
        public decimal? DOCPCOST { get; set; }
       
        public decimal? EPVALUE { get; set; }
     
        public decimal? ITCPCUST { get; set; }
  
        public decimal? ITCFINVV { get; set; }

     
        public decimal? ITCPEMP { get; set; }

        public decimal? MISCCOSTRIDT { get; set; }
        public decimal? IMPORTDUTY { get; set; }
        public decimal? PCDEMURRAGE { get; set; }
        public decimal? REWORKCOST { get; set; }
        public decimal? SPCOST { get; set; }





        public string USERPC { get; set; }
        public Int64? INSUSERID { get; set; }

     
        public DateTime? INSTIME { get; set; }

      
        public string INSIPNO { get; set; }
        public string INSLTUDE { get; set; }
        public Int64? UPDUSERID { get; set; }

    
        public DateTime? UPDTIME { get; set; }

     
        public string UPDIPNO { get; set; }
        public string UPDLTUDE { get; set; }



        public string Partyname { get; set; }
        public string BillForwardingDate { get; set; }
    }
}