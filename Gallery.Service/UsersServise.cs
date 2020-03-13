using Gallery.DAL;
using Gallery.Service.Contract;
using System;
using System.Threading.Tasks;

namespace Gallery.Service
{
    public class UsersService : IUserService
    {
        private readonly IRepository _repo;
        public UsersService(IRepository repo)
        {
            _repo = repo 
                ?? throw new ArgumentNullException(nameof(repo));
        }
        public async Task<bool> IsUserExistAsync(string username, string password)
        {
            return await _repo.IsUserExistAsync(username, password);
        }
        public Task<UserDTO> FindUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

    }
}
