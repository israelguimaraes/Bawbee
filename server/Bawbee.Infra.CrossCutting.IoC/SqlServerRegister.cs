using Bawbee.Infra.Data.SQLServer;
using Bawbee.Infra.Data.SQLServer.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bawbee.Infra.CrossCutting.IoC
{
    public static class SqlServerRegister
    {
        public static void RegisterSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlServerConfig = configuration.GetSection(nameof(SqlServerConfig)).Get<SqlServerConfig>();
            services.AddScoped<IDapperConnection>(dapper => new DapperConnection(sqlServerConfig.ToString()));
        }
    }
}