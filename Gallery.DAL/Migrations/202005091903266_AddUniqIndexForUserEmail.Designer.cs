﻿// <auto-generated />
namespace Gallery.DAL.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.4.0")]
    public sealed partial class AddUniqIndexForUserEmail : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddUniqIndexForUserEmail));
        
        string IMigrationMetadata.Id
        {
            get { return "202005091903266_AddUniqIndexForUserEmail"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
