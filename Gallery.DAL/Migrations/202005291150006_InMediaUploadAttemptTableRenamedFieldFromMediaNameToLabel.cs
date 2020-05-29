namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InMediaUploadAttemptTableRenamedFieldFromMediaNameToLabel : DbMigration
    {
        public override void Up()
        {
            AddColumn("Gallery.MediaUploadAttempts", "Label", c => c.String(nullable: false, maxLength: 64));
            DropColumn("Gallery.MediaUploadAttempts", "MediaName");
        }
        
        public override void Down()
        {
            AddColumn("Gallery.MediaUploadAttempts", "MediaName", c => c.String(nullable: false, maxLength: 64));
            DropColumn("Gallery.MediaUploadAttempts", "Label");
        }
    }
}
