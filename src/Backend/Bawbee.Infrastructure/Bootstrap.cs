using Bawbee.Infrastructure.Persistence.Sql.EfContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bawbee.Infrastructure
{
    public static class Bootstrap
    {
        public static void InitializeDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BawbeeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BawbeeDbConnection")));
        }
    }
}
