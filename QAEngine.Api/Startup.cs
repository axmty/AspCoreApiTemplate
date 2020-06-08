using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using QAEngine.Infra.Data;
using QAEngine.Core.Services;
using QAEngine.Core.Repositories;

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
            services.AddDbContext<QAEngineContext>(options => options.UseInMemoryDatabase("QAEngine"));
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
