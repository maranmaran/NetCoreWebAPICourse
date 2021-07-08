using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.DTOModels;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests.BusinessLayer")]

namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class PokemonService : IPokemonService
    {
        private readonly IRepository<Pokemon, PokemonDTO> _repository;
        private readonly IMapper _mapper;
        private readonly IPokemonValidator _validator;
        private readonly ILogger<PokemonService> _logger;



        public PokemonService(IRepository<Pokemon, PokemonDTO> repository, IMapper mapper, IPokemonValidator validator, ILogger<PokemonService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<IEnumerable<PokemonDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            var pokemons = await _repository.GetAll(
                include: source => source
                    .Include(x => x.Abilities)
                    .ThenInclude(x => x.Ability),
                cancellationToken: cancellationToken
            );

            _logger.LogInformation("Get all pokemons request succesfull");
            return pokemons;
        }

        public async Task<PokemonDTO> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var pokemon = await _repository.Get(
                filter: dbPokemon => dbPokemon.Id == id,
                include: source => source
                    .Include(x => x.Abilities)
                    .ThenInclude(x => x.Ability)
            );

            if (pokemon == null)
            {
                throw new NotFoundException(id);
            }

            _logger.LogInformation($"Get request for pokemon: {id} succesfull");
            return pokemon;
        }

        public async Task<Guid> Create(PokemonDTO pokemon, CancellationToken cancellationToken = default)
        {

            _validator.IsValid(pokemon);
            _logger.LogInformation($"Validation for pokemon: {pokemon.Id} successfull");

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
                _logger.LogInformation($"Pokemon with ID: {pokemon.Id} succesfully updated");
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
                _logger.LogInformation($"Pokemon with ID: {id} succesfully deleted");
            }
            catch (Exception e)
            {
                throw new DeleteException(id, e);
            }
        }
    }
}
