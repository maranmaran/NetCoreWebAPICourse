using System;
using System.Threading.Tasks;
using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Validator;
using PokemonAPI.DomainLayer.Enums;
using PokemonAPI.PersistenceLayer.DTOModels;
using Xunit;

namespace Tests.BusinessLayer
{
    public class PokemonValidatorTests
    {
        [Fact]
        public async Task IsName_IsEmptyOrWhiteSpace_ValidationException()
        {
            // ARRANGE
            var pokemon = new PokemonDTO()
            {
                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                Name = "",
                Avatar = "Edipus",
                Generation = 2,
                Height = 100,
                Weight = 100,
                Type = PokemonType.Fire
            };

            // ACT
            var validator = new PokemonValidator();

            // ASSERT
            Assert.Throws<ValidationException>(() => validator.IsValid(pokemon));

        }

        [Fact]
        public async Task IsName_OnlyLetters_ValidationException()
        {
            // ARRANGE
            var pokemon = new PokemonDTO()
            {
                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                Name = "1234",
                Avatar = "Edipus",
                Generation = 2,
                Height = 100,
                Weight = 100,
                Type = PokemonType.Fire
            };

            // ACT
            var validator = new PokemonValidator();

            // ASSERT
            Assert.Throws<ValidationException>(() => validator.IsValid(pokemon));

        }

        [Fact]
        public async Task IsGeneration_ValueLessThan8AndMoreThan1_ValidationException()
        {
            // ARRANGE
            var pokemon = new PokemonDTO()
            {
                Id = Guid.Parse("8372c3f3-8281-4c21-8d0f-8830817bc2fb"),
                Name = "Georigus",
                Avatar = "Edipus",
                Generation = 10,
                Height = 100,
                Weight = 100,
                Type = PokemonType.Fire
            };

            // ACT
            var validator = new PokemonValidator();

           // ASSERT
            Assert.Throws<ValidationException>(() => validator.IsValid(pokemon));

        }


    }



}
