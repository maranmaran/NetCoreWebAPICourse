using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers
{
    public class CatchController : BaseController
    {
        private readonly ICatchService _catchService;

        public CatchController(ICatchService catchService)
        {
            _catchService = catchService;
        }

        [AllowAnonymous]
        [HttpGet("{pokemonId}/{trainerId}")]

        public async Task<IActionResult> Catch(Guid pokemonId, Guid trainerId, CancellationToken cancellationToken = default)
        {
            await _catchService.CatchPokemon(pokemonId, trainerId, cancellationToken);
            return Accepted();
        }

    }
}
