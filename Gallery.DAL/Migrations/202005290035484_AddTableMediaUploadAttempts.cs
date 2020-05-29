namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableMediaUploadAttempts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Gallery.MediaUploadAttempts",
                c => new
                    {
                        AttemptId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        UserId = c.Int(nullable: false),
                        IsInProgress = c.Boolean(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.AttemptId)
                .ForeignKey("Gallery.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Gallery.MediaUploadAttempts", "UserId", "Gallery.Users");
            DropIndex("Gallery.MediaUploadAttempts", new[] { "UserId" });
            DropTable("Gallery.MediaUploadAttempts");
        }
    }
}
