namespace Mvc_CNFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AslCompanies",
                c => new
                    {
                        AslCompanyId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        COMPNM = c.String(nullable: false),
                        ADDRESS = c.String(nullable: false),
                        CONTACTNO = c.String(nullable: false),
                        EMAILID = c.String(nullable: false),
                        WEBID = c.String(),
                        STATUS = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.AslCompanyId);
            
            CreateTable(
                "dbo.ASL_DELETE",
                c => new
                    {
                        Asl_DeleteID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        USERID = c.Long(),
                        DELSLNO = c.Long(),
                        DELDATE = c.String(),
                        DELTIME = c.String(),
                        DELIPNO = c.String(),
                        DELLTUDE = c.String(),
                        TABLEID = c.String(),
                        DELDATA = c.String(),
                        USERPC = c.String(),
                    })
                .PrimaryKey(t => t.Asl_DeleteID);
            
            CreateTable(
                "dbo.ASL_DREPORT",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        RPTNM = c.String(),
                        RPTTP = c.String(nullable: false),
                        RPTSL = c.Long(),
                        TABLEID = c.String(),
                        FIELDID = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ASL_LOG",
                c => new
                    {
                        Asl_LOGid = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        USERID = c.Long(),
                        LOGTYPE = c.String(),
                        LOGSLNO = c.Long(),
                        LOGDATE = c.DateTime(),
                        LOGTIME = c.String(),
                        LOGIPNO = c.String(),
                        LOGLTUDE = c.String(),
                        TABLEID = c.String(),
                        LOGDATA = c.String(),
                        USERPC = c.String(),
                    })
                .PrimaryKey(t => t.Asl_LOGid);
            
            CreateTable(
                "dbo.ASL_MENU",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MODULEID = c.String(),
                        SERIAL = c.Long(),
                        MENUTP = c.String(),
                        MENUID = c.String(),
                        MENUNM = c.String(),
                        ACTIONNAME = c.String(),
                        CONTROLLERNAME = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ASL_MENUMST",
                c => new
                    {
                        MODULEID = c.String(nullable: false, maxLength: 128),
                        MODULENM = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                    })
                .PrimaryKey(t => t.MODULEID);
            
            CreateTable(
                "dbo.ASL_ROLE",
                c => new
                    {
                        ASL_ROLEId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        USERID = c.Long(nullable: false),
                        MODULEID = c.String(nullable: false),
                        SERIAL = c.Long(),
                        MENUTP = c.String(nullable: false),
                        MENUID = c.String(),
                        STATUS = c.String(),
                        INSERTR = c.String(),
                        UPDATER = c.String(),
                        DELETER = c.String(),
                        MENUNAME = c.String(),
                        ACTIONNAME = c.String(),
                        CONTROLLERNAME = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                    })
                .PrimaryKey(t => t.ASL_ROLEId);
            
            CreateTable(
                "dbo.ASL_TABLE",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TABLEID = c.String(),
                        TABLENAME = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ASL_TABFIELD",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TABLEID = c.String(),
                        FIELDID = c.String(),
                        FIELDNAME = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AslUsercoes",
                c => new
                    {
                        AslUsercoId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        USERID = c.Long(),
                        USERNM = c.String(nullable: false),
                        DEPTNM = c.String(),
                        OPTP = c.String(nullable: false),
                        ADDRESS = c.String(nullable: false),
                        MOBNO = c.String(nullable: false),
                        EMAILID = c.String(),
                        LOGINBY = c.String(nullable: false),
                        LOGINID = c.String(nullable: false),
                        LOGINPW = c.String(nullable: false),
                        TIMEFR = c.String(nullable: false),
                        TIMETO = c.String(nullable: false),
                        STATUS = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.AslUsercoId);
            
            CreateTable(
                "dbo.CNF_COMMISSION",
                c => new
                    {
                        Cnf_ComissionID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        PARTYID = c.Long(),
                        COMMSL = c.Long(),
                        EXCTP = c.String(),
                        VALUEFR = c.Decimal(precision: 18, scale: 2),
                        VALUETO = c.Decimal(precision: 18, scale: 2),
                        VALUETP = c.String(),
                        COMMAMT = c.Decimal(precision: 18, scale: 2),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.Cnf_ComissionID);
            
            CreateTable(
                "dbo.CNF_EXPENSE",
                c => new
                    {
                        CNF_EXPID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        EXPCID = c.Long(),
                        EXPID = c.Long(),
                        EXPNM = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.CNF_EXPID);
            
            CreateTable(
                "dbo.CNF_EXPMST",
                c => new
                    {
                        CNF_EXPMSTID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        EXPCID = c.Long(),
                        EXPCNM = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.CNF_EXPMSTID);
            
            CreateTable(
                "dbo.CNF_JOBBILL",
                c => new
                    {
                        Cnf_JobBillID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        JOBNO = c.Long(),
                        JOBYY = c.Long(),
                        JOBTP = c.String(),
                        EXPID = c.Long(),
                        BILLAMT = c.Decimal(precision: 18, scale: 2),
                        BILLPDT = c.DateTime(),
                        EXPPDT = c.DateTime(),
                        EXPNM = c.String(),
                        PARTYNM = c.String(),
                        REMARKS = c.String(),
                        BILLSL = c.Long(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.Cnf_JobBillID);
            
            CreateTable(
                "dbo.CNF_JOB",
                c => new
                    {
                        Cnf_JobID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        JOBCDT = c.DateTime(),
                        REGID = c.String(nullable: false),
                        JOBYY = c.Long(nullable: false),
                        JOBTP = c.String(nullable: false),
                        JOBNO = c.Long(nullable: false),
                        PARTYID = c.Long(nullable: false),
                        FLIGHTNO = c.String(),
                        CONSIGNEENM = c.String(),
                        CONSIGNEEADD = c.String(),
                        SUPPLIERNM = c.String(),
                        PKGS = c.String(),
                        GOODS = c.String(),
                        WTGROSS = c.Decimal(precision: 18, scale: 2),
                        WTNET = c.Decimal(precision: 18, scale: 2),
                        CNFV_USD = c.Decimal(precision: 18, scale: 2),
                        CNFV_ETP = c.String(),
                        CNFV_ERT = c.Decimal(precision: 18, scale: 2),
                        CNFV_BDT = c.Decimal(precision: 18, scale: 2),
                        CRFV_USD = c.Decimal(precision: 18, scale: 2),
                        ASSV_BDT = c.Decimal(precision: 18, scale: 2),
                        COMMAMT = c.Decimal(precision: 18, scale: 2),
                        CBM = c.Decimal(precision: 18, scale: 2),
                        CONTNO = c.String(),
                        DOCINVNO = c.String(),
                        DOCINVDT = c.DateTime(),
                        PERMITNO = c.String(),
                        PERMITDT = c.DateTime(),
                        BILLNOM = c.String(),
                        BILLDTM = c.DateTime(),
                        BILLFDT = c.DateTime(),
                        BENO = c.String(),
                        BEDT = c.DateTime(),
                        LCNO = c.String(),
                        LCDT = c.DateTime(),
                        STATUS = c.String(nullable: false),
                        EXPNO = c.String(),
                        EXPDT = c.DateTime(),
                        PARTSHIPMENT = c.String(),
                        CRFNO = c.String(),
                        CRFDT = c.DateTime(),
                        BLNO = c.String(),
                        BLDT = c.DateTime(),
                        BTNO = c.String(),
                        BTDT = c.DateTime(),
                        LCANO = c.String(),
                        LCADT = c.DateTime(),
                        AWBNO = c.String(),
                        AWBDT = c.DateTime(),
                        HAWBNO = c.String(),
                        HAWBDT = c.DateTime(),
                        UNDTNO = c.String(),
                        UNDTDT = c.DateTime(),
                        MVESSEL = c.String(),
                        FVESSEL = c.String(),
                        MARKSNO = c.String(),
                        ETB = c.DateTime(),
                        BERTHSNO = c.String(),
                        ARRIVEDT = c.DateTime(),
                        ROTNO = c.String(),
                        LINENO = c.String(),
                        DELIVERYDT = c.DateTime(),
                        CLDT = c.DateTime(),
                        WFDT = c.DateTime(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.Cnf_JobID);
            
            CreateTable(
                "dbo.CNF_JOBEXP",
                c => new
                    {
                        cnf_JobEXPID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        TRANSDT = c.DateTime(),
                        TRANSYY = c.Long(),
                        TRANSNO = c.Long(),
                        JOBYY = c.Long(),
                        JOBTP = c.String(),
                        JOBNO = c.Long(),
                        EXPCD = c.Long(),
                        EXPSL = c.Long(),
                        EXPID = c.Long(),
                        EXPAMT = c.Decimal(precision: 18, scale: 2),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.cnf_JobEXPID);
            
            CreateTable(
                "dbo.CNF_JOBEXPMST",
                c => new
                    {
                        cnf_JobExpmstID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        TRANSDT = c.DateTime(),
                        TRANSYY = c.Long(),
                        TRANSNO = c.Long(),
                        JOBYY = c.Long(),
                        JOBTP = c.String(),
                        JOBNO = c.Long(),
                        EXPCD = c.Long(),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(nullable: false),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(nullable: false),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.cnf_JobExpmstID);
            
            CreateTable(
                "dbo.CNF_JOBRCV",
                c => new
                    {
                        CNF_JOBRCVID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        TRANSDT = c.DateTime(nullable: false),
                        TRANSYY = c.Long(nullable: false),
                        TRANSNO = c.Long(),
                        TRANSFOR = c.String(nullable: false),
                        JOBYY = c.Int(nullable: false),
                        JOBTP = c.String(),
                        JOBNO = c.Long(),
                        PARTYID = c.Long(),
                        DEBITCD = c.Long(),
                        REMARKS = c.String(),
                        AMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ADVSHOW = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.CNF_JOBRCVID);
            
            CreateTable(
                "dbo.CNF_PARTY",
                c => new
                    {
                        CNF_PARTYId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        PARTYID = c.Long(),
                        PARTYNM = c.String(nullable: false),
                        ADDRESS = c.String(),
                        CONTACTNO = c.String(nullable: false),
                        EMAILID = c.String(),
                        WEBID = c.String(),
                        APNM = c.String(),
                        APNO = c.String(),
                        STATUS = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.CNF_PARTYId);
            
            CreateTable(
                "dbo.GL_ACCHARMST",
                c => new
                    {
                        ACCHARMSTId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        HEADTP = c.Int(nullable: false),
                        HEADCD = c.Long(),
                        HEADNM = c.String(nullable: false),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.ACCHARMSTId);
            
            CreateTable(
                "dbo.GL_ACCHART",
                c => new
                    {
                        ACCHARTId = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        HEADTP = c.Int(nullable: false),
                        HEADCD = c.Long(),
                        ACCOUNTCD = c.Long(),
                        ACCOUNTNM = c.String(),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.ACCHARTId);
            
            CreateTable(
                "dbo.GL_COSTPMST",
                c => new
                    {
                        CostMstID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        COSTCID = c.Long(),
                        COSTCNM = c.String(nullable: false),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.CostMstID);
            
            CreateTable(
                "dbo.GL_COSTPOOL",
                c => new
                    {
                        CostPLID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        COSTCID = c.Long(),
                        COSTPID = c.Long(),
                        COSTPNM = c.String(nullable: false),
                        COSTPSID = c.String(),
                        REMARKS = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.CostPLID);
            
            CreateTable(
                "dbo.GL_MASTER",
                c => new
                    {
                        GL_MasterID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        TRANSTP = c.String(),
                        TRANSDT = c.DateTime(nullable: false),
                        TRANSMY = c.String(),
                        TRANSNO = c.Long(),
                        TRANSSL = c.Long(),
                        TRANSDRCR = c.String(),
                        TRANSFOR = c.String(),
                        TRANSMODE = c.String(),
                        COSTPID = c.Long(),
                        CREDITCD = c.Long(),
                        DEBITCD = c.Long(),
                        CHEQUENO = c.String(),
                        CHEQUEDT = c.DateTime(),
                        REMARKS = c.String(),
                        DEBITAMT = c.Decimal(precision: 18, scale: 2),
                        CREDITAMT = c.Decimal(precision: 18, scale: 2),
                        TABLEID = c.String(),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.GL_MasterID);
            
            CreateTable(
                "dbo.GL_STRANS",
                c => new
                    {
                        Gl_StransID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(),
                        TRANSTP = c.String(),
                        TRANSDT = c.DateTime(nullable: false),
                        TRANSMY = c.String(),
                        TRANSNO = c.Long(),
                        TRANSFOR = c.String(nullable: false),
                        TRANSMODE = c.String(),
                        COSTPID = c.Long(),
                        CREDITCD = c.Long(),
                        DEBITCD = c.Long(),
                        CHEQUENO = c.String(),
                        CHEQUEDT = c.DateTime(),
                        REMARKS = c.String(),
                        AMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USERPC = c.String(),
                        INSUSERID = c.Long(),
                        INSTIME = c.DateTime(),
                        INSIPNO = c.String(),
                        INSLTUDE = c.String(),
                        UPDUSERID = c.Long(),
                        UPDTIME = c.DateTime(),
                        UPDIPNO = c.String(),
                        UPDLTUDE = c.String(),
                    })
                .PrimaryKey(t => t.Gl_StransID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GL_STRANS");
            DropTable("dbo.GL_MASTER");
            DropTable("dbo.GL_COSTPOOL");
            DropTable("dbo.GL_COSTPMST");
            DropTable("dbo.GL_ACCHART");
            DropTable("dbo.GL_ACCHARMST");
            DropTable("dbo.CNF_PARTY");
            DropTable("dbo.CNF_JOBRCV");
            DropTable("dbo.CNF_JOBEXPMST");
            DropTable("dbo.CNF_JOBEXP");
            DropTable("dbo.CNF_JOB");
            DropTable("dbo.CNF_JOBBILL");
            DropTable("dbo.CNF_EXPMST");
            DropTable("dbo.CNF_EXPENSE");
            DropTable("dbo.CNF_COMMISSION");
            DropTable("dbo.AslUsercoes");
            DropTable("dbo.ASL_TABFIELD");
            DropTable("dbo.ASL_TABLE");
            DropTable("dbo.ASL_ROLE");
            DropTable("dbo.ASL_MENUMST");
            DropTable("dbo.ASL_MENU");
            DropTable("dbo.ASL_LOG");
            DropTable("dbo.ASL_DREPORT");
            DropTable("dbo.ASL_DELETE");
            DropTable("dbo.AslCompanies");
        }
    }
}
