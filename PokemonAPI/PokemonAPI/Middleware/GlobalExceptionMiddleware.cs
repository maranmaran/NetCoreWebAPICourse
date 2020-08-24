﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

                if (exception is ValidationException validationEx)
                {
                    errorDetails.Status = HttpStatusCode.BadRequest;
                    errorDetails.Message = validationEx.Message;
                    errorDetails.Errors = JsonConvert.SerializeObject(validationEx.ValidationResult);
                }

                await context.Response.WriteAsync(errorDetails.ToString());
            }
        }
    }

}
