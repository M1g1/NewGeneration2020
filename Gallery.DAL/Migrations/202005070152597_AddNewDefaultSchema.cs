namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewDefaultSchema : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Media", newSchema: "Gallery");
            MoveTable(name: "dbo.MediaTypes", newSchema: "Gallery");
            MoveTable(name: "dbo.Users", newSchema: "Gallery");
            MoveTable(name: "dbo.Roles", newSchema: "Gallery");
            MoveTable(name: "dbo.RolesUsers", newSchema: "Gallery");
        }
        
        public override void Down()
        {
            MoveTable(name: "Gallery.RolesUsers", newSchema: "dbo");
            MoveTable(name: "Gallery.Roles", newSchema: "dbo");
            MoveTable(name: "Gallery.Users", newSchema: "dbo");
            MoveTable(name: "Gallery.MediaTypes", newSchema: "dbo");
            MoveTable(name: "Gallery.Media", newSchema: "dbo");
        }
    }
}
