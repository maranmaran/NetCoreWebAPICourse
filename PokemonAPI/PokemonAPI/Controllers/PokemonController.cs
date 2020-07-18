﻿using Microsoft.AspNetCore.Mvc;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers
{

    /// <summary>
    /// Pokemon CRUD management controller
    /// </summary>
    public class PokemonController : BaseController
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        /// <summary>
        /// Gets single pokemon
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await _pokemonService.Get(id, cancellationToken));
        }

        /// <summary>
        /// Gets all pokemon
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            return Ok(await _pokemonService.GetAll(cancellationToken));
        }

        /// <summary>
        /// Creates single pokemon
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            return Ok(await _pokemonService.Create(pokemon, cancellationToken));
        }

        /// <summary>
        /// Updates single pokemon
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            await _pokemonService.Update(pokemon, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Deletes single pokemon with provided id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await _pokemonService.Delete(id, cancellationToken);
            return Ok();
        }
    }
}
