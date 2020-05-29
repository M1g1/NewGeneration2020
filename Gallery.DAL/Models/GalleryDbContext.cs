using System.Data.Entity;
using Gallery.DAL.Models.EntitiesConfiguration;

namespace Gallery.DAL.Models
{
    public class GalleryDbContext : DbContext
    {
        public GalleryDbContext() { }
        public GalleryDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<LoginAttempt> LoginAttempts { get; set; }
        public DbSet<MediaUploadAttempt> MediaUploadAttempts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Gallery");
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new MediaConfiguration());
            modelBuilder.Configurations.Add(new MediaTypeConfiguration());
            modelBuilder.Configurations.Add(new LoginAttemptConfiguration());
        }
    }
}
