using Clay.Api.Models;
using Clay.Application.Exceptions;
using Clay.Domain.DomainObjects;
using System.Net;

namespace Clay.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError($"{ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, ex.Message);
            }
            catch (DomainActionNotPermittedException ex)
            {
                _logger.LogError($"{ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.Forbidden, ex.Message);
            }
            catch (DomainException ex)
            {
                _logger.LogError($"{ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
