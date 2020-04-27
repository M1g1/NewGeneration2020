using System.Data.Entity;

namespace Gallery.DAL.Models
{
    public class UserContext : DbContext
    {
        public UserContext(string nameOrConnectionString) : base(nameOrConnectionString)
        { }
        public DbSet<User> Users { get; set; }
    }
}
