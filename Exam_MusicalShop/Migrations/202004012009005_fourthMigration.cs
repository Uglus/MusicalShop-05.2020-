namespace Exam_MusicalShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Storages", "Amount", c => c.Int(nullable: false));
            DropColumn("dbo.Storages", "Capacity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Storages", "Capacity", c => c.Int(nullable: false));
            DropColumn("dbo.Storages", "Amount");
        }
    }
}
