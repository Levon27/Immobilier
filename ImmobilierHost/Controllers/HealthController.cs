using Microsoft.AspNetCore.Mvc;

namespace Immobilier.Host.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("")]
        public string Health() => "Healthy";
    }
}
