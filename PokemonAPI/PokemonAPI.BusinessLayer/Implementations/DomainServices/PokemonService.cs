using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.BusinessLayer.Validator;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.DTOModels;
using PokemonAPI.PersistenceLayer.Interfaces;


namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class PokemonService : IPokemonService
    {
        private readonly IRepository<Pokemon> _repository;
        private readonly IMapper _mapper;
        private readonly IPokemonValidator _validator;
        private readonly ILogger<PokemonService> _logger;

        public PokemonService(IRepository<Pokemon> repository, IMapper mapper, IPokemonValidator validator, ILogger<PokemonService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<IEnumerable<PokemonDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching pokemons");

            var pokemons = await _repository.GetAll(
                include: source => source
                    .Include(x => x.Abilities)
                    .ThenInclude(x => x.Ability),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<IEnumerable<PokemonDTO>>(pokemons);
        }

        public async Task<PokemonDTO> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var pokemon = await _repository.Get(
                filter: dbPokemon => dbPokemon.Id == id,
                include: source => source
                    .Include(x => x.Abilities)
                    .ThenInclude(x => x.Ability),
                cancellationToken: cancellationToken
            );

            if (pokemon == null)
                throw new NotFoundException(id);

            return _mapper.Map<PokemonDTO>(pokemon);
        }

        public async Task<Guid> Create(PokemonDTO pokemon, CancellationToken cancellationToken = default)
        {

            _validator.IsValid(pokemon);

            try
            {
                var pokemonEntity = _mapper.Map<Pokemon>(pokemon);
                return await _repository.Insert(pokemonEntity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
            


            
            
        }

        public async Task Update(PokemonDTO pokemon, CancellationToken cancellationToken = default)
        {
            try
            {
                var pokemonEntity = _mapper.Map<Pokemon>(pokemon);

                await _repository.Update(pokemonEntity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new UpdateException(pokemon.Id, e);
            }
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _repository.Delete(id, cancellationToken);
            }
            catch (Exception e)
            {
                throw new DeleteException(id, e);
            }
        }
    }
}
