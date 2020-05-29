using Gallery.Service.Contract;
using System.Threading.Tasks;

namespace Gallery.Service
{
    public interface IUsersService
    { 
        Task<bool> IsUserExistAsync(string email, string password);
        Task<bool> IsUserExistAsync(string email);
        Task<bool> IsUserExistAsync(int id);
        Task AddUserToDatabaseAsync(UserDto userDto);
        Task AddLoginAttemptToDatabaseAsync(LoginAttemptDto loginAttemptDto);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserDto> GetUserByIdAsync(int id);
    }

}
