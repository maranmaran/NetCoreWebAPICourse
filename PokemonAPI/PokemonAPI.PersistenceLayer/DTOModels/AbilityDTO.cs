using System;

namespace PokemonAPI.PersistenceLayer.DTOModels
{
    public class AbilityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
