using Immobilier.DataAccess.Config;
using Immobilier.DataAccess.Repository.Contracts;
using Immobilier.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immobilier.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public ulong CreateUser(User user)
        {
            var newUser = _context.Users.Add(user).Entity;
            _context.SaveChanges();
            return newUser.Id;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.AsNoTracking().ToArrayAsync();
        }

        public async Task<User?> GetUserById(ulong userId)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> UpdateUser(User user)
        {
            var userToEdit = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (userToEdit == null) return null;

            userToEdit.Name = user.Name;
            userToEdit.Age = user.Age;
            _context.SaveChanges();

            return userToEdit;
        }

    }
}
