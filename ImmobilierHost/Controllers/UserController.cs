using Immobilier.DataAccess.Repository.Contracts;
using Immobilier.Domain;
using Immobilier.Interfaces.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Immobilier.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userRepository.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(ulong id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(CreateUserRequest request)
        {
            if (request == null) return BadRequest();
            var id = _userRepository.CreateUser(new User(request.UserName, request.Email, request.Age));
            return Ok(new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(User user)
        {
            if (user == null) return BadRequest();
            var updatedUser = await _userRepository.UpdateUser(user);
            return Ok(new { Id = updatedUser.Id });
        }

        /* dados teste 
            //var userTeste = new User { Age = 25, Name = "Jonas teste", Properties = { }, UserId = 123 };
            //_context.Users.Add(userTeste);
            //_context.SaveChanges();
        */
    }
}
