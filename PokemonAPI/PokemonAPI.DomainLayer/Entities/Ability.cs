using System;
using System.Collections.Generic;

namespace PokemonAPI.DomainLayer.Entities
{
    public class Ability
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<PokemonAbility> Pokemons { get; set; } = new HashSet<PokemonAbility>();
    }
}