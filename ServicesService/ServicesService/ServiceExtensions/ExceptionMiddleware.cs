﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace ServicesService.ServiceExtensions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
                    var ex = exceptionDetails?.Error;

                    if (ex != null)
                    {
                        var title = "An error occured: " + ex.Message;
                        var details = ex.ToString();

                        var problem = new ProblemDetails
                        {
                            Status = context.Response.StatusCode,
                            Title = title,
                            Detail = details
                        };

                        var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
                        if (traceId != null)
                        {
                            problem.Extensions["traceId"] = traceId;
                        }

                        //Serialize the problem details object to the Response as JSON (using System.Text.Json)
                        var stream = context.Response.Body;
                        await JsonSerializer.SerializeAsync(stream, problem);
                    }
                });
            });
        }
    }
}
