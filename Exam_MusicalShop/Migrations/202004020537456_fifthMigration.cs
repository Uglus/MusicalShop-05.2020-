namespace Exam_MusicalShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifthMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiskId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        BuyDateTime = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Disks", t => t.DiskId, cascadeDelete: true)
                .Index(t => t.DiskId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyHistories", "DiskId", "dbo.Disks");
            DropForeignKey("dbo.BuyHistories", "ClientId", "dbo.Clients");
            DropIndex("dbo.BuyHistories", new[] { "ClientId" });
            DropIndex("dbo.BuyHistories", new[] { "DiskId" });
            DropTable("dbo.BuyHistories");
        }
    }
}
