using System;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateToken(Guid userId);
    }
}