namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttemptsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Gallery.Attempts",
                c => new
                    {
                        AttemptId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        IpAddress = c.String(nullable: false, maxLength: 64),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.AttemptId)
                .ForeignKey("Gallery.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Gallery.Attempts", "UserId", "Gallery.Users");
            DropIndex("Gallery.Attempts", new[] { "UserId" });
            DropTable("Gallery.Attempts");
        }
    }
}
