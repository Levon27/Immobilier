using Microsoft.AspNetCore.Mvc;

namespace Immobilier.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("")]
        public string Health() => "Healthy";
    }
}
