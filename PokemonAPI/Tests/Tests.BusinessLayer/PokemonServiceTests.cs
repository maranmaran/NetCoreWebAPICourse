using Microsoft.EntityFrameworkCore.Query;
using Moq;
using PokemonAPI.BusinessLayer.Implementations.DomainServices;
using PokemonAPI.BusinessLayer.Validator;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.DTOModels;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.BusinessLayer
{
    public class PokemonServiceTests
    {
        

        [Fact]
        public async Task GetAll_TwoPokemonInDb_GetsBoth()
        {
            var pokemons = new List<Pokemon>()
            {
                new Pokemon()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                    Abilities = new List<PokemonAbility>()
                    {
                        new PokemonAbility()
                        {
                            Ability = new Ability()
                            {
                                Name = "Leaf strike"
                            }
                        }
                    }
                },
                new Pokemon()
                {
                    Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967b")
                }
            };

            // arrange
            var repository = new Mock<IRepository<Pokemon>>();
            repository.Setup(x => 
                x.GetAll(
                    null,
                    null,
                    It.IsAny<Func<IQueryable<Pokemon>, IIncludableQueryable<Pokemon, object>>>(),
                    true,
                    CancellationToken.None 
            )).ReturnsAsync(pokemons);

            var mapper = TestHelper.GetMapper();

            var service = new PokemonService(mapper: mapper, validator: null, repository: repository.Object);

            // act
            var result = (await service.GetAll(CancellationToken.None)).ToList();

            // assert
            Assert.Equal(pokemons[0].Id, result[0].Id);
            Assert.Equal(pokemons[0].Abilities.ToList()[0].Ability.Name, result[0].Abilities.ToList()[0].Name);

            Assert.Equal(pokemons[1].Id, result[1].Id);

            repository.Verify(x => x.GetAll(null,
                    null,
                    It.IsAny<Func<IQueryable<Pokemon>, IIncludableQueryable<Pokemon, object>>>(),
                    true,
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddPokemonToDb_InsertsPokemon()
        {

            var pokemon = new Pokemon()
            {
                Id = Guid.Parse("47b176a6-535f-447b-9f09-86465f07967a"),
                Name = "Evegenij",
                Avatar = "Evgavatar",
                Generation = 2,
                Height = 100,
                Weight = 100,
                Abilities = new List<PokemonAbility>()
                {
                    new PokemonAbility()
                    {
                        Ability = new Ability()
                        {
                            Name = "Leaf strike"
                        }
                    }
                }
            };
               
          
            // arrange
            var repository = new Mock<IRepository<Pokemon>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<Pokemon>(),
                    CancellationToken.None
            )).ReturnsAsync(pokemon.Id);

            var mapper = TestHelper.GetMapper();

            var validator = new PokemonValidator();

            var service = new PokemonService(mapper: mapper, validator: validator, repository: repository.Object);

            // act
            var pokemonDTO = mapper.Map<PokemonDTO>(pokemon);
            var result = (await service.Create(pokemonDTO, CancellationToken.None));

            // assert
           
            repository.Verify(x => x.Insert(
                    It.IsAny<Pokemon>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Delete_DeletePokemonFromDb_DeletesPokemon()
        {

            var pokemonId = new Guid("47b176a6-535f-447b-9f09-86465f07967a");
            var request = new Pokemon();
            request.Id = pokemonId;
            var inputGuidToAssert = Guid.Empty;

            // arrange
            var repository = new Mock<IRepository<Pokemon>>();
            var insertSetup = repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None
            ));
            
            insertSetup.Callback((Guid guid, CancellationToken token) => inputGuidToAssert = guid);

            var mapper = TestHelper.GetMapper();

            var service = new PokemonService(mapper: mapper, validator: null, repository: repository.Object);

            // act

            await service.Delete(pokemonId, CancellationToken.None);

            // assert
            Assert.Equal(pokemonId, inputGuidToAssert);
            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }
    }
}
