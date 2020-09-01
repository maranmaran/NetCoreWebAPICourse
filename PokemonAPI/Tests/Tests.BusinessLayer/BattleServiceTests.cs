using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using PokemonAPI.BusinessLayer.Implementations.DomainServices;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.ValueObjects;
using PokemonAPI.PersistenceLayer.DTOModels;
using Xunit;

namespace Tests.BusinessLayer
{
    public class BattleServiceTests
    {
        [Fact]
        public async Task Battle_FirstPokemonStronger_FirstPokemonWins()
        {
            // ARRANGE
            var firstPokemon = new PokemonDTO()
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

            var secondPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("9372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                BaseStats = new BaseStat()
                {
                    HealthPoints = 50,
                    SpecialAttack = 10,
                    Attack = 10,
                    SpecialDefense = 10,
                    Defense = 10,
                    Speed = 2
                }
            };

            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock
                .Setup(x => x.Get(Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
                .ReturnsAsync(firstPokemon);
            pokemonServiceMock
                .Setup(x => x.Get(Guid.Parse("9372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
                .ReturnsAsync(secondPokemon);

            var service = new BattleService(pokemonServiceMock.Object);

            // ACT
            var result = await service.Battle(firstPokemon.Id, secondPokemon.Id, CancellationToken.None);

            // ASSERT
            Assert.Equal(firstPokemon.Id, result.WinnerID);
            Assert.Equal(secondPokemon.Id, result.LoserID);
            Assert.False(result.Draw);
        }

        [Fact]
        public async Task Battle_SecondPokemonStronger_FirstPokemonWins()
        {
            // ARRANGE
            var firstPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                BaseStats = new BaseStat()
                {
                    HealthPoints = 50,
                    SpecialAttack = 10,
                    Attack = 10,
                    SpecialDefense = 10,
                    Defense = 10,
                    Speed = 2
                }
            };

            var secondPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("9372c3f3-8281-4c21-8d0f-8830817bc2fb"),
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

            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock
                .Setup(x => x.Get(Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
                .ReturnsAsync(firstPokemon);
            pokemonServiceMock
                .Setup(x => x.Get(Guid.Parse("9372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
                .ReturnsAsync(secondPokemon);

            var service = new BattleService(pokemonServiceMock.Object);

            // ACT
            var result = await service.Battle(firstPokemon.Id, secondPokemon.Id, CancellationToken.None);

            // ASSERT
            Assert.Equal(secondPokemon.Id, result.WinnerID);
            Assert.Equal(firstPokemon.Id, result.LoserID);
            Assert.False(result.Draw);
        }

        [Fact]
        public async Task Battle_FirstPokemonWayStronger_FirstPokemonWins()
        {
            // ARRANGE
            var firstPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                BaseStats = new BaseStat()
                {
                    HealthPoints = 50,
                    SpecialAttack = 100,
                    Attack = 10,
                    SpecialDefense = 10,
                    Defense = 10,
                    Speed = 2
                }
            };

            var secondPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("9372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                BaseStats = new BaseStat()
                {
                    HealthPoints = 100,
                    SpecialAttack = 10,
                    Attack = 10,
                    SpecialDefense = 10,
                    Defense = 10,
                    Speed = 3
                }
            };

            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock
                .Setup(x => x.Get(Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
                .ReturnsAsync(firstPokemon);
            pokemonServiceMock
                .Setup(x => x.Get(Guid.Parse("9372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
                .ReturnsAsync(secondPokemon);

            var service = new BattleService(pokemonServiceMock.Object);

            // ACT
            var result = await service.Battle(firstPokemon.Id, secondPokemon.Id, CancellationToken.None);

            // ASSERT
            Assert.Equal(firstPokemon.Id, result.WinnerID);
            Assert.Equal(secondPokemon.Id, result.LoserID);
            Assert.False(result.Draw);
        }

        [Fact]
        public async Task Battle_SecondPokemonWayStronger_FirstPokemonWins()
        {
            // ARRANGE
            var firstPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                BaseStats = new BaseStat()
                {
                    HealthPoints = 50,
                    SpecialAttack = 10,
                    Attack = 10,
                    SpecialDefense = 10,
                    Defense = 10,
                    Speed = 2
                }
            };

            var secondPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("9372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                BaseStats = new BaseStat()
                {
                    HealthPoints = 100,
                    SpecialAttack = 100,
                    Attack = 10,
                    SpecialDefense = 10,
                    Defense = 10,
                    Speed = 2
                }
            };

            var pokemonServiceMock = new Mock<IPokemonService>();
            pokemonServiceMock
                .Setup(x => x.Get(Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
                .ReturnsAsync(firstPokemon);
            pokemonServiceMock
                .Setup(x => x.Get(Guid.Parse("9372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
                .ReturnsAsync(secondPokemon);

            var service = new BattleService(pokemonServiceMock.Object);

            // ACT
            var result = await service.Battle(firstPokemon.Id, secondPokemon.Id, CancellationToken.None);

            // ASSERT
            Assert.Equal(secondPokemon.Id, result.WinnerID);
            Assert.Equal(firstPokemon.Id, result.LoserID);
            Assert.False(result.Draw);
        }

        [Fact]
        public void GetFirstAttacker_FirstPokemonIsQuicker_FirstPokemonIsAttacker()
        {
            // ARRANGE
            var firstPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                BaseStats = new BaseStat()
                {
                    Speed = 2
                }
            };

            var secondPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("23ed4579-a8a3-4258-ae11-b52f48ee563b"),
                BaseStats = new BaseStat()
                {
                    Speed = 1
                }
            };

            var service = new BattleService(null);

            // ACT
            var result = service.GetFirstAttacker(firstPokemon, secondPokemon);

            // ASSERT
            Assert.Equal(Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"), result);
        }

        [Fact]
        public void GetFirstAttacker_SecondPokemonIsQuicker_SecondPokemonIsAttacker()
        {
            // ARRANGE
            var firstPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                BaseStats = new BaseStat()
                {
                    Speed = 2
                }
            };

            var secondPokemon = new PokemonDTO()
            {
                Id = Guid.Parse("23ed4579-a8a3-4258-ae11-b52f48ee563b"),
                BaseStats = new BaseStat()
                {
                    Speed = 3
                }
            };

            var service = new BattleService(null);

            // ACT
            var result = service.GetFirstAttacker(firstPokemon, secondPokemon);

            // ASSERT
            Assert.Equal(Guid.Parse("23ed4579-a8a3-4258-ae11-b52f48ee563b"), result);
        }
    }
}
