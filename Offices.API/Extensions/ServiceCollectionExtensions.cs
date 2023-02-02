using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Offices.API.Validators;
using Offices.Application.Features.Office.Queries;
using Offices.Application.Interfaces.Repositories;
using Offices.Application.MappingProfiles;
using Offices.Persistence.Contexts;
using Offices.Persistence.Helpers;
using Offices.Persistence.Migrations;
using Offices.Persistence.Repositories;
using System.Reflection;

namespace Offices.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void Addrepositories(this IServiceCollection services)
        {
            services.AddTransient<IOfficeRepository, OfficeRepository>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<OfficesDbContext>();
            services.AddScoped<DatabaseInitializer>();
            services.AddFluentMigratorCore()
                .ConfigureRunner(r => r
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("OfficesDbConnection"))
                .ScanIn(typeof(InitialTables_202302010714).Assembly));
            services.MigrateDatabase();
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
            services.AddValidatorsFromAssemblyContaining<GetOfficesRequestModelValidator>();
            services.AddFluentValidationAutoValidation();
        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetOfficesQuery).Assembly);
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                    .AddAutoMapper(typeof(OfficeProfile).Assembly);
        }

        private static void MigrateDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                databaseInitializer.CreateDatabase("innowiseclinic_officesapi");

                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                migrationService.MigrateUp();
            };
        }
    }
}
