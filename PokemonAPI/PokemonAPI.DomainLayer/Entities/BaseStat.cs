using System;

namespace PokemonAPI.DomainLayer.Entities
{
    public class BaseStat
    {
        public Guid Id { get; set; }
        public int HealthPoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }

        public Guid PokemonId { get; set; }
        public virtual Pokemon Pokemon { get; set; }
    }
}