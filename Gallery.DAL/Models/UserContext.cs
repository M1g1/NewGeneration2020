using System.Data.Entity;
using Gallery.DAL.Models.ModelsMappings;

namespace Gallery.DAL.Models
{
    public class UserContext : DbContext
    {
        public UserContext() { }
        public UserContext(string nameOrConnectionString) : base(nameOrConnectionString)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new MediaMap());
            modelBuilder.Configurations.Add(new MediaTypeMap());
        }
    }
}
