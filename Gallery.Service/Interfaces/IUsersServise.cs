using Gallery.Service.Contract;
using System.Threading.Tasks;

namespace Gallery.Service
{
    public interface IUsersService
    { 
        Task<bool> IsUserExistAsync(string email, string password);
        Task<bool> IsUserExistAsync(string email);
        Task<UserDto> FindUserAsync(string email, string password);
        Task AddUserToDatabaseAsync(string email, string password);
        Task AddLoginAttemptToDatabaseAsync(string email, string ipAddress, bool isSuccess);
        Task<UserDto> GetUserByEmailAsync(string email);
    }

}
