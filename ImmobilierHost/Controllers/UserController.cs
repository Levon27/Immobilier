using FluentValidation;
using Immobilier.DataAccess.Repository.Contracts;
using Immobilier.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Immobilier.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;

        public UserController(IUserRepository userRepository, IValidator<User> createUserValidator)
        {
            _userRepository = userRepository;
            _userValidator = createUserValidator;
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
        public IActionResult Post(User user)
        {
            if (user == null) return BadRequest();
            var result = _userValidator.Validate(user);
            if (!result.IsValid) return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            var id = _userRepository.CreateUser(user);
            return Ok(new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(User user)
        {
            if (user == null) return BadRequest();
            var result = _userValidator.Validate(user);
            if (!result.IsValid) return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            var updatedUser = await _userRepository.UpdateUser(user);
            return Ok(new { updatedUser.Id });
        }
    }
}
