using Immobilier.Domain;
using Immobilier.Infrastructure.Config;
using Immobilier.Infrastructure.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immobilier.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public ulong CreateUser(string name, string email, string password, int age)
        {
            var userToCreate = new User(name, password, email, age);
            var newUser = _context.Users.Add(userToCreate).Entity;

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

        public async Task<User?> GetAuthenticatedUser(string email, string password)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> UpdateUser(ulong id, string name, string email, uint age)
        {
            var userToEdit = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userToEdit == null) return null;

            userToEdit.Name = name;
            userToEdit.Age = (int)age;
            _context.SaveChanges();

            return userToEdit;
        }
    }
}
