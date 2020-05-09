using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public interface IRepository
    {
        Task<bool> IsUserExistAsync(string email, string password);
        Task<bool> IsUserExistAsync(string email);
        Task AddUserToDatabaseAsync(string email, string password);
        Task AddLoginAttemptToDatabaseAsync(string email, string ipAddress, bool isSuccess);
        Task<User> GetUserByEmailAsync(string email);

    }
}
