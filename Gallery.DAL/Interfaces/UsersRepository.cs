﻿using Gallery.DAL.Models;
using System;
using System.Data.Entity;
using System.Linq;
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
                                             u.Password == password.Trim());
        }

        public async Task AddUserToDatabaseAsync(string email, string password)
        {
            _ctx.Users.Add(new User { Email = email, Password = password });
            await _ctx.SaveChangesAsync();
        }
  
        public User GetUserByEmail(string email)
        {
            return _ctx.Users.Where(u => u.Email == email).Select(u => u).FirstOrDefault();
        }

    }

}
