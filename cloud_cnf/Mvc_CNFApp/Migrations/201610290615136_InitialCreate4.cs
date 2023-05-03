namespace Mvc_CNFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CNF_JOBDTL",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        COMPID = c.Long(nullable: false),
                        JOBTP = c.String(),
                        JOBYY = c.Long(nullable: false),
                        JOBNO = c.Long(nullable: false),
                        SBANKPAY = c.Decimal(precision: 18, scale: 2),
                        LABORCHG = c.Decimal(precision: 18, scale: 2),
                        SSHIPCHG = c.Decimal(precision: 18, scale: 2),
                        TRANSCHG = c.Decimal(precision: 18, scale: 2),
                        HMISCCHG = c.Decimal(precision: 18, scale: 2),
                        RECPTCHG = c.Decimal(precision: 18, scale: 2),
                        OTHERCHG = c.Decimal(precision: 18, scale: 2),
                        DEMURCHG = c.Decimal(precision: 18, scale: 2),
                        DOCPCOST = c.Decimal(precision: 18, scale: 2),
                        EPVALUE = c.Decimal(precision: 18, scale: 2),
                        ITCPCUST = c.Decimal(precision: 18, scale: 2),
                        ITCFINVV = c.Decimal(precision: 18, scale: 2),
                        ITCPEMP = c.Decimal(precision: 18, scale: 2),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CNF_JOBDTL");
        }
    }
}
