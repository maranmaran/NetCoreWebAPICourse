using Microsoft.AspNetCore.Mvc;

namespace PokemonAPI.Controllers
{
    public class HealthCheckController : BaseController
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Healthy");
        }
    }
}
