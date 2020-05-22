namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueIndexesFor_RoleName_MediaPath_MediaType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Gallery.Media", "Path", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("Gallery.Media", "Path", unique: true);
            CreateIndex("Gallery.MediaTypes", "Type", unique: true);
            CreateIndex("Gallery.Roles", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("Gallery.Roles", new[] { "Name" });
            DropIndex("Gallery.MediaTypes", new[] { "Type" });
            DropIndex("Gallery.Media", new[] { "Path" });
            AlterColumn("Gallery.Media", "Path", c => c.String(nullable: false));
        }
    }
}
