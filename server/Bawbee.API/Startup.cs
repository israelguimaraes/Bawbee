using Bawbee.API.Setups;
using Bawbee.Domain.Commands;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Queries.Users.Queries;
using Bawbee.Infra.Data.RavenDB.EventHandlers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Bawbee.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddOptions();

            services.AddMediatR(typeof(Startup));

            // Bawbee.Domain.Core
            services.AddMediatR(typeof(Command).GetTypeInfo().Assembly);

            // Bawbee.Domain
            services.AddMediatR(typeof(BaseCommandHandler).GetTypeInfo().Assembly);

            // Bawbee.Domain.Queries
            services.AddMediatR(typeof(GetAllUsersQuery).GetTypeInfo().Assembly);

            // Bawbee.Infra.Data
            services.AddMediatR(typeof(UserRavenDBHandler).GetTypeInfo().Assembly);

            services.AddAllBawbeeDependencies(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
