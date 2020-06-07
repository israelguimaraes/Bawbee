using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bawbee.Infra.CrossCutting.IoC
{
    public static class JwtRegister
    {
        public static void RegisterJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetSection("JwtConfig").GetSection("secret").Value;

            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthenticationCore(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults
            })
        }
    }
}
