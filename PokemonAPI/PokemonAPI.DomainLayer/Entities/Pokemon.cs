using PokemonAPI.DomainLayer.Enums;
using PokemonAPI.DomainLayer.ValueObjects;
using System;
using System.Collections.Generic;

namespace PokemonAPI.DomainLayer.Entities
{
    public class Pokemon : EntityBase
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public int? Generation { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public PokemonType Type { get; set; }
        public virtual Trainer Trainer { get; set; }
        public Guid? TrainerId { get; set; }
        public virtual BaseStat BaseStats { get; set; }

        public ICollection<PokemonAbility> Abilities { get; set; } = new HashSet<PokemonAbility>();
    }
}
