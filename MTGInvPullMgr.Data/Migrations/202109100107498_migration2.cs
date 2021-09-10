namespace MTGInvPullMgr.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PullRequest", "ExpirationDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PullRequest", "ExpirationDateTime");
        }
    }
}
