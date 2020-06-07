using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Bawbee.Infra.CrossCutting.IoC
{
    public static class SwaggerRegister
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bawbee API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Bearer xxxxxxxxxx",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });
        }
    }
}
