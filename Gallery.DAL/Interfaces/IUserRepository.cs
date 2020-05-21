using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public interface IUserRepository
    {
        Task<bool> IsUserExistAsync(string email, string password);
        Task<bool> IsUserExistAsync(string email);
        Task<bool> IsUserExistAsync(int id);
        Task AddUserToDatabaseAsync(User user);
        Task AddLoginAttemptToDatabaseAsync(User user, string ipAddress, bool isSuccess);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);

    }
}
