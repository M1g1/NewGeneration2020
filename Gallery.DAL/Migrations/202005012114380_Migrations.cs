namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Media", "MediaTypeId", "dbo.MediaTypes");
            DropForeignKey("dbo.Media", "UserId", "dbo.Users");
            DropIndex("dbo.Media", new[] { "UserId" });
            DropIndex("dbo.Media", new[] { "MediaTypeId" });
            AddColumn("dbo.Media", "PathToMedia", c => c.String());
            AlterColumn("dbo.Media", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Media", "MediaTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.MediaTypes", "Type", c => c.String());
            CreateIndex("dbo.Media", "UserId");
            CreateIndex("dbo.Media", "MediaTypeId");
            AddForeignKey("dbo.Media", "MediaTypeId", "dbo.MediaTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Media", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.Media", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Media", "Name", c => c.String());
            DropForeignKey("dbo.Media", "UserId", "dbo.Users");
            DropForeignKey("dbo.Media", "MediaTypeId", "dbo.MediaTypes");
            DropIndex("dbo.Media", new[] { "MediaTypeId" });
            DropIndex("dbo.Media", new[] { "UserId" });
            AlterColumn("dbo.MediaTypes", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.Media", "MediaTypeId", c => c.Int());
            AlterColumn("dbo.Media", "UserId", c => c.Int());
            DropColumn("dbo.Media", "PathToMedia");
            CreateIndex("dbo.Media", "MediaTypeId");
            CreateIndex("dbo.Media", "UserId");
            AddForeignKey("dbo.Media", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Media", "MediaTypeId", "dbo.MediaTypes", "Id");
        }
    }
}
