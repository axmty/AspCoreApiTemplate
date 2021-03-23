using BookstoreApi.App.Middleware;
using BookstoreApi.Core.Mappers;
using BookstoreApi.Core.Repositories;
using BookstoreApi.Core.Services;
using BookstoreApi.Infrastructure;
using BookstoreApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookstoreApi.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<ICustomersRepository, CustomersRepository>();

            services.AddTransient<IMapper<Core.Entities.Customer, Core.Models.Customer>, CustomerEntityToModelMapper>();
            services.AddTransient<IMapper<Core.Entities.Address, Core.Models.Address>, AddressEntityToModelMapper>();

            services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(this.Configuration.GetConnectionString("Bookstore")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseNotFoundHandler();

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
