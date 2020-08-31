using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.BusinessLayer.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers
{
    public class BattleController : BaseController
    {
        private readonly IBattleService _battleService;

        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        [AllowAnonymous]
        [HttpGet("{firstPokemonId}/{secondPokemonId}")]
        public async Task<IActionResult> Battle(Guid firstPokemonId, Guid secondPokemonId, CancellationToken cancellationToken = default)
        {
            return Ok(await _battleService.Battle(firstPokemonId, secondPokemonId, cancellationToken));
        }
    }
}
