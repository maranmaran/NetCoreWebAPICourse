using Microsoft.AspNetCore.Mvc;

namespace PokemonAPI.Controllers
{
    /// <summary>
    /// Base controller implementation
    /// </summary>
    [ApiController]
    [Route("{controller}/{action}")]
    public abstract class BaseController : ControllerBase
    {
    }
}
