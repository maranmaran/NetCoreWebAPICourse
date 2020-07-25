using PokemonAPI.BusinessLayer.Helpers;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Implementations
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<Admin> _repository;

        public AuthenticationService(IRepository<Admin> repository)
        {
            _repository = repository;
        }

        public async Task<string> SignIn(string username, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var admins = await _repository.GetAll(cancellationToken: cancellationToken);

                var admin = admins
                    .FirstOrDefault(x => x.Username == username && x.PasswordHash == PasswordHasher.GetPasswordHash(password));

                if (admin == null)
                    throw new AuthenticationException();

                return JwtGenerator.GenerateToken(admin.Id);
            }
            catch (Exception e)
            {
                throw new AuthenticationException("Wrong username or password", e);
            }
        }
    }
}
