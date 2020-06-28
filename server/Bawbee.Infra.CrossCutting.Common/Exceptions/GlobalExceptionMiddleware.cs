using Bawbee.Infra.CrossCutting.Common.Exceptions.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Bawbee.Infra.CrossCutting.Common.Exceptions
{
    public static class GlobalExceptionMiddleware
    {
        public static void UseApiExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        //logger.LogError($"Something went wrong: {contextFeature.Error}");

                        var error = new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = $"Internal Server Error {contextFeature.Error}"
                        };

                        await context.Response.WriteAsync(error.ToString());
                    }
                });
            });
        }
    }
}
