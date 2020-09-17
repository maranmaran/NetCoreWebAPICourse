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
    class PokemonValidator : IPokemonValidator
    {
        Dictionary<string, List<string>> errorMap = new Dictionary<string, List<string>>();
        public bool valid = true;
      
        
        public void IsValid(PokemonDTO pokemon)
        {

            if (string.IsNullOrEmpty(pokemon.Name))
            {
                List<string> errors = new List<string>();
                errors.Add("Pokemon name is required");
                errorMap.Add("Name",errors);
                valid = false;
            }

            if (!(Regex.IsMatch(pokemon.Name, @"^[a-zA-Z]+$")))
            {
                List<string> errors = new List<string>();
                errors.Add("Pokemon name must cointain only letters");
                errorMap.Add("Name", errors);
                valid = false;
            }

            if (string.IsNullOrEmpty(pokemon.Avatar))
            {
                List<string> errors = new List<string>();
                errors.Add("Pokemon avatar is required");
                errorMap.Add("Avatar", errors);
                valid = false;
            }

            if (!(Regex.IsMatch(pokemon.Avatar, @"^[a-zA-Z]+$")))
            {
                List<string> errors = new List<string>();
                errors.Add("Pokemon avatar must cointain only letters");
                errorMap.Add("Avatar", errors);
                valid = false;
            }

            if (pokemon.Generation == null)
            {
                List<string> errors = new List<string>();
                errors.Add("Pokemon generation is required");
                errorMap.Add("Generation", errors);
                valid = false;
            }

            if (pokemon.Generation < 1 || pokemon.Generation > 8)
            {
                List<string> errors = new List<string>();
                errors.Add("Pokemon generation needs to be between 1 and 8 inclusive");
                errorMap.Add("Generation", errors);
                valid = false;
            }
            

            if (pokemon.Height == 0)
            {
                List<string> errors = new List<string>();
                errors.Add("Pokemon height is required");
                errorMap.Add("Height", errors);
                valid = false;
            }

            if (pokemon.Weight == 0)
            {
                List<string> errors = new List<string>();
                errors.Add("Pokemon weight is required");
                errorMap.Add("Weight", errors);
                valid = false;
            }

            if (!(pokemon.Type.Equals("Normal") || pokemon.Type.Equals("Fire") || pokemon.Type.Equals("Fighting")  ||  pokemon.Type.Equals("Water") ||
                pokemon.Type.Equals("Flying") || pokemon.Type.Equals("Grass") || pokemon.Type.Equals("Poison") || pokemon.Type.Equals("Electric") ||
                pokemon.Type.Equals("Ground")  || pokemon.Type.Equals("Psychic") || pokemon.Type.Equals("Rock")  ||  pokemon.Type.Equals("Ice") ||
                pokemon.Type.Equals("Bug") || pokemon.Type.Equals("Dragon") || pokemon.Type.Equals("Ghost") || pokemon.Type.Equals("Dark") ||
                pokemon.Type.Equals("Steel") ||  pokemon.Type.Equals("Fairy")))
            {
                List<string> errors = new List<string>();
                errors.Add("Pokemon type needs to be one of these types: Normal, Fire, Fighting, Water, Flying, Grass, Poison, ElectricGround, Psychic, Rock, Ice, Bug, Dragon, Ghost, Dark, Steel, Fairy");
                errorMap.Add("Type", errors);
                valid = false;
            }

            if (!valid)
            {
                throw new ValidationException("pokemon", errorMap);
            }
            
        }

    }
}
