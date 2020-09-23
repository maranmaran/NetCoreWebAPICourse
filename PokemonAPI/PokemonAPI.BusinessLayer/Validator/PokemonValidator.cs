using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.PersistenceLayer.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            else
            {
                errorMap.Add(errorKey, new List<string>() { message });
            }

        }

        public void IsValid(PokemonDTO pokemon)
        {

            if (string.IsNullOrWhiteSpace(pokemon.Name))
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon name is required", nameof(pokemon.Name));
            }

            if (!pokemon.Name.All(Char.IsLetter))
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon name must contain only letters", nameof(pokemon.Name));
            }

            if (string.IsNullOrWhiteSpace(pokemon.Avatar))
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon avatar is required", nameof(pokemon.Avatar));
            }

            if (!pokemon.Avatar.All(Char.IsLetter))
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon avatar must contain only letters", nameof(pokemon.Avatar));
            }

            if (pokemon.Generation == null)
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon generation is required", nameof(pokemon.Generation));
            }

            if (pokemon.Generation < 1 || pokemon.Generation > 8)
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon generation must be between 1 and 8 inclusive", nameof(pokemon.Generation));
            }
            
            if (pokemon.Height == 0)
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon height is required", nameof(pokemon.Height));
            }

            if (pokemon.Weight == 0)
            {
                AddOrUpdatePokemonErrors(errorMap, "Pokemon weight is required", nameof(pokemon.Weight));
            }

            if (errorMap.Count > 0)
            {
                throw new ValidationException("pokemon", errorMap);
            }
            
        }

    }
}
