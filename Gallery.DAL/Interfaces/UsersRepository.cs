using Gallery.DAL.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Gallery.DAL
{
    public class UsersRepository : IRepository
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

        public async Task AddUserToDatabaseAsync(string email, string password)
        {
            _ctx.Users.Add(new User { Email = email, Password = password });
            await _ctx.SaveChangesAsync();
        }
        public async Task AddLoginAttemptToDatabaseAsync(string email, string ipAddress, bool isSuccess)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null)
                throw new ArgumentNullException(nameof(user));

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
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

    }

}
