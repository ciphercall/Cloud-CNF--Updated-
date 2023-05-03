namespace Mvc_CNFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CNF_JOB", "PCNO", c => c.String());
            AddColumn("dbo.CNF_JOB", "PCDT", c => c.DateTime());
            AddColumn("dbo.CNF_JOB", "SPAGNM", c => c.String());
            AddColumn("dbo.CNF_JOB", "FWAGNM", c => c.String());
            AddColumn("dbo.CNF_JOB", "DESTN", c => c.String());
            AddColumn("dbo.CNF_JOB", "PTEMP", c => c.String());
            AddColumn("dbo.CNF_JOB", "VATRNO", c => c.String());
            AddColumn("dbo.CNF_JOB", "CSEALNO", c => c.String());
            AddColumn("dbo.CNF_JOB", "REMARKS", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CNF_JOB", "REMARKS");
            DropColumn("dbo.CNF_JOB", "CSEALNO");
            DropColumn("dbo.CNF_JOB", "VATRNO");
            DropColumn("dbo.CNF_JOB", "PTEMP");
            DropColumn("dbo.CNF_JOB", "DESTN");
            DropColumn("dbo.CNF_JOB", "FWAGNM");
            DropColumn("dbo.CNF_JOB", "SPAGNM");
            DropColumn("dbo.CNF_JOB", "PCDT");
            DropColumn("dbo.CNF_JOB", "PCNO");
        }
    }
}
