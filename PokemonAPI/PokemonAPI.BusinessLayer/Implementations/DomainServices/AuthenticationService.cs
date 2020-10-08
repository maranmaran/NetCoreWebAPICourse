using Microsoft.Extensions.Logging;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.BusinessLayer.Implementations.DomainServices
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<Admin, Admin> _repository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(IRepository<Admin, Admin> repository, IJwtGenerator jwtGenerator, IPasswordHasher passwordHasher, ILogger<AuthenticationService> logger)
        {
            _repository = repository;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<string> SignIn(string username, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var admin = await _repository.Get(
                    filter: user => user.Username == username && user.PasswordHash == _passwordHasher.GetPasswordHash(password));

                if (admin == null)
                    throw new AuthenticationException();

                _logger.LogInformation($"Authentication for user {username} successfull");
                return _jwtGenerator.GenerateToken(admin.Id);
            }
            catch (Exception e)
            {
                throw new AuthenticationException("Wrong username or password", e);
            }
        }
    }
}
