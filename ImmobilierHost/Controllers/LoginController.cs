using Immobilier.DataAccess.Repository.Contracts;
using Immobilier.Host.Requests;
using Immobilier.Infrastructure.Auth.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Immobilier.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userRepository.GetAuthenticatedUser(request.Email, request.Password);

            if (user == null) return Unauthorized();
            
            var token = _tokenService.GenerateToken(user);

            return Ok(new { user, token });
        }

    }
}
