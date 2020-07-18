using PokemonAPI.DomainLayer.Interfaces;

namespace PokemonAPI.DomainLayer.Entities
{
    public class Admin : EntityBase
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
