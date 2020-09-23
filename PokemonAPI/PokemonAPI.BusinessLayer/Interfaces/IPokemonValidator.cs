using PokemonAPI.PersistenceLayer.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    public interface IPokemonValidator
    {
         void IsValid(PokemonDTO pokemon);
       
    }
}
