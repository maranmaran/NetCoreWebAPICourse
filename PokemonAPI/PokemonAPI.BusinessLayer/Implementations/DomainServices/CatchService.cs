using AutoMapper;
using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests.BusinessLayer")]
namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class CatchService : ICatchService
    {
        private readonly IPokemonService _pokemonService;
        private readonly IMapper _mapper;
        private readonly IChanceGenerator _chanceGenerator;

        public CatchService(IPokemonService pokemonService, IMapper mapper, IChanceGenerator chanceGenerator)
        {
            _pokemonService = pokemonService;
            _mapper = mapper;
            _chanceGenerator = chanceGenerator;
        }

        public async Task CatchPokemon(Guid pokemonId, Guid trainerId, CancellationToken cancellationToken = default)
        {
            var pokemon = await _pokemonService.Get(pokemonId, cancellationToken);

            if (pokemon.Trainer != null)
                throw new CatchPokemonException("Pokemon already has a trainer");

            if (_chanceGenerator.getChance(2) != 0)
            {
                throw new CatchPokemonException("Unfortunately pokemon dodged your pokeball");
            }

            pokemon.TrainerId = trainerId;

            await _pokemonService.Update(pokemon, cancellationToken);
        }
    }
}
