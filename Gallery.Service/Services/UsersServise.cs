using Gallery.DAL;
using Gallery.Service.Contract;
using System;
using System.Threading.Tasks;

namespace Gallery.Service
{
    public class UsersService : IUsersService
    {
        private readonly IRepository _repo;
        public UsersService(IRepository repo)
        {
            _repo = repo 
                ?? throw new ArgumentNullException(nameof(repo));
        }
        public async Task<bool> IsUserExistAsync(string email, string password)
        {
            return await _repo.IsUserExistAsync(email, password);
        }
        public Task<UserDTO> FindUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }


    }
}
