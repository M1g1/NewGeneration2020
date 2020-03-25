using System.Threading.Tasks;

namespace Gallery.DAL
{
    public interface IRepository
    {
        Task<bool> IsUserExistAsync(string email, string password);

        Task AddUserToDatabaseAsync(string email, string password);

        int GetUserId(string email);

        Task<bool> IsConnectionAvailableAsync();
       
    }
}
