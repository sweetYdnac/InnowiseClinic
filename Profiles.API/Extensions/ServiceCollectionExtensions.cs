using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Profiles.API.Validators.Patient;
using Profiles.Application.Features.Patient.Commands;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Application.MappingProfiles;
using Profiles.Domain.Entities;
using Profiles.Persistence.Contexts;
using Profiles.Persistence.Helpers;
using Profiles.Persistence.Migrations;
using Profiles.Persistence.Repositories;
using Shared.Models.Response;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Reflection;

namespace Profiles.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IDoctorInformationRepository, DoctorInformationRepository>();
            services.AddTransient<IGenericRepository<DoctorSummary>, DoctorSummaryRepository>();
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
                options.IncludeXmlComments(xmlPath, true);
                options.ExampleFilters();
            });

            services.AddFluentValidationRulesToSwagger();
            services.AddSwaggerExamplesFromAssemblyOf<Program>();
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
            services.AddAutoMapper(typeof(PatientProfile).Assembly);
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration.GetValue<string>("JWTBearerConfiguration:Authority");
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(new BaseResponseModel(
                            HttpStatusCode.Unauthorized,
                            "Unauthorized",
                            "Please, check your token."
                            ).ToString());
                    },
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync(new BaseResponseModel(
                            HttpStatusCode.Forbidden,
                            "Forbidden",
                            "Please, check your token."
                            ).ToString());
                    }
                };
            });
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
