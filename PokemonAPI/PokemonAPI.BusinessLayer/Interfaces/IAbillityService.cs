using PokemonAPI.PersistenceLayer.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    public interface IAbillityService
    {
        Task<IEnumerable<AbilityDTO>> GetAll(CancellationToken cancellationToken = default);
    }
}
