using PokemonAPI.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PokemonAPI.PersistenceLayer.DTOModels;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    /// <summary>
    /// CRUD service for pokemons
    /// </summary>
    public interface IPokemonService
    {
        Task<IEnumerable<PokemonDTO>> GetAll(CancellationToken cancellationToken = default);
        Task<PokemonDTO> Get(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> Create(PokemonDTO pokemon, CancellationToken cancellationToken = default);
        Task Update(PokemonDTO pokemon, CancellationToken cancellationToken = default);
        Task Delete(Guid id, CancellationToken cancellationToken = default);
    }
}
