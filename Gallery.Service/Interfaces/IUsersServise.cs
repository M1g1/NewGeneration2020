using Gallery.Service.Contract;
using System.Threading.Tasks;

namespace Gallery.Service
{
    public interface IUsersService
    { 
        Task<bool> IsUserExistAsync(string email, string password);
        Task<UserDTO> FindUserAsync(string email, string password);
        int GetUserId(string email);
    }


}
