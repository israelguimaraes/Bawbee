using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bawbee.API.Setups;
using Bawbee.Domain.CommandHandlers;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Domain.Queries.Users.Queries;
using Bawbee.Infra.CrossCutting.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            services.AddMediatR(typeof(Startup));


            //services.AddMediatR(cfg => cfg.Using<INotificationHandler<DomainNotification>>().AsScoped(), typeof(DomainNotification));
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();



            services.AddAllBawbeeDependencies();

            // Bawbee.Domain.Core
            services.AddMediatR(typeof(Command).GetTypeInfo().Assembly);

            // Bawbee.Domain
            services.AddMediatR(typeof(BaseCommandHandler).GetTypeInfo().Assembly);

            // Bawbee.Domain.Queries
            services.AddMediatR(typeof(GetAllUsersQuery).GetTypeInfo().Assembly);

            /*
            // Bawbee.Infra.CrossCutting.IoC
            services.AddMediatR(typeof(BawbeeInjectorBootstrapper).GetTypeInfo().Assembly);
            */
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
