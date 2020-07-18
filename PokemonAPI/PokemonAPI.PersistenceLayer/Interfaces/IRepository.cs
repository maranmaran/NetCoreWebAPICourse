using PokemonAPI.DomainLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.PersistenceLayer
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// Gets all entities
        /// </summary>
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets single entity by ID
        /// If you wish another way to fetch you can implement specific IRepository
        /// </summary>
        Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts single entity
        /// </summary>
        Task<Guid> Insert(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates single entity
        /// </summary>
        Task Update(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes single entity
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken = default);
    }
}