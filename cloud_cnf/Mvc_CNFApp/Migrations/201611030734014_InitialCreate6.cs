namespace Mvc_CNFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CNF_JOBDTL", "BILLREF", c => c.String());
            AddColumn("dbo.CNF_JOBDTL", "BILLRANGE", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CNF_JOBDTL", "BILLRANGE");
            DropColumn("dbo.CNF_JOBDTL", "BILLREF");
        }
    }
}
