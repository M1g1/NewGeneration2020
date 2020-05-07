using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.ModelsMappings
{
    public class MediaTypeMap : EntityTypeConfiguration<MediaType>
    {
        public MediaTypeMap()
        {
            this.HasKey(mt => mt.Id);

            this.Property(mt => mt.Id)
                .IsRequired()
                .HasColumnName("MediaTypeId");

            this.Property(mt => mt.Type)
                .IsRequired()
                .HasMaxLength(16);
        }
    }
}