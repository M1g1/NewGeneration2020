namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbModelsMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RoleUsers", newName: "RolesUsers");
            RenameColumn(table: "dbo.Media", name: "Id", newName: "MediaId");
            RenameColumn(table: "dbo.MediaTypes", name: "Id", newName: "MediaTypeId");
            RenameColumn(table: "dbo.Users", name: "Id", newName: "UserId");
            RenameColumn(table: "dbo.Roles", name: "Id", newName: "RoleId");
            RenameColumn(table: "dbo.RolesUsers", name: "Role_Id", newName: "UserId");
            RenameColumn(table: "dbo.RolesUsers", name: "User_Id", newName: "RoleId");
            RenameIndex(table: "dbo.RolesUsers", name: "IX_User_Id", newName: "IX_RoleId");
            RenameIndex(table: "dbo.RolesUsers", name: "IX_Role_Id", newName: "IX_UserId");
            DropPrimaryKey("dbo.RolesUsers");
            AlterColumn("dbo.Media", "PathToMedia", c => c.String(nullable: false));
            AlterColumn("dbo.MediaTypes", "Type", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 26));
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false, maxLength: 26));
            AddPrimaryKey("dbo.RolesUsers", new[] { "RoleId", "UserId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RolesUsers");
            AlterColumn("dbo.Roles", "Name", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.MediaTypes", "Type", c => c.String());
            AlterColumn("dbo.Media", "PathToMedia", c => c.String());
            AddPrimaryKey("dbo.RolesUsers", new[] { "Role_Id", "User_Id" });
            RenameIndex(table: "dbo.RolesUsers", name: "IX_UserId", newName: "IX_Role_Id");
            RenameIndex(table: "dbo.RolesUsers", name: "IX_RoleId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.RolesUsers", name: "RoleId", newName: "User_Id");
            RenameColumn(table: "dbo.RolesUsers", name: "UserId", newName: "Role_Id");
            RenameColumn(table: "dbo.Roles", name: "RoleId", newName: "Id");
            RenameColumn(table: "dbo.Users", name: "UserId", newName: "Id");
            RenameColumn(table: "dbo.MediaTypes", name: "MediaTypeId", newName: "Id");
            RenameColumn(table: "dbo.Media", name: "MediaId", newName: "Id");
            RenameTable(name: "dbo.RolesUsers", newName: "RoleUsers");
        }
    }
}
