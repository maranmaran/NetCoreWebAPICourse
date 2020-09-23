using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    public interface IChanceGenerator
    {
        int getChance(int maxNonInclusive);
    }
}
