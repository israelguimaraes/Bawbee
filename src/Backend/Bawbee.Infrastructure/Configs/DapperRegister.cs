using Bawbee.Infrastructure.Persistence.Sql;
using Bawbee.Infrastructure.Persistence.Sql.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bawbee.Infrastructure.Configs
{
    public static class DapperRegister
    {
        public static void RegisterDapper(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlServerConfig = configuration.GetSection(nameof(SqlServerConfig)).Get<SqlServerConfig>();
            services.AddScoped<IDapperConnection>(dapper => new DapperConnection(sqlServerConfig.ToString()));
        }
    }
}
