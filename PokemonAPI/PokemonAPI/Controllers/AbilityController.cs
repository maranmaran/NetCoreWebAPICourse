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
    public class AbilityController : BaseController
    {
        private readonly IAbillityService _abillityService;

        public AbilityController(IAbillityService abillityService)
        {
            _abillityService = abillityService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            return Ok(await _abillityService.GetAll(cancellationToken));
        }
    }
}
