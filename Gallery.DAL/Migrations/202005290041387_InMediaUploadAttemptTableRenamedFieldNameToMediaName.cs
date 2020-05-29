namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InMediaUploadAttemptTableRenamedFieldNameToMediaName : DbMigration
    {
        public override void Up()
        {
            AddColumn("Gallery.MediaUploadAttempts", "MediaName", c => c.String(nullable: false, maxLength: 64));
            DropColumn("Gallery.MediaUploadAttempts", "Name");
        }
        
        public override void Down()
        {
            AddColumn("Gallery.MediaUploadAttempts", "Name", c => c.String(nullable: false, maxLength: 64));
            DropColumn("Gallery.MediaUploadAttempts", "MediaName");
        }
    }
}
