using PokemonAPI.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    public interface IBattleService
    {
        Task<BattleResult> Battle(Guid firstPokemonId, Guid secondPokemonId, CancellationToken cancellationToken = default);
    }
}
