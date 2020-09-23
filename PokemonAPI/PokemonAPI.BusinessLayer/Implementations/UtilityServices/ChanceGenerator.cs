using PokemonAPI.BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonAPI.BusinessLayer.Implementations.UtilityServices
{
    public class ChanceGenerator : IChanceGenerator
    {
        public int getChance(int maxNonInclusive)
        {
            Random rand = new Random();

            return rand.Next(0, 2);
        }
    }
}
