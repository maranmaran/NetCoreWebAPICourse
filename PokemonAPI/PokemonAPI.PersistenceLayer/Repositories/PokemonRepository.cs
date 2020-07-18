using Microsoft.EntityFrameworkCore;
using PokemonAPI.DomainLayer;
using PokemonAPI.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.PersistenceLayer.Repositories
{
    internal class PokemonRepository : Repository<Pokemon>, IPokemonRepository
    {

        public PokemonRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Pokemon>> GetAll(CancellationToken cancellationToken = default)
        {
            return await Entities
                .Include(x => x.Abilities)
                .ThenInclude(x => x.Ability)
                .ToListAsync(cancellationToken);
        }
    }
}
