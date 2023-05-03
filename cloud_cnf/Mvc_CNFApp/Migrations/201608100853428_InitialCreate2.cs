namespace Mvc_CNFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CNF_PARTY", "EMAILID2", c => c.String());
            AddColumn("dbo.CNF_PARTY", "CONTACTNO2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CNF_PARTY", "CONTACTNO2");
            DropColumn("dbo.CNF_PARTY", "EMAILID2");
        }
    }
}
