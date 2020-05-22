using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.EntitiesConfiguration
{
    public class MediaTypeConfiguration : EntityTypeConfiguration<MediaType>
    {
        public MediaTypeConfiguration()
        {
            this.HasKey(mt => mt.Id);

            this.Property(mt => mt.Id)
                .IsRequired()
                .HasColumnName("MediaTypeId");

            this.Property(mt => mt.Type)
                .IsRequired()
                .HasMaxLength(16);

            this.HasIndex(mt => mt.Type)
                .IsUnique();

            this.ToTable("MediaTypes");
        }
    }
}