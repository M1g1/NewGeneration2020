namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedToMediaTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("Gallery.Media", "IsDeleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("Gallery.Media", "IsDeleted");
        }
    }
}
