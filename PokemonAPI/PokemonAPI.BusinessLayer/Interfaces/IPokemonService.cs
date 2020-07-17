using PokemonAPI.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    /// <summary>
    /// CRUD service for pokemons
    /// </summary>
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> GetAll();
        Task<Pokemon> Get(Guid id);
        Task<Guid> Create(Pokemon pokemon);
        Task Update(Guid id, Pokemon pokemon);
        Task Delete(Guid id);
    }
}
