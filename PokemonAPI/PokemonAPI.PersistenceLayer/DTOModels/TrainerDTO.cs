using PokemonAPI.PersistenceLayer.DTOModels;
using System;
using System.Collections.Generic;

namespace PokemonAPI.DomainLayer.Entities
{
    public class TrainerDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public ICollection<PokemonDTO> CaughtPokemons { get; set; } = new HashSet<PokemonDTO>();
    }

    
}
