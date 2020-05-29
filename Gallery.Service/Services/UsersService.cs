using Gallery.DAL;
using Gallery.Service.Contract;
using System;
using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepo;
        public UsersService(IUserRepository repo)
        {
            _userRepo = repo
                ?? throw new ArgumentNullException(nameof(repo));
        }
        public async Task<bool> IsUserExistAsync(string email, string password)
        {
            return await _userRepo.IsUserExistAsync(email, password);
        }
        public async Task<bool> IsUserExistAsync(string email)
        {
            return await _userRepo.IsUserExistAsync(email);
        }

        public async Task<bool> IsUserExistAsync(int id)
        {
            return await _userRepo.IsUserExistAsync(id);
        }


        public async Task AddUserToDatabaseAsync(UserDto userDto)
        {
            await _userRepo.AddUserToDatabaseAsync(new User { Email = userDto.Email, Password = userDto.Password });
        }

        public async Task AddLoginAttemptToDatabaseAsync(LoginAttemptDto loginAttemptDto)
        {

            await _userRepo.AddLoginAttemptToDatabaseAsync(new LoginAttempt
            {
                IsSuccess = loginAttemptDto.IsSuccess,
                IpAddress = loginAttemptDto.IpAddress,
                UserId = loginAttemptDto.UserId,
                TimeStamp = loginAttemptDto.TimeStamp
            });
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepo.GetUserByEmailAsync(email);
            if (user != null)
                return new UserDto { Id = user.Id, Email = user.Email, Password = user.Password };
            return null;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user != null)
                return new UserDto { Id = user.Id, Email = user.Email, Password = user.Password };
            return null;
        }
    }
}
