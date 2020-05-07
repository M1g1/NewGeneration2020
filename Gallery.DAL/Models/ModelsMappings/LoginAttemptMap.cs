using System.Data.Entity.ModelConfiguration;
using System.Runtime.CompilerServices;

namespace Gallery.DAL.Models.ModelsMappings
{
    public class LoginAttemptMap : EntityTypeConfiguration<LoginAttempt>
    {
        public LoginAttemptMap()
        {
            this.HasKey(a => a.Id);

            this.Property(a => a.Id)
                .IsRequired()
                .HasColumnName("AttemptId");

            this.Property(a => a.IsSuccess)
                .IsRequired();

            this.Property(a => a.IpAddress)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(a => a.TimeStamp)
                .IsRequired()
                .HasColumnType("datetime2");

            this.HasRequired(a => a.User)
                .WithMany(u => u.LoginAttempts)
                .HasForeignKey(a => a.UserId);

            this.ToTable("LoginAttempts");
        }
    }
}