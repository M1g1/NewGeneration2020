using System.Data.Entity.ModelConfiguration;

namespace Gallery.DAL.Models.EntitiesConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasKey(u => u.Id);

            this.Property(u => u.Id)
                .IsRequired()
                .HasColumnName("UserId");

            this.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(64);
            this.HasIndex(u => u.Email)
                .IsUnique();

            this.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(26);

            this.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(ur =>
                {
                    ur.MapLeftKey("RoleId");
                    ur.MapRightKey("UserId");
                    ur.ToTable("RolesUsers");
                });

            this.HasMany(u => u.Media)
                .WithRequired(m => m.User)
                .HasForeignKey(m => m.UserId);

            this.ToTable("Users");
        }
    }
}
