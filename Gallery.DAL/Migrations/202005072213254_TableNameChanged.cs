namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNameChanged : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Gallery.Attempts", newName: "LoginAttempts");
        }
        
        public override void Down()
        {
            RenameTable(name: "Gallery.LoginAttempts", newName: "Attempts");
        }
    }
}
