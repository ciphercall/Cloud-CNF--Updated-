namespace Mvc_CNFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ASL_ROLE", "ERRORCODE", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ASL_ROLE", "ERRORCODE");
        }
    }
}
