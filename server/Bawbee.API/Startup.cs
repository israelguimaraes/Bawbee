using Bawbee.API.Setups;
using Bawbee.Core.Bus;
using Bawbee.Domain.Events;
using Bawbee.Domain.Events.BankAccounts;
using Bawbee.Domain.Events.Categories;
using Bawbee.Domain.Events.Entries;
using Bawbee.Infra.CrossCutting.IoC;
using Bawbee.Infra.Data.SQLServer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            // TODO: change...
            services.AddDbContext<BawbeeDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("BawbeeDbConnection")));

            services.AddControllers();
            services.AddOptions();
            services.AddMediatR(typeof(Startup));
            services.AddAllBawbeeDependencies(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseApiExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureSwagger();

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<UserRegisteredEvent>();
            eventBus.Subscribe<ExpenseCreatedEvent>();
            eventBus.Subscribe<ExpenseUpdatedEvent>();
            eventBus.Subscribe<ExpenseDeletedEvent>();
            eventBus.Subscribe<CategoryCreatedEvent>();
            eventBus.Subscribe<BankAccountCreatedEvent>();
        }
    }
}
