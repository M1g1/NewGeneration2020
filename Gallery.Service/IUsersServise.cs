using Gallery.Service.Contract;
using System.Threading.Tasks;

namespace Gallery.Service
{
    public interface IUserService
    { 
        Task<bool> IsUserExistAsync(string username, string password);
        Task<UserDTO> FindUserAsync(string username, string password);
    }


}
