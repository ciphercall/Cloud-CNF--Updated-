namespace Mvc_CNFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CNF_JOBDTL", "MISCCOSTRIDT", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CNF_JOBDTL", "IMPORTDUTY", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CNF_JOBDTL", "PCDEMURRAGE", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CNF_JOBDTL", "REWORKCOST", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CNF_JOBDTL", "SPCOST", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CNF_JOBDTL", "SPCOST");
            DropColumn("dbo.CNF_JOBDTL", "REWORKCOST");
            DropColumn("dbo.CNF_JOBDTL", "PCDEMURRAGE");
            DropColumn("dbo.CNF_JOBDTL", "IMPORTDUTY");
            DropColumn("dbo.CNF_JOBDTL", "MISCCOSTRIDT");
        }
    }
}
