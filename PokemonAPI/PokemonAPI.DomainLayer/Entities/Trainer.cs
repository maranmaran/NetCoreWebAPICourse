using PokemonAPI.DomainLayer.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonAPI.DomainLayer.Entities
{
    public class Trainer : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public ICollection<Pokemon> CaughtPokemons { get; set; } = new HashSet<Pokemon>();



    }

    
}
