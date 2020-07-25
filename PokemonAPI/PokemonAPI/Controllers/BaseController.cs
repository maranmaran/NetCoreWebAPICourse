using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PokemonAPI.Controllers
{
    /// <summary>
    /// Base controller implementation
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
