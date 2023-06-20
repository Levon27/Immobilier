using Immobilier.Host.Config;
using Immobilier.Domain;
using Immobilier.Interfaces.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Immobilier.DataAccess.Config;

namespace Immobilier.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PropertiesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Properties.ToArrayAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(ulong id)
        {
            var user = _context.Properties.FirstOrDefault(p => p.PropertyId == id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreatePropertyRequest request)
        {
            if (request == null) return BadRequest();
            var newProperty = new Property { Address = request.Address, Name = request.Name };
            _context.Properties.Add(newProperty);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Property property, ulong id)
        {
            if (property == null) return BadRequest();

            var propertyToEdit = await _context.Properties.FirstOrDefaultAsync(p => p.PropertyId == id);
            if (propertyToEdit == null) return NotFound();

            propertyToEdit.Name = property.Name;
            propertyToEdit.Address = property.Address;

            _context.SaveChanges();

            return Ok();
        }

    }
}
