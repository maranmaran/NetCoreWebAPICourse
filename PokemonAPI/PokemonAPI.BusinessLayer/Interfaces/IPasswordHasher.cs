namespace PokemonAPI.BusinessLayer.Interfaces
{
    public interface IPasswordHasher
    {
        string GetPasswordHash(string password);
    }
}