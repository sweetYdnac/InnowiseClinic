using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Profiles.API.Validators;
using Profiles.Application.Features.Patient.Commands;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Application.MappingProfiles;
using Profiles.Persistence.Contexts;
using Profiles.Persistence.Helpers;
using Profiles.Persistence.Migrations;
using Profiles.Persistence.Repositories;
using System.Reflection;

namespace Profiles.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPatientRepository, PatientRepository>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ProfilesDbContext>();
            services.AddSingleton<DatabaseInitializer>();
            services.AddFluentMigratorCore()
                .ConfigureRunner(r => r
                .AddSqlServer()
                .WithGlobalConnectionString(configuration.GetConnectionString("ProfilesDbConnection"))
                .ScanIn(typeof(InitialTables_202302034110).Assembly));
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
            services.AddValidatorsFromAssemblyContaining<CreatePatientRequestModelValidator>();
            services.AddFluentValidationAutoValidation();
        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreatePatientCommand).Assembly);
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                    .AddAutoMapper(typeof(PatientProfile).Assembly);
        }

        private static void MigrateDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                databaseInitializer.CreateDatabase("InnowiseClinic.ProfilesAPI");

                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                migrationService.MigrateUp();
            };
        }
    }
}
