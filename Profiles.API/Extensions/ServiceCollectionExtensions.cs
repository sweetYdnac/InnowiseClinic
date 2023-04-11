using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Profiles.API.Consumers;
using Profiles.API.Validators.Patient;
using Profiles.Business.Implementations.Services;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.Contexts;
using Profiles.Data.Helpers;
using Profiles.Data.Implementations.Repositories;
using Profiles.Data.Interfaces.Repositories;
using Profiles.Data.Migrations;
using Shared.Messages;
using Shared.Models.Request.Profiles.Doctor.SwaggerExamples;
using Shared.Models.Response;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Reflection;

namespace Profiles.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPatientsService, PatientsService>();
            services.AddScoped<IDoctorsService, DoctorsService>();
            services.AddScoped<IReceptionistsService, ReceptionistsService>();
            services.AddScoped<IProfilesService, ProfilesService>();
            services.AddScoped<IMessageService, MessageService>();
        }

        internal static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPatientsRepository, PatientsRepository>();
            services.AddTransient<IDoctorsRepository, DoctorsRepository>();
            services.AddTransient<IDoctorSummaryRepository, DoctorSummaryRepository>();
            services.AddTransient<IReceptionistsRepository, ReceptionistsRepository>();
            services.AddTransient<IReceptionistSummaryRepository, ReceptionistSummaryRepository>();
            services.AddTransient<IProfilesRepository, ProfilesRepository>();
        }

        internal static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
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

        internal static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(Environment.CurrentDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);
                options.ExampleFilters();
            });

            services.AddFluentValidationRulesToSwagger();
            services.AddSwaggerExamplesFromAssemblyOf<CreateDoctorRequestExample>();
        }

        internal static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreatePatientRequestValidator>();
            services.AddFluentValidationAutoValidation();
        }

        internal static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        internal static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
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
                        await context.Response.WriteAsync(new BaseResponse(
                            HttpStatusCode.Unauthorized,
                            "Unauthorized",
                            "Please, check your token."
                            ).ToString());
                    },
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync(new BaseResponse(
                            HttpStatusCode.Forbidden,
                            "Forbidden",
                            "Please, check your token."
                            ).ToString());
                    }
                };
            });
        }

        internal static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<DisableOfficeConsumer>();
                x.AddConsumer<UpdateOfficeConsumer>();
                x.AddConsumer<DisableSpecializationConsumer>();
                x.AddConsumer<UpdateSpecializationConsumer>();

                x.UsingRabbitMq((context, config) => config.ConfigureEndpoints(context));

                EndpointConvention.Map<UpdateAccountStatusMessage>(new Uri(configuration.GetValue<string>("Messages:UpdateAccountStatusEndpoint")));
                EndpointConvention.Map<DeletePhotoMessage>(new Uri(configuration.GetValue<string>("Messages:DeletePhotoEndpoint")));
                EndpointConvention.Map<UpdatePatientMessage>(new Uri(configuration.GetValue<string>("Messages:UpdatePatientEndpoint")));
                EndpointConvention.Map<UpdateDoctorMessage>(new Uri(configuration.GetValue<string>("Messages:UpdateDoctorEndpoint")));
            });
        }

        internal static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
        }

        private static void MigrateDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                databaseInitializer.CreateDatabase("InnowiseClinic.ProfilesDb");

                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                migrationService.MigrateUp();
            }
        }
    }
}
