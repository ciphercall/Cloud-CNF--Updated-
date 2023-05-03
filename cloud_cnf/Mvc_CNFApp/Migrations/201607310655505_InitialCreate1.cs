namespace Mvc_CNFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CNF_PARTY", "REMARKS", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CNF_PARTY", "REMARKS");
        }
    }
}
