using Microsoft.AspNetCore.Mvc;
namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet("/health-check")]
        public IActionResult HealthCheck()
        {
            return Ok(new
            {
                Status = "OK"
            });
        }
    }
}
