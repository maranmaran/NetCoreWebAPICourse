using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers
{
    public class TrainerController : BaseController
    {
        private readonly ITrainerService _trainerService;
        
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            return Ok(await _trainerService.GetAll(cancellationToken));
        }
    }
}
