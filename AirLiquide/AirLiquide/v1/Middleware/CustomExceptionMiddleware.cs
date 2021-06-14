using AirLiquide.Application.v1.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AirLiquide.Api.v1.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;
        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomNotFoundException ex)
            {
                _logger.LogWarning(ex.ToString());
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            catch (CustomValidationException ex)
            {
                _logger.LogWarning(ex.ToString());
                httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(ex.Errors,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
            }
            catch (CustomNoRowsAffectedException ex)
            {
                _logger.LogWarning(ex.ToString());
                httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

    }
}
