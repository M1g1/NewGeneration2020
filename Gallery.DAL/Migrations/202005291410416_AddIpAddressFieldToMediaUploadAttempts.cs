namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIpAddressFieldToMediaUploadAttempts : DbMigration
    {
        public override void Up()
        {
            AddColumn("Gallery.MediaUploadAttempts", "IpAddress", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("Gallery.MediaUploadAttempts", "IpAddress");
        }
    }
}
