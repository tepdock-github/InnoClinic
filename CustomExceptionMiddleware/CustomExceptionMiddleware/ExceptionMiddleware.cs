using CustomExceptionMiddleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                int statusCode;
                switch (ex)
                {
                    case NotFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case BadRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                await WriteErrorMessage(ex, context);
            }
        }


        async Task WriteErrorMessage(Exception ex, HttpContext context)
        {
            var message = ex.Message;
            var details = ex.ToString();

            var problem = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = message,
                Detail = details
            };

            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problem);
        }
    }
}
