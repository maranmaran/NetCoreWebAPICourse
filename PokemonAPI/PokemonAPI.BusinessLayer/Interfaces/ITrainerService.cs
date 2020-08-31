using PokemonAPI.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerDTO>> GetAll(CancellationToken cancellation = default);
        Task<TrainerDTO> Get(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> Create(TrainerDTO trainer, CancellationToken cancellationToken = default);
        Task Update(TrainerDTO trainer, CancellationToken cancellationToken = default);
        Task Delete(Guid id, CancellationToken cancellationToken = default);
    }
}
