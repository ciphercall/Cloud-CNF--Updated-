
using Mvc_CNFApp.Models.CNF;
using Mvc_CNFApp.Models.GL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Mvc_CNFApp.Models.ASL;

namespace Mvc_CNFApp.Models
{
    public class CnfDbContext:DbContext
    {
        public DbSet<AslCompany> AslCompanyDbSet { get; set; }
        public DbSet<AslUserco> AslUsercoDbSet { get; set; }
        public DbSet<ASL_LOG> AslLogDbSet { get; set; }
        public DbSet<ASL_DELETE> AslDeleteDbSet { get; set; }
        public DbSet<ASL_MENUMST> AslMenumstDbSet { get; set; }
        public DbSet<ASL_MENU> AslMenuDbSet { get; set; }
        public DbSet<ASL_ROLE> AslRoleDbSet { get; set; }

        public DbSet<GL_ACCHARMST> GlAccharmstDbSet { get; set; }
        public DbSet<GL_ACCHART> GlAcchartDbSet { get; set; }
        public DbSet<CNF_PARTY> CnfPartyDbSet { get; set; }
        public DbSet<CNF_COMMISSION> CnfCommissionDbSet { get; set; }
        public DbSet<CNF_EXPMST> CnfExpmstDbSet { get; set; }
        public DbSet<CNF_EXPENSE> CnfExpenseDbSet { get; set; }
        public DbSet<CNF_JOB> CnfJobDbSet { get; set; }
        public DbSet<CNF_JOBEXPMST> CnfJobexpmstDbSet { get; set; }
        public DbSet<CNF_JOBEXP> CnfJobexpDbSet { get; set; }
        public DbSet<CNF_JOBRCV> CnfJobrcvs { get; set; }
        public DbSet<CNF_JOBBILL> CnfJobbillsDbSet { get; set; }
        public DbSet<ASL_TABLE> AslTableDbSet { get; set; }
        public DbSet<ASL_TABFIELD> AslTableFieldDbSet { get; set; }
        public DbSet<ASL_DREPORT> AslDreportDbSet { get; set; }

        public DbSet<CNF_JOBDTL> CnfJobDetailDbSet { get; set; }
        //GL Db Set
    

        public DbSet<GL_COSTPMST> GLCostPMSTDbSet { get; set; }
        public DbSet<GL_COSTPOOL> GlCostPoolDbSet { get; set; }
        public DbSet<GL_STRANS> GlStransDbSet { get; set; }
        public DbSet<GL_MASTER> GlMasterDbSet { get; set; }


        public DbSet<GL_MTRANS> MtransdbSet { get; set; }
        public DbSet<GL_MTRANSMST> MtransMasterdbSet { get; set; }


        //Upload Excel File Data module
        public DbSet<ASL_PCONTACTS> UploadContactDbSet { get; set; }
        public DbSet<ASL_PGROUPS> UploadGroupDbSet { get; set; }
        public DbSet<ASL_PEMAIL> SendLogEmailDbSet { get; set; }
        public DbSet<ASL_PSMS> SendLogSMSDbSet { get; set; }


        //Promotional
        public DbSet<ASL_PCalendarImage> CalendarImageDbSet { get; set; }
        public DbSet<ASL_SchedularCalendar> SchedularCalendarDbSet { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }
}