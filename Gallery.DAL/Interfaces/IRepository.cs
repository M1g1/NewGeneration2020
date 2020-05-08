using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public interface IRepository
    {
        Task<bool> IsUserExistAsync(string email, string password);
        Task AddUserToDatabaseAsync(string email, string password);
        User GetUserByEmail(string email);

    }
}
