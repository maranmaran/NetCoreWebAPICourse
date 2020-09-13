using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.DTOModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PokemonAPI.BusinessLayer.Validator
{
    class PokemonValidator : IPokemonValidator
    {
        public string message;

        public bool IsValid(PokemonDTO pokemon)
        {

            if (pokemon.Name.Equals("") || pokemon.Name == null)
            {
                message = "Pokemon name is required";
                return false;
            }

            else if(pokemon.Avatar.Equals("") || pokemon.Avatar == null)
            {
                message = "Pokemon avatar is required";
                return false;
            }

            else if (pokemon.Generation == null)
            {
                message = "Pokemon generation is required";
                return false;
            }

            else if (pokemon.Generation >= 1 && pokemon.Generation <= 8)
            {
                message = "Pokemon generation must be between 1 and 8 inclusive";
                return false;
            }

            return true;
        }

        public string getErrorMessage()
        {
            return message;
        }
    }
}
