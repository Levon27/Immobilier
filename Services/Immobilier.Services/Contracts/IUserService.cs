using Immobilier.Domain;

namespace Immobilier.Services.Contracts
{
    public interface IUserService
    {
        ulong CreateUser(User newUser);
        User? GetUserById(ulong userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> UpdateUser(User user);
    }
}
