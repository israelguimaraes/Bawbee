using Bawbee.Infrastructure.Persistence.Sql.EfContexts;
using Bawbee.Infrastructure.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bawbee.Infrastructure
{
    public static class Bootstrap
    {
        public static void InitializeDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // EF
            services.AddDbContext<BawbeeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BawbeeDbConnection")));

            // Swagger
            services.RegisterSwagger();
        }
    }
}
