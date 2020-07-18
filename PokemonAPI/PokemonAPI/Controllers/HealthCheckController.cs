using Microsoft.AspNetCore.Mvc;

namespace PokemonAPI.Controllers
{
    /// <summary>
    /// Controller which provides endpoints to check if the system is healthy
    /// </summary>
    public class HealthCheckController : BaseController
    {

        /// <summary>
        /// Ping endpoint which returns Healthy if system is healthy
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Healthy");
        }
    }
}
