using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public class UsersRepository : IUserRepository
    {
        private readonly GalleryDbContext _ctx;

        public UsersRepository(GalleryDbContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }


        public async Task<bool> IsUserExistAsync(string email, string password)
        {
            return await _ctx.Users.AnyAsync(u => u.Email == email.Trim().ToLower() &&
                                             u.Password == password);
        }

        public async Task<bool> IsUserExistAsync(string email)
        {
            return await _ctx.Users.AnyAsync(u => u.Email == email.Trim().ToLower());
        }

        public async Task AddUserToDatabaseAsync(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
        }
        public async Task AddLoginAttemptToDatabaseAsync(User user, string ipAddress, bool isSuccess)
        {
            _ctx.LoginAttempts.Add(
                new LoginAttempt
                {
                    IsSuccess = isSuccess,
                    IpAddress = ipAddress,
                    User = user,
                    TimeStamp = DateTime.Now
                });

            await _ctx.SaveChangesAsync();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email.Trim().ToLower());
        }

    }

}
