using Immobilier.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immobilier.DataAccess.Repository.Contracts
{
    public interface IUserRepository
    {
        ulong CreateUser(User newUser);
        Task<User?> GetUserById(ulong userId);
        Task<User?> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> UpdateUser(User user);
    }
}
