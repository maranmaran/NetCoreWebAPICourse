using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Implementations
{
    internal class PokemonService : IPokemonService
    {
        public Task<IEnumerable<Pokemon>> GetAll()
        {
            throw new NotImplementedException("Getting all");
        }

        public async Task<Pokemon> Get(Guid id)
        {
            throw new NotImplementedException($"Get {id}");
        }

        public async Task<Guid> Create(Pokemon pokemon)
        {
            throw new NotImplementedException("Creating");
        }

        public async Task Update(Guid id, Pokemon pokemon)
        {
            throw new NotImplementedException($"Updating {id}");
        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException($"Deleting ${id}");
        }
    }
}
