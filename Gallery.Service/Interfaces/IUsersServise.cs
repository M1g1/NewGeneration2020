using Gallery.Service.Contract;
using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.Service
{
    public interface IUsersService
    { 
        Task<bool> IsUserExistAsync(string email, string password);
        Task<UserDTO> FindUserAsync(string email, string password);
        Task AddUserToDatabaseAsync(string email, string password);
        User GetUserByEmail(string email);
    }


}
