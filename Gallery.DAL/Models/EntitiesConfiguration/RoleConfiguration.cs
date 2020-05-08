using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.EntitiesConfiguration
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            this.HasKey(r => r.Id);

            this.Property(r => r.Id)
                .IsRequired()
                .HasColumnName("RoleId");

            this.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(26);

            this.ToTable("Roles");
        }
    }
}