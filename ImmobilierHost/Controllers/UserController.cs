using FluentValidation;
using Immobilier.Host.Requests;
using Immobilier.Infrastructure.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IValidator<CreateUserRequest> _createUserValidator;

        public UserController(IUserRepository userRepository, IValidator<CreateUserRequest> createUserValidator)
        {
            _userRepository = userRepository;
            _createUserValidator = createUserValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userRepository.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(uint id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserRequest request)
        {
            if (request == null) return BadRequest();
            var result = _createUserValidator.Validate(request);
            if (!result.IsValid) return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            var alreadyCreated = (await _userRepository.GetUserByEmail(request.Email)) != null;
            if (alreadyCreated) return BadRequest($"User with e-mail {request.Email} already exists!");

            var id = _userRepository.CreateUser(request.Name, request.Email, request.Password, request.Age);
            return Ok(new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditUserRequest request)
        {
            if (request == null) return BadRequest();
            //var result = _userValidator.Validate(user);
            //if (!result.IsValid) return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            var updatedUser = await _userRepository.UpdateUser(request.Id, request.Name, request.Email, request.Age);
            return Ok(new { updatedUser.Id });
        }
    }
}
