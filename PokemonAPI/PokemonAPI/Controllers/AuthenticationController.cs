using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.BusinessLayer.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers
{
    /// <summary>
    /// Provides a way to sign in and manipulate resources
    /// </summary>
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Sign in as admin
        /// </summary>
        [AllowAnonymous]
        [HttpGet("SignIn/{username}/{password}")]
        public async Task<IActionResult> SignIn(string username, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var token = await _authenticationService.SignIn(username, password, cancellationToken);

                Response.Cookies.Append(JwtBearerDefaults.AuthenticationScheme, token);

                return Accepted();
            }
            catch
            {
                throw new UnauthorizedAccessException();
            }
            
        }
    }
}
