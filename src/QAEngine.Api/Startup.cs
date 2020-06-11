using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using QAEngine.Api.Mvc;
using QAEngine.Domain.Persistence;
using QAEngine.Domain.Resources;
using QAEngine.Domain.Services;
using QAEngine.Infra.Persistence;

namespace QAEngine.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

            this.ConfigureValidatorPropertyNameResolver();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTransient<ProblemDetailsFactory, ApiProblemDetailsFactory>()
                .AddTransient<IValidator<QuestionCreateRequest>, QuestionCreateRequestValidator>()
                .AddScoped<IQuestionsService, QuestionsService>()
                .AddScoped<IQuestionsRepository, QuestionsRepository>()
                .AddSingleton(this.BuildSqlConnectionFactory())
                .AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(this.ConfigureFluentValidation);
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

        private void ConfigureFluentValidation(FluentValidationMvcConfiguration config)
        {
            config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            config.LocalizationEnabled = false;
        }

        private ISqlConnectionFactory BuildSqlConnectionFactory()
        {
            return new SqlConnectionFactory(this.Configuration.GetConnectionString("Database"));
        }

        private void ConfigureValidatorPropertyNameResolver()
        {
            ValidatorOptions.PropertyNameResolver = (type, member, expr) =>
            {
                return new CamelCasePropertyNamesContractResolver().GetResolvedPropertyName(member?.Name);
            };
        }
    }
}
