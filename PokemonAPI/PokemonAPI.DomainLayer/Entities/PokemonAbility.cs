using PokemonAPI.DomainLayer.Interfaces;
using System;

namespace PokemonAPI.DomainLayer.Entities
{
    public class PokemonAbility : EntityBase
    {

        public Guid AbilityId { get; set; }
        public virtual Ability Ability { get; set; }

        public Guid PokemonId { get; set; }
        public virtual Pokemon Pokemon { get; set; }
    }
}