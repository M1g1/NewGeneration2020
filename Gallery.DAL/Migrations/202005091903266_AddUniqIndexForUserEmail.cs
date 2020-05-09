namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqIndexForUserEmail : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Gallery.Users", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("Gallery.Users", new[] { "Email" });
        }
    }
}
