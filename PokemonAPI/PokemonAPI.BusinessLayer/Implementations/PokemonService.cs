using Microsoft.EntityFrameworkCore;
using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer;
using PokemonAPI.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Implementations
{
    internal class PokemonService : IPokemonService
    {
        private readonly ApplicationDbContext _context;
        public PokemonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pokemon>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.Pokemons.Include(x => x.BaseStats).Include(x => x.Abilities).ThenInclude(x => x.Ability).ToListAsync(cancellationToken);
        }

        public async Task<Pokemon> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(pok => pok.Id == id, cancellationToken);
            if (pokemon == null)
                throw new NotFoundException(id);

            return pokemon;
        }

        public async Task<Guid> Create(Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Pokemons.AddAsync(pokemon, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return pokemon.Id;
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }

        public async Task Update(Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            try
            {
                _context.Pokemons.Update(pokemon);

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new UpdateException(pokemon.Id, e);
            }
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (pokemon == null)
                throw new Exception("Pokemon not found");

            try
            {
                _context.Pokemons.Remove(pokemon);

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new DeleteException(id, e);
            }
        }
    }
}
