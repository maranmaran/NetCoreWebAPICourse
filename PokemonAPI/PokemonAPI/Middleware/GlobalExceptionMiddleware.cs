using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PokemonAPI.BusinessLayer.Exceptions;
using PokemonAPI.Models;

namespace PokemonAPI.Middleware
{
    namespace API.Middleware
    {
        /// <summary>
        /// Middleware that goes into request pipeline and handles any kind of exception
        /// </summary>
        public class GlobalExceptionMiddleware
        {
            private readonly RequestDelegate _next;

            public GlobalExceptionMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext httpContext, ILogger<GlobalExceptionMiddleware> logger)
            {
                try
                {
                    await _next(httpContext);
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(logger, httpContext, ex);
                }
            }

            private async Task HandleExceptionAsync(ILogger logger, HttpContext context, Exception exception)
            {
                logger.LogError(exception, $"{exception.Message} {exception.InnerException?.Message}");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                // generic internal server error
                var errorDetails = new ErrorDetails()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = exception.Message,
                };

                if (exception is BusinessLayer.Exceptions.ValidationException validationEx)
                {
                    errorDetails.Status = HttpStatusCode.BadRequest;
                    errorDetails.Message = validationEx.Message; 
                    errorDetails.Errors = JsonConvert.SerializeObject(validationEx._errors, Formatting.Indented);
                }

                else if (exception is NotFoundException)
                {
                    errorDetails.Status = HttpStatusCode.NotFound;
                    errorDetails.Message = "Not found";
                }

                else if (exception is CreateException)
                {
                    errorDetails.Status = HttpStatusCode.InternalServerError;
                    errorDetails.Message = "Could not create entity";
                }

                else if (exception is UpdateException)
                {
                    errorDetails.Status = HttpStatusCode.InternalServerError;
                    errorDetails.Message = "Could not update entity";
                }

                else if (exception is DeleteException)
                {
                    errorDetails.Status = HttpStatusCode.InternalServerError;
                    errorDetails.Message = "Could not delete entity";
                }

                else if (exception is AuthenticationException)
                {
                    errorDetails.Status = HttpStatusCode.Unauthorized;
                    errorDetails.Message = "Wrong username or password";
                }

                else if (exception is CatchPokemonException catchEx)
                {
                    errorDetails.Status = HttpStatusCode.BadRequest;
                    errorDetails.Message = catchEx.Message;
                }

                else
                {
                    errorDetails.Status = HttpStatusCode.InternalServerError;
                    errorDetails.Message = "Internal Server Error";
                }

                await context.Response.WriteAsync(errorDetails.ToString());
            }
        }
    }

}
