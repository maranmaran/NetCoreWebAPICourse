//using System;
//using System.Collections.Generic;
////using System.Text;
////using System.Threading;
////using System.Threading.Tasks;
////using Moq;
////using PokemonAPI.BusinessLayer.Interfaces;
////using PokemonAPI.BusinessLayer.Validator;
////using PokemonAPI.PersistenceLayer.DTOModels;
////using Xunit;

////namespace Tests.BusinessLayer
////{
////    public class PokemonValidatorTests
////    {
////        [Fact]
////        public async Task IsValid()
////        {
////            // ARRANGE
////            var pokemon = new PokemonDTO()
////            {
////                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
////                Name = "",
////                Avatar = "",
////                Generation = 2
////            };

////            var pokemonServiceMock = new Mock<IPokemonService>();
////            pokemonServiceMock
////                .Setup(x => x.Get(Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"), CancellationToken.None))
////                .ReturnsAsync(pokemon);

////            var validator = new PokemonValidator(pokemonServiceMock.Object);

////            // ACT
////            validator.IsValid(pokemon);

////            // ASSERT
////            Assert.Throws<Vali>()
////        }
////    }
//}
