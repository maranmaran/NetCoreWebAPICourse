using PokemonAPI.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    /// <summary>
    /// CRUD service for pokemons
    /// </summary>
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> GetAll(CancellationToken cancellationToken = default);
        Task<Pokemon> Get(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> Create(Pokemon pokemon, CancellationToken cancellationToken = default);
        Task Update(Pokemon pokemon, CancellationToken cancellationToken = default);
        Task Delete(Guid id, CancellationToken cancellationToken = default);
    }
}
