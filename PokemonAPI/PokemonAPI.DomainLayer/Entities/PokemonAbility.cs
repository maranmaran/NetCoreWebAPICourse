using System;

namespace PokemonAPI.DomainLayer.Entities
{
    public class PokemonAbility
    {
        public Guid Id { get; set; }

        public Guid AbilityId { get; set; }
        public virtual Ability Ability { get; set; }

        public Guid PokemonId { get; set; }
        public virtual Pokemon Pokemon { get; set; }
    }
}