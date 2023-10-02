using FluentValidation;
using Immobilier.Domain;
using Immobilier.Host.Requests;
using Immobilier.Infrastructure.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Immobilier.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreatePropertyRequest> _createPropertyValidator;
        private readonly IValidator<EditPropertyRequest> _editPropertyValidator;

        public PropertiesController(IPropertyRepository propertyRepository, IValidator<CreatePropertyRequest> createPropertyValidator, IValidator<EditPropertyRequest> editPropertyValidator, IUserRepository userRepository)
        {
            _propertyRepository = propertyRepository;
            _createPropertyValidator = createPropertyValidator;
            _editPropertyValidator = editPropertyValidator;
            _userRepository = userRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _propertyRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(ulong id)
        {
            var user = await _propertyRepository.GetById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePropertyRequest request)
        {
            if (request == null) return BadRequest();
            var result = _createPropertyValidator.Validate(request);

            if (!result.IsValid) return BadRequest(result.Errors.Select(e => e.ErrorMessage));
            var owner = await _userRepository.GetUserByEmail(GetEmailFromToken());
            if (owner == null) return NotFound();

            var created = _propertyRepository.CreateProperty(new Property(request.Name, request.Address, owner.Id));
 
            return Ok(created);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(uint propertyId) 
        {
            if (propertyId == 0) return BadRequest();
            var deleted = await  _propertyRepository.DeleteProperty(propertyId);
            return Ok(deleted);
        }

        [HttpPut()]
        public async Task<IActionResult> Put(EditPropertyRequest request)
        {
            if (request == null) return BadRequest();
            var result = _editPropertyValidator.Validate(request);
            if (!result.IsValid) return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            var propertyToEdit = await _propertyRepository.GetById(request.Id);
            if (propertyToEdit == null) return NotFound();

            propertyToEdit.Name = request.Name;
            propertyToEdit.Address = request.Address;

            return Ok();
        }

        private string GetEmailFromToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) return null;
            
            return identity.FindFirst(ClaimTypes.Email).Value; 
        }
    }
}
