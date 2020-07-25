using System;
using PokemonAPI.DomainLayer.Interfaces;

namespace PokemonAPI.DomainLayer.Entities
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}