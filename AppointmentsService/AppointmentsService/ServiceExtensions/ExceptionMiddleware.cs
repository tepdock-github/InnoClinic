using AppointmentsService.ServiceExtensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace AppointmentsService.ServiceExtensions
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
                if (ex is NotFoundException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.ContentType = "application/json";

                    await WriteErrorMessage(ex, context);
                }
                else if (ex is BadRequestException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";

                    await WriteErrorMessage(ex, context);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    await WriteErrorMessage(ex, context);
                }
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
