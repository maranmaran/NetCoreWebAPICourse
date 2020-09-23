using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonAPI.BusinessLayer.Exceptions
{
    public class CatchPokemonException : Exception
    {
        public CatchPokemonException(string message, Exception ex = null)
            : base($"{message}", ex)
        {
        }

        public CatchPokemonException(Exception ex = null)
            : base($"Could not catch pokemon.", ex)
        {
        }
    }
}
