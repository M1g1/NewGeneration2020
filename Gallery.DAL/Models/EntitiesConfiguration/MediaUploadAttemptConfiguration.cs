using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.EntitiesConfiguration
{
    public class MediaUploadAttemptConfiguration : EntityTypeConfiguration<MediaUploadAttempt>
    {
        public MediaUploadAttemptConfiguration()
        {
            this.HasKey(a => a.Id);

            this.Property(a => a.Id)
                .IsRequired()
                .HasColumnName("AttemptId");

            this.Property(a => a.Label)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(a => a.IsInProgress)
                .IsRequired();

            this.Property(a => a.IsSuccess)
                .IsRequired();

            this.Property(a => a.TimeStamp)
                .IsRequired()
                .HasColumnType("datetime2");

            this.HasRequired(a => a.User)
                .WithMany(u => u.MediaUploadAttempts)
                .HasForeignKey(a => a.UserId);

            this.ToTable("MediaUploadAttempts");
        }
    }
}