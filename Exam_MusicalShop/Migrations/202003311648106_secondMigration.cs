namespace Exam_MusicalShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Disks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        BandId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        PublishYear = c.Int(nullable: false),
                        SelfPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bands", t => t.BandId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.BandId)
                .Index(t => t.GenreId)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.DiscountDisks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiskId = c.Int(nullable: false),
                        Percent = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disks", t => t.DiskId, cascadeDelete: true)
                .Index(t => t.DiskId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiskId = c.Int(nullable: false),
                        Percent = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disks", t => t.DiskId, cascadeDelete: true)
                .Index(t => t.DiskId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiskId = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disks", t => t.DiskId, cascadeDelete: true)
                .Index(t => t.DiskId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        Number = c.String(nullable: false, maxLength: 50),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiscountClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Percent = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Storages", "DiskId", "dbo.Disks");
            DropForeignKey("dbo.Disks", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.Prices", "DiskId", "dbo.Disks");
            DropForeignKey("dbo.Disks", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.DiscountDisks", "DiskId", "dbo.Disks");
            DropForeignKey("dbo.Disks", "BandId", "dbo.Bands");
            DropIndex("dbo.Storages", new[] { "DiskId" });
            DropIndex("dbo.Prices", new[] { "DiskId" });
            DropIndex("dbo.DiscountDisks", new[] { "DiskId" });
            DropIndex("dbo.Disks", new[] { "PublisherId" });
            DropIndex("dbo.Disks", new[] { "GenreId" });
            DropIndex("dbo.Disks", new[] { "BandId" });
            DropTable("dbo.DiscountClients");
            DropTable("dbo.Clients");
            DropTable("dbo.Storages");
            DropTable("dbo.Publishers");
            DropTable("dbo.Prices");
            DropTable("dbo.Genres");
            DropTable("dbo.DiscountDisks");
            DropTable("dbo.Disks");
            DropTable("dbo.Bands");
        }
    }
}
