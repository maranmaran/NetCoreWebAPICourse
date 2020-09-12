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
        public bool IsValid(PokemonDTO pokemon)
        {
            if (pokemon.Name.Equals(""))
            {
                return false;
            }

            return true;
        }
    }
}
