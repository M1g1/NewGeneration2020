using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.EntitiesConfiguration
{
    public class MediaConfiguration : EntityTypeConfiguration<Media>
    {
        public MediaConfiguration()
        {
            this.HasKey(m => m.Id);

            this.Property(m => m.Id)
                .IsRequired()
                .HasColumnName("MediaId");

            this.Property(m => m.Path)
                .IsRequired();

            this.HasRequired(m => m.Type)
                .WithMany(mt => mt.Media)
                .HasForeignKey(m => m.MediaTypeId);
            
            this.ToTable("Media");
        }
    }
}