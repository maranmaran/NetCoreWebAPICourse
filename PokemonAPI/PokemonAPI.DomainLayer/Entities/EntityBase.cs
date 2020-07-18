using System;

namespace PokemonAPI.DomainLayer.Interfaces
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}