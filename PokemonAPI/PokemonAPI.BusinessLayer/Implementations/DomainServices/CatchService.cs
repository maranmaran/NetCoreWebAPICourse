using AutoMapper;
using Microsoft.Extensions.Logging;
using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class CatchService : ICatchService
    {
        private readonly IPokemonService _pokemonService;
        private readonly IMapper _mapper;
        private readonly IRepository<Pokemon> _pokeRepository;
        private readonly IChanceGenerator _chanceGenerator;
        private readonly ILogger<CatchService> _logger;

        public CatchService(IPokemonService pokemonService, IMapper mapper, IRepository<Pokemon> pokeRepository, IChanceGenerator chanceGenerator, ILogger<CatchService> logger)
        {
            _pokemonService = pokemonService;
            _mapper = mapper;
            _pokeRepository = pokeRepository;
            _chanceGenerator = chanceGenerator;
            _logger = logger;
        }

        public async Task CatchPokemon(Guid pokemonId, Guid trainerId, CancellationToken cancellationToken = default)
        {
            var pokemon = await _pokemonService.Get(pokemonId, cancellationToken);
            var pokemonEntity = _mapper.Map<Pokemon>(pokemon);

            if (pokemonEntity.Trainer != null)
                throw new CatchPokemonException("Pokemon already has a trainer");

            var chance = _chanceGenerator.getChance(2);

            _logger.LogInformation("Chance to catch a pokemon is 50%");
            _logger.LogDebug($"User rolled {chance} (Only rolling 0 equals to success)");

            if (chance != 0)
            {
                throw new CatchPokemonException("Unfortunately pokemon dodged your pokeball");
            }

            pokemonEntity.TrainerId = trainerId;

            await _pokeRepository.Update(pokemonEntity, cancellationToken);
        }
    }
}
