using FluentValidation;
using Immobilier.DataAccess.Repository.Contracts;
using Immobilier.Domain;
using Immobilier.Interfaces.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Immobilier.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IValidator<Property> _propertyValidator;

        public PropertiesController(IPropertyRepository propertyRepository, IValidator<Property> propertyValidator)
        {
            _propertyRepository = propertyRepository;
            _propertyValidator = propertyValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _propertyRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(ulong id)
        {
            var user = await _propertyRepository.GetById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(Property property)
        {
            if (property == null) return BadRequest();
            var result = _propertyValidator.Validate(property);
            if (!result.IsValid) return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            _propertyRepository.CreateProperty(new Property(property.Name, property.Address, property.IdOwner));
 
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Put(Property property)
        {
            if (property == null) return BadRequest();
            var result = _propertyValidator.Validate(property);
            if (!result.IsValid) return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            var propertyToEdit = await _propertyRepository.GetById(property.Id);
            if (propertyToEdit == null) return NotFound();

            propertyToEdit.Name = property.Name;
            propertyToEdit.Address = property.Address;

            return Ok();
        }

    }
}
