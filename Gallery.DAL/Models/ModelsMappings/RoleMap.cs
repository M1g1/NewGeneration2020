using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.ModelsMappings
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.HasKey(r => r.Id);

            this.Property(r => r.Id)
                .IsRequired()
                .HasColumnName("RoleId");

            this.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(26);
        }
    }
}