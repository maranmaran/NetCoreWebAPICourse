using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.DTOModels;
using System;
using System.Collections.Generic;

using System.Text;
using System.Text.RegularExpressions;

namespace PokemonAPI.BusinessLayer.Validator
{
    public class PokemonValidator : IPokemonValidator
    {
        private Dictionary<string, List<string>> errorMap = new Dictionary<string, List<string>>();
        

        private void AddOrUpdatePokemonErrors(Dictionary<string, List<string>> errorMap, String message, String errorKey)
        {
            if (errorMap.ContainsKey(errorKey))
            {
                errorMap[errorKey].Add(message);
            }

            errorMap.Add(errorKey, new List<string>() { message });
        }

        public void IsValid(PokemonDTO pokemon)
        {

            if (string.IsNullOrEmpty(pokemon.Name))
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon name is required", "Name");
            }

            if (!(Regex.IsMatch(pokemon.Name, @"^[a-zA-Z]+$")))
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon name must contain only letters", "Name");
            }

            if (string.IsNullOrEmpty(pokemon.Avatar))
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon avatar is required", "Avatar");
            }

            if (!(Regex.IsMatch(pokemon.Avatar, @"^[a-zA-Z]+$")))
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon avatar must contain only letters", "Avatar");
            }

            if (pokemon.Generation == null)
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon generation is required", "Generation");
            }

            if (pokemon.Generation < 1 || pokemon.Generation > 8)
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon generation must be between 1 and 8 inclusive", "Generation");
            }
            
            if (pokemon.Height == 0)
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon height is required", "Height");
            }

            if (pokemon.Weight == 0)
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon weight is required", "Weight");
            }

            if (errorMap.Count > 0)
            {
                throw new ValidationException("pokemon", errorMap);
            }
            
        }

    }
}
