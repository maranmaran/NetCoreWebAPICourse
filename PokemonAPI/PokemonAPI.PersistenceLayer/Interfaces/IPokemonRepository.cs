using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PokemonAPI.DomainLayer.Entities;

namespace PokemonAPI.PersistenceLayer.Repositories
{
    public interface IPokemonRepository : IRepository<Pokemon>
    {
        new Task<IEnumerable<Pokemon>> GetAll(CancellationToken cancellationToken = default);
    }
}