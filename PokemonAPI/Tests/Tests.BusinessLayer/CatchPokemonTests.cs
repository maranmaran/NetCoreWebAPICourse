using AutoMapper;
using Moq;
using PokemonAPI.BusinessLayer;
using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Implementations.DomainServices;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.DomainLayer.ValueObjects;
using PokemonAPI.PersistenceLayer.DTOModels;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.BusinessLayer
{
    public class CatchPokemonTests
    {
       

        [Fact]
        public async Task CatchPokemon_CaughtOrNot_CatchPokemonException()
        {
            // ARRANGE
            var pokemon = new PokemonDTO()
            {
                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                BaseStats = new BaseStat()
                {
                    HealthPoints = 100,
                    SpecialAttack = 10,
                    Attack = 10,
                    SpecialDefense = 10,
                    Defense = 10,
                    Speed = 2
                }
            };

            var trainer = new TrainerDTO()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                FullName = "Marko Urh"
            };

            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock
                .Setup(x => x.Get(Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
                .ReturnsAsync(pokemon);

            var chanceGeneratorMock = new Mock<IChanceGenerator>();
            chanceGeneratorMock
                .Setup(x => x.getChance(It.IsAny<int>()))
                .Returns(4);

            var pokeRepoMock = new Mock<IRepository<Pokemon>>();

            // ACT
            var service = new CatchService(pokemonServiceMock.Object, TestHelper.GetMapper(), pokeRepoMock.Object, chanceGeneratorMock.Object);

            // ASSERT
            await Assert.ThrowsAsync<CatchPokemonException>(() => service.CatchPokemon(pokemon.Id, trainer.Id, CancellationToken.None));
        }

        [Fact]
        public async Task CatchPokemon_CaughtOrNot_Success()
        {
            // ARRANGE
            var pokemon = new PokemonDTO()
            {
                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                BaseStats = new BaseStat()
                {
                    HealthPoints = 100,
                    SpecialAttack = 10,
                    Attack = 10,
                    SpecialDefense = 10,
                    Defense = 10,
                    Speed = 2
                }
            };

            var trainer = new TrainerDTO()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                FullName = "Marko Urh"
            };

            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock
                .Setup(x => x.Get(Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
                .ReturnsAsync(pokemon);

            var chanceGeneratorMock = new Mock<IChanceGenerator>();
            chanceGeneratorMock
                .Setup(x => x.getChance(It.IsAny<int>()))
                .Returns(0);

            var pokeRepoMock = new Mock<IRepository<Pokemon>>();

            // ACT
            var service = new CatchService(pokemonServiceMock.Object, TestHelper.GetMapper(), pokeRepoMock.Object, chanceGeneratorMock.Object);

            // ASSERT
            await service.CatchPokemon(pokemon.Id, trainer.Id, CancellationToken.None);
        }
    }
}
