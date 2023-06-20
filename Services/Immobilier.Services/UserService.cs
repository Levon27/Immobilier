//using Immobilier.DataAccess.Repository.Contracts;
//using Immobilier.Domain;
//using Immobilier.Services.Contracts;
//using Microsoft.EntityFrameworkCore;

//namespace Immobilier.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly IUserRepository _userRepository;

//        public UserService(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public ulong CreateUser(User user)
//        {
//            var newUser = new User { Name = user.Name, Age = user.Age };
//            _context.Users.Add(newUser);
//            _context.SaveChanges();
//            return newUser.UserId;
//        }

//        public async Task<IEnumerable<User>> GetAllUsers()
//        {
//            return await _context.Users.ToArrayAsync();
//        }

//        public User? GetUserById(ulong userId)
//        {
//            return _context.Users.FirstOrDefault(u => u.UserId == userId);
//        }

//        public async Task<User?> UpdateUser(User user)
//        {
//            var userToEdit = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
//            if (userToEdit == null) return null;

//            userToEdit.Name = user.Name;
//            userToEdit.Age = user.Age;
//            //userToEdit.Properties = user.Properties;
//            _context.SaveChanges();

//            return userToEdit;
//        }
//    }
//}
