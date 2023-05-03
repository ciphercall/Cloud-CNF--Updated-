namespace Mvc_CNFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CNF_JOB", "PartyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CNF_JOB", "PartyName");
        }
    }
}
