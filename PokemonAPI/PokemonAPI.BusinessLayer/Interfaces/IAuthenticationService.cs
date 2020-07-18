using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> SignIn(string username, string password, CancellationToken cancellationToken = default);
    }
}
