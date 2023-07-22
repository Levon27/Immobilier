using Immobilier.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immobilier.DataAccess.Repository.Contracts
{
    public interface IUserRepository
    {
        ulong CreateUser(string name, string email, string password, int age);
        Task<User?> GetUserById(ulong userId);
        Task<User?> GetAuthenticatedUser(string email, string password);
        Task<User?> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> UpdateUser(ulong id, string name, string email, uint age);
    }
}
