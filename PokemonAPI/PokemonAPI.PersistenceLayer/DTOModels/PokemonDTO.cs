using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.DomainLayer.Enums;
using PokemonAPI.DomainLayer.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.PersistenceLayer.DTOModels
{
    public class PokemonDTO
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public int? Generation { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public PokemonType Type { get; set; }
        public TrainerDTO Trainer { get; set; }
        public Guid? TrainerId { get; set; }

        public virtual BaseStat BaseStats { get; set; }

        public ICollection<AbilityDTO> Abilities { get; set; } = new HashSet<AbilityDTO>();
    }
}
