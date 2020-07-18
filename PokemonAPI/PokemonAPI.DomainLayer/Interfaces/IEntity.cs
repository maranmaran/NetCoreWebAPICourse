using System;

namespace PokemonAPI.DomainLayer.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
