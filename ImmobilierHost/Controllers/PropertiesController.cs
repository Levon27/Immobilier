using Immobilier.DataAccess.Repository.Contracts;
using Immobilier.Domain;
using Immobilier.Interfaces.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Immobilier.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertiesController(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
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
        public IActionResult Post(CreatePropertyRequest request)
        {
            if (request == null) return BadRequest();
            _propertyRepository.CreateProperty(new Property(request.Name, request.Address, request.IdOwner));
 
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Put(Property property)
        {
            if (property == null) return BadRequest();

            var propertyToEdit = await _propertyRepository.GetById(property.Id);
            if (propertyToEdit == null) return NotFound();

            propertyToEdit.Name = property.Name;
            propertyToEdit.Address = property.Address;

            return Ok();
        }

    }
}
