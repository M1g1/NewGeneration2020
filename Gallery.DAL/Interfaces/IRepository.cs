using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL
{
    public interface IRepository
    {
        Task<bool> IsUserExistAsync(string email, string password);

        Task AddUserToDatabase(string email, string password);

        int GetUserId(string email);

        Task<bool> IsConnectionAvailableAsync();
       
    }
}
