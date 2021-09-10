namespace MTGInvPullMgr.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        NameFirst = c.String(),
                        NameLast = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.DealerInventory",
                c => new
                    {
                        SKU = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ApiObjectURI = c.String(),
                        CurrentInventory = c.Int(nullable: false),
                        SetName = c.String(),
                        Set = c.String(),
                        CollectorNumber = c.Int(nullable: false),
                        IsFoil = c.Boolean(nullable: false),
                        IsVariant = c.Boolean(nullable: false),
                        Rarity = c.String(),
                        Lang = c.String(),
                    })
                .PrimaryKey(t => t.SKU);
            
            CreateTable(
                "dbo.PullRequestItem",
                c => new
                    {
                        PullRequestItemId = c.Int(nullable: false, identity: true),
                        PullRequestId = c.Int(nullable: false),
                        SKU = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PullRequestItemId)
                .ForeignKey("dbo.DealerInventory", t => t.SKU, cascadeDelete: true)
                .ForeignKey("dbo.PullRequest", t => t.PullRequestId, cascadeDelete: true)
                .Index(t => t.PullRequestId)
                .Index(t => t.SKU);
            
            CreateTable(
                "dbo.PullRequest",
                c => new
                    {
                        PullRequestId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        IsPulled = c.Boolean(nullable: false),
                        IsFinalized = c.Boolean(nullable: false),
                        IsPriority = c.Boolean(nullable: false),
                        TransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PullRequestId)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.PullRequestItem", "PullRequestId", "dbo.PullRequest");
            DropForeignKey("dbo.PullRequest", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.PullRequestItem", "SKU", "dbo.DealerInventory");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.PullRequest", new[] { "CustomerId" });
            DropIndex("dbo.PullRequestItem", new[] { "SKU" });
            DropIndex("dbo.PullRequestItem", new[] { "PullRequestId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.PullRequest");
            DropTable("dbo.PullRequestItem");
            DropTable("dbo.DealerInventory");
            DropTable("dbo.Customer");
        }
    }
}
