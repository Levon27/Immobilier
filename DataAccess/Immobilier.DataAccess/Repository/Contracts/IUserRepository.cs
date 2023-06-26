using Immobilier.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immobilier.DataAccess.Repository.Contracts
{
    public interface IUserRepository
    {
        ulong CreateUser(User newUser);
        Task<User?> GetUserById(ulong userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> UpdateUser(User user);
    }
}
