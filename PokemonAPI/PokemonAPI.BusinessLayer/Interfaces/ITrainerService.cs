using PokemonAPI.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerDTO>> GetAll(CancellationToken cancellation = default);
    }
}
