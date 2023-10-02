using Immobilier.Domain;

namespace Immobilier.Infrastructure.Repository.Contracts
{
    public interface IUserRepository
    {
        uint CreateUser(string name, string email, string password, int age);
        Task<User?> GetUserById(uint userId);
        Task<User?> GetAuthenticatedUser(string email, string password);
        Task<User?> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> UpdateUser(uint id, string name, string email, uint age);
    }
}
