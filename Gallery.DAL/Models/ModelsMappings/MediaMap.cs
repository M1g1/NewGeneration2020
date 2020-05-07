using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.ModelsMappings
{
    public class MediaMap : EntityTypeConfiguration<Media>
    {
        public MediaMap()
        {
            this.HasKey(m => m.Id);

            this.Property(m => m.Id)
                .IsRequired()
                .HasColumnName("MediaId");

            this.Property(m => m.PathToMedia)
                .IsRequired();

            this.HasRequired(m => m.Type)
                .WithMany(mt => mt.Media)
                .HasForeignKey(m => m.MediaTypeId);
            
            this.ToTable("Media");
        }
    }
}