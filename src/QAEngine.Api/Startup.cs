using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QAEngine.Core.Repositories;
using QAEngine.Core.Services;
using QAEngine.Infra.Data;
using QAEngine.Infra.Repositories;

namespace QAEngine.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QAEngineContext>(options => options.UseSqlServer("DatabaseConnection"));
            services.AddControllers();
            services.AddScoped<IQuestionsService, QuestionsService>();
            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/api/error");
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
