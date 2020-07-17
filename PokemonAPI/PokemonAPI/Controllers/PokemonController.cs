using Microsoft.AspNetCore.Mvc;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using System;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers
{

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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _pokemonService.Get(id));
        }

        /// <summary>
        /// Gets all pokemon
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pokemonService.GetAll());
        }

        /// <summary>
        /// Creates single pokemon
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pokemon pokemon)
        {
            return Ok(await _pokemonService.Create(pokemon));
        }

        /// <summary>
        /// Updates single pokemon
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Pokemon pokemon)
        {
            return Ok(await _pokemonService.Create(pokemon));
        }

        /// <summary>
        /// Deletes single pokemon with provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _pokemonService.Delete(id);
            return Ok();
        }
    }
}
