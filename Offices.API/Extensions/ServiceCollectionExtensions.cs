using FluentValidation;
using FluentValidation.AspNetCore;
using Offices.Persistence.Contexts;
using Shared.Models.Request.Offices;
using System.Reflection;

namespace Offices.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
        }

        public static void ConfigureDbContext(this IServiceCollection services)
        {
            services.AddScoped<OfficesDbContext>();
        }

        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(Environment.CurrentDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<GetOfficesRequestModel>();
            services.AddFluentValidationAutoValidation();
        }
    }
}
