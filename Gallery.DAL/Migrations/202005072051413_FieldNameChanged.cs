namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldNameChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("Gallery.Media", "Path", c => c.String(nullable: false));
            DropColumn("Gallery.Media", "PathToMedia");
        }
        
        public override void Down()
        {
            AddColumn("Gallery.Media", "PathToMedia", c => c.String(nullable: false));
            DropColumn("Gallery.Media", "Path");
        }
    }
}
