using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CatchController> _logger;

        public CatchController(ICatchService catchService, ILogger<CatchController> logger)
        {
            _catchService = catchService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("{pokemonId}/{trainerId}")]

        public async Task<IActionResult> Catch(Guid pokemonId, Guid trainerId, CancellationToken cancellationToken = default)
        {
            await _catchService.CatchPokemon(pokemonId, trainerId, cancellationToken);
            _logger.LogInformation($"Pokemon: {pokemonId} was succesfully caught by trainer: {trainerId}");
            return Accepted();
        }

    }
}
