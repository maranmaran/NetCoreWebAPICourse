using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.Interfaces;

namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<Admin> _repository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IRepository<Admin> repository, IJwtGenerator jwtGenerator, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> SignIn(string username, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var admin = await _repository.Get(
                    filter: user => user.Username == username && user.PasswordHash == _passwordHasher.GetPasswordHash(password),
                    cancellationToken: cancellationToken);

                if (admin == null)
                    throw new AuthenticationException();

                return _jwtGenerator.GenerateToken(admin.Id);
            }
            catch (Exception e)
            {
                throw new AuthenticationException("Wrong username or password", e);
            }
        }
    }
}
