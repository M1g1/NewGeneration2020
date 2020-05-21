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
        public Task<UserDto> FindUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task AddUserToDatabaseAsync(UserDto userDto)
        {
            await _userRepo.AddUserToDatabaseAsync(new User { Email = userDto.Email, Password = userDto.Password });
        }

        public async Task AddLoginAttemptToDatabaseAsync(UserDto userDto, string ipAddress, bool isSuccess)
        {
            var user = await _userRepo.GetUserByEmailAsync(userDto.Email);
            await _userRepo.AddLoginAttemptToDatabaseAsync(user, ipAddress, isSuccess);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepo.GetUserByEmailAsync(email);
            return new UserDto{ Id = user.Id, Email = user.Email, Password = user.Password };
        }
    }
}
