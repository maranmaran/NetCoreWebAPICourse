using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.DTOModels;
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

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await _trainerService.Get(id, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrainerDTO trainer, CancellationToken cancellationToken = default)
        {
            return Ok(await _trainerService.Create(trainer, cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TrainerDTO trainer, CancellationToken cancellationToken = default)
        {
            await _trainerService.Update(trainer, cancellationToken);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await _trainerService.Delete(id, cancellationToken);
            return Accepted();
        }
    }
}
