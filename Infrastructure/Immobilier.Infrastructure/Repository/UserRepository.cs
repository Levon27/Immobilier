using Immobilier.Domain;
using Immobilier.Infrastructure.Config;
using Immobilier.Infrastructure.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Immobilier.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public uint CreateUser(string name, string email, string password)
        {
            var userToCreate = new User(name, password, email);
            var newUser = _context.Users.Add(userToCreate).Entity;

            _context.SaveChanges();

            return newUser.Id;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.AsNoTracking().ToArrayAsync();
        }

        public async Task<User?> GetUserById(uint userId)
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

        public async Task<User?> UpdateUser(uint id, string name, string email)
        {
            var userToEdit = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userToEdit == null) return null;

            userToEdit.Name = name;
            _context.SaveChanges();

            return userToEdit;
        }
    }
}
